namespace AutoPromotion
{
    partial class _UserSales
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_UserSales));
            this.liv_Sales_O = new System.Windows.Forms.ListView();
            this.button2 = new System.Windows.Forms.Button();
            this.txt_Sales_Excel = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txt_A = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.btn_PGMEnd = new System.Windows.Forms.Button();
            this.btn_Pause = new System.Windows.Forms.Button();
            this.btn_ReStart = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_End = new System.Windows.Forms.Label();
            this.lbl_Start = new System.Windows.Forms.Label();
            this.txt_ElapsedTime = new System.Windows.Forms.TextBox();
            this.txt_Start = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_End = new System.Windows.Forms.TextBox();
            this.txt_Delay = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // liv_Sales_O
            // 
            this.liv_Sales_O.ForeColor = System.Drawing.SystemColors.WindowText;
            this.liv_Sales_O.FullRowSelect = true;
            this.liv_Sales_O.GridLines = true;
            this.liv_Sales_O.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.liv_Sales_O.HideSelection = false;
            this.liv_Sales_O.Location = new System.Drawing.Point(2, 119);
            this.liv_Sales_O.Name = "liv_Sales_O";
            this.liv_Sales_O.Size = new System.Drawing.Size(792, 691);
            this.liv_Sales_O.TabIndex = 879;
            this.liv_Sales_O.UseCompatibleStateImageBehavior = false;
            this.liv_Sales_O.View = System.Windows.Forms.View.Details;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.Location = new System.Drawing.Point(158, 1);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(150, 44);
            this.button2.TabIndex = 1003;
            this.button2.Text = "db 저장";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txt_Sales_Excel
            // 
            this.txt_Sales_Excel.Location = new System.Drawing.Point(314, 24);
            this.txt_Sales_Excel.Name = "txt_Sales_Excel";
            this.txt_Sales_Excel.Size = new System.Drawing.Size(248, 21);
            this.txt_Sales_Excel.TabIndex = 1004;
            this.txt_Sales_Excel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(2, 1);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 44);
            this.button1.TabIndex = 1005;
            this.button1.Text = "불러오기";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txt_A
            // 
            this.txt_A.Location = new System.Drawing.Point(314, 1);
            this.txt_A.Name = "txt_A";
            this.txt_A.Size = new System.Drawing.Size(164, 21);
            this.txt_A.TabIndex = 1006;
            this.txt_A.Text = "몰리스트2.xlsx";
            this.txt_A.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.Location = new System.Drawing.Point(588, 69);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(111, 44);
            this.button3.TabIndex = 1007;
            this.button3.Text = "테이블 클리어";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btn_PGMEnd
            // 
            this.btn_PGMEnd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.btn_PGMEnd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_PGMEnd.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_PGMEnd.ForeColor = System.Drawing.Color.White;
            this.btn_PGMEnd.Image = ((System.Drawing.Image)(resources.GetObject("btn_PGMEnd.Image")));
            this.btn_PGMEnd.Location = new System.Drawing.Point(484, 78);
            this.btn_PGMEnd.Name = "btn_PGMEnd";
            this.btn_PGMEnd.Size = new System.Drawing.Size(78, 35);
            this.btn_PGMEnd.TabIndex = 1010;
            this.btn_PGMEnd.Text = "작업 종료";
            this.btn_PGMEnd.UseVisualStyleBackColor = false;
            this.btn_PGMEnd.Click += new System.EventHandler(this.btn_PGMEnd_Click);
            // 
            // btn_Pause
            // 
            this.btn_Pause.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btn_Pause.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Pause.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_Pause.ForeColor = System.Drawing.Color.Black;
            this.btn_Pause.Image = ((System.Drawing.Image)(resources.GetObject("btn_Pause.Image")));
            this.btn_Pause.Location = new System.Drawing.Point(314, 78);
            this.btn_Pause.Name = "btn_Pause";
            this.btn_Pause.Size = new System.Drawing.Size(80, 35);
            this.btn_Pause.TabIndex = 1008;
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
            this.btn_ReStart.Location = new System.Drawing.Point(398, 78);
            this.btn_ReStart.Name = "btn_ReStart";
            this.btn_ReStart.Size = new System.Drawing.Size(80, 35);
            this.btn_ReStart.TabIndex = 1009;
            this.btn_ReStart.Text = "▶ 재 시작";
            this.btn_ReStart.UseVisualStyleBackColor = false;
            this.btn_ReStart.Click += new System.EventHandler(this.btn_ReStart_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Gray;
            this.label4.Location = new System.Drawing.Point(6, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 12);
            this.label4.TabIndex = 1011;
            this.label4.Text = "시작시간 :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(153, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 12);
            this.label1.TabIndex = 1012;
            this.label1.Text = "종료시간 :";
            // 
            // lbl_End
            // 
            this.lbl_End.AutoSize = true;
            this.lbl_End.ForeColor = System.Drawing.Color.Black;
            this.lbl_End.Location = new System.Drawing.Point(220, 56);
            this.lbl_End.Name = "lbl_End";
            this.lbl_End.Size = new System.Drawing.Size(29, 12);
            this.lbl_End.TabIndex = 1014;
            this.lbl_End.Text = "종료";
            // 
            // lbl_Start
            // 
            this.lbl_Start.AutoSize = true;
            this.lbl_Start.ForeColor = System.Drawing.Color.Black;
            this.lbl_Start.Location = new System.Drawing.Point(73, 57);
            this.lbl_Start.Name = "lbl_Start";
            this.lbl_Start.Size = new System.Drawing.Size(29, 12);
            this.lbl_Start.TabIndex = 1013;
            this.lbl_Start.Text = "시작";
            // 
            // txt_ElapsedTime
            // 
            this.txt_ElapsedTime.Location = new System.Drawing.Point(314, 51);
            this.txt_ElapsedTime.Name = "txt_ElapsedTime";
            this.txt_ElapsedTime.Size = new System.Drawing.Size(248, 21);
            this.txt_ElapsedTime.TabIndex = 1015;
            this.txt_ElapsedTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_Start
            // 
            this.txt_Start.Location = new System.Drawing.Point(74, 78);
            this.txt_Start.Name = "txt_Start";
            this.txt_Start.Size = new System.Drawing.Size(69, 21);
            this.txt_Start.TabIndex = 1016;
            this.txt_Start.Text = "376";
            this.txt_Start.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Gray;
            this.label2.Location = new System.Drawing.Point(6, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(162, 12);
            this.label2.TabIndex = 1017;
            this.label2.Text = "위치 :                             ~";
            // 
            // txt_End
            // 
            this.txt_End.Location = new System.Drawing.Point(174, 78);
            this.txt_End.Name = "txt_End";
            this.txt_End.Size = new System.Drawing.Size(75, 21);
            this.txt_End.TabIndex = 1018;
            this.txt_End.Text = "2";
            this.txt_End.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_Delay
            // 
            this.txt_Delay.Location = new System.Drawing.Point(495, 1);
            this.txt_Delay.Name = "txt_Delay";
            this.txt_Delay.Size = new System.Drawing.Size(67, 21);
            this.txt_Delay.TabIndex = 1019;
            this.txt_Delay.Text = "1";
            this.txt_Delay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // _UserSales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(563, 261);
            this.Controls.Add(this.txt_Delay);
            this.Controls.Add(this.txt_End);
            this.Controls.Add(this.txt_Start);
            this.Controls.Add(this.txt_ElapsedTime);
            this.Controls.Add(this.lbl_End);
            this.Controls.Add(this.lbl_Start);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btn_PGMEnd);
            this.Controls.Add(this.btn_Pause);
            this.Controls.Add(this.btn_ReStart);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.txt_A);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txt_Sales_Excel);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.liv_Sales_O);
            this.Controls.Add(this.label2);
            this.Name = "_UserSales";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "_UserSales";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView liv_Sales_O;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txt_Sales_Excel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txt_A;
        private System.Windows.Forms.Button button3;
        public System.Windows.Forms.Button btn_PGMEnd;
        private System.Windows.Forms.Button btn_Pause;
        private System.Windows.Forms.Button btn_ReStart;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label lbl_End;
        internal System.Windows.Forms.Label lbl_Start;
        private System.Windows.Forms.TextBox txt_ElapsedTime;
        private System.Windows.Forms.TextBox txt_Start;
        internal System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_End;
        private System.Windows.Forms.TextBox txt_Delay;
    }
}