namespace AutoPromotion
{
    partial class frm_Post
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Post));
            this.lbl_Info01 = new System.Windows.Forms.Label();
            this.txt_Prom_Length = new System.Windows.Forms.TextBox();
            this.txt_Promotion_Post = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btn_Save = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.timer_Load = new System.Windows.Forms.Timer(this.components);
            this.clib_Channel = new System.Windows.Forms.CheckedListBox();
            this.clib_Channel_Num = new System.Windows.Forms.CheckedListBox();
            this.btn_NewUserInsert = new System.Windows.Forms.Button();
            this.txt_Promotion_Post_Standard = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_Standard = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.chx_U = new System.Windows.Forms.CheckBox();
            this.chx_M = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lbl_Info01
            // 
            this.lbl_Info01.AutoSize = true;
            this.lbl_Info01.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbl_Info01.Location = new System.Drawing.Point(199, 14);
            this.lbl_Info01.Name = "lbl_Info01";
            this.lbl_Info01.Size = new System.Drawing.Size(401, 12);
            this.lbl_Info01.TabIndex = 960;
            this.lbl_Info01.Text = "★★★ 홍보글의 글자수는 채널별 최대글자수 이하로 작성 하세요! ★★★";
            // 
            // txt_Prom_Length
            // 
            this.txt_Prom_Length.BackColor = System.Drawing.Color.White;
            this.txt_Prom_Length.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Prom_Length.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt_Prom_Length.ForeColor = System.Drawing.Color.Red;
            this.txt_Prom_Length.Location = new System.Drawing.Point(198, 31);
            this.txt_Prom_Length.Name = "txt_Prom_Length";
            this.txt_Prom_Length.ReadOnly = true;
            this.txt_Prom_Length.Size = new System.Drawing.Size(574, 21);
            this.txt_Prom_Length.TabIndex = 959;
            this.txt_Prom_Length.Text = "1 / 1000";
            this.txt_Prom_Length.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_Promotion_Post
            // 
            this.txt_Promotion_Post.BackColor = System.Drawing.Color.White;
            this.txt_Promotion_Post.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt_Promotion_Post.Location = new System.Drawing.Point(198, 58);
            this.txt_Promotion_Post.Multiline = true;
            this.txt_Promotion_Post.Name = "txt_Promotion_Post";
            this.txt_Promotion_Post.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_Promotion_Post.Size = new System.Drawing.Size(574, 558);
            this.txt_Promotion_Post.TabIndex = 958;
            this.txt_Promotion_Post.TextChanged += new System.EventHandler(this.txt_Promotion_Post_TextChanged);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(180, 40);
            this.textBox1.TabIndex = 957;
            this.textBox1.Text = "\r\n채널 리스트";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btn_Save
            // 
            this.btn_Save.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.btn_Save.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Save.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_Save.ForeColor = System.Drawing.Color.White;
            this.btn_Save.Image = ((System.Drawing.Image)(resources.GetObject("btn_Save.Image")));
            this.btn_Save.Location = new System.Drawing.Point(12, 632);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(180, 30);
            this.btn_Save.TabIndex = 962;
            this.btn_Save.Text = "포스트 저장";
            this.btn_Save.UseVisualStyleBackColor = false;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btn_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Cancel.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_Cancel.ForeColor = System.Drawing.Color.Black;
            this.btn_Cancel.Image = ((System.Drawing.Image)(resources.GetObject("btn_Cancel.Image")));
            this.btn_Cancel.Location = new System.Drawing.Point(198, 632);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(60, 30);
            this.btn_Cancel.TabIndex = 961;
            this.btn_Cancel.Text = "닫기";
            this.btn_Cancel.UseVisualStyleBackColor = false;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // timer_Load
            // 
            this.timer_Load.Tick += new System.EventHandler(this.timer_Load_Tick);
            // 
            // clib_Channel
            // 
            this.clib_Channel.CheckOnClick = true;
            this.clib_Channel.FormattingEnabled = true;
            this.clib_Channel.Location = new System.Drawing.Point(12, 58);
            this.clib_Channel.Name = "clib_Channel";
            this.clib_Channel.Size = new System.Drawing.Size(180, 244);
            this.clib_Channel.TabIndex = 966;
            this.clib_Channel.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clib_Channel_ItemCheck);
            this.clib_Channel.SelectedIndexChanged += new System.EventHandler(this.clib_Channel_SelectedIndexChanged);
            this.clib_Channel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.clib_Channel_MouseDown);
            // 
            // clib_Channel_Num
            // 
            this.clib_Channel_Num.CheckOnClick = true;
            this.clib_Channel_Num.FormattingEnabled = true;
            this.clib_Channel_Num.Location = new System.Drawing.Point(12, 308);
            this.clib_Channel_Num.Name = "clib_Channel_Num";
            this.clib_Channel_Num.Size = new System.Drawing.Size(180, 260);
            this.clib_Channel_Num.TabIndex = 967;
            this.clib_Channel_Num.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clib_Channel_Num_ItemCheck);
            this.clib_Channel_Num.SelectedIndexChanged += new System.EventHandler(this.clib_Channel_Num_SelectedIndexChanged);
            this.clib_Channel_Num.MouseDown += new System.Windows.Forms.MouseEventHandler(this.clib_Channel_Num_MouseDown);
            // 
            // btn_NewUserInsert
            // 
            this.btn_NewUserInsert.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.btn_NewUserInsert.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_NewUserInsert.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_NewUserInsert.ForeColor = System.Drawing.Color.Black;
            this.btn_NewUserInsert.Image = ((System.Drawing.Image)(resources.GetObject("btn_NewUserInsert.Image")));
            this.btn_NewUserInsert.Location = new System.Drawing.Point(298, 632);
            this.btn_NewUserInsert.Name = "btn_NewUserInsert";
            this.btn_NewUserInsert.Size = new System.Drawing.Size(328, 30);
            this.btn_NewUserInsert.TabIndex = 968;
            this.btn_NewUserInsert.Text = "로그인 아이디로 기본 포스트 생성";
            this.btn_NewUserInsert.UseVisualStyleBackColor = false;
            this.btn_NewUserInsert.Click += new System.EventHandler(this.btn_NewUserInsert_Click);
            // 
            // txt_Promotion_Post_Standard
            // 
            this.txt_Promotion_Post_Standard.BackColor = System.Drawing.Color.Gainsboro;
            this.txt_Promotion_Post_Standard.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt_Promotion_Post_Standard.ForeColor = System.Drawing.Color.Black;
            this.txt_Promotion_Post_Standard.Location = new System.Drawing.Point(201, 563);
            this.txt_Promotion_Post_Standard.Multiline = true;
            this.txt_Promotion_Post_Standard.Name = "txt_Promotion_Post_Standard";
            this.txt_Promotion_Post_Standard.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_Promotion_Post_Standard.Size = new System.Drawing.Size(574, 53);
            this.txt_Promotion_Post_Standard.TabIndex = 969;
            this.txt_Promotion_Post_Standard.Text = resources.GetString("txt_Promotion_Post_Standard.Text");
            this.txt_Promotion_Post_Standard.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(272, 521);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(434, 27);
            this.label3.TabIndex = 1000;
            this.label3.Text = "배포시 고정된 비밀번호 공백처리";
            this.label3.Visible = false;
            // 
            // btn_Standard
            // 
            this.btn_Standard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btn_Standard.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Standard.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_Standard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btn_Standard.Image = ((System.Drawing.Image)(resources.GetObject("btn_Standard.Image")));
            this.btn_Standard.Location = new System.Drawing.Point(632, 632);
            this.btn_Standard.Name = "btn_Standard";
            this.btn_Standard.Size = new System.Drawing.Size(143, 30);
            this.btn_Standard.TabIndex = 1001;
            this.btn_Standard.Text = "기본 포스트 가져오기";
            this.btn_Standard.UseVisualStyleBackColor = false;
            this.btn_Standard.Click += new System.EventHandler(this.btn_Standard_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(154, 340);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(600, 81);
            this.label1.TabIndex = 1002;
            this.label1.Text = "univent 포스트 생성 전 공백이 나와야 되는데,\r\n체크 변동시 글이 올라옴.\r\n안나오도록 처리";
            this.label1.Visible = false;
            // 
            // chx_U
            // 
            this.chx_U.AutoSize = true;
            this.chx_U.ForeColor = System.Drawing.Color.Black;
            this.chx_U.Location = new System.Drawing.Point(14, 600);
            this.chx_U.Name = "chx_U";
            this.chx_U.Size = new System.Drawing.Size(100, 16);
            this.chx_U.TabIndex = 1004;
            this.chx_U.Text = "사용설명 정비";
            this.chx_U.UseVisualStyleBackColor = true;
            this.chx_U.CheckedChanged += new System.EventHandler(this.chx_U_CheckedChanged);
            // 
            // chx_M
            // 
            this.chx_M.AutoSize = true;
            this.chx_M.ForeColor = System.Drawing.Color.Black;
            this.chx_M.Location = new System.Drawing.Point(14, 581);
            this.chx_M.Name = "chx_M";
            this.chx_M.Size = new System.Drawing.Size(88, 16);
            this.chx_M.TabIndex = 1003;
            this.chx_M.Text = "메뉴얼 정비";
            this.chx_M.UseVisualStyleBackColor = true;
            this.chx_M.CheckedChanged += new System.EventHandler(this.chx_M_CheckedChanged);
            // 
            // frm_Post
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 676);
            this.Controls.Add(this.chx_U);
            this.Controls.Add(this.chx_M);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_Standard);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_Promotion_Post_Standard);
            this.Controls.Add(this.btn_NewUserInsert);
            this.Controls.Add(this.clib_Channel);
            this.Controls.Add(this.clib_Channel_Num);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.lbl_Info01);
            this.Controls.Add(this.txt_Prom_Length);
            this.Controls.Add(this.txt_Promotion_Post);
            this.Controls.Add(this.textBox1);
            this.Name = "frm_Post";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "포스트 관리";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frm_Post_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label lbl_Info01;
        public System.Windows.Forms.TextBox txt_Prom_Length;
        public System.Windows.Forms.TextBox txt_Promotion_Post;
        private System.Windows.Forms.TextBox textBox1;
        public System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Timer timer_Load;
        private System.Windows.Forms.CheckedListBox clib_Channel;
        private System.Windows.Forms.CheckedListBox clib_Channel_Num;
        public System.Windows.Forms.Button btn_NewUserInsert;
        public System.Windows.Forms.TextBox txt_Promotion_Post_Standard;
        internal System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_Standard;
        internal System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chx_U;
        private System.Windows.Forms.CheckBox chx_M;
    }
}