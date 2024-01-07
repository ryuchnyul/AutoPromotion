namespace AutoPromotion
{
    partial class frm_PROMOTION
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_PROMOTION));
            this.lbl_Line = new System.Windows.Forms.Label();
            this.btn_ManualStart = new System.Windows.Forms.Button();
            this.btn_PGMEnd = new System.Windows.Forms.Button();
            this.btn_PGMStart = new System.Windows.Forms.Button();
            this.btn_Pause = new System.Windows.Forms.Button();
            this.btn_ReStart = new System.Windows.Forms.Button();
            this.clib_Channel = new System.Windows.Forms.CheckedListBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.ProgressBar1 = new System.Windows.Forms.ProgressBar();
            this.timer_Load = new System.Windows.Forms.Timer(this.components);
            this.timer_PGMStart_Auto = new System.Windows.Forms.Timer(this.components);
            this.timer_Loop = new System.Windows.Forms.Timer(this.components);
            this.clib_Cate1 = new System.Windows.Forms.CheckedListBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.clib_Cate2 = new System.Windows.Forms.CheckedListBox();
            this.clib_Cate3 = new System.Windows.Forms.CheckedListBox();
            this.clib_Cate4 = new System.Windows.Forms.CheckedListBox();
            this.txt_Rank_Start = new System.Windows.Forms.TextBox();
            this.lbl_Info02 = new System.Windows.Forms.Label();
            this.txt_Rank_End = new System.Windows.Forms.TextBox();
            this.lbl_Info03 = new System.Windows.Forms.Label();
            this.txt_Promotion_Post = new System.Windows.Forms.TextBox();
            this.txt_Prom_Length = new System.Windows.Forms.TextBox();
            this.lbl_Info01 = new System.Windows.Forms.Label();
            this.clib_Channel_Num = new System.Windows.Forms.CheckedListBox();
            this.btn_Save = new System.Windows.Forms.Button();
            this.chk_BrowserView = new System.Windows.Forms.CheckBox();
            this.txt_Work_Count = new System.Windows.Forms.TextBox();
            this.txt_TestKeyword = new System.Windows.Forms.TextBox();
            this.chk_KeyTest = new System.Windows.Forms.CheckBox();
            this.txt_Scroll = new System.Windows.Forms.TextBox();
            this.txt_WorkNum = new System.Windows.Forms.TextBox();
            this.chk_CateSelect = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_Login = new System.Windows.Forms.Button();
            this.lbl_Info05 = new System.Windows.Forms.Label();
            this.txt_UserLastAct = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.txt_WorkKeyword = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.cbx_ScrollSec = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_CheckingRank = new System.Windows.Forms.TextBox();
            this.txt_CateID = new System.Windows.Forms.TextBox();
            this.panelSideMenu = new System.Windows.Forms.Panel();
            this.checkBoxHide = new System.Windows.Forms.CheckBox();
            this.timerSliding = new System.Windows.Forms.Timer(this.components);
            this.txt_TTLProdCount = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_01 = new System.Windows.Forms.Button();
            this.txt_WorkChannel = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btn_Test = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.chk_Error = new System.Windows.Forms.CheckBox();
            this.panelSideMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_Line
            // 
            this.lbl_Line.BackColor = System.Drawing.SystemColors.Control;
            this.lbl_Line.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_Line.ForeColor = System.Drawing.Color.Black;
            this.lbl_Line.Location = new System.Drawing.Point(13, 58);
            this.lbl_Line.Name = "lbl_Line";
            this.lbl_Line.Size = new System.Drawing.Size(1318, 2);
            this.lbl_Line.TabIndex = 935;
            this.lbl_Line.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btn_ManualStart
            // 
            this.btn_ManualStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.btn_ManualStart.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_ManualStart.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_ManualStart.ForeColor = System.Drawing.Color.White;
            this.btn_ManualStart.Image = ((System.Drawing.Image)(resources.GetObject("btn_ManualStart.Image")));
            this.btn_ManualStart.Location = new System.Drawing.Point(587, 69);
            this.btn_ManualStart.Name = "btn_ManualStart";
            this.btn_ManualStart.Size = new System.Drawing.Size(176, 35);
            this.btn_ManualStart.TabIndex = 934;
            this.btn_ManualStart.Text = "작업 이어하기";
            this.btn_ManualStart.UseVisualStyleBackColor = false;
            this.btn_ManualStart.Click += new System.EventHandler(this.btn_ManualStart_Click);
            // 
            // btn_PGMEnd
            // 
            this.btn_PGMEnd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.btn_PGMEnd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_PGMEnd.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_PGMEnd.ForeColor = System.Drawing.Color.White;
            this.btn_PGMEnd.Image = ((System.Drawing.Image)(resources.GetObject("btn_PGMEnd.Image")));
            this.btn_PGMEnd.Location = new System.Drawing.Point(481, 69);
            this.btn_PGMEnd.Name = "btn_PGMEnd";
            this.btn_PGMEnd.Size = new System.Drawing.Size(100, 35);
            this.btn_PGMEnd.TabIndex = 933;
            this.btn_PGMEnd.Text = "작업 종료";
            this.btn_PGMEnd.UseVisualStyleBackColor = false;
            this.btn_PGMEnd.Click += new System.EventHandler(this.btn_PGMEnd_Click);
            // 
            // btn_PGMStart
            // 
            this.btn_PGMStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.btn_PGMStart.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_PGMStart.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_PGMStart.ForeColor = System.Drawing.Color.Black;
            this.btn_PGMStart.Image = ((System.Drawing.Image)(resources.GetObject("btn_PGMStart.Image")));
            this.btn_PGMStart.Location = new System.Drawing.Point(13, 69);
            this.btn_PGMStart.Name = "btn_PGMStart";
            this.btn_PGMStart.Size = new System.Drawing.Size(292, 35);
            this.btn_PGMStart.TabIndex = 930;
            this.btn_PGMStart.Text = "프로그램 시작";
            this.btn_PGMStart.UseVisualStyleBackColor = false;
            this.btn_PGMStart.Click += new System.EventHandler(this.btn_PGMStart_Click);
            // 
            // btn_Pause
            // 
            this.btn_Pause.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btn_Pause.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Pause.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_Pause.ForeColor = System.Drawing.Color.Black;
            this.btn_Pause.Image = ((System.Drawing.Image)(resources.GetObject("btn_Pause.Image")));
            this.btn_Pause.Location = new System.Drawing.Point(311, 69);
            this.btn_Pause.Name = "btn_Pause";
            this.btn_Pause.Size = new System.Drawing.Size(80, 35);
            this.btn_Pause.TabIndex = 931;
            this.btn_Pause.Text = "|| 일시정지";
            this.btn_Pause.UseVisualStyleBackColor = false;
            this.btn_Pause.Click += new System.EventHandler(this.btn_Pause_Click);
            // 
            // btn_ReStart
            // 
            this.btn_ReStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btn_ReStart.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_ReStart.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_ReStart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btn_ReStart.Image = ((System.Drawing.Image)(resources.GetObject("btn_ReStart.Image")));
            this.btn_ReStart.Location = new System.Drawing.Point(395, 69);
            this.btn_ReStart.Name = "btn_ReStart";
            this.btn_ReStart.Size = new System.Drawing.Size(80, 35);
            this.btn_ReStart.TabIndex = 932;
            this.btn_ReStart.Text = "▶ 재 시작";
            this.btn_ReStart.UseVisualStyleBackColor = false;
            this.btn_ReStart.Click += new System.EventHandler(this.btn_ReStart_Click);
            // 
            // clib_Channel
            // 
            this.clib_Channel.CheckOnClick = true;
            this.clib_Channel.FormattingEnabled = true;
            this.clib_Channel.Location = new System.Drawing.Point(397, 162);
            this.clib_Channel.Name = "clib_Channel";
            this.clib_Channel.Size = new System.Drawing.Size(180, 340);
            this.clib_Channel.TabIndex = 929;
            this.clib_Channel.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clib_Channel_ItemCheck);
            this.clib_Channel.SelectedIndexChanged += new System.EventHandler(this.clib_Channel_SelectedIndexChanged);
            this.clib_Channel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.clib_Channel_MouseDown);
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.richTextBox1.ForeColor = System.Drawing.Color.LightGray;
            this.richTextBox1.Location = new System.Drawing.Point(11, 136);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(380, 276);
            this.richTextBox1.TabIndex = 925;
            this.richTextBox1.Text = "";
            // 
            // ProgressBar1
            // 
            this.ProgressBar1.Location = new System.Drawing.Point(14, 114);
            this.ProgressBar1.Name = "ProgressBar1";
            this.ProgressBar1.Size = new System.Drawing.Size(377, 16);
            this.ProgressBar1.TabIndex = 924;
            // 
            // timer_Load
            // 
            this.timer_Load.Tick += new System.EventHandler(this.timer_Load_Tick);
            // 
            // timer_PGMStart_Auto
            // 
            this.timer_PGMStart_Auto.Tick += new System.EventHandler(this.timer_PGMStart_Auto_Tick);
            // 
            // timer_Loop
            // 
            this.timer_Loop.Tick += new System.EventHandler(this.timer_Loop_Tick);
            // 
            // clib_Cate1
            // 
            this.clib_Cate1.CheckOnClick = true;
            this.clib_Cate1.FormattingEnabled = true;
            this.clib_Cate1.Location = new System.Drawing.Point(583, 162);
            this.clib_Cate1.Name = "clib_Cate1";
            this.clib_Cate1.Size = new System.Drawing.Size(180, 564);
            this.clib_Cate1.TabIndex = 937;
            this.clib_Cate1.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clib_Cate1_ItemCheck);
            this.clib_Cate1.SelectedIndexChanged += new System.EventHandler(this.clib_Cate1_SelectedIndexChanged);
            this.clib_Cate1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.clib_Cate1_MouseDown);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox1.Location = new System.Drawing.Point(397, 116);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(180, 40);
            this.textBox1.TabIndex = 940;
            this.textBox1.Text = "\r\n채널 리스트";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.SystemColors.Control;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox2.Location = new System.Drawing.Point(583, 116);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(180, 40);
            this.textBox2.TabIndex = 941;
            this.textBox2.Text = "\r\n대분류";
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.SystemColors.Control;
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox3.Location = new System.Drawing.Point(774, 116);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(180, 40);
            this.textBox3.TabIndex = 942;
            this.textBox3.Text = "\r\n중분류";
            this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.SystemColors.Control;
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox4.Location = new System.Drawing.Point(960, 116);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(180, 40);
            this.textBox4.TabIndex = 943;
            this.textBox4.Text = "\r\n소분류";
            this.textBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox5
            // 
            this.textBox5.BackColor = System.Drawing.SystemColors.Control;
            this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox5.Location = new System.Drawing.Point(1146, 116);
            this.textBox5.Multiline = true;
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new System.Drawing.Size(180, 40);
            this.textBox5.TabIndex = 944;
            this.textBox5.Text = "\r\n세분류";
            this.textBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // clib_Cate2
            // 
            this.clib_Cate2.CheckOnClick = true;
            this.clib_Cate2.FormattingEnabled = true;
            this.clib_Cate2.Location = new System.Drawing.Point(774, 162);
            this.clib_Cate2.Name = "clib_Cate2";
            this.clib_Cate2.Size = new System.Drawing.Size(180, 564);
            this.clib_Cate2.TabIndex = 945;
            this.clib_Cate2.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clib_Cate2_ItemCheck);
            this.clib_Cate2.SelectedIndexChanged += new System.EventHandler(this.clib_Cate2_SelectedIndexChanged);
            this.clib_Cate2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.clib_Cate2_MouseDown);
            // 
            // clib_Cate3
            // 
            this.clib_Cate3.CheckOnClick = true;
            this.clib_Cate3.FormattingEnabled = true;
            this.clib_Cate3.Location = new System.Drawing.Point(960, 162);
            this.clib_Cate3.Name = "clib_Cate3";
            this.clib_Cate3.Size = new System.Drawing.Size(180, 564);
            this.clib_Cate3.TabIndex = 946;
            this.clib_Cate3.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clib_Cate3_ItemCheck);
            this.clib_Cate3.SelectedIndexChanged += new System.EventHandler(this.clib_Cate3_SelectedIndexChanged);
            this.clib_Cate3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.clib_Cate3_MouseDown);
            // 
            // clib_Cate4
            // 
            this.clib_Cate4.CheckOnClick = true;
            this.clib_Cate4.FormattingEnabled = true;
            this.clib_Cate4.Location = new System.Drawing.Point(1146, 162);
            this.clib_Cate4.Name = "clib_Cate4";
            this.clib_Cate4.Size = new System.Drawing.Size(180, 564);
            this.clib_Cate4.TabIndex = 947;
            this.clib_Cate4.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clib_Cate4_ItemCheck);
            this.clib_Cate4.SelectedIndexChanged += new System.EventHandler(this.clib_Cate4_SelectedIndexChanged);
            // 
            // txt_Rank_Start
            // 
            this.txt_Rank_Start.BackColor = System.Drawing.Color.White;
            this.txt_Rank_Start.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Rank_Start.ForeColor = System.Drawing.Color.Black;
            this.txt_Rank_Start.Location = new System.Drawing.Point(100, 29);
            this.txt_Rank_Start.Name = "txt_Rank_Start";
            this.txt_Rank_Start.Size = new System.Drawing.Size(40, 21);
            this.txt_Rank_Start.TabIndex = 948;
            this.txt_Rank_Start.Text = "31";
            this.txt_Rank_Start.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lbl_Info02
            // 
            this.lbl_Info02.AutoSize = true;
            this.lbl_Info02.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbl_Info02.Location = new System.Drawing.Point(12, 33);
            this.lbl_Info02.Name = "lbl_Info02";
            this.lbl_Info02.Size = new System.Drawing.Size(79, 12);
            this.lbl_Info02.TabIndex = 949;
            this.lbl_Info02.Text = "■ 작업 영역 :";
            // 
            // txt_Rank_End
            // 
            this.txt_Rank_End.BackColor = System.Drawing.Color.White;
            this.txt_Rank_End.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Rank_End.ForeColor = System.Drawing.Color.Black;
            this.txt_Rank_End.Location = new System.Drawing.Point(178, 29);
            this.txt_Rank_End.Name = "txt_Rank_End";
            this.txt_Rank_End.Size = new System.Drawing.Size(40, 21);
            this.txt_Rank_End.TabIndex = 950;
            this.txt_Rank_End.Text = "200";
            this.txt_Rank_End.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lbl_Info03
            // 
            this.lbl_Info03.AutoSize = true;
            this.lbl_Info03.ForeColor = System.Drawing.Color.Gray;
            this.lbl_Info03.Location = new System.Drawing.Point(141, 32);
            this.lbl_Info03.Name = "lbl_Info03";
            this.lbl_Info03.Size = new System.Drawing.Size(94, 12);
            this.lbl_Info03.TabIndex = 951;
            this.lbl_Info03.Text = "위  ~            위";
            // 
            // txt_Promotion_Post
            // 
            this.txt_Promotion_Post.BackColor = System.Drawing.Color.White;
            this.txt_Promotion_Post.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt_Promotion_Post.Location = new System.Drawing.Point(11, 445);
            this.txt_Promotion_Post.Multiline = true;
            this.txt_Promotion_Post.Name = "txt_Promotion_Post";
            this.txt_Promotion_Post.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_Promotion_Post.Size = new System.Drawing.Size(380, 279);
            this.txt_Promotion_Post.TabIndex = 952;
            this.txt_Promotion_Post.Text = resources.GetString("txt_Promotion_Post.Text");
            this.txt_Promotion_Post.TextChanged += new System.EventHandler(this.txt_Promotion_Post_TextChanged);
            // 
            // txt_Prom_Length
            // 
            this.txt_Prom_Length.BackColor = System.Drawing.Color.White;
            this.txt_Prom_Length.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Prom_Length.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt_Prom_Length.ForeColor = System.Drawing.Color.Red;
            this.txt_Prom_Length.Location = new System.Drawing.Point(11, 418);
            this.txt_Prom_Length.Name = "txt_Prom_Length";
            this.txt_Prom_Length.ReadOnly = true;
            this.txt_Prom_Length.Size = new System.Drawing.Size(114, 21);
            this.txt_Prom_Length.TabIndex = 954;
            this.txt_Prom_Length.Text = "1 / 1000";
            this.txt_Prom_Length.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lbl_Info01
            // 
            this.lbl_Info01.AutoSize = true;
            this.lbl_Info01.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbl_Info01.Location = new System.Drawing.Point(12, 400);
            this.lbl_Info01.Name = "lbl_Info01";
            this.lbl_Info01.Size = new System.Drawing.Size(341, 12);
            this.lbl_Info01.TabIndex = 955;
            this.lbl_Info01.Text = "★★★ 홍보글의 글자수는 1,000자 이하로 작성 하세요! ★★★";
            this.lbl_Info01.Visible = false;
            // 
            // clib_Channel_Num
            // 
            this.clib_Channel_Num.CheckOnClick = true;
            this.clib_Channel_Num.FormattingEnabled = true;
            this.clib_Channel_Num.Location = new System.Drawing.Point(397, 514);
            this.clib_Channel_Num.Name = "clib_Channel_Num";
            this.clib_Channel_Num.Size = new System.Drawing.Size(180, 212);
            this.clib_Channel_Num.TabIndex = 964;
            this.clib_Channel_Num.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clib_Channel_Num_ItemCheck);
            this.clib_Channel_Num.SelectedIndexChanged += new System.EventHandler(this.clib_Channel_Num_SelectedIndexChanged);
            this.clib_Channel_Num.MouseDown += new System.Windows.Forms.MouseEventHandler(this.clib_Channel_Num_MouseDown);
            // 
            // btn_Save
            // 
            this.btn_Save.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.btn_Save.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Save.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_Save.ForeColor = System.Drawing.Color.White;
            this.btn_Save.Image = ((System.Drawing.Image)(resources.GetObject("btn_Save.Image")));
            this.btn_Save.Location = new System.Drawing.Point(131, 418);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(165, 21);
            this.btn_Save.TabIndex = 966;
            this.btn_Save.Text = "포스트 저장";
            this.btn_Save.UseVisualStyleBackColor = false;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // chk_BrowserView
            // 
            this.chk_BrowserView.AutoSize = true;
            this.chk_BrowserView.Checked = true;
            this.chk_BrowserView.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_BrowserView.ForeColor = System.Drawing.Color.Black;
            this.chk_BrowserView.Location = new System.Drawing.Point(102, 8);
            this.chk_BrowserView.Name = "chk_BrowserView";
            this.chk_BrowserView.Size = new System.Drawing.Size(96, 16);
            this.chk_BrowserView.TabIndex = 936;
            this.chk_BrowserView.Text = "브라우저보기";
            this.chk_BrowserView.UseVisualStyleBackColor = true;
            // 
            // txt_Work_Count
            // 
            this.txt_Work_Count.BackColor = System.Drawing.Color.White;
            this.txt_Work_Count.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Work_Count.ForeColor = System.Drawing.Color.Black;
            this.txt_Work_Count.Location = new System.Drawing.Point(642, 29);
            this.txt_Work_Count.Name = "txt_Work_Count";
            this.txt_Work_Count.ReadOnly = true;
            this.txt_Work_Count.Size = new System.Drawing.Size(50, 21);
            this.txt_Work_Count.TabIndex = 970;
            this.txt_Work_Count.Text = "0";
            this.txt_Work_Count.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_TestKeyword
            // 
            this.txt_TestKeyword.BackColor = System.Drawing.Color.White;
            this.txt_TestKeyword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_TestKeyword.Enabled = false;
            this.txt_TestKeyword.ForeColor = System.Drawing.Color.Black;
            this.txt_TestKeyword.Location = new System.Drawing.Point(884, 29);
            this.txt_TestKeyword.Name = "txt_TestKeyword";
            this.txt_TestKeyword.Size = new System.Drawing.Size(100, 21);
            this.txt_TestKeyword.TabIndex = 972;
            this.txt_TestKeyword.Text = "원피스";
            this.txt_TestKeyword.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // chk_KeyTest
            // 
            this.chk_KeyTest.AutoSize = true;
            this.chk_KeyTest.ForeColor = System.Drawing.Color.Black;
            this.chk_KeyTest.Location = new System.Drawing.Point(884, 12);
            this.chk_KeyTest.Name = "chk_KeyTest";
            this.chk_KeyTest.Size = new System.Drawing.Size(100, 16);
            this.chk_KeyTest.TabIndex = 973;
            this.chk_KeyTest.Text = "키워드 테스트";
            this.chk_KeyTest.UseVisualStyleBackColor = true;
            this.chk_KeyTest.CheckedChanged += new System.EventHandler(this.chk_KeyTest_CheckedChanged);
            // 
            // txt_Scroll
            // 
            this.txt_Scroll.BackColor = System.Drawing.Color.White;
            this.txt_Scroll.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Scroll.ForeColor = System.Drawing.Color.Black;
            this.txt_Scroll.Location = new System.Drawing.Point(1155, 9);
            this.txt_Scroll.Name = "txt_Scroll";
            this.txt_Scroll.Size = new System.Drawing.Size(57, 21);
            this.txt_Scroll.TabIndex = 975;
            this.txt_Scroll.Text = "-100";
            this.txt_Scroll.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_WorkNum
            // 
            this.txt_WorkNum.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txt_WorkNum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_WorkNum.ForeColor = System.Drawing.Color.Black;
            this.txt_WorkNum.Location = new System.Drawing.Point(586, 29);
            this.txt_WorkNum.Name = "txt_WorkNum";
            this.txt_WorkNum.Size = new System.Drawing.Size(50, 21);
            this.txt_WorkNum.TabIndex = 976;
            this.txt_WorkNum.Text = "위치";
            this.txt_WorkNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // chk_CateSelect
            // 
            this.chk_CateSelect.AutoSize = true;
            this.chk_CateSelect.Checked = true;
            this.chk_CateSelect.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_CateSelect.ForeColor = System.Drawing.Color.Black;
            this.chk_CateSelect.Location = new System.Drawing.Point(204, 8);
            this.chk_CateSelect.Name = "chk_CateSelect";
            this.chk_CateSelect.Size = new System.Drawing.Size(112, 16);
            this.chk_CateSelect.TabIndex = 978;
            this.chk_CateSelect.Text = "세분류 선택방식";
            this.chk_CateSelect.UseVisualStyleBackColor = true;
            this.chk_CateSelect.CheckedChanged += new System.EventHandler(this.chk_CateSelect_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 12);
            this.label2.TabIndex = 979;
            this.label2.Text = "■ 작업 조건 :";
            // 
            // btn_Login
            // 
            this.btn_Login.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btn_Login.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Login.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_Login.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btn_Login.Image = ((System.Drawing.Image)(resources.GetObject("btn_Login.Image")));
            this.btn_Login.Location = new System.Drawing.Point(774, 69);
            this.btn_Login.Name = "btn_Login";
            this.btn_Login.Size = new System.Drawing.Size(80, 35);
            this.btn_Login.TabIndex = 980;
            this.btn_Login.Text = "로그인하기";
            this.btn_Login.UseVisualStyleBackColor = false;
            this.btn_Login.Click += new System.EventHandler(this.btn_Login_Click);
            // 
            // lbl_Info05
            // 
            this.lbl_Info05.AutoSize = true;
            this.lbl_Info05.ForeColor = System.Drawing.Color.Gray;
            this.lbl_Info05.Location = new System.Drawing.Point(1093, 12);
            this.lbl_Info05.Name = "lbl_Info05";
            this.lbl_Info05.Size = new System.Drawing.Size(53, 12);
            this.lbl_Info05.TabIndex = 971;
            this.lbl_Info05.Text = "화면위치";
            // 
            // txt_UserLastAct
            // 
            this.txt_UserLastAct.BackColor = System.Drawing.Color.White;
            this.txt_UserLastAct.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_UserLastAct.Enabled = false;
            this.txt_UserLastAct.ForeColor = System.Drawing.Color.Black;
            this.txt_UserLastAct.Location = new System.Drawing.Point(365, 7);
            this.txt_UserLastAct.Name = "txt_UserLastAct";
            this.txt_UserLastAct.Size = new System.Drawing.Size(219, 21);
            this.txt_UserLastAct.TabIndex = 981;
            this.txt_UserLastAct.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_UserLastAct.Visible = false;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.ForeColor = System.Drawing.Color.Black;
            this.checkBox1.Location = new System.Drawing.Point(810, 128);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(116, 16);
            this.checkBox1.TabIndex = 982;
            this.checkBox1.Text = "중분류 부터 작업";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Visible = false;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.ForeColor = System.Drawing.Color.Black;
            this.checkBox2.Location = new System.Drawing.Point(994, 128);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(116, 16);
            this.checkBox2.TabIndex = 983;
            this.checkBox2.Text = "소분류 부터 작업";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.Visible = false;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Checked = true;
            this.checkBox3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox3.ForeColor = System.Drawing.Color.Black;
            this.checkBox3.Location = new System.Drawing.Point(1182, 128);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(116, 16);
            this.checkBox3.TabIndex = 984;
            this.checkBox3.Text = "세분류 부터 작업";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.Visible = false;
            // 
            // txt_WorkKeyword
            // 
            this.txt_WorkKeyword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txt_WorkKeyword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_WorkKeyword.ForeColor = System.Drawing.Color.Black;
            this.txt_WorkKeyword.Location = new System.Drawing.Point(365, 29);
            this.txt_WorkKeyword.Name = "txt_WorkKeyword";
            this.txt_WorkKeyword.Size = new System.Drawing.Size(219, 21);
            this.txt_WorkKeyword.TabIndex = 985;
            this.txt_WorkKeyword.Text = "소분류";
            this.txt_WorkKeyword.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.Location = new System.Drawing.Point(1216, 69);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(110, 35);
            this.button2.TabIndex = 986;
            this.button2.Text = "시작번호 위치로 브라우저 띄우기";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // cbx_ScrollSec
            // 
            this.cbx_ScrollSec.FormattingEnabled = true;
            this.cbx_ScrollSec.Items.AddRange(new object[] {
            "100",
            "300",
            "500",
            "1000",
            "1500",
            "2000",
            "2500",
            "3000"});
            this.cbx_ScrollSec.Location = new System.Drawing.Point(1155, 34);
            this.cbx_ScrollSec.Name = "cbx_ScrollSec";
            this.cbx_ScrollSec.Size = new System.Drawing.Size(57, 20);
            this.cbx_ScrollSec.TabIndex = 987;
            this.cbx_ScrollSec.Text = "100";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Gray;
            this.label4.Location = new System.Drawing.Point(1056, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 988;
            this.label4.Text = "스크롤대기시간";
            // 
            // txt_CheckingRank
            // 
            this.txt_CheckingRank.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.txt_CheckingRank.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_CheckingRank.ForeColor = System.Drawing.Color.Black;
            this.txt_CheckingRank.Location = new System.Drawing.Point(244, 29);
            this.txt_CheckingRank.Name = "txt_CheckingRank";
            this.txt_CheckingRank.Size = new System.Drawing.Size(30, 21);
            this.txt_CheckingRank.TabIndex = 989;
            this.txt_CheckingRank.Text = "1";
            this.txt_CheckingRank.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_CateID
            // 
            this.txt_CateID.BackColor = System.Drawing.Color.White;
            this.txt_CateID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_CateID.Enabled = false;
            this.txt_CateID.ForeColor = System.Drawing.Color.Black;
            this.txt_CateID.Location = new System.Drawing.Point(960, 704);
            this.txt_CateID.Name = "txt_CateID";
            this.txt_CateID.Size = new System.Drawing.Size(366, 21);
            this.txt_CateID.TabIndex = 990;
            this.txt_CateID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_CateID.Visible = false;
            // 
            // panelSideMenu
            // 
            this.panelSideMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panelSideMenu.Controls.Add(this.checkBoxHide);
            this.panelSideMenu.Location = new System.Drawing.Point(713, 116);
            this.panelSideMenu.Name = "panelSideMenu";
            this.panelSideMenu.Size = new System.Drawing.Size(50, 40);
            this.panelSideMenu.TabIndex = 991;
            // 
            // checkBoxHide
            // 
            this.checkBoxHide.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxHide.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.checkBoxHide.FlatAppearance.BorderSize = 0;
            this.checkBoxHide.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.checkBoxHide.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.checkBoxHide.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxHide.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.checkBoxHide.ForeColor = System.Drawing.Color.White;
            this.checkBoxHide.Location = new System.Drawing.Point(0, 0);
            this.checkBoxHide.Name = "checkBoxHide";
            this.checkBoxHide.Size = new System.Drawing.Size(50, 40);
            this.checkBoxHide.TabIndex = 2;
            this.checkBoxHide.Text = ">";
            this.checkBoxHide.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxHide.UseVisualStyleBackColor = true;
            this.checkBoxHide.CheckedChanged += new System.EventHandler(this.checkBoxHide_CheckedChanged);
            // 
            // timerSliding
            // 
            this.timerSliding.Interval = 10;
            this.timerSliding.Tick += new System.EventHandler(this.timerSliding_Tick);
            // 
            // txt_TTLProdCount
            // 
            this.txt_TTLProdCount.BackColor = System.Drawing.Color.White;
            this.txt_TTLProdCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_TTLProdCount.ForeColor = System.Drawing.Color.Black;
            this.txt_TTLProdCount.Location = new System.Drawing.Point(696, 29);
            this.txt_TTLProdCount.Name = "txt_TTLProdCount";
            this.txt_TTLProdCount.Size = new System.Drawing.Size(50, 21);
            this.txt_TTLProdCount.TabIndex = 993;
            this.txt_TTLProdCount.Text = "-1";
            this.txt_TTLProdCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Gray;
            this.label5.Location = new System.Drawing.Point(638, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 992;
            this.label5.Text = "총작업수";
            // 
            // btn_01
            // 
            this.btn_01.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btn_01.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_01.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_01.ForeColor = System.Drawing.Color.Gray;
            this.btn_01.Image = ((System.Drawing.Image)(resources.GetObject("btn_01.Image")));
            this.btn_01.Location = new System.Drawing.Point(302, 418);
            this.btn_01.Name = "btn_01";
            this.btn_01.Size = new System.Drawing.Size(90, 21);
            this.btn_01.TabIndex = 994;
            this.btn_01.Text = "사용법";
            this.btn_01.UseVisualStyleBackColor = false;
            this.btn_01.Click += new System.EventHandler(this.btn_01_Click);
            // 
            // txt_WorkChannel
            // 
            this.txt_WorkChannel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txt_WorkChannel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_WorkChannel.ForeColor = System.Drawing.Color.Black;
            this.txt_WorkChannel.Location = new System.Drawing.Point(283, 29);
            this.txt_WorkChannel.Name = "txt_WorkChannel";
            this.txt_WorkChannel.Size = new System.Drawing.Size(80, 21);
            this.txt_WorkChannel.TabIndex = 995;
            this.txt_WorkChannel.Text = "채널";
            this.txt_WorkChannel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Silver;
            this.label1.Location = new System.Drawing.Point(692, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 996;
            this.label1.Text = "상품수량";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.label6.Location = new System.Drawing.Point(586, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 997;
            this.label6.Text = "작업위치";
            // 
            // btn_Test
            // 
            this.btn_Test.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btn_Test.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Test.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_Test.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btn_Test.Image = ((System.Drawing.Image)(resources.GetObject("btn_Test.Image")));
            this.btn_Test.Location = new System.Drawing.Point(1146, 69);
            this.btn_Test.Name = "btn_Test";
            this.btn_Test.Size = new System.Drawing.Size(64, 35);
            this.btn_Test.TabIndex = 998;
            this.btn_Test.Text = "포스트 관리";
            this.btn_Test.UseVisualStyleBackColor = false;
            this.btn_Test.Click += new System.EventHandler(this.btn_Test_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(778, 569);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(434, 27);
            this.label3.TabIndex = 999;
            this.label3.Text = "배포시 고정된 비밀번호 공백처리";
            this.label3.Visible = false;
            // 
            // chk_Error
            // 
            this.chk_Error.AutoSize = true;
            this.chk_Error.Checked = true;
            this.chk_Error.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_Error.ForeColor = System.Drawing.Color.Black;
            this.chk_Error.Location = new System.Drawing.Point(322, 9);
            this.chk_Error.Name = "chk_Error";
            this.chk_Error.Size = new System.Drawing.Size(72, 16);
            this.chk_Error.TabIndex = 1000;
            this.chk_Error.Text = "오류체크";
            this.chk_Error.UseVisualStyleBackColor = true;
            // 
            // frm_PROMOTION
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1334, 731);
            this.Controls.Add(this.chk_Error);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btn_Test);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_WorkChannel);
            this.Controls.Add(this.btn_01);
            this.Controls.Add(this.txt_TTLProdCount);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.panelSideMenu);
            this.Controls.Add(this.txt_CateID);
            this.Controls.Add(this.txt_CheckingRank);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbx_ScrollSec);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txt_WorkKeyword);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.txt_UserLastAct);
            this.Controls.Add(this.btn_Login);
            this.Controls.Add(this.lbl_Info01);
            this.Controls.Add(this.txt_Promotion_Post);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chk_CateSelect);
            this.Controls.Add(this.txt_WorkNum);
            this.Controls.Add(this.txt_Scroll);
            this.Controls.Add(this.chk_KeyTest);
            this.Controls.Add(this.txt_TestKeyword);
            this.Controls.Add(this.clib_Channel);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.clib_Cate1);
            this.Controls.Add(this.lbl_Line);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.lbl_Info05);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.txt_Work_Count);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.clib_Cate2);
            this.Controls.Add(this.ProgressBar1);
            this.Controls.Add(this.clib_Cate3);
            this.Controls.Add(this.clib_Channel_Num);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.clib_Cate4);
            this.Controls.Add(this.txt_Prom_Length);
            this.Controls.Add(this.txt_Rank_End);
            this.Controls.Add(this.lbl_Info02);
            this.Controls.Add(this.txt_Rank_Start);
            this.Controls.Add(this.chk_BrowserView);
            this.Controls.Add(this.btn_ManualStart);
            this.Controls.Add(this.btn_PGMEnd);
            this.Controls.Add(this.btn_PGMStart);
            this.Controls.Add(this.btn_Pause);
            this.Controls.Add(this.btn_ReStart);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.lbl_Info03);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_PROMOTION";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "홍보프로그램";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frm_PROMOTION_FormClosed);
            this.Load += new System.EventHandler(this.frm_PROMOTION_Load);
            this.panelSideMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_Line;
        public System.Windows.Forms.Button btn_ManualStart;
        public System.Windows.Forms.Button btn_PGMEnd;
        public System.Windows.Forms.Button btn_PGMStart;
        private System.Windows.Forms.Button btn_Pause;
        private System.Windows.Forms.Button btn_ReStart;
        private System.Windows.Forms.CheckedListBox clib_Channel;
        public System.Windows.Forms.RichTextBox richTextBox1;
        internal System.Windows.Forms.ProgressBar ProgressBar1;
        private System.Windows.Forms.Timer timer_Load;
        private System.Windows.Forms.Timer timer_PGMStart_Auto;
        private System.Windows.Forms.Timer timer_Loop;
        private System.Windows.Forms.CheckedListBox clib_Cate1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.CheckedListBox clib_Cate2;
        private System.Windows.Forms.CheckedListBox clib_Cate3;
        private System.Windows.Forms.CheckedListBox clib_Cate4;
        public System.Windows.Forms.TextBox txt_Rank_Start;
        internal System.Windows.Forms.Label lbl_Info02;
        public System.Windows.Forms.TextBox txt_Rank_End;
        internal System.Windows.Forms.Label lbl_Info03;
        public System.Windows.Forms.TextBox txt_Prom_Length;
        internal System.Windows.Forms.Label lbl_Info01;
        public System.Windows.Forms.TextBox txt_Promotion_Post;
        private System.Windows.Forms.CheckedListBox clib_Channel_Num;
        public System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.CheckBox chk_BrowserView;
        public System.Windows.Forms.TextBox txt_Work_Count;
        public System.Windows.Forms.TextBox txt_TestKeyword;
        private System.Windows.Forms.CheckBox chk_KeyTest;
        public System.Windows.Forms.TextBox txt_Scroll;
        public System.Windows.Forms.TextBox txt_WorkNum;
        private System.Windows.Forms.CheckBox chk_CateSelect;
        internal System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_Login;
        internal System.Windows.Forms.Label lbl_Info05;
        public System.Windows.Forms.TextBox txt_UserLastAct;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox3;
        public System.Windows.Forms.TextBox txt_WorkKeyword;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox cbx_ScrollSec;
        internal System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox txt_CheckingRank;
        public System.Windows.Forms.TextBox txt_CateID;
        private System.Windows.Forms.Panel panelSideMenu;
        private System.Windows.Forms.CheckBox checkBoxHide;
        private System.Windows.Forms.Timer timerSliding;
        public System.Windows.Forms.TextBox txt_TTLProdCount;
        internal System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btn_01;
        public System.Windows.Forms.TextBox txt_WorkChannel;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btn_Test;
        internal System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chk_Error;
    }
}