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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Map)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_VIEW)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox_Map
            // 
            this.pictureBox_Map.Image = global::RCWS_Situation_room.Properties.Resources.demomap;
            this.pictureBox_Map.Location = new System.Drawing.Point(1101, 50);
            this.pictureBox_Map.Name = "pictureBox_Map";
            this.pictureBox_Map.Size = new System.Drawing.Size(444, 713);
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
            this.pn_mapcontainer.Location = new System.Drawing.Point(1094, 50);
            this.pn_mapcontainer.Name = "pn_mapcontainer";
            this.pn_mapcontainer.Size = new System.Drawing.Size(451, 719);
            this.pn_mapcontainer.TabIndex = 2;
            // 
            // rtb_sendtcp
            // 
            this.rtb_sendtcp.Location = new System.Drawing.Point(13, 587);
            this.rtb_sendtcp.Name = "rtb_sendtcp";
            this.rtb_sendtcp.Size = new System.Drawing.Size(237, 176);
            this.rtb_sendtcp.TabIndex = 3;
            this.rtb_sendtcp.Text = "";
            // 
            // rtb_receivetcp
            // 
            this.rtb_receivetcp.Location = new System.Drawing.Point(256, 587);
            this.rtb_receivetcp.Name = "rtb_receivetcp";
            this.rtb_receivetcp.Size = new System.Drawing.Size(237, 176);
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
            this.pictureBox_VIEW.Location = new System.Drawing.Point(636, 50);
            this.pictureBox_VIEW.Name = "pictureBox_VIEW";
            this.pictureBox_VIEW.Size = new System.Drawing.Size(452, 501);
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
            this.groupBox1.Size = new System.Drawing.Size(211, 69);
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
            this.groupBox2.Location = new System.Drawing.Point(230, 92);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(211, 69);
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
            // GUI
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1558, 782);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox_Map);
            this.Controls.Add(this.pictureBox_VIEW);
            this.Controls.Add(this.Connect);
            this.Controls.Add(this.rtb_receivetcp);
            this.Controls.Add(this.rtb_sendtcp);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.pn_mapcontainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "GUI";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MotionControl";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Map)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_VIEW)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
    }
}