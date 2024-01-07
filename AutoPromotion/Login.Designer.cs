namespace AutoPromotion
{
    partial class Login
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.txt_PW = new System.Windows.Forms.TextBox();
            this.txt_ID = new System.Windows.Forms.TextBox();
            this.btn_Log = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblVer = new System.Windows.Forms.Label();
            this.chk_DB_Handler = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // txt_PW
            // 
            this.txt_PW.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt_PW.ForeColor = System.Drawing.Color.Blue;
            this.txt_PW.Location = new System.Drawing.Point(56, 280);
            this.txt_PW.Name = "txt_PW";
            this.txt_PW.Size = new System.Drawing.Size(150, 21);
            this.txt_PW.TabIndex = 831;
            this.txt_PW.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_PW.UseSystemPasswordChar = true;
            this.txt_PW.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_PW_KeyDown);
            // 
            // txt_ID
            // 
            this.txt_ID.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt_ID.ForeColor = System.Drawing.Color.Blue;
            this.txt_ID.Location = new System.Drawing.Point(56, 252);
            this.txt_ID.Name = "txt_ID";
            this.txt_ID.Size = new System.Drawing.Size(150, 21);
            this.txt_ID.TabIndex = 830;
            this.txt_ID.Text = "admin";
            this.txt_ID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_ID.TextChanged += new System.EventHandler(this.txt_ID_TextChanged);
            // 
            // btn_Log
            // 
            this.btn_Log.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.btn_Log.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Log.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_Log.ForeColor = System.Drawing.Color.White;
            this.btn_Log.Image = ((System.Drawing.Image)(resources.GetObject("btn_Log.Image")));
            this.btn_Log.Location = new System.Drawing.Point(56, 315);
            this.btn_Log.Name = "btn_Log";
            this.btn_Log.Size = new System.Drawing.Size(150, 50);
            this.btn_Log.TabIndex = 832;
            this.btn_Log.Text = "로그인";
            this.btn_Log.UseVisualStyleBackColor = false;
            this.btn_Log.Click += new System.EventHandler(this.btn_Log_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(0, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(269, 402);
            this.pictureBox2.TabIndex = 834;
            this.pictureBox2.TabStop = false;
            // 
            // lblVer
            // 
            this.lblVer.BackColor = System.Drawing.Color.Transparent;
            this.lblVer.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblVer.ForeColor = System.Drawing.Color.Silver;
            this.lblVer.Location = new System.Drawing.Point(1, 222);
            this.lblVer.Name = "lblVer";
            this.lblVer.Size = new System.Drawing.Size(262, 20);
            this.lblVer.TabIndex = 835;
            this.lblVer.Text = "Ver";
            this.lblVer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chk_DB_Handler
            // 
            this.chk_DB_Handler.AutoSize = true;
            this.chk_DB_Handler.ForeColor = System.Drawing.Color.Black;
            this.chk_DB_Handler.Location = new System.Drawing.Point(58, 200);
            this.chk_DB_Handler.Name = "chk_DB_Handler";
            this.chk_DB_Handler.Size = new System.Drawing.Size(144, 16);
            this.chk_DB_Handler.TabIndex = 937;
            this.chk_DB_Handler.Text = "DB업데이트 화면 가기";
            this.chk_DB_Handler.UseVisualStyleBackColor = true;
            this.chk_DB_Handler.Visible = false;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(264, 396);
            this.Controls.Add(this.chk_DB_Handler);
            this.Controls.Add(this.lblVer);
            this.Controls.Add(this.txt_PW);
            this.Controls.Add(this.txt_ID);
            this.Controls.Add(this.btn_Log);
            this.Controls.Add(this.pictureBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "로그인";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Login_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_PW;
        private System.Windows.Forms.TextBox txt_ID;
        public System.Windows.Forms.Button btn_Log;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox pictureBox2;
        internal System.Windows.Forms.Label lblVer;
        private System.Windows.Forms.CheckBox chk_DB_Handler;

    }
}