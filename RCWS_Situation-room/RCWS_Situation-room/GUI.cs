﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.IO.Ports;
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
using System.Windows.Forms.Design;

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

        /* SG90 */
        private SerialPort sg90Port;
        Process RCWSCam;

        /* Packet */
        Packet.SendTCP command;
        Packet.ReceiveTCP receivedStruct;


        public GUI(StreamWriter streamWriter)
        {
            InitializeComponent();

            mapImage = new Bitmap(@"C:\JHIWHOON_ws\2023 Hanium\_file photo\demomap.bmp");
            UpdateMapImage();

            command = new Packet.SendTCP();
            receivedStruct = new Packet.ReceiveTCP();

            sg90Port = new SerialPort("COM6",9600);
            try
            {
                sg90Port.Open();
                sg90Port.WriteLine("I");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Cannot open SG90 Serial Port" + ex.Message);
            }

            pictureBox_Map.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox_Map.MouseWheel += MapPictureBox_MouseWheel;
            pictureBox_Map.MouseDown += MapPictureBox_MouseDown;
            pictureBox_Map.MouseMove += MapPictureBox_MouseMove;
            pictureBox_Map.MouseUp += MapPictureBox_MouseUp;

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

            if (pressedKeys.Contains(Keys.Z) && pressedKeys.Contains(Keys.I)) //배율 확대 C# GUI -> Arduino
            {
                try
                {
                    sg90Port.WriteLine("I");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Cannot send SG90 scope data" + ex.Message);
                }
            }

            if (pressedKeys.Contains(Keys.Z) && pressedKeys.Contains(Keys.O)) //배율 축소 C# GUI -> Arduino
            {
                try
                {
                    sg90Port.WriteLine("A");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Cannot send SG90 scope data" + ex.Message);
                }
            }

            SendTcp($"Pan: {command.BodyPan}, Tilt: {command.BodyTilt}, Permission: {command.Permission}\n");

            byte[] commandBytes = TcpReturn.StructToBytes(command);
            await streamWriter.BaseStream.WriteAsync(commandBytes, 0, commandBytes.Length);
            await streamWriter.BaseStream.FlushAsync();
        }
        #endregion

        #region TCP Connect
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
                    byte[] receivedData = new byte[Marshal.SizeOf(typeof(Packet.ReceiveTCP))];
                    await networkStream.ReadAsync(receivedData, 0, receivedData.Length);

                    receivedStruct = TcpReturn.BytesToStruct<Packet.ReceiveTCP>(receivedData);

                    ReceiveTcp($"OpticalTilt: {receivedStruct.OpticalTilt}, OpticalPan: {receivedStruct.OpticalPan}, BodyTilt: {receivedStruct.BodyTilt}" +
                        $", BodyPan: {receivedStruct.BodyPan}, pointdistance: {receivedStruct.Permission}, Permission: {receivedStruct.Permission}" +
                        $", distance{receivedStruct.distance}, TakeAim: {receivedStruct.TakeAim}, Remaining_bullets: {receivedStruct.Remaining_bullets}" +
                        $", Magnification{receivedStruct.Magnification}, Fire: {receivedStruct.Fire}, Gun Voltage: {receivedStruct.GunVoltage}");

                    /* picturebox display */
                    pictureBox_azimuth.Invalidate();
                    //pictureBox_azimuth.Refresh();
                    pictureBox_elevation.Invalidate();
                    //pictureBox_elevation.Refresh();

                    /* textbox display */
                    tb_body_azimuth.Text = receivedStruct.BodyPan.ToString();
                    tb_body_elevation.Text = receivedStruct.BodyTilt.ToString();

                    tb_optical_azimuth.Text = receivedStruct.OpticalPan.ToString();
                    tb_optical_elevation.Text = receivedStruct.OpticalTilt.ToString();

                    tb_Pointdistance.Text = receivedStruct.pointdistance.ToString();
                    tb_Distance.Text = receivedStruct.distance.ToString();

                    tb_TakeAim.Text = receivedStruct.TakeAim.ToString();
                    tb_RemainingBullets.Text = receivedStruct.Remaining_bullets.ToString();

                    tb_Magnification.Text = receivedStruct.Magnification.ToString();
                    tb_Fire.Text = receivedStruct.Fire.ToString();
                    tb_gunvoltage.Text = receivedStruct.GunVoltage.ToString();
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
                        btn_Permission.Text = "No Permission data. RETRY";
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

        private async void btn_connect_Click(object sender, EventArgs e)
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

        #region AZEL GUI
        private void pictureBox_azimuth_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            int centerX = pictureBox_azimuth.Width / 2;
            int centerY = pictureBox_azimuth.Height / 2;
            
            /* */
            Pen redPen = new Pen(Color.Red, 8);
            g.DrawLine(redPen, new Point(centerX - 10, centerY - 10), new Point(centerX + 10, centerY + 10));
            g.DrawLine(redPen, new Point(centerX + 10, centerY - 10), new Point(centerX - 10, centerY + 10));
            redPen.Dispose(); // 리소스 해제
            /* */

            /* */
            int lineLength = centerX;
            g.DrawEllipse(Pens.Black, 0, 0, pictureBox_azimuth.Width - 1, pictureBox_azimuth.Height - 1);
            /* */

            /* Body Pan */
            double radianAngleRCWS = receivedStruct.BodyPan * Math.PI / 180.0;
            int endXRCWS = centerX + (int)(lineLength * Math.Sin(radianAngleRCWS));
            int endYRCWS = centerY - (int)(lineLength * Math.Cos(radianAngleRCWS));
            g.DrawLine(Pens.Red, new Point(centerX, centerY), new Point(endXRCWS, endYRCWS));
            /* */

            /* Optical Pan */
            double radianAngleOptical = receivedStruct.OpticalPan * Math.PI / 180.0;
            int endXOptical = centerX + (int)(lineLength * Math.Sin(radianAngleOptical));
            int endYOptical = centerY - (int)(lineLength * Math.Cos(radianAngleOptical));
            g.DrawLine(Pens.Blue, new Point(centerX, centerY), new Point(endXOptical, endYOptical));
            /* */
        }

        private void pictureBox_elevation_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            int centerX = pictureBox_elevation.Width / 2;
            int centerY = pictureBox_elevation.Height / 2;

            /* */
            Pen redPen = new Pen(Color.Red, 8);
            g.DrawLine(redPen, new Point(centerX - 10, centerY - 10), new Point(centerX + 10, centerY + 10));
            g.DrawLine(redPen, new Point(centerX + 10, centerY - 10), new Point(centerX - 10, centerY + 10));
            redPen.Dispose(); // 리소스 해제
            /* */

            /* */
            int lineLength = pictureBox_elevation.Height / 2;
            /* */

            /* Body Tilt*/
            double radianAngleRCWS = receivedStruct.BodyTilt * Math.PI / 180.0;
            int endXRCWS = centerX + (int)(lineLength * Math.Cos(radianAngleRCWS));
            int endYRCWS = centerY - (int)(lineLength * Math.Sin(radianAngleRCWS));
            g.DrawLine(Pens.Red, new Point(centerX, centerY), new Point(endXRCWS, endYRCWS));
            /* */

            /* Optical Tilt */
            double radianAngleOptical = receivedStruct.OpticalTilt * Math.PI / 180.0;
            int endXOptical = centerX + (int)(lineLength * Math.Cos(radianAngleOptical));
            int endYOptical = centerY - (int)(lineLength * Math.Sin(radianAngleOptical));
            g.DrawLine(Pens.Blue, new Point(centerX, centerY), new Point(endXOptical, endYOptical));
            /* */
        }

        private void pictureBox_azimuth_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point clickLocation = e.Location;

                Bitmap bmp;
                if (pictureBox_azimuth.Image == null) bmp = new Bitmap(pictureBox_azimuth.Width, pictureBox_azimuth.Height);
                else bmp = new Bitmap(pictureBox_azimuth.Image);

                using (Graphics g = Graphics.FromImage(bmp))
                using (Pen redPen = new Pen(Color.Red, 8))
                {
                    int crossSize = 10;

                    g.DrawLine(redPen, clickLocation.X - crossSize, clickLocation.Y - crossSize, clickLocation.X + crossSize, clickLocation.Y + crossSize);
                    g.DrawLine(redPen, clickLocation.X - crossSize, clickLocation.Y + crossSize, clickLocation.X + crossSize, clickLocation.Y - crossSize);

                    pictureBox_azimuth.Image = bmp;
                }
            }
        }

        private void pictureBox_elevation_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point clickLocation = e.Location;

                Bitmap bmp;
                if (pictureBox_elevation.Image == null) bmp = new Bitmap(pictureBox_elevation.Width, pictureBox_elevation.Height);
                else bmp = new Bitmap(pictureBox_elevation.Image);

                using (Graphics g = Graphics.FromImage(bmp))
                using (Pen redPen = new Pen(Color.Red, 8))
                {
                    int crossSize = 10;

                    g.DrawLine(redPen, clickLocation.X - crossSize, clickLocation.Y - crossSize, clickLocation.X + crossSize, clickLocation.Y + crossSize);
                    g.DrawLine(redPen, clickLocation.X - crossSize, clickLocation.Y + crossSize, clickLocation.X + crossSize, clickLocation.Y - crossSize);

                    pictureBox_elevation.Image = bmp;
                }
            }
        }
        #endregion

        private void btn_close_Click(object sender, EventArgs e)
        {
            Close();
            Application.Exit();
            if (sg90Port.IsOpen)
            {
                sg90Port.Close();
            }
            RCWSCam.Kill();
        }

        private void btn_disconnect_Click(object sender, EventArgs e)
        {
            Close();
            Application.Exit();
            if (sg90Port.IsOpen)
            {
                sg90Port.Close();
            }
            RCWSCam.Kill();
        }
    }
}