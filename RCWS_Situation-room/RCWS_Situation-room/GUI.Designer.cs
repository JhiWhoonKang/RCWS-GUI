namespace RCWS_Situation_room
{
    partial class GUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUI));
            this.pictureBox_Map = new System.Windows.Forms.PictureBox();
            this.btn_close = new System.Windows.Forms.Button();
            this.pn_mapcontainer = new System.Windows.Forms.Panel();
            this.rtb_sendtcp = new System.Windows.Forms.RichTextBox();
            this.rtb_receivetcp = new System.Windows.Forms.RichTextBox();
            this.Connect = new System.Windows.Forms.Button();
            this.pictureBox_VIEW = new System.Windows.Forms.PictureBox();
            this.tb_body_azimuth = new System.Windows.Forms.TextBox();
            this.tb_body_elevation = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tb_optical_azimuth = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_optical_elevation = new System.Windows.Forms.TextBox();
            this.btn_Permission = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.tb_Distance = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_Pointdistance = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.tb_Magnification = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tb_TakeAim = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tb_Fire = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tb_RemainingBullets = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Map)).BeginInit();
            this.pn_mapcontainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_VIEW)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox_Map
            // 
            this.pictureBox_Map.Image = global::RCWS_Situation_room.Properties.Resources.demomap;
            this.pictureBox_Map.Location = new System.Drawing.Point(3, 7);
            this.pictureBox_Map.Name = "pictureBox_Map";
            this.pictureBox_Map.Size = new System.Drawing.Size(445, 710);
            this.pictureBox_Map.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox_Map.TabIndex = 0;
            this.pictureBox_Map.TabStop = false;
            // 
            // btn_close
            // 
            this.btn_close.BackColor = System.Drawing.Color.White;
            this.btn_close.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_close.BackgroundImage")));
            this.btn_close.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_close.Location = new System.Drawing.Point(1518, 13);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(27, 27);
            this.btn_close.TabIndex = 1;
            this.btn_close.UseVisualStyleBackColor = false;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // pn_mapcontainer
            // 
            this.pn_mapcontainer.Controls.Add(this.pictureBox_Map);
            this.pn_mapcontainer.Location = new System.Drawing.Point(1094, 46);
            this.pn_mapcontainer.Name = "pn_mapcontainer";
            this.pn_mapcontainer.Size = new System.Drawing.Size(451, 723);
            this.pn_mapcontainer.TabIndex = 2;
            // 
            // rtb_sendtcp
            // 
            this.rtb_sendtcp.Location = new System.Drawing.Point(13, 593);
            this.rtb_sendtcp.Name = "rtb_sendtcp";
            this.rtb_sendtcp.Size = new System.Drawing.Size(420, 170);
            this.rtb_sendtcp.TabIndex = 3;
            this.rtb_sendtcp.Text = "";
            // 
            // rtb_receivetcp
            // 
            this.rtb_receivetcp.Location = new System.Drawing.Point(439, 593);
            this.rtb_receivetcp.Name = "rtb_receivetcp";
            this.rtb_receivetcp.Size = new System.Drawing.Size(454, 170);
            this.rtb_receivetcp.TabIndex = 4;
            this.rtb_receivetcp.Text = "";
            // 
            // Connect
            // 
            this.Connect.Location = new System.Drawing.Point(13, 13);
            this.Connect.Name = "Connect";
            this.Connect.Size = new System.Drawing.Size(92, 29);
            this.Connect.TabIndex = 5;
            this.Connect.Text = "Connect";
            this.Connect.UseVisualStyleBackColor = true;
            this.Connect.Click += new System.EventHandler(this.Connect_Click);
            // 
            // pictureBox_VIEW
            // 
            this.pictureBox_VIEW.Location = new System.Drawing.Point(13, 167);
            this.pictureBox_VIEW.Name = "pictureBox_VIEW";
            this.pictureBox_VIEW.Size = new System.Drawing.Size(420, 420);
            this.pictureBox_VIEW.TabIndex = 6;
            this.pictureBox_VIEW.TabStop = false;
            this.pictureBox_VIEW.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_VIEW_Paint);
            // 
            // tb_body_azimuth
            // 
            this.tb_body_azimuth.Location = new System.Drawing.Point(6, 38);
            this.tb_body_azimuth.Name = "tb_body_azimuth";
            this.tb_body_azimuth.Size = new System.Drawing.Size(93, 21);
            this.tb_body_azimuth.TabIndex = 7;
            // 
            // tb_body_elevation
            // 
            this.tb_body_elevation.Location = new System.Drawing.Point(106, 38);
            this.tb_body_elevation.Name = "tb_body_elevation";
            this.tb_body_elevation.Size = new System.Drawing.Size(93, 21);
            this.tb_body_elevation.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "방위각";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(104, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "고각";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tb_body_azimuth);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tb_body_elevation);
            this.groupBox1.Location = new System.Drawing.Point(13, 92);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(205, 70);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "RCWS Body";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tb_optical_azimuth);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.tb_optical_elevation);
            this.groupBox2.Location = new System.Drawing.Point(228, 91);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(205, 70);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Optical Body";
            // 
            // tb_optical_azimuth
            // 
            this.tb_optical_azimuth.Location = new System.Drawing.Point(6, 38);
            this.tb_optical_azimuth.Name = "tb_optical_azimuth";
            this.tb_optical_azimuth.Size = new System.Drawing.Size(93, 21);
            this.tb_optical_azimuth.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "방위각";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(104, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "고각";
            // 
            // tb_optical_elevation
            // 
            this.tb_optical_elevation.Location = new System.Drawing.Point(106, 38);
            this.tb_optical_elevation.Name = "tb_optical_elevation";
            this.tb_optical_elevation.Size = new System.Drawing.Size(93, 21);
            this.tb_optical_elevation.TabIndex = 8;
            // 
            // btn_Permission
            // 
            this.btn_Permission.Location = new System.Drawing.Point(6, 35);
            this.btn_Permission.Name = "btn_Permission";
            this.btn_Permission.Size = new System.Drawing.Size(442, 23);
            this.btn_Permission.TabIndex = 13;
            this.btn_Permission.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btn_Permission);
            this.groupBox3.Location = new System.Drawing.Point(439, 92);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(454, 69);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Control Authority Status";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.groupBox4);
            this.groupBox7.Controls.Add(this.groupBox8);
            this.groupBox7.Location = new System.Drawing.Point(439, 167);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(454, 420);
            this.groupBox7.TabIndex = 16;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "RCWS Status";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.tb_Magnification);
            this.groupBox8.Controls.Add(this.label12);
            this.groupBox8.Controls.Add(this.tb_Pointdistance);
            this.groupBox8.Controls.Add(this.label6);
            this.groupBox8.Controls.Add(this.tb_Distance);
            this.groupBox8.Controls.Add(this.label5);
            this.groupBox8.Location = new System.Drawing.Point(6, 45);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(442, 70);
            this.groupBox8.TabIndex = 17;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Range Founder";
            // 
            // tb_Distance
            // 
            this.tb_Distance.Location = new System.Drawing.Point(117, 41);
            this.tb_Distance.Name = "tb_Distance";
            this.tb_Distance.Size = new System.Drawing.Size(105, 21);
            this.tb_Distance.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(115, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "Distance";
            // 
            // tb_Pointdistance
            // 
            this.tb_Pointdistance.Location = new System.Drawing.Point(228, 41);
            this.tb_Pointdistance.Name = "tb_Pointdistance";
            this.tb_Pointdistance.Size = new System.Drawing.Size(105, 21);
            this.tb_Pointdistance.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(226, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 12);
            this.label6.TabIndex = 14;
            this.label6.Text = "Point Distance";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 26);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(80, 12);
            this.label12.TabIndex = 21;
            this.label12.Text = "Magnification";
            // 
            // tb_Magnification
            // 
            this.tb_Magnification.Location = new System.Drawing.Point(6, 41);
            this.tb_Magnification.Name = "tb_Magnification";
            this.tb_Magnification.Size = new System.Drawing.Size(105, 21);
            this.tb_Magnification.TabIndex = 22;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tb_TakeAim);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.tb_RemainingBullets);
            this.groupBox4.Controls.Add(this.tb_Fire);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Location = new System.Drawing.Point(6, 121);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(442, 74);
            this.groupBox4.TabIndex = 20;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Weapon";
            // 
            // tb_TakeAim
            // 
            this.tb_TakeAim.Location = new System.Drawing.Point(8, 43);
            this.tb_TakeAim.Name = "tb_TakeAim";
            this.tb_TakeAim.Size = new System.Drawing.Size(105, 21);
            this.tb_TakeAim.TabIndex = 28;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 28);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 12);
            this.label8.TabIndex = 27;
            this.label8.Text = "Take Aim";
            // 
            // tb_Fire
            // 
            this.tb_Fire.Location = new System.Drawing.Point(119, 43);
            this.tb_Fire.Name = "tb_Fire";
            this.tb_Fire.Size = new System.Drawing.Size(105, 21);
            this.tb_Fire.TabIndex = 23;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(228, 28);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(107, 12);
            this.label11.TabIndex = 26;
            this.label11.Text = "Remaining Bullets";
            // 
            // tb_RemainingBullets
            // 
            this.tb_RemainingBullets.Location = new System.Drawing.Point(230, 43);
            this.tb_RemainingBullets.Name = "tb_RemainingBullets";
            this.tb_RemainingBullets.Size = new System.Drawing.Size(105, 21);
            this.tb_RemainingBullets.TabIndex = 24;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(117, 28);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(26, 12);
            this.label13.TabIndex = 25;
            this.label13.Text = "Fire";
            // 
            // GUI
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1558, 782);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox_VIEW);
            this.Controls.Add(this.Connect);
            this.Controls.Add(this.rtb_receivetcp);
            this.Controls.Add(this.rtb_sendtcp);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.pn_mapcontainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "GUI";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MotionControl";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GUI_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.GUI_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Map)).EndInit();
            this.pn_mapcontainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_VIEW)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_Map;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Panel pn_mapcontainer;
        private System.Windows.Forms.RichTextBox rtb_sendtcp;
        private System.Windows.Forms.RichTextBox rtb_receivetcp;
        private System.Windows.Forms.Button Connect;
        private System.Windows.Forms.PictureBox pictureBox_VIEW;
        private System.Windows.Forms.TextBox tb_body_azimuth;
        private System.Windows.Forms.TextBox tb_body_elevation;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tb_optical_azimuth;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_optical_elevation;
        private System.Windows.Forms.Button btn_Permission;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.TextBox tb_Distance;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tb_Pointdistance;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tb_Magnification;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox tb_TakeAim;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tb_Fire;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tb_RemainingBullets;
        private System.Windows.Forms.Label label13;
    }
}