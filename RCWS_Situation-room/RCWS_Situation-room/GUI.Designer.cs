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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Map)).BeginInit();
            this.pn_mapcontainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox_Map
            // 
            this.pictureBox_Map.Image = global::RCWS_Situation_room.Properties.Resources.demomap;
            this.pictureBox_Map.Location = new System.Drawing.Point(3, 3);
            this.pictureBox_Map.Name = "pictureBox_Map";
            this.pictureBox_Map.Size = new System.Drawing.Size(275, 385);
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
            this.btn_close.Location = new System.Drawing.Point(760, 13);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(27, 27);
            this.btn_close.TabIndex = 1;
            this.btn_close.UseVisualStyleBackColor = false;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // pn_mapcontainer
            // 
            this.pn_mapcontainer.Controls.Add(this.pictureBox_Map);
            this.pn_mapcontainer.Location = new System.Drawing.Point(508, 46);
            this.pn_mapcontainer.Name = "pn_mapcontainer";
            this.pn_mapcontainer.Size = new System.Drawing.Size(282, 391);
            this.pn_mapcontainer.TabIndex = 2;
            // 
            // rtb_sendtcp
            // 
            this.rtb_sendtcp.Location = new System.Drawing.Point(13, 302);
            this.rtb_sendtcp.Name = "rtb_sendtcp";
            this.rtb_sendtcp.Size = new System.Drawing.Size(235, 135);
            this.rtb_sendtcp.TabIndex = 3;
            this.rtb_sendtcp.Text = "";
            // 
            // rtb_receivetcp
            // 
            this.rtb_receivetcp.Location = new System.Drawing.Point(267, 302);
            this.rtb_receivetcp.Name = "rtb_receivetcp";
            this.rtb_receivetcp.Size = new System.Drawing.Size(235, 135);
            this.rtb_receivetcp.TabIndex = 4;
            this.rtb_receivetcp.Text = "";
            // 
            // GUI
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(800, 450);
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
            this.pn_mapcontainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_Map;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Panel pn_mapcontainer;
        private System.Windows.Forms.RichTextBox rtb_sendtcp;
        private System.Windows.Forms.RichTextBox rtb_receivetcp;
    }
}