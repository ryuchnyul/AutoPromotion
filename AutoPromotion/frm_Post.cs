using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoPromotion
{
    public partial class frm_Post : Form
    {
        int P_Width = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width;
        int P_Height = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height;

        SqlConnection ADONet_Conn = new SqlConnection(AutoPromotion.cfg.DBConnString);
        public string SQL;
        public SqlDataAdapter adapter = null;
        public SqlCommand RsCmd = null;
        public DataTable RsTbl = null;
        public DataSet customers = null;

        public string p_UserID = AutoPromotion.cfg.p_UserID;

        public static bool Process_Pause;
        public static bool Process_Stop;

        #region     // Delay 함수 ==============================================
        // Delay(Rnd.Next(1500, 3000)); 해당시간만큼 UI멈춤
        // Delay 는 UI 작동이 됨  //메모리 문제가 있을 수 있음
        private static DateTime Delay(int MS)
        {
            DateTime ThisMoment = DateTime.Now;
            TimeSpan duration = new TimeSpan(0, 0, 0, 0, MS);
            DateTime AfterWards = ThisMoment.Add(duration);

            while (AfterWards >= ThisMoment)
            {
                System.Windows.Forms.Application.DoEvents();
                ThisMoment = DateTime.Now;
            }
            return DateTime.Now;
        }

        private static DateTime _Delay_Pause(int MS)
        {
            DateTime ThisMoment = DateTime.Now;
            TimeSpan duration = new TimeSpan(0, 0, 0, 0, MS);
            DateTime AfterWards = ThisMoment.Add(duration);

            while (AfterWards >= ThisMoment)
            {
                System.Windows.Forms.Application.DoEvents();
                if (Process_Pause == true)
                {
                    for (int z = 1; z < 1000000000; z++)
                    {
                        if (Process_Pause == false)
                        {
                            break;
                        }
                        else
                        {
                            Delay(1000);
                        }
                    }
                }
                ThisMoment = DateTime.Now;
            }
            return DateTime.Now;
        }
        #endregion

        #region     // DB_Connect ======================================
        public void ADONet_Conn_Routine()
        {
            adapter = new SqlDataAdapter();
            RsCmd = new SqlCommand(SQL, ADONet_Conn);
            adapter.SelectCommand = RsCmd;
            customers = new DataSet();
            RsTbl = new DataTable();
            adapter.Fill(RsTbl);
        }

        public void ADONet_Close_Routine()
        {
            adapter.Dispose();
            ADONet_Conn.Close();
        }
        #endregion

        #region UBoundSplit Function    // UBound  ==============================================
        public int UBound_int(string target, string split)
        {
            string[] target_result = target.Split(new string[] { split }, StringSplitOptions.None);
            return target_result.Length;
        }

        public string UBound_str(string target, string split, int index)
        {
            string[] target_result = target.Split(new string[] { split }, StringSplitOptions.None);
            return target_result[index];
        }
        // 엑셀 VBA 함수 Split 처럼 함수로 만들어서 사용
        public static string SSplit(string _body, string _parseString, int index)
        {
            string varTemp = _body.Split(new string[] { _parseString }, StringSplitOptions.None)[index];
            return varTemp;
        }
        #endregion

        #region LEFT / RIGHT / MID 함수 Function
        public string Mid(string target, int start, int length)
        {
            if (start <= target.Length)
            {
                if (start + length - 1 <= target.Length)
                {
                    return target.Substring(start - 1, length);
                }
                return target.Substring(start - 1);
            }
            return string.Empty;
        }
        #endregion

        #region     // 숫자만 가져오기 ======================================
        public object ImportNumbersOnly(string xString)
        {
            string strTemp = "";
            for (int intNum = 1; intNum <= xString.Length; intNum++)
            {
                long number1 = 0;
                bool canConvert = long.TryParse(Mid(xString, intNum, 1), out number1);
                if (canConvert == true)
                {
                    strTemp = strTemp + Mid(xString, intNum, 1);
                }
            }
            return strTemp;
        }
        #endregion


        public int Channel_PostLength = 1000;
        frm_PROMOTION _frm_PROMOTION = new frm_PROMOTION();
        public string[,] var_Channel = new string[20, 3];

        string _Path = System.IO.Directory.GetCurrentDirectory();



        private void frm_Post_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.TopMost = false;
        }

        public frm_Post()
        {
            InitializeComponent();
            this.TopMost = true;

            this.Text = this.Text + "  [ 로그인 : " + p_UserID + " ]";

            SQL = " SELECT *   FROM PROMOTION_POST   WHERE PP_UserID = 'PADMIN' AND PP_Market_Dno = 0 ";
            ADONet_Conn_Routine();
            if (RsTbl.Rows.Count > 0)
            {
                string textValue = RsTbl.Rows[0].Field<string>("PP_Post");
                textValue = textValue.Replace("<br>", "\r\n");
                txt_Promotion_Post_Standard.Text = textValue;
            }
            ADONet_Conn.Close();

            timer_Load.Interval = 100;
            timer_Load.Start();
        }

        private void timer_Load_Tick(object sender, EventArgs e)
        {
            timer_Load.Stop();

            clib_Channel.Items.Clear();
            clib_Channel_Num.Items.Clear();

            SQL = "SELECT *   FROM PROMOTION_MarketList   ORDER BY PCH_No ASC ";
            ADONet_Conn_Routine();
            if (RsTbl.Rows.Count > 0)
            {
                for (int xi = 0; xi < RsTbl.Rows.Count; xi++)
                {
                    clib_Channel.Items.Add(RsTbl.Rows[xi].Field<string>("PCH_Market"));
                    var_Channel[xi, 0] = RsTbl.Rows[xi].Field<string>("PCH_Market");
                    var_Channel[xi, 1] = RsTbl.Rows[xi].Field<int>("PCH_Market_Post_Len").ToString();
                }
            }
            ADONet_Conn.Close();
            System.Windows.Forms.Application.DoEvents();
            clib_Channel.SetItemChecked(0, true);
            clib_Channel.SetSelected(0, true);

            for (int xi = 0; xi < 1; xi++)
            {
                for (int xj = 0; xj < 10; xj++)
                {
                    clib_Channel_Num.Items.Add(var_Channel[xi, 0] + "_포스트" + (xj + 1));
                }
            }
            clib_Channel_Num.SetItemChecked(0, true);
            clib_Channel_Num.SetSelected(0, true);

            int SelRow = clib_Channel.SelectedIndex;
            int SelRowN = clib_Channel_Num.SelectedIndex + 1;
            string SelText = clib_Channel.SelectedItem.ToString();
            string SelTextN = clib_Channel_Num.SelectedItem.ToString();

            if (SelRow == -1) { SelRow = 0; }
            txt_Prom_Length.Text = txt_Promotion_Post.Text.Length.ToString("#,##0") + "  / " + var_Channel[SelRow, 1];
            Channel_PostLength = Convert.ToInt32(var_Channel[SelRow, 1]);

            SQL = " SELECT *   FROM PROMOTION_POST   WHERE PP_UserID = '" + p_UserID + "' AND PP_Market = '" + SelText + "' AND PP_Market_Dno = " + SelRowN + " ";
            ADONet_Conn_Routine();
            if (RsTbl.Rows.Count > 0)
            {
                string textValue = RsTbl.Rows[0].Field<string>("PP_Post");
                textValue = textValue.Replace("<br>", "\r\n");
                txt_Promotion_Post.Text = textValue;
            }
            else
            {
                txt_Promotion_Post.Text = "";
            }
            ADONet_Conn.Close();
            lbl_Info01.Focus();
        }


        private void clib_Channel_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int SelRow = clib_Channel.SelectedIndex;
                string SelText = clib_Channel.SelectedItem.ToString();
                clib_Channel.SetItemChecked(SelRow, true);

                Channel_PostLength = Convert.ToInt32(ImportNumbersOnly(SelText));
            }
            catch { }
        }
        
        private void clib_Channel_MouseDown(object sender, MouseEventArgs e)
        {
            chx_M.Checked = false;
            chx_U.Checked = false;

            int SelRow = clib_Channel.SelectedIndex;
            int SelRowN = clib_Channel_Num.SelectedIndex + 1;
            string SelText = clib_Channel.SelectedItem.ToString();
            string SelTextN = clib_Channel_Num.SelectedItem.ToString();
            txt_Prom_Length.Text = txt_Promotion_Post.Text.Length.ToString("#,##0") + "  / " + var_Channel[SelRow, 1];

            clib_Channel_Num.Items.Clear();
            for (int xi = 0; xi < 10; xi++)
            {
                clib_Channel_Num.Items.Add(SelText + "_포스트" + (xi + 1));
            }
            clib_Channel_Num.SetItemChecked(0, true);
            clib_Channel_Num.SetSelected(0, true);

            SQL = " SELECT *   FROM PROMOTION_POST   WHERE PP_UserID = '" + p_UserID + "' AND PP_Market = '" + SelText + "' AND PP_Market_Dno = " + SelRowN + " ";
            ADONet_Conn_Routine();
            if (RsTbl.Rows.Count > 0)
            {
                string textValue = RsTbl.Rows[0].Field<string>("PP_Post");
                textValue = textValue.Replace("<br>", "\r\n");
                txt_Promotion_Post.Text = textValue;
            }
            else
            {
                txt_Promotion_Post.Text = "";
            }
            ADONet_Conn.Close();
        }

        private void clib_Channel_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //체크 리스트 박스 한 아이템만 선택하도록 허용
            if (e.NewValue == CheckState.Checked)
            {
                CheckedListBox checkedListBox = sender as CheckedListBox; //캐스팅

                for (int count = 0; count < checkedListBox.Items.Count; ++count)
                {
                    if (e.Index != count)
                    {
                        checkedListBox.SetItemChecked(count, false);
                    }
                }
            }
        }





        private void clib_Channel_Num_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int SelRow = clib_Channel_Num.SelectedIndex;
                string SelText = clib_Channel_Num.SelectedItem.ToString();
                clib_Channel_Num.SetItemChecked(SelRow, true);

                Channel_PostLength = Convert.ToInt32(ImportNumbersOnly(SelText));
            }
            catch { }
        }
        private void clib_Channel_Num_MouseDown(object sender, MouseEventArgs e)
        {
            chx_M.Checked = false;
            chx_U.Checked = false;

            int SelRow = clib_Channel.SelectedIndex;
            int SelRowN = clib_Channel_Num.SelectedIndex;
            string SelText = clib_Channel.SelectedItem.ToString();
            string SelTextN = clib_Channel_Num.SelectedItem.ToString();
            txt_Prom_Length.Text = txt_Promotion_Post.Text.Length.ToString("#,##0") + "  / " + var_Channel[SelRow, 1];

            SQL = " SELECT *   FROM PROMOTION_POST   WHERE PP_UserID = '" + p_UserID + "' AND PP_Market = '" + SelText + "' AND PP_Market_Dno = " + (SelRowN + 1) + " ";
            ADONet_Conn_Routine();
            if (RsTbl.Rows.Count > 0)
            {
                string textValue = RsTbl.Rows[0].Field<string>("PP_Post");
                textValue = textValue.Replace("<br>", "\r\n");
                txt_Promotion_Post.Text = textValue;
            }
            else
            {
                txt_Promotion_Post.Text = "";
            }
            ADONet_Conn.Close();
        }
        private void clib_Channel_Num_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //체크 리스트 박스 한 아이템만 선택하도록 허용
            if (e.NewValue == CheckState.Checked)
            {
                CheckedListBox checkedListBox = sender as CheckedListBox; //캐스팅

                for (int count = 0; count < checkedListBox.Items.Count; ++count)
                {
                    if (e.Index != count)
                    {
                        checkedListBox.SetItemChecked(count, false);
                    }
                }
            }
        }





        private void txt_Promotion_Post_TextChanged(object sender, EventArgs e)
        {
            int SelRow = clib_Channel.SelectedIndex;
            txt_Prom_Length.Text = txt_Promotion_Post.Text.Length.ToString("#,##0") + "  / " + var_Channel[SelRow, 1];
        }





        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.TopMost = false;
            this.Close();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (btn_Save.Text == "포스트 저장")
            {
                int SelRow = clib_Channel.SelectedIndex;
                int SelRowN = clib_Channel_Num.SelectedIndex;
                string SelText = clib_Channel.SelectedItem.ToString();
                string SelTextN = clib_Channel_Num.SelectedItem.ToString();
                string textValue = txt_Promotion_Post.Text;
                textValue = textValue.Replace("'", "");
                //textValue = textValue.Replace("<br>", "\r\n");
                textValue = textValue.Replace("\r\n", "<br>");

                SQL = " SELECT *   FROM PROMOTION_POST   WHERE PP_UserID = '" + p_UserID + "' AND PP_Market = '" + SelText + "' AND PP_Market_Dno = " + (SelRow + 1) + " ";
                ADONet_Conn_Routine();
                if (RsTbl.Rows.Count > 0)
                {
                    ADONet_Conn.Close();

                    string upSQL = "UPDATE PROMOTION_POST   ";
                    upSQL += " SET PP_Post = '" + textValue + "'  ";
                    upSQL += " WHERE PP_UserID = '" + p_UserID + "' AND PP_Market = '" + SelText + "' AND PP_Market_Dno = " + (SelRow + 1) + " ";
                    ADONet_Conn.Open();
                    SqlCommand cmd0_1 = new SqlCommand();
                    cmd0_1.Connection = ADONet_Conn;
                    cmd0_1.CommandText = upSQL;
                    cmd0_1.ExecuteNonQuery();
                    ADONet_Conn.Close();
                }
                else
                {
                    string inSQL = "INSERT PROMOTION_POST ";
                    inSQL += "( PP_UserID, PP_Market, PP_Market_Dno, PP_Post ";
                    inSQL += " ) VALUES ( ";
                    inSQL += " '" + p_UserID + "', ";
                    inSQL += " '" + SelText + "', ";
                    inSQL += "  " + (SelRow + 1) + " , ";
                    inSQL += " '" + textValue + "'  ";
                    inSQL += "  ) ";
                    ADONet_Conn.Open();
                    SqlCommand cmd0_1 = new SqlCommand();
                    cmd0_1.Connection = ADONet_Conn;
                    cmd0_1.CommandText = inSQL;
                    cmd0_1.ExecuteNonQuery();
                    ADONet_Conn.Close();
                }
            }
            else if (btn_Save.Text == "메뉴얼 저장")
            {
                string textValue = txt_Promotion_Post.Text;
                textValue = textValue.Replace("'", "");
                textValue = textValue.Replace("\r\n", "<br>");

                string upSQL = "UPDATE PROMOTION_POST   ";
                upSQL += " SET PP_Post = '" + textValue + "'  ";
                upSQL += " WHERE PP_UserID = 'PADMIN' AND PP_Market = '메뉴얼' AND PP_Market_Dno = 0 ";
                ADONet_Conn.Open();
                SqlCommand cmd0_1 = new SqlCommand();
                cmd0_1.Connection = ADONet_Conn;
                cmd0_1.CommandText = upSQL;
                cmd0_1.ExecuteNonQuery();
                ADONet_Conn.Close();
            }
            else if (btn_Save.Text == "사용설명 저장")
            {
                string textValue = txt_Promotion_Post.Text;
                textValue = textValue.Replace("'", "");
                textValue = textValue.Replace("\r\n", "<br>");

                string upSQL = "UPDATE PROMOTION_POST   ";
                upSQL += " SET PP_Post = '" + textValue + "'  ";
                upSQL += " WHERE PP_UserID = 'PADMIN' AND PP_Market = '사용설명' AND PP_Market_Dno = 0 ";
                ADONet_Conn.Open();
                SqlCommand cmd0_1 = new SqlCommand();
                cmd0_1.Connection = ADONet_Conn;
                cmd0_1.CommandText = upSQL;
                cmd0_1.ExecuteNonQuery();
                ADONet_Conn.Close();
            }
            
            MessageBox.Show(new Form() { TopMost = true }, "저장 완료", "홍보프로그램");
        }

        private void btn_NewUserInsert_Click(object sender, EventArgs e)
        {
            SQL = " SELECT *   FROM PROMOTION_POST   WHERE PP_UserID = '" + p_UserID + "' ";
            ADONet_Conn_Routine();
            if (RsTbl.Rows.Count > 0)
            {
                ADONet_Conn.Close();
                MessageBox.Show(new Form() { TopMost = true }, "기본 포스트 가 있습니다.", "홍보프로그램");
            }
            else
            {
                ADONet_Conn.Close();

                string textValue = txt_Promotion_Post.Text;
                textValue = txt_Promotion_Post_Standard.Text.Replace("'", "");
                textValue = textValue.Replace("\r\n", "<br>");
                //textValue = textValue.Replace("<br>", "\r\n");

                for (int xi = 0; xi < clib_Channel.Items.Count; xi++)
                {
                    for (int xj = 1; xj <= 10; xj++)
                    {
                        string inSQL = "INSERT PROMOTION_POST ";
                        inSQL += "( PP_UserID, PP_Market, PP_Market_Dno, PP_Post ";
                        inSQL += " ) VALUES ( ";
                        inSQL += " '" + p_UserID + "', ";
                        inSQL += " '" + clib_Channel.Items[xi].ToString() + "', ";
                        inSQL += "  " + xj + " , ";
                        if (xi == 0 && xj == 0)
                        {
                            inSQL += " '" + textValue + "' ";
                        }
                        else
                        {
                            inSQL += " '' ";
                        }
                        inSQL += "  ) ";
                        ADONet_Conn.Open();
                        SqlCommand cmd0_1 = new SqlCommand();
                        cmd0_1.Connection = ADONet_Conn;
                        cmd0_1.CommandText = inSQL;
                        cmd0_1.ExecuteNonQuery();
                        ADONet_Conn.Close();
                    }
                }

                MessageBox.Show(new Form() { TopMost = true }, "저장 완료", "홍보프로그램");
            }
        }

        private void btn_Standard_Click(object sender, EventArgs e)
        {
            txt_Promotion_Post.Text = txt_Promotion_Post_Standard.Text;
        }

        private void chx_M_CheckedChanged(object sender, EventArgs e)
        {
            if (chx_M.Checked == true)
            {
                btn_NewUserInsert.Enabled = false;
                btn_Standard.Enabled = false;
                chx_U.Checked = false;
                btn_Save.Text = "메뉴얼 저장";

                SQL = " SELECT *   FROM PROMOTION_POST   WHERE PP_UserID = 'PADMIN' AND PP_Market = '메뉴얼' AND PP_Market_Dno = 0 ";
                ADONet_Conn_Routine();
                if (RsTbl.Rows.Count > 0)
                {
                    string textValue = RsTbl.Rows[0].Field<string>("PP_Post");
                    textValue = textValue.Replace("<br>", "\r\n");
                    txt_Promotion_Post.Text = textValue;
                }
                ADONet_Conn.Close();
                txt_Prom_Length.Text = "∞";
            }
            else
            {
                btn_NewUserInsert.Enabled = true;
                btn_Standard.Enabled = true;
                btn_Save.Text = "포스트 저장";
            }
        }

        private void chx_U_CheckedChanged(object sender, EventArgs e)
        {
            if (chx_U.Checked == true)
            {
                btn_NewUserInsert.Enabled = false;
                btn_Standard.Enabled = false;
                chx_M.Checked = false;
                btn_Save.Text = "사용설명 저장";

                SQL = " SELECT *   FROM PROMOTION_POST   WHERE PP_UserID = 'PADMIN' AND PP_Market = '사용설명' AND PP_Market_Dno = 0 ";
                ADONet_Conn_Routine();
                if (RsTbl.Rows.Count > 0)
                {
                    string textValue = RsTbl.Rows[0].Field<string>("PP_Post");
                    textValue = textValue.Replace("<br>", "\r\n");
                    txt_Promotion_Post.Text = textValue;
                }
                ADONet_Conn.Close();
                txt_Prom_Length.Text = "∞";
            }
            else
            {
                btn_NewUserInsert.Enabled = true;
                btn_Standard.Enabled = true;
                btn_Save.Text = "포스트 저장";
            }
        }
    }
}
