﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Remoting.Messaging;
using System.Diagnostics;
using System.Net.NetworkInformation;
using static RCWS_Situation_room.Packet;

namespace RCWS_Situation_room
{ 
    public partial class GUI : Form
    {
        /*map*/
        private Bitmap mapImage;
        private float currentScale = 1.0f;
        private float zoomFactor = 1.1f;
        private bool isDragging = false;    
        private int lastX;
        private int lastY;

        /*motion control*/
        private HashSet<Keys> pressedKeys = new HashSet<Keys>();
        
        StreamWriter streamWriter;
        StreamReader streamReader;

        private NetworkStream networkStream;

        SendTCP command;
        ReceiveTCP receivedStruct;

        double currentRCWSDirection;

        public GUI(StreamWriter streamWriter)
        {
            InitializeComponent();

            mapImage = new Bitmap(@"C:\JHIWHOON_ws\2023 Hanium\_file photo\demomap.bmp");
            UpdateMapImage();

            command = new SendTCP();
            receivedStruct = new ReceiveTCP();

            pictureBox_Map.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox_Map.MouseWheel += MapPictureBox_MouseWheel;
            pictureBox_Map.MouseDown += MapPictureBox_MouseDown;
            pictureBox_Map.MouseMove += MapPictureBox_MouseMove;
            pictureBox_Map.MouseUp += MapPictureBox_MouseUp;

            // pictureBox_Map.Click += new MouseEventHandler(pictureBox_Map_Click);

            this.streamWriter = streamWriter;

            KeyDown += new KeyEventHandler(GUI_KeyDown);
            KeyUp += new KeyEventHandler(GUI_KeyUp);
            this.Focus();
        }

        #region Map
        private void UpdateMapImage()
        {
            int newWidth = (int)(mapImage.Width * currentScale);
            int newHeight = (int)(mapImage.Height * currentScale);
            var resizedImage = new Bitmap(newWidth, newHeight);
            using (var g = Graphics.FromImage(resizedImage))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(mapImage, new Rectangle(0, 0, newWidth, newHeight));
            }
            pictureBox_Map.Image = resizedImage;
        }

        private void MapPictureBox_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
                currentScale *= zoomFactor;
            else
                currentScale /= zoomFactor;

            UpdateMapImage();
        }

        private void MapPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                lastX = e.X;
                lastY = e.Y;
            }
        }

        private void MapPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (isDragging)
                {
                    int deltaX = e.X - lastX;
                    int deltaY = e.Y - lastY;

                    int map_newX = pictureBox_Map.Location.X + deltaX;
                    int map_newY = pictureBox_Map.Location.Y + deltaY;

                    pictureBox_Map.Location = new Point(map_newX, map_newY);
                    lastX = e.X;
                    lastY = e.Y;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error in MapPictureBox_MouseMove: " + ex.Message);
            }
        }

        private void MapPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
            }
        }
        #endregion

        #region motion control

        private readonly SemaphoreSlim keySemaphore = new SemaphoreSlim(1, 1);

        private async void GUI_KeyDown(object sender, KeyEventArgs e)
        {
            await keySemaphore.WaitAsync();

            try
            {
                if (pressedKeys.Add(e.KeyCode))
                {
                    await SendCommandStructure();
                }
            }
            finally
            {
                keySemaphore.Release();

            }
        }

        private async void GUI_KeyUp(object sender, KeyEventArgs e)
        {
            await keySemaphore.WaitAsync();

            try
            {
                if (pressedKeys.Remove(e.KeyCode))
                {
                    await SendCommandStructure();
                }
            }
            finally
            {
                keySemaphore.Release();
            }
        }

        private async Task SendCommandStructure()
        {
            command.BodyPan = 0;
            command.BodyTilt = 0;

            if (pressedKeys.Contains(Keys.A))
                command.BodyPan = 1;

            if (pressedKeys.Contains(Keys.D))
                command.BodyPan = -1;

            if (pressedKeys.Contains(Keys.W))
                command.BodyTilt = 1;

            if (pressedKeys.Contains(Keys.S))
                command.BodyTilt = -1;

            if (pressedKeys.Contains(Keys.C))
            {
                if (command.Permission == 1)
                    command.Permission = 2;
                else
                    command.Permission = 1;
            }

            SendTcp($"Pan: {command.BodyPan}, Tilt: {command.BodyTilt}, Permission: {command.Permission}\n");

            byte[] commandBytes = TcpReturn.StructToBytes(command);
            await streamWriter.BaseStream.WriteAsync(commandBytes, 0, commandBytes.Length);
            await streamWriter.BaseStream.FlushAsync();
        }

        private async Task TcpConnectAsync()
        {
            TcpClient tcpClient = new TcpClient();

            try
            {
                SendTcp("Connecting...");
                tcpClient.Connect(define.SERVER_IP, define.TCPPORT);

                networkStream = tcpClient.GetStream();
                streamReader = new StreamReader(networkStream);
                streamWriter = new StreamWriter(networkStream);
                streamWriter.AutoFlush = true;
            }
            catch (Exception ex)
            {
                ReceiveTcp("Connect ERROR: " + ex.Message);
                return;
            }

            ReceiveTcp("Server Connected");

            try
            {
                while (true)
                {
                    byte[] receivedData = new byte[Marshal.SizeOf(typeof(ReceiveTCP))];
                    await networkStream.ReadAsync(receivedData, 0, receivedData.Length);

                    receivedStruct = TcpReturn.BytesToStruct<ReceiveTCP>(receivedData);

                    ReceiveTcp($"OpticalTilt: {receivedStruct.OpticalTilt}, OpticalPan: {receivedStruct.OpticalPan}, BodyTilt: {receivedStruct.BodyTilt}" +
                        $", BodyPan: {receivedStruct.BodyPan}, pointdistance: {receivedStruct.Permission}, Permission: {receivedStruct.Permission}" +
                        $", distance{receivedStruct.distance}, TakeAim: {receivedStruct.TakeAim}, Remaining_bullets: {receivedStruct.Remaining_bullets}" +
                        $", Magnification{receivedStruct.Magnification}, Fire: {receivedStruct.Fire}");

                    /* textbox display */
                    tb_body_azimuth.Text = receivedStruct.BodyPan.ToString();
                    tb_body_elevation.Text = receivedStruct.BodyTilt.ToString();

                    tb_optical_azimuth.Text=receivedStruct.OpticalPan.ToString();
                    tb_optical_elevation.Text=receivedStruct.OpticalTilt.ToString();

                    tb_Pointdistance.Text=receivedStruct.pointdistance.ToString();
                    tb_Distance.Text=receivedStruct.distance.ToString();

                    tb_TakeAim.Text=receivedStruct.TakeAim.ToString();
                    tb_RemainingBullets.Text=receivedStruct.Remaining_bullets.ToString();
                    
                    tb_Magnification.Text=receivedStruct.Magnification.ToString();
                    tb_Fire.Text=receivedStruct.Fire.ToString();
                    /* */

                    /* button display */
                    if (receivedStruct.Permission == 0 || receivedStruct.Permission == 2)
                    {
                        btn_Permission.BackColor = Color.Green;
                        btn_Permission.Text = "Controlable";
                    }

                    else if (receivedStruct.Permission == 1)
                    {
                        btn_Permission.BackColor = Color.Red;
                        btn_Permission.Text = "Uncontrolable";
                    }

                    else
                    {
                        btn_Permission.BackColor = Color.Empty;
                        btn_Permission.Text = "No data. RETRY";
                    }
                    /* */
                }
            }

            catch (Exception ex)
            {
                ReceiveTcp("Connect ERROR: " + ex.Message);
                return;
            }
        }

        private void pictureBox_VIEW_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen BlackPen = new Pen(Color.Black, 2);
            Pen RedPen = new Pen(Color.Red, 2);
            Pen BluePen=new Pen(Color.Blue, 2);

            double angleInRadians = Math.PI * (currentRCWSDirection / 180.0);

            int centerX = pictureBox_VIEW.Width / 2;
            int centerY = pictureBox_VIEW.Height / 2;
            int radius = Math.Min(centerX, centerY);

            double opticalAngle = Math.PI * (receivedStruct.OpticalPan/ 180.0);
            double bodyAngle = Math.PI * (receivedStruct.BodyPan / 180.0);

            int opticalX = centerX + (int)(radius * Math.Cos(opticalAngle));
            int opticalY = centerY - (int)(radius * Math.Sin(opticalAngle));

            int bodyX = centerX + (int)(radius * Math.Cos(bodyAngle));
            int bodyY = centerY - (int)(radius * Math.Sin(bodyAngle));

            using (BlackPen)
            {
                g.DrawEllipse(BlackPen, 0, 0, pictureBox_VIEW.Width, pictureBox_VIEW.Height);
            }

            /* Optical Pan */
            using (RedPen)
            {
                e.Graphics.DrawLine(RedPen, centerX, centerY, opticalX, opticalY);
            }

            /* Body Pan */
            using (BluePen)
            {   
                e.Graphics.DrawLine(BluePen, centerX, centerY, bodyX, bodyY);
            }
        }

        private double CalculateRCWSDirection(double bodyPan, double bodyTilt)
        {
            double directionInRadians = 0/*방위각, 고각 계산 여기 예정*/ ;
            return directionInRadians;
        }

        private void UpdateRCWSDirection()
        {
            double bodyPan = receivedStruct.BodyPan;
            double bodyTilt = receivedStruct.BodyTilt;

            currentRCWSDirection = CalculateRCWSDirection(bodyPan, bodyTilt);

            pictureBox_VIEW.Invalidate();
        }

        private async void Connect_Click(object sender, EventArgs e)
        {
            await Task.Run(() => TcpConnectAsync());
        }

        private void SendTcp(string str)
        {
            rtb_sendtcp.Invoke((MethodInvoker)delegate { rtb_sendtcp.AppendText(str + "\r\n"); });
            rtb_sendtcp.Invoke((MethodInvoker)delegate { rtb_sendtcp.ScrollToCaret(); });
        }

        private void ReceiveTcp(string str)
        {
            rtb_receivetcp.Invoke((MethodInvoker)delegate { rtb_receivetcp.AppendText(str + "\r\n"); });
            rtb_receivetcp.Invoke((MethodInvoker)delegate { rtb_receivetcp.ScrollToCaret(); });
        }
        #endregion

        private void btn_close_Click(object sender, EventArgs e)
        {
            Close();
            Application.Exit();
        }
    }
}