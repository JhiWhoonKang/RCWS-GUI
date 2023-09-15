using System;
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

        /* SG90 */
        private SerialPort sg90Port;
        private int scope = 0;

        private NetworkStream networkStream;

        Packet.SendTCP command;
        Packet.ReceiveTCP receivedStruct;

        double currentRCWSDirection;

        Process RCWSCam;

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
                    Sendsg90Scope();
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

            if (pressedKeys.Contains(Keys.Z) && pressedKeys.Contains(Keys.I))
            {
                int angle = 30;
                try
                {
                    sg90Port.WriteLine(angle.ToString());
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

        private void Sendsg90Scope()
        {
            if (pressedKeys.Contains(Keys.Z) && pressedKeys.Contains(Keys.I))
            {
                int angle = (scope == 0) ? define.MAX_ANGLE : define.MIN_ANGLE;
                try
                {
                    sg90Port.WriteLine(angle.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Cannot send SG90 scope data" + ex.Message);
                }

                scope = (scope == 0) ? 1 : 0;
            }
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

        private async void btn_connect_Click(object sender, EventArgs e)
        {
            await Task.Run(() => TcpConnectAsync());
            RCWSCam = new Process();
            RCWSCam.StartInfo.FileName = "C:\\JHIWHOON_ws\\2023 Hanium\\My_Server\\obj\\Debug\\My_Server.exe";
            RCWSCam.Start();
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

            g.DrawEllipse(Pens.Black, 0, 0, pictureBox_azimuth.Width - 1, pictureBox_azimuth.Height - 1);

            int lineLength = centerX;

            double radianAngleRCWS = receivedStruct.BodyPan * Math.PI / 180.0;

            int endXRCWS = centerX + (int)(lineLength * Math.Sin(radianAngleRCWS));
            int endYRCWS = centerY - (int)(lineLength * Math.Cos(radianAngleRCWS));

            g.DrawLine(Pens.Red, new Point(centerX, centerY), new Point(endXRCWS, endYRCWS));

            double radianAngleOptical = receivedStruct.OpticalPan * Math.PI / 180.0;

            int endXOptical = centerX + (int)(lineLength * Math.Sin(radianAngleOptical));
            int endYOptical = centerY - (int)(lineLength * Math.Cos(radianAngleOptical));

            g.DrawLine(Pens.Blue, new Point(centerX, centerY), new Point(endXOptical, endYOptical));
        }

        private void pictureBox_elevation_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            int startX = 0;
            int startY = pictureBox_elevation.Height / 2;

            int lineLength = pictureBox_elevation.Width;

            double radianAngleRCWS = receivedStruct.BodyTilt * Math.PI / 180.0;

            string text = "RCWS 고각: " + receivedStruct.BodyTilt.ToString() + ", Optical 고각: " + receivedStruct.OpticalTilt.ToString();

            float x = 10;
            float y = 10;

            using (Font font = new Font("Arial", 12))
            {
                g.DrawString(text, font, Brushes.Black, x, y);
            }

            int endXRCWS = startX + (int)(lineLength * Math.Cos(radianAngleRCWS));
            int endYRCWS = startY - (int)(lineLength * Math.Sin(radianAngleRCWS));

            g.DrawLine(Pens.Red, new Point(startX, startY), new Point(endXRCWS, endYRCWS));

            double radianAngleOptical = receivedStruct.OpticalTilt * Math.PI / 180.0;

            int endXOptical = startX + (int)(lineLength * Math.Cos(radianAngleOptical));
            int endYOptical = startY - (int)(lineLength * Math.Sin(radianAngleOptical));

            g.DrawLine(Pens.Blue, new Point(startX, startY), new Point(endXOptical, endYOptical));
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