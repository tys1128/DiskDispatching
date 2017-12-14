namespace Show
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.PictureBoxFCFS = new System.Windows.Forms.PictureBox();
            this.pictureBoxSSTF = new System.Windows.Forms.PictureBox();
            this.pictureBoxSCAN = new System.Windows.Forms.PictureBox();
            this.pictureBoxLOOK = new System.Windows.Forms.PictureBox();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.FCFS_Button = new System.Windows.Forms.Button();
            this.SSTF_Button = new System.Windows.Forms.Button();
            this.LOOK_Button = new System.Windows.Forms.Button();
            this.SCAN_Button = new System.Windows.Forms.Button();
            this.ReBot = new System.Windows.Forms.Button();
            this.BotOthers = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxFCFS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSSTF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSCAN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLOOK)).BeginInit();
            this.SuspendLayout();
            // 
            // PictureBoxFCFS
            // 
            this.PictureBoxFCFS.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("PictureBoxFCFS.BackgroundImage")));
            this.PictureBoxFCFS.Location = new System.Drawing.Point(0, 0);
            this.PictureBoxFCFS.Name = "PictureBoxFCFS";
            this.PictureBoxFCFS.Size = new System.Drawing.Size(675, 368);
            this.PictureBoxFCFS.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBoxFCFS.TabIndex = 0;
            this.PictureBoxFCFS.TabStop = false;
            // 
            // pictureBoxSSTF
            // 
            this.pictureBoxSSTF.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBoxSSTF.BackgroundImage")));
            this.pictureBoxSSTF.Location = new System.Drawing.Point(675, 0);
            this.pictureBoxSSTF.Name = "pictureBoxSSTF";
            this.pictureBoxSSTF.Size = new System.Drawing.Size(675, 368);
            this.pictureBoxSSTF.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxSSTF.TabIndex = 1;
            this.pictureBoxSSTF.TabStop = false;
            // 
            // pictureBoxSCAN
            // 
            this.pictureBoxSCAN.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBoxSCAN.BackgroundImage")));
            this.pictureBoxSCAN.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxSCAN.Image")));
            this.pictureBoxSCAN.Location = new System.Drawing.Point(675, 369);
            this.pictureBoxSCAN.Name = "pictureBoxSCAN";
            this.pictureBoxSCAN.Size = new System.Drawing.Size(675, 368);
            this.pictureBoxSCAN.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxSCAN.TabIndex = 3;
            this.pictureBoxSCAN.TabStop = false;
            // 
            // pictureBoxLOOK
            // 
            this.pictureBoxLOOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBoxLOOK.BackgroundImage")));
            this.pictureBoxLOOK.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxLOOK.Image")));
            this.pictureBoxLOOK.Location = new System.Drawing.Point(0, 369);
            this.pictureBoxLOOK.Name = "pictureBoxLOOK";
            this.pictureBoxLOOK.Size = new System.Drawing.Size(675, 368);
            this.pictureBoxLOOK.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxLOOK.TabIndex = 2;
            this.pictureBoxLOOK.TabStop = false;
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 1;
            this.timer.Tick += new System.EventHandler(this.Update);
            // 
            // FCFS_Button
            // 
            this.FCFS_Button.Font = new System.Drawing.Font("宋体", 20.25F);
            this.FCFS_Button.Location = new System.Drawing.Point(0, 0);
            this.FCFS_Button.Name = "FCFS_Button";
            this.FCFS_Button.Size = new System.Drawing.Size(97, 36);
            this.FCFS_Button.TabIndex = 8;
            this.FCFS_Button.Text = "FCFS";
            this.FCFS_Button.UseVisualStyleBackColor = true;
            this.FCFS_Button.Click += new System.EventHandler(this.FCFS_Button_Click);
            // 
            // SSTF_Button
            // 
            this.SSTF_Button.Font = new System.Drawing.Font("宋体", 20.25F);
            this.SSTF_Button.Location = new System.Drawing.Point(675, 0);
            this.SSTF_Button.Name = "SSTF_Button";
            this.SSTF_Button.Size = new System.Drawing.Size(97, 36);
            this.SSTF_Button.TabIndex = 9;
            this.SSTF_Button.Text = "SSTF";
            this.SSTF_Button.UseVisualStyleBackColor = true;
            this.SSTF_Button.Click += new System.EventHandler(this.SSTF_Button_Click);
            // 
            // LOOK_Button
            // 
            this.LOOK_Button.Font = new System.Drawing.Font("宋体", 20.25F);
            this.LOOK_Button.Location = new System.Drawing.Point(0, 369);
            this.LOOK_Button.Name = "LOOK_Button";
            this.LOOK_Button.Size = new System.Drawing.Size(97, 36);
            this.LOOK_Button.TabIndex = 10;
            this.LOOK_Button.Text = "LOOK";
            this.LOOK_Button.UseVisualStyleBackColor = true;
            this.LOOK_Button.Click += new System.EventHandler(this.LOOK_Button_Click);
            // 
            // SCAN_Button
            // 
            this.SCAN_Button.Font = new System.Drawing.Font("宋体", 20.25F);
            this.SCAN_Button.Location = new System.Drawing.Point(675, 369);
            this.SCAN_Button.Name = "SCAN_Button";
            this.SCAN_Button.Size = new System.Drawing.Size(97, 36);
            this.SCAN_Button.TabIndex = 11;
            this.SCAN_Button.Text = "SCAN";
            this.SCAN_Button.UseVisualStyleBackColor = true;
            this.SCAN_Button.Click += new System.EventHandler(this.SCAN_Button_Click);
            // 
            // ReBot
            // 
            this.ReBot.Font = new System.Drawing.Font("宋体", 20.25F);
            this.ReBot.Location = new System.Drawing.Point(352, 0);
            this.ReBot.Name = "ReBot";
            this.ReBot.Size = new System.Drawing.Size(170, 36);
            this.ReBot.TabIndex = 12;
            this.ReBot.Text = "重置";
            this.ReBot.UseVisualStyleBackColor = true;
            this.ReBot.Click += new System.EventHandler(this.OnRebot);
            // 
            // BotOthers
            // 
            this.BotOthers.Font = new System.Drawing.Font("宋体", 20.25F);
            this.BotOthers.Location = new System.Drawing.Point(352, 42);
            this.BotOthers.Name = "BotOthers";
            this.BotOthers.Size = new System.Drawing.Size(170, 36);
            this.BotOthers.TabIndex = 13;
            this.BotOthers.Text = "启动剩余的";
            this.BotOthers.UseVisualStyleBackColor = true;
            this.BotOthers.Click += new System.EventHandler(this.RebotOthers);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 729);
            this.Controls.Add(this.BotOthers);
            this.Controls.Add(this.ReBot);
            this.Controls.Add(this.SCAN_Button);
            this.Controls.Add(this.LOOK_Button);
            this.Controls.Add(this.SSTF_Button);
            this.Controls.Add(this.FCFS_Button);
            this.Controls.Add(this.pictureBoxSCAN);
            this.Controls.Add(this.pictureBoxLOOK);
            this.Controls.Add(this.pictureBoxSSTF);
            this.Controls.Add(this.PictureBoxFCFS);
            this.MaximumSize = new System.Drawing.Size(1366, 768);
            this.MinimumSize = new System.Drawing.Size(1366, 768);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxFCFS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSSTF)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSCAN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLOOK)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox PictureBoxFCFS;
        private System.Windows.Forms.PictureBox pictureBoxSSTF;
        private System.Windows.Forms.PictureBox pictureBoxSCAN;
        private System.Windows.Forms.PictureBox pictureBoxLOOK;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Button FCFS_Button;
        private System.Windows.Forms.Button SSTF_Button;
        private System.Windows.Forms.Button LOOK_Button;
        private System.Windows.Forms.Button SCAN_Button;
        private System.Windows.Forms.Button ReBot;
        private System.Windows.Forms.Button BotOthers;
    }
}

