    using System;
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
            private StreamWriter _streamWriter;
            private HashSet<Keys> _pressedKeys = new HashSet<Keys>();

            StreamReader streamReader;
            StreamWriter streamWriter;

        public GUI()
        {
            InitializeComponent();

            mapImage = new Bitmap(@"C:\JHIWHOON_ws\2023 Hanium\_file photo\demomap.bmp");
            UpdateMapImage();

            pictureBox_Map.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox_Map.MouseWheel += MapPictureBox_MouseWheel;
            pictureBox_Map.MouseDown += MapPictureBox_MouseDown;
            pictureBox_Map.MouseMove += MapPictureBox_MouseMove;
            pictureBox_Map.MouseUp += MapPictureBox_MouseUp;

            // pictureBox_Map.Click += new MouseEventHandler(pictureBox_Map_Click);

            this._streamWriter = streamWriter;
            this.KeyDown += new KeyEventHandler(MotionControl_KeyDown);
            this.KeyUp += new KeyEventHandler(MotionControl_KeyUp);

            Thread _thread = new Thread(TcpConnect);
            _thread.IsBackground = true;
            _thread.Start();
        }

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

        private void pictureBox_Map_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // 클릭한 위치의 좌표를 화면 좌표에서 이미지 좌표로 변환
                int imageX = (int)(e.X / currentScale);
                int imageY = (int)(e.Y / currentScale);

                // RCWS 위치로 표시
                DrawRCWSLocation(imageX, imageY);

                // RCWS가 해당 위치를 바라보는 방향을 나타내는 막대 직선 그리기
                DrawDirection(imageX, imageY);
            }
        }

        private void DrawRCWSLocation(int x, int y)
        {
            // RCWS 위치를 표시할 그래픽스 객체 생성
            using (Graphics g = Graphics.FromImage(pictureBox_Map.Image))
            {
                // 원을 그려서 RCWS 위치를 나타냅니다.
                int rcwsRadius = 10; // 원의 반지름
                Pen pen = new Pen(Color.Red, 2); // 원의 색상과 선 두께 설정
                g.DrawEllipse(pen, x - rcwsRadius, y - rcwsRadius, rcwsRadius * 2, rcwsRadius * 2);
            }

            // 지도 이미지 업데이트
            pictureBox_Map.Invalidate();
        }

        private void DrawDirection(int rcwsX, int rcwsY)
        {
            // RCWS 위치에서 클릭한 위치까지의 선을 그리는 그래픽스 객체 생성
            using (Graphics g = Graphics.FromImage(pictureBox_Map.Image))
            {
                Pen pen = new Pen(Color.Blue, 2); // 선의 색상과 선 두께 설정

                // RCWS 위치에서 클릭한 위치까지 선 그리기
                g.DrawLine(pen, rcwsX, rcwsY, lastX, lastY);
            }

            // 지도 이미지 업데이트
            pictureBox_Map.Invalidate();
        }

        private void pictureBox_Map_Paint(object sender, PaintEventArgs e)
        {
            // PictureBox의 Paint 이벤트 핸들러에서 지도 이미지를 다시 그립니다.
            e.Graphics.DrawImage(pictureBox_Map.Image, pictureBox_Map.Location);
        }

        #region motion control
        private async void MotionControl_KeyDown(object sender, KeyEventArgs e)
        {
            _pressedKeys.Add(e.KeyCode);
            await SendCommandStructure();
        }
        
        private async void MotionControl_KeyUp(object sender, KeyEventArgs e)
        {
            _pressedKeys.Remove(e.KeyCode);
            await SendCommandStructure();
        }
        
        private async Task SendCommandStructure()
        {
            Packet.SendTCP command = new Packet.SendTCP();
            
            if (_pressedKeys.Contains(Keys.A))
                command.Pan = 1;
            if (_pressedKeys.Contains(Keys.D))
                command.Pan = -1;
            if (_pressedKeys.Contains(Keys.W))
                command.Tilt = (char)3;
            if (_pressedKeys.Contains(Keys.S))
                command.Tilt = (char)4;
            if (_pressedKeys.Contains(Keys.C))
                command.Permission = 1;
            
            byte[] commandBytes = TcpReturn.StructToBytes(command);
            await _streamWriter.BaseStream.WriteAsync(commandBytes, 0, commandBytes.Length);
            await _streamWriter.BaseStream.FlushAsync();
        }
        
        private void TcpConnect()
        {
            TcpClient tcpClient = new TcpClient();
            
            int TCPPORT = define.TCPPORT;
            try
            {
                writeTcpRichTextbox("Connecting...");
                tcpClient.Connect(define.SERVER_IP, TCPPORT);
                
                NetworkStream networkStream = tcpClient.GetStream();
                streamReader = new StreamReader(networkStream);
                streamWriter = new StreamWriter(networkStream);
                streamWriter.AutoFlush = true;
            }
            catch (Exception ex)
            {
                writeTcpRichTextbox("Connect ERROR: " + ex.Message);
                return;
            }
            
            writeTcpRichTextbox("Server Connected");
            
            try
            {
                while (true)
                {
                    byte[] receivedData = new byte[Marshal.SizeOf(typeof(Packet.ReceiveTCP))];
                    streamReader.BaseStream.Read(receivedData, 0, receivedData.Length);
                    Packet.ReceiveTCP receivedStruct = TcpReturn.BytesToStruct<Packet.ReceiveTCP>(receivedData);
                    
                    writeTcpRichTextbox($"OpticalTilt: {receivedStruct.OpticalTilt}, OpticalPan: {receivedStruct.OpticalPan}, BodyTilt: {receivedStruct.BodyTilt}" +
                        $", BodyPan: {receivedStruct.BodyPan}, pointdistance: {receivedStruct.Permission}, Permission: {receivedStruct.Permission}");
                }
            }
            
            catch (Exception ex)
            {
                writeTcpRichTextbox("Connect ERROR: " + ex.Message);
                return;
            }
        }
        
        private void writeTcpRichTextbox(string str)
        {
            rtb_sendtcp.Invoke((MethodInvoker)delegate { rtb_sendtcp.AppendText(str + "\r\n"); });
            rtb_sendtcp.Invoke((MethodInvoker)delegate { rtb_sendtcp.ScrollToCaret(); });
        }
        #endregion

        private void btn_close_Click(object sender, EventArgs e)
        {
            Close();
            Application.Exit();
        }
    }
}