using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.Drawing.Imaging;
using System.Net;
using System.Net.Sockets;
using WinHttp;

namespace AutoPromotion
{
    public partial class frm_PROMOTION : Form
    {
        int P_Width = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width;
        int P_Height = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height;

        ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService();
        ChromeOptions chromeOptions = new OpenQA.Selenium.Chrome.ChromeOptions();
        //IWebDriver chromeDriver = null;
        ChromeDriver chromeDriver = null;


        SqlConnection ADONet_Conn = new SqlConnection(AutoPromotion.cfg.DBConnString);
        public string SQL;
        public SqlDataAdapter adapter = null;
        public SqlCommand RsCmd = null;
        public DataTable RsTbl = null;
        public DataSet customers = null;

        public string p_UserID = AutoPromotion.cfg.p_UserID;

        public string db_Market = "";
        public string db_SKeyword = "";
        public int db_Rank = 0;
        public int db_CateRank = 0;        
        public string db_StoreName = "";
        public string db_ProductID = "";
        public string db_ProductLink = "";
        public string db_ProductCate = "";
        public string db_ProductName = "";
        public string db_ProductImage = "";
        public string db_SellerName = "";
        public string db_RepresentativeName = "";        
        public string db_CallNumber = "";
        public string db_Address = "";
        public string db_Email = "";
        public string db_ChannelUid = "";
        public int db_Vtotal;
        public int db_Vtoday;
        

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
                        if (Process_Pause == false || Process_Stop == true)
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

        public bool exeClear(string svrName)
        {
            bool result = false;
            try
            {
                int num = 0;
                foreach (Process process in Process.GetProcessesByName(svrName))
                {
                    try
                    {
                        process.Kill();
                        num++;
                    }
                    catch
                    {
                    }
                }
                bool flag = num > 0;
                bool flag2 = flag;
                if (flag2)
                {
                    Thread.Sleep(500);
                }
                return result;
            }
            catch
            {
            }
            return result;
        }

        public void chromedriverClear()
        {
            try
            {
                chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles.First());
                string BaseWindow01 = chromeDriver.CurrentWindowHandle;
                for (; ; )
                {
                    chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles.Last());
                    chromeDriver.Close();
                    Delay(new Random().Next(800, 1000));
                }
            }
            catch { }
        }

        public bool WebDoc_Window_ScrollTo(IWebDriver driver, int ux, int uy)
        {
            string text = "";
            text = text + "var wx = " + ux.ToString() + ";\r\n";
            text = text + "var wy = " + uy.ToString() + ";\r\n";
            text += "window.scrollTo(wx,wy);\r\n";
            this.execJava(driver, text);
            return true;
        }

        public string execJava(IWebDriver webDriver, string jscript)
        {
            string result = string.Empty;
            try
            {
                result = (string)(webDriver as IJavaScriptExecutor).ExecuteScript(jscript, new object[0]);
            }
            catch { }
            return result;
        }


        public static bool PGM_Start = true;
        public static bool PGM_Start_Manual = false;
        public static int ReCheck_Point = -1;
        public static string Source = "";
        public static bool Key_WorkState = false;
        public static string Continue_WorkKey = "";
        public static string Continue_WorkNum = "";

        public string[] Recent_StoreList = new string[10000];
        public static bool Recent_Store_YN = false;
        public static int Recent_Store_dno = 0;


        public string p_User_KakaoID = AutoPromotion.cfg.p_User_KakaoID;
        public string p_User_KakaoPW = AutoPromotion.cfg.p_User_KakaoPW;

        public int Channel_PostLength = 1000;
        public string[,] var_Channel = new string[20, 3];

        string _Path = System.IO.Directory.GetCurrentDirectory();
        //string _LastPostSave = System.IO.Directory.GetCurrentDirectory() + @"\P_Post\마지막사용한포스트.txt";






        private void frm_PROMOTION_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();

            this.구동종료();

            Application.Exit();
        }

        public frm_PROMOTION()
        {
            InitializeComponent();
        }

        private void frm_PROMOTION_Load(object sender, EventArgs e)
        {
            //string textValue = System.IO.File.ReadAllText(_LastPostSave, Encoding.Default);
            //txt_Promotion_Post.Text = textValue;

            // 프로그램 초기 위치
            this.StartPosition = FormStartPosition.Manual;
            this.Top = 0;
            this.Left = Screen.PrimaryScreen.Bounds.Width - this.Width;

            this.Text = this.Text + "  [ 로그인 : " + p_UserID + " ]";

            if (p_UserID.ToUpper().IndexOf("GUEST") > -1 || p_UserID.ToUpper().IndexOf("ADMIN") > -1)
            {
                btn_Test.Visible = true;
            }

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
            //clib_Channel.SetItemChecked(0, true);
            //clib_Channel.SetSelected(0, true);

            for (int xi = 0; xi < 1; xi++)
            {
                for (int xj = 0; xj < 10; xj++)
                {
                    clib_Channel_Num.Items.Add(var_Channel[xi, 0] + "_포스트" + (xj + 1));
                }
            }
            clib_Channel_Num.SetItemChecked(0, true);
            clib_Channel_Num.SetSelected(0, true);

            this.Sel_ListView_Reset();

            SQL = "SELECT *   FROM PROMOTION_UserRecord   WHERE UserID = '" + p_UserID + "'   ORDER BY User_dno ASC ";
            ADONet_Conn_Routine();
            if (RsTbl.Rows.Count > 0)
            {
                string SelPost = RsTbl.Rows[0].Field<string>("UserPostSel");
                try
                {
                    SelPost = SSplit(SelPost, "^", 1);
                }
                catch
                {
                    SelPost = "0";
                }
                int SelPRow = Convert.ToInt32(SelPost);

                int SelRow1 = Convert.ToInt32(RsTbl.Rows[0].Field<string>("UserCate1Sel"));
                int SelRow2 = Convert.ToInt32(RsTbl.Rows[0].Field<string>("UserCate2Sel"));
                int SelRow3 = Convert.ToInt32(RsTbl.Rows[0].Field<string>("UserCate3Sel"));
                int SelRow4 = Convert.ToInt32(RsTbl.Rows[0].Field<string>("UserCate4Sel"));
                txt_UserLastAct.Text = RsTbl.Rows[0].Field<string>("UserLastAct");
                ADONet_Conn.Close();

                try
                {
                    txt_WorkChannel.Text = SSplit(txt_UserLastAct.Text, "^", 0);
                    txt_WorkKeyword.Text = SSplit(txt_UserLastAct.Text, "^", 1);
                    txt_WorkNum.Text = SSplit(txt_UserLastAct.Text, "^", 2);
                }
                catch
                {
                    txt_WorkChannel.Text = "";
                    txt_WorkKeyword.Text = "";
                    txt_WorkNum.Text = "";
                }

                string SelText_t = "";
                if (txt_WorkChannel.Text != "")
                {
                    for (int xi = 0; xi < clib_Channel.Items.Count; xi++)
                    {
                        int SelRow0 = xi;
                        SelText_t = clib_Channel.Items[xi].ToString();

                        if (txt_WorkChannel.Text == SelText_t)
                        {
                            clib_Channel.SetItemChecked(SelRow0, true);
                            clib_Channel.SetSelected(SelRow0, true);
                            txt_Prom_Length.Text = txt_Promotion_Post.Text.Length.ToString("#,##0") + "  / " + var_Channel[SelRow0, 1];

                            clib_Cate1.Items.Clear();
                            clib_Cate2.Items.Clear();
                            clib_Cate3.Items.Clear();
                            clib_Cate4.Items.Clear();


                            break;
                        }
                    }
                }
                System.Windows.Forms.Application.DoEvents();

                //clib_Channel.SetItemChecked(0, true);
                //clib_Channel.SetSelected(0, true);
                try
                {
                    clib_Channel_Num.SetItemChecked(SelPRow, true);
                    clib_Channel_Num.SetSelected(SelPRow, true);
                    string _ReadPath = _Path + @"\P_Post\" + SelText_t + "_포스트" + (SelPRow + 1) + ".txt";
                    string textValue = System.IO.File.ReadAllText(_ReadPath, Encoding.Default);
                    txt_Promotion_Post.Text = textValue;
                }
                catch { }

                //int SelRow_0 = clib_Channel.SelectedIndex;
                //int SelRowN_0 = clib_Channel_Num.SelectedIndex;
                //string SelText_0 = clib_Channel.SelectedItem.ToString();
                //string SelTextN_0 = clib_Channel_Num.SelectedItem.ToString();
                // 카테고리1 DB 가져오기
                //clib_Cate1.Items.Clear();
                //SQL = " SELECT channel_category1   FROM PROMOTION_CATEGORY_ALL   WHERE channel_name = '" + SelText_0 + "' AND channel_category2 LIKE ''   ORDER BY channel_category_no ASC ";
                //ADONet_Conn_Routine();
                //if (RsTbl.Rows.Count > 0)
                //{
                //    for (int xi = 0; xi < RsTbl.Rows.Count; xi++)
                //    {
                //        clib_Cate1.Items.Add(RsTbl.Rows[xi].Field<string>("channel_category1"));
                //    }
                //}
                //ADONet_Conn.Close();
                try
                {
                // 카테고리1 정보 업데이트
                clib_Cate1.SetItemChecked(SelRow1, true);
                clib_Cate1.SetSelected(SelRow1, true);
                System.Windows.Forms.Application.DoEvents();

                this.clib_ALL_Cate1_DB_Read();
                System.Windows.Forms.Application.DoEvents();

                this.clib_ALL_Cate2_DB_Read();
                }
                catch { }

                try
                {
                    clib_Cate2.SetItemChecked(SelRow2, true);
                    clib_Cate2.SetSelected(SelRow2, true);
                }
                catch { }
                // 선택변경하여 화면에 업데이트를 위해 DB재설정
                if (txt_WorkChannel.Text != "카카오쇼핑")
                {
                    // 카테고리 분류가 없으므로 미실행
                    this.clib_ALL_Cate2_DB_Read();
                }
                System.Windows.Forms.Application.DoEvents();

                try
                {
                    if (txt_WorkChannel.Text != "카카오쇼핑")
                    {
                        this.clib_ALL_Cate3_DB_Read();
                    }
                    clib_Cate3.SetItemChecked(SelRow3, true);
                    clib_Cate3.SetSelected(SelRow3, true);
                }
                catch { }
                System.Windows.Forms.Application.DoEvents();

                try
                {
                    clib_Cate4.SetItemChecked(SelRow4, true);
                    clib_Cate4.SetSelected(SelRow4, true);
                }
                catch { }
                System.Windows.Forms.Application.DoEvents();
            }
            else
            {
                ADONet_Conn.Close();
            }

            this.SellerName_7Days_Pick();

            //lbl_Info01.Focus();

            int SelRow = clib_Channel.SelectedIndex;
            int SelRowN = clib_Channel_Num.SelectedIndex;
            string SelText = clib_Channel.SelectedItem.ToString();
            string SelTextN = clib_Channel_Num.SelectedItem.ToString();
            if (SelRow == -1) { SelRow = 0; }
            txt_Prom_Length.Text = txt_Promotion_Post.Text.Length.ToString("#,##0") + "  / " + var_Channel[SelRow, 1];
            Channel_PostLength = Convert.ToInt32(var_Channel[SelRow, 1]);
            System.Windows.Forms.Application.DoEvents();

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

        private void Sel_ListView_Reset()
        {
            clib_Cate1.Items.Clear();
            //liv_N_Cate1.Columns.Clear();
            //clib_N_Cate1.Columns.Add("대분류", 175, HorizontalAlignment.Left);
            ReCheck_Point = -1;

            string SelText = clib_Channel.Items[0].ToString();
            if (chk_CateSelect.Checked == false)
            {
                clib_Cate2.Items.Clear();
                clib_Cate3.Items.Clear();
                clib_Cate4.Items.Clear();
            }

            SQL = "SELECT channel_category1   FROM PROMOTION_CATEGORY_ALL   WHERE channel_name = '" + SelText + "' AND channel_category2 = ''   ORDER BY channel_category_no ASC ";
            ADONet_Conn_Routine();
            if (RsTbl.Rows.Count > 0)
            {
                for (int xi = 0; xi < RsTbl.Rows.Count; xi++)
                {
                    clib_Cate1.Items.Add(RsTbl.Rows[xi].Field<string>("channel_category1"));
                }
            }
            ADONet_Conn.Close();
            clib_Channel.SetItemChecked(0, true);
            clib_Channel.SetSelected(0, true);
            clib_Cate1.SetItemChecked(0, true);
            clib_Cate1.SetSelected(0, true);

            if (chk_KeyTest.Checked == true)
            {
                clib_Cate1.Items[0] = txt_TestKeyword.Text;
            }

            PGM_Start = false;
        }

        public void clib_ALL_Cate1_DB_Read()
        {
            try
            {
                string C_SelText = clib_Channel.SelectedItem.ToString();
                int SelRow = clib_Cate1.SelectedIndex;
                string SelText = clib_Cate1.SelectedItem.ToString();

                SQL = " SELECT channel_category2   FROM PROMOTION_CATEGORY_ALL   ";
                SQL += " WHERE channel_name = '" + C_SelText + "' AND channel_category1 like '" + SelText + "' AND channel_category2 <> '' AND channel_category3 LIKE '' ";
                SQL += " ORDER BY channel_category_no ASC ";
                ADONet_Conn_Routine();
                if (RsTbl.Rows.Count > 0)
                {
                    clib_Cate2.Items.Clear();
                    clib_Cate3.Items.Clear();
                    clib_Cate4.Items.Clear();
                    for (int xi = 0; xi < RsTbl.Rows.Count; xi++)
                    {
                        clib_Cate2.Items.Add(RsTbl.Rows[xi].Field<string>("channel_category2"));
                    }
                    clib_Cate2.SetItemChecked(0, true);
                    clib_Cate2.SetSelected(0, true);
                }
                else
                {
                    clib_Cate2.Items.Clear();
                    clib_Cate3.Items.Clear();
                    clib_Cate4.Items.Clear();
                }
                ADONet_Conn.Close();
            }
            catch { }
        }

        public void clib_ALL_Cate2_DB_Read()
        {
            try
            {
                string C_SelText = clib_Channel.SelectedItem.ToString();
                int SelRow = clib_Cate2.SelectedIndex;
                string SelText = clib_Cate2.SelectedItem.ToString();

                SQL = " SELECT channel_category3   FROM PROMOTION_CATEGORY_ALL   ";
                SQL += " WHERE channel_name = '" + C_SelText + "' AND channel_category2 like '" + SelText + "' AND channel_category3 <> '' AND channel_category4 LIKE '' ";
                SQL += " ORDER BY channel_category_no ASC ";
                ADONet_Conn_Routine();
                if (RsTbl.Rows.Count > 0)
                {
                    clib_Cate3.Items.Clear();
                    clib_Cate4.Items.Clear();
                    for (int xi = 0; xi < RsTbl.Rows.Count; xi++)
                    {
                        clib_Cate3.Items.Add(RsTbl.Rows[xi].Field<string>("channel_category3"));
                    }
                    //clib_Cate3.SetItemChecked(0, true);
                    //clib_Cate3.SetSelected(0, true);
                }
                else
                {
                    clib_Cate3.Items.Clear();
                    clib_Cate4.Items.Clear();
                }
                ADONet_Conn.Close();
            }
            catch { }
        }

        public void clib_ALL_Cate3_DB_Read()
        {
            try
            {
                string C_SelText = clib_Channel.SelectedItem.ToString();
                int SelRow = clib_Cate3.SelectedIndex;
                string SelText1 = clib_Cate1.SelectedItem.ToString();
                string SelText2 = clib_Cate2.SelectedItem.ToString();
                string SelText3 = clib_Cate3.SelectedItem.ToString();

                SQL = " SELECT channel_category4   FROM PROMOTION_CATEGORY_ALL   ";
                SQL += " WHERE channel_name = '" + C_SelText + "' AND channel_category1 like '" + SelText1 + "' AND channel_category2 like '" + SelText2 + "' AND channel_category3 like '" + SelText3 + "' AND channel_category4 <> '' ";
                SQL += " ORDER BY channel_category_no ASC ";
                ADONet_Conn_Routine();
                if (RsTbl.Rows.Count > 0)
                {
                    clib_Cate4.Items.Clear();
                    for (int xi = 0; xi < RsTbl.Rows.Count; xi++)
                    {
                        clib_Cate4.Items.Add(RsTbl.Rows[xi].Field<string>("channel_category4"));
                    }
                    clib_Cate4.SetItemChecked(0, true);
                    clib_Cate4.SetSelected(0, true);
                }
                else
                {
                    clib_Cate4.Items.Clear();
                }
                ADONet_Conn.Close();
            }
            catch { }
            //try
            //{
            //    clib_N_Cate4.SetItemChecked(0, true);
            //    clib_N_Cate4.SetSelected(0, true);
            //}
            //catch { }
        }

        public void SellerName_7Days_Pick()             // 7일간 작업한 이력이 있는 업체리스트 가져오기
        {
            // 7일간 작업한 이력이 있는 업체리스트 가져오기
            string SelText = clib_Channel.SelectedItem.ToString();
            DateTime db_StartDate = DateTime.Now.AddDays(-6);
            DateTime db_EndDate = DateTime.Now;

            string[] xStoreList = new string[10000];
            SQL = "SELECT *   FROM PROMOTION_WorkData   WHERE PACT_UserID = '" + p_UserID + "' AND PACT_Market = '" + SelText + "' AND PACT_InputTime BETWEEN '" + db_StartDate + "' AND '" + db_EndDate + "'   ORDER BY PACT_dno DESC ";
            ADONet_Conn_Routine();
            if (RsTbl.Rows.Count > 0)
            {
                for (int xi = 0; xi < RsTbl.Rows.Count; xi++)
                {
                    xStoreList[xi] = RsTbl.Rows[xi].Field<string>("PACT_SellerName");
                    //slList.Add(RsTbl.Rows[xi].Field<string>("PACT_StoreName"));
                    Recent_Store_dno += 1;
                }
            }
            ADONet_Conn.Close();
            // 중복 스토어 제거    
            List<string> slList = new List<string>(xStoreList);
            List<string> x_StoreList = new List<string>();
            x_StoreList = slList.Distinct().ToList();
            for (int xzi = 0; xzi < x_StoreList.Count; xzi++)
            {
                Recent_StoreList[xzi] = x_StoreList[xzi];
                Recent_Store_dno = xzi;
            }
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
            int SelRow = clib_Channel.SelectedIndex;
            int SelRowN = clib_Channel_Num.SelectedIndex;
            string SelText = clib_Channel.SelectedItem.ToString();
            string SelTextN = clib_Channel_Num.SelectedItem.ToString();
            txt_Prom_Length.Text = txt_Promotion_Post.Text.Length.ToString("#,##0") + "  / " + var_Channel[SelRow, 1];
            this.SellerName_7Days_Pick();

            clib_Channel_Num.Items.Clear();
            for (int xi = 0; xi < 10; xi++)
            {
                clib_Channel_Num.Items.Add(SelText + "_포스트" + (xi + 1));
            }
            clib_Channel_Num.SetItemChecked(0, true);
            clib_Channel_Num.SetSelected(0, true);
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



            clib_Cate1.Items.Clear();
            ReCheck_Point = -1;
            if (chk_CateSelect.Checked == false)
            {
                clib_Cate2.Items.Clear();
                clib_Cate3.Items.Clear();
                clib_Cate4.Items.Clear();
            }
            SQL = " SELECT channel_category1   FROM PROMOTION_CATEGORY_ALL   WHERE channel_name = '" + SelText + "' AND channel_category2 LIKE ''   ORDER BY channel_category_no ASC ";
            ADONet_Conn_Routine();
            if (RsTbl.Rows.Count > 0)
            {
                for (int xi = 0; xi < RsTbl.Rows.Count; xi++)
                {
                    clib_Cate1.Items.Add(RsTbl.Rows[xi].Field<string>("channel_category1"));
                }
                clib_Cate1.SetItemChecked(0, true);
                clib_Cate1.SetSelected(0, true);
            }
            else
            {
                //clib_Cate1.SetItemChecked(0, true);
                //clib_Cate1.SetSelected(0, true);
                clib_Cate2.Items.Clear();
                clib_Cate3.Items.Clear();
                clib_Cate4.Items.Clear();
            }
            ADONet_Conn.Close();
            PGM_Start = false;

            this.clib_ALL_Cate1_DB_Read();    // DB 읽어오기
            txt_TTLProdCount.Visible = true;
            if (SelText.IndexOf("카카오선물하기") > -1 || SelText == "에이블리" || SelText == "쿠팡")
            {
                clib_Cate1.Items.Clear();
                clib_Cate2.Items.Clear();
                clib_Cate3.Items.Clear();
                clib_Cate4.Items.Clear();

                txt_TTLProdCount.Visible = true;

                btn_PGMStart.Enabled = false;
                btn_Pause.Enabled = false;
                btn_ReStart.Enabled = false;
                btn_PGMEnd.Enabled = false;
                btn_ManualStart.Enabled = false;
                btn_Login.Enabled = false;
            }
            else
            {
                btn_PGMStart.Enabled = true;
                btn_Pause.Enabled = true;
                btn_ReStart.Enabled = true;
                btn_PGMEnd.Enabled = true;
                btn_ManualStart.Enabled = true;
                btn_Login.Enabled = true;
            }
            txt_WorkNum.Text = "1";

            if (SelText == "네이버")
            {
                txt_Rank_Start.Text = "41";
                txt_Rank_End.Text = "400";
            }
            else
            {
                txt_Rank_Start.Text = "31";
                txt_Rank_End.Text = "200";
            }
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
            int SelRow = clib_Channel.SelectedIndex;
            int SelRowN = clib_Channel_Num.SelectedIndex;
            string SelText = clib_Channel.SelectedItem.ToString();
            string SelTextN = clib_Channel_Num.SelectedItem.ToString();
            try
            {
                string _ReadPath = _Path + @"\P_Post\" + SelText + ".txt";
                string textValue = System.IO.File.ReadAllText(_ReadPath, Encoding.Default);
                txt_Promotion_Post.Text = textValue;
            }
            catch
            {
                txt_Promotion_Post.Text = "";
            }
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

            txt_WorkNum.Text = "1";
            txt_Rank_Start.Text = "31";
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



        private void clib_Cate1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //체크 리스트 박스 한 아이템만 선택하도록 허용
            if (e.NewValue == CheckState.Checked)
            {
                CheckedListBox checkedListBox = sender as CheckedListBox; //캐스팅

                for (int count = 0; count < checkedListBox.Items.Count; ++count)
                {
                    if (e.Index != count) checkedListBox.SetItemChecked(count, false);
                }
            }
        }
        private void clib_Cate1_MouseDown(object sender, MouseEventArgs e)
        {
            string SelText = clib_Channel.SelectedItem.ToString();
            this.clib_ALL_Cate1_DB_Read();    // DB 읽어오기
            txt_WorkNum.Text = "1";
            txt_Rank_Start.Text = "31";
        }
        private void clib_Cate1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int SelRow = clib_Cate1.SelectedIndex;
                string SelText = clib_Cate1.SelectedItem.ToString();
                clib_Cate1.SetItemChecked(SelRow, true);
            }
            catch { }
        }
        


        private void clib_Cate2_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //체크 리스트 박스 한 아이템만 선택하도록 허용
            if (e.NewValue == CheckState.Checked)
            {
                CheckedListBox checkedListBox = sender as CheckedListBox; //캐스팅

                for (int count = 0; count < checkedListBox.Items.Count; ++count)
                {
                    if (e.Index != count) checkedListBox.SetItemChecked(count, false);
                }
            }
        }
        private void clib_Cate2_MouseDown(object sender, MouseEventArgs e)
        {
            string SelText = clib_Channel.SelectedItem.ToString();
            if (SelText != "카카오쇼핑")
            {
                //this.clib_KASHO_Cate2_DB_Read();    // DB 읽어오기
                this.clib_ALL_Cate2_DB_Read();    // DB 읽어오기
            }
            txt_WorkNum.Text = "1";
            txt_Rank_Start.Text = "31";
        }
        private void clib_Cate2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int SelRow = clib_Cate2.SelectedIndex;
                string SelText = clib_Cate2.SelectedItem.ToString();
                clib_Cate2.SetItemChecked(SelRow, true);
            }
            catch { }
        }

        

        private void clib_Cate3_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //체크 리스트 박스 한 아이템만 선택하도록 허용
            if (e.NewValue == CheckState.Checked)
            {
                CheckedListBox checkedListBox = sender as CheckedListBox; //캐스팅

                for (int count = 0; count < checkedListBox.Items.Count; ++count)
                {
                    if (e.Index != count) checkedListBox.SetItemChecked(count, false);
                }
            }
        }
        private void clib_Cate3_MouseDown(object sender, MouseEventArgs e)
        {
            string SelText = clib_Channel.SelectedItem.ToString();
            if (SelText != "카카오쇼핑")
            {
                this.clib_ALL_Cate3_DB_Read();    // DB 읽어오기
            }
            txt_WorkNum.Text = "1";
            txt_Rank_Start.Text = "31";
        }
        private void clib_Cate3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int SelRow = clib_Cate3.SelectedIndex;
                string SelText = clib_Cate3.SelectedItem.ToString();
                clib_Cate3.SetItemChecked(SelRow, true);
            }
            catch { }
        }




        private void clib_Cate4_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //체크 리스트 박스 한 아이템만 선택하도록 허용
            if (e.NewValue == CheckState.Checked)
            {
                CheckedListBox checkedListBox = sender as CheckedListBox; //캐스팅

                for (int count = 0; count < checkedListBox.Items.Count; ++count)
                {
                    if (e.Index != count) checkedListBox.SetItemChecked(count, false);
                }
            }
        }
        private void clib_Cate4_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int SelRow = clib_Cate4.SelectedIndex;
                string SelText = clib_Cate4.SelectedItem.ToString();
                clib_Cate4.SetItemChecked(SelRow, true);
            }
            catch { }
        }




        private void btn_PGMStart_Click(object sender, EventArgs e)
        {
            try { ADONet_Conn.Close(); }
            catch { }

            Process_Pause = false;
            Process_Stop = false;
            string textValue = txt_Promotion_Post.Text;

            int SelRow = clib_Channel.SelectedIndex;
            if (SelRow == -1) { SelRow = 0; }
            if (txt_Promotion_Post.Text.Length > Convert.ToInt32(var_Channel[SelRow, 1]))
            {
                richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "   홍보글은 " + var_Channel[SelRow, 1] + "자 이내로 작성하세요!";
                AutoClosingMessageBox.Show("홍보글은 " + var_Channel[SelRow, 1] + "자 이내로 작성하세요!", "메세지박스 자동종료창", 3000);
                return;
            }

            if (btn_PGMStart.Text == "프로그램 시작")
            {
                //this.구동종료();

                Process_Pause = false;
                Process_Stop = false;

                clib_Channel.BackColor = Color.WhiteSmoke;
                clib_Channel_Num.BackColor = Color.WhiteSmoke;
                clib_Cate1.BackColor = Color.WhiteSmoke;
                clib_Cate2.BackColor = Color.WhiteSmoke;
                clib_Cate3.BackColor = Color.WhiteSmoke;
                clib_Cate4.BackColor = Color.WhiteSmoke;
                //clib_Channel.Enabled = false;
                //clib_Channel_Num.Enabled = false;
                //clib_N_Cate1.Enabled = false;            

                string upSQL = "UPDATE PROMOTION_UserRecord   ";
                upSQL += " SET UserPostSel = '" + clib_Channel.SelectedIndex + "^" + clib_Channel_Num.SelectedIndex + "'  ";
                upSQL += " WHERE UserID = '" + p_UserID + "' ";
                ADONet_Conn.Open();
                SqlCommand cmd0_1 = new SqlCommand();
                cmd0_1.Connection = ADONet_Conn;
                cmd0_1.CommandText = upSQL;
                cmd0_1.ExecuteNonQuery();
                ADONet_Conn.Close();

                if (chk_CateSelect.Checked == false)
                {
                    if (clib_Channel.SelectedItem.ToString() == "카카오쇼핑")
                    {
                        clib_Cate2.Items.Clear();
                        clib_Cate3.Items.Clear();
                        clib_Cate4.Items.Clear();
                    }
                }
                //clib_N_Cate2.Enabled = false;
                //clib_N_Cate3.Enabled = false;
                //clib_N_Cate4.Enabled = false;

                //btn_ManualStart.Enabled = false;

                // 화면 줄이기 ----------------------------
                if (chk_CateSelect.Checked == true)
                {
                    checkBoxHide.Checked = true;
                    timerSliding.Start();
                }
                // 화면 줄이기 ----------------------------

                timer_PGMStart_Auto.Interval = 1000 * 2;    // 최초에 2초 후에 내상품 프로그램 실행
                timer_PGMStart_Auto.Start();
            }
            else if (btn_PGMStart.Text == "다시 시작")
            {
                richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "   [다시 시작]";
                timer_Loop.Interval = 1000 * 2;    // 최초에 1초 후에 내상품 프로그램 실행
                timer_Loop.Start();
            }
        }

        private void btn_Pause_Click(object sender, EventArgs e)
        {
            try
            {
                Process_Pause = true;
                ProgressBar1.Style = ProgressBarStyle.Blocks;
                richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "   [일시정지]" + "\n" + richTextBox1.Text;
            }
            catch { }
        }

        private void btn_ReStart_Click(object sender, EventArgs e)
        {
            try
            {
                Process_Pause = false;
                ProgressBar1.Style = ProgressBarStyle.Marquee;
                ProgressBar1.MarqueeAnimationSpeed = 50;
                richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "   [재시작]" + "\n" + richTextBox1.Text;
            }
            catch { }
        }

        private void btn_PGMEnd_Click(object sender, EventArgs e)
        {
            try
            {
                string upSQL = "UPDATE PROMOTION_UserRecord   ";
                if (clib_Channel.SelectedItem.ToString() == "카카오쇼핑")
                {
                    upSQL += " SET UserLastAct = '" + clib_Channel.SelectedItem.ToString() + "^" + txt_WorkKeyword.Text + "^" + txt_WorkNum.Text + "'  ";
                }
                else
                {
                    upSQL += " SET UserLastAct = '" + clib_Channel.SelectedItem.ToString() + "^" + txt_WorkKeyword.Text + "^" + txt_CheckingRank.Text + "'  ";
                }
                upSQL += " WHERE UserID = '" + p_UserID + "' ";
                ADONet_Conn.Open();
                SqlCommand cmd0_1 = new SqlCommand();
                cmd0_1.Connection = ADONet_Conn;
                cmd0_1.CommandText = upSQL;
                cmd0_1.ExecuteNonQuery();
                ADONet_Conn.Close();

                txt_UserLastAct.Text = txt_WorkKeyword.Text + "^" + txt_WorkNum.Text;
            }
            catch { }

            // 화면 늘리기 ----------------------------
            checkBoxHide.Checked = false;
            timerSliding.Start();
            Delay(2000);
            // 화면 늘리기 ----------------------------

            this.구동종료();

            richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "   [홍보작업 종료]" + "\n" + richTextBox1.Text;

            MessageBox.Show("작업종료");
        }

        private void 구동종료()
        {
            Process_Pause = false;
            Process_Stop = true;
            Console.WriteLine("구동 종료");
            try
            {
                ProgressBar1.Style = ProgressBarStyle.Blocks;
                btn_PGMStart.Text = "프로그램 시작";
            }
            catch { }

            clib_Channel.BackColor = SystemColors.Window;
            clib_Channel_Num.BackColor = SystemColors.Window;
            clib_Cate1.BackColor = SystemColors.Window;
            clib_Cate2.BackColor = SystemColors.Window;
            clib_Cate3.BackColor = SystemColors.Window;
            clib_Cate4.BackColor = SystemColors.Window;
            //clib_Channel.Enabled = true;
            //clib_Channel_Num.Enabled = true;
            //clib_N_Cate1.Enabled = true;
            //clib_N_Cate2.Enabled = true;
            //clib_N_Cate3.Enabled = true;
            //clib_N_Cate4.Enabled = true;

            btn_ManualStart.Enabled = true;
            System.Windows.Forms.Application.DoEvents();

            try
            {
                chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles.First());
                string BaseWindow01 = chromeDriver.CurrentWindowHandle;
                for (; ; )
                {
                    chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles.Last());
                    chromeDriver.Close();
                    Delay(new Random().Next(800, 1000));
                }
            }
            catch
            {
                try
                {
                    chromeDriver.Quit();
                }
                catch { }
            }


            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var LocalIp in host.AddressList)
            {
                if (LocalIp.AddressFamily == AddressFamily.InterNetwork)
                {
                    Console.WriteLine("IP Address = " + LocalIp.ToString());
                    if (LocalIp.ToString() == "192.168.0.21")
                    {
                        break;
                    }
                    else
                    {
                        this.exeClear("chrome");
                        break;
                    }
                }
            }

            try
            {
                chromeDriver.Quit();
            }
            catch { }


        }

        private void txt_Promotion_Post_TextChanged(object sender, EventArgs e)
        {
            int SelRow = clib_Channel.SelectedIndex;
            if (SelRow == -1) { SelRow = 0; }
            txt_Prom_Length.Text = txt_Promotion_Post.Text.Length.ToString("#,##0") + "  / " + var_Channel[SelRow, 1];
        }

        private void btn_Post_Modify_Click(object sender, EventArgs e)
        {
            frm_Post _frm_Post = new frm_Post();
            _frm_Post.Show(); // Form2 를 보이게 한다 위치는 조정
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            //int SelRowN = 0;
            //string SelTextN = "";
            //for (int xi = 0; xi <= (clib_Channel_Num.Items.Count - 1); xi++)
            //{
            //    if (clib_Channel_Num.GetItemChecked(xi))
            //    {
            //        SelRowN = xi;
            //        SelTextN = clib_Channel_Num.Items[xi].ToString();
            //    }
            //}

            //string _SavePath = _Path + @"\P_Post\" + SelText + ".txt";
            //string textValue = txt_Promotion_Post.Text;
            //System.IO.File.WriteAllText(_SavePath, textValue, Encoding.Default);


            int SelRow = clib_Channel.SelectedIndex;
            int SelRowN = clib_Channel_Num.SelectedIndex;
            string SelText = clib_Channel.SelectedItem.ToString();
            string SelTextN = clib_Channel_Num.SelectedItem.ToString();

            if (txt_Promotion_Post.Text.Length > Convert.ToInt32(var_Channel[SelRowN, 1]))
            {
                MessageBox.Show("등록 가능한 최대 글자수를 초과하였습니다.", "Auto PR");
            }
            else
            {
                string textValue = txt_Promotion_Post.Text;
                textValue = textValue.Replace("'", "");
                textValue = textValue.Replace("\r\n", "<br>");

                SQL = " SELECT *   FROM PROMOTION_POST   WHERE PP_UserID = '" + p_UserID + "' AND PP_Market = '" + SelText + "' AND PP_Market_Dno = " + (SelRowN + 1) + " ";
                ADONet_Conn_Routine();
                if (RsTbl.Rows.Count > 0)
                {
                    ADONet_Conn.Close();

                    string upSQL = "UPDATE PROMOTION_POST   ";
                    upSQL += " SET PP_Post = '" + textValue + "'  ";
                    upSQL += " WHERE PP_UserID = '" + p_UserID + "' AND PP_Market = '" + SelText + "' AND PP_Market_Dno = " + (SelRowN + 1) + " ";
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
                    inSQL += "  " + (SelRowN + 1) + " , ";
                    inSQL += " '" + textValue + "'  ";
                    inSQL += "  ) ";
                    ADONet_Conn.Open();
                    SqlCommand cmd0_1 = new SqlCommand();
                    cmd0_1.Connection = ADONet_Conn;
                    cmd0_1.CommandText = inSQL;
                    cmd0_1.ExecuteNonQuery();
                    ADONet_Conn.Close();
                }

                AutoClosingMessageBox.Show("포스트를 저장하였습니다.", "Auto PR  [메세지 자동종료]", 2000);
            }
        }






        public string Selenium_Parcing(string xUrl)
        {
            // 롯데온은 셀레니움으로 가져올 것
            try
            {
                chromeDriver.Navigate().GoToUrl(xUrl);
            }
            catch
            {
                this.Selenium_Browser();
                chromeDriver.Navigate().GoToUrl(xUrl);
            }
            WebDriverWait wait = new WebDriverWait(chromeDriver, new TimeSpan(0, 0, 5));

            Delay(1000);

            // 스크롤 업데이트 기능이 있는 경우 페이지 다운 필요 (오늘의집)
            string Source = chromeDriver.PageSource;

            Delay(500);
            //chromeDriver.Navigate().Back();
            return Source;
        }

        public void Selenium_Paging(string xUrl)
        {
            // 롯데온은 셀레니움으로 가져올 것
            try
            {
                chromeDriver.Navigate().GoToUrl(xUrl);
            }
            catch
            {
                this.Selenium_Browser();
                chromeDriver.Navigate().GoToUrl(xUrl);
            }
            if (Process_Stop == true) { goto End_Loop; }
            WebDriverWait wait = new WebDriverWait(chromeDriver, new TimeSpan(0, 0, 5));
            Delay(new Random().Next(2000, 2200));

        End_Loop:
            Console.WriteLine("작업을 종료합니다.");
        }

        public void Selenium_Browser()
        {
            chromeDriverService.HideCommandPromptWindow = true;
            if (clib_Channel.SelectedItem.ToString() == "쿠팡")
            {
                chromeOptions.AddArgument("--incognito");     // 시크릿모드 사용 (쿠팡은 반드시 필요함)
            }
            else
            {

            }
            //chromeOptions.AddArgument("--disable-blink-features=AutomationControlled");     // 셀레니움이 아니라는 설정 처리 (navigator.webdriver) : 브라우저=false, 셀레니움=true임)
                                                                                            // 개발자모드 -> 콘솔 열기 -> navigator.webdriver 입력시 true 표시를 false 로 바꾸어줌
            chromeOptions.AddArgument("--disable-extensions");
            chromeOptions.AddArgument("--disable-notifications");
            chromeOptions.AddArgument("--disable-infobars");
            chromeOptions.AddArgument("--user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36");
            chromeOptions.AddExcludedArgument("enable-automation");
            chromeOptions.AddExcludedArgument("disable-popup-blocking");
            chromeOptions.AddAdditionalCapability("useAutomationExtension", false);
            //chromeOptions.AddArgument(@"C:\Program Files\Google\Chrome\Application\chrome.exe --user-data-dir=" + Application.StartupPath + "\\TempCookie");   // 실제 크롬브라우저 쿠키를 변경경로 에서 사용하는 방법
            chromeOptions.AddArgument("--user-data-dir=" + Application.StartupPath + "\\TempCookie");
            // 화면 숨기기 -------------------------------
            if (chk_BrowserView.Checked == false)
            {
                chromeOptions.AddArgument("--headless");
                chromeOptions.AddArgument("disable-gpu");
                chromeOptions.AddArgument("lang=ko_KR");
            }
            // 화면 숨기기 -------------------------------
            try
            {
                chromeDriver = new ChromeDriver(chromeDriverService, chromeOptions);

                if (clib_Channel.SelectedItem.ToString() == "오늘의집")
                {
                    chromeDriver.Manage().Window.Size = new Size(1200, 1400);  // 화면에 3 * 3 표시되어야 요소 선택에 오류가 적어짐
                    chromeDriver.Manage().Window.Position = new Point(0, 0);
                }
                else if (clib_Channel.SelectedItem.ToString() == "카카오쇼핑")
                {
                    //chromeDriver.Manage().Window.Size = new Size(1000, P_Height - 50);
                    chromeDriver.Manage().Window.Size = new Size(1100, 1400);  // 화면에 3 * 3 표시되어야 요소 선택에 오류가 적어짐
                    chromeDriver.Manage().Window.Position = new Point(0, 0);
                }
                else
                {
                    //chromeDriver.Manage().Window.Size = new Size(1000, P_Height - 50);
                    chromeDriver.Manage().Window.Size = new Size(1200, 1400);  // 화면에 3 * 3 표시되어야 요소 선택에 오류가 적어짐
                    chromeDriver.Manage().Window.Position = new Point(0, 0);
                }
            }
            catch { }
            //chromeDriver.Navigate().GoToUrl("https://www.google.co.kr");
        }

        public void Selenium_Browser_사이즈자동변경()
        {
            //pc_device = ["1920,1400","1920,1200","1920,1080","1600,1200","11600,900","1536,864","1440,1080","1440,900","1360,768"]
            //mo_device = ["360,640","360,740","375,667","375,812","412,732","415,846","412,869","412,915"]
            //Width,Height = Random.choice(mo_device).split(",")
            string[] pcDevice = { "1920,1400", "1920,1200", "1920,1080", "1600,1200", "11600,900", "1536,864", "1440,1080", "1440,900", "1360,768" };
            string[] moDevice = { "360,640", "360,740", "375,667", "375,812", "412,732", "415,846", "412,869", "412,915" };
            Random rand = new Random();
            int randomIndex = rand.Next(moDevice.Length);
            string dimensions = moDevice[randomIndex];
            int width = Convert.ToInt32(SSplit(dimensions, ",", 0));
            int height = Convert.ToInt32(SSplit(dimensions, ",", 1));

            chromeDriverService.HideCommandPromptWindow = true;
            chromeOptions.AddArgument("--disable-blink-features=AutomationControlled");     // 셀레니움이 아니라는 설정 처리 (navigator.webdriver) : 브라우저=false, 셀레니움=true임)
            // 개발자모드 -> 콘솔 열기 -> navigator.webdriver 입력시 true 표시를 false 로 바꾸어줌
            chromeOptions.AddArgument("--disable-extensions");
            chromeOptions.AddArgument("--disable-notifications");
            chromeOptions.AddArgument("--disable-infobars");
            //chromeOptions.AddArgument("--window-size=" + dimensions + @""""); // 안먹힘
            chromeOptions.AddArgument("--user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36");
            chromeOptions.AddExcludedArgument("enable-automation");
            chromeOptions.AddExcludedArgument("disable-popup-blocking");
            chromeOptions.AddAdditionalCapability("useAutomationExtension", false);
            chromeOptions.AddArgument(@"C:\Program Files\Google\Chrome\Application\chrome.exe --user-data-dir=" + Application.StartupPath + "\\TempCookie");   // 실제 크롬브라우저 쿠키를 변경경로 에서 사용하는 방법
            //chromeOptions.AddArgument("--user-data-dir=" + Application.StartupPath + "\\TempCookie");
            // 화면 숨기기 -------------------------------
            if (chk_BrowserView.Checked == false)
            {
                chromeOptions.AddArgument("--headless");
                chromeOptions.AddArgument("disable-gpu");
                chromeOptions.AddArgument("lang=ko_KR");
            }
            // 화면 숨기기 -------------------------------
            try
            {
                chromeDriver = new ChromeDriver(chromeDriverService, chromeOptions);

                chromeDriver.Manage().Window.Size = new Size(width, height);  // 화면에 3 * 3 표시되어야 요소 선택에 오류가 적어짐
            }
            catch { }
            chromeDriver.Navigate().GoToUrl("https://www.google.co.kr");
        }

        private void timer_PGMStart_Auto_Tick(object sender, EventArgs e)
        {
            timer_PGMStart_Auto.Stop();

            if (btn_PGMStart.Text == "프로그램 시작")
            {
                btn_PGMStart.Text = "작동중";
                ProgressBar1.Style = ProgressBarStyle.Marquee;
                ProgressBar1.MarqueeAnimationSpeed = 50;
                if (PGM_Start_Manual == true)
                {
                    richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "   [작업 이어하기 시작]" + "\n" + richTextBox1.Text;
                }
                else
                {
                    richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "   [프로그램 시작]" + "\n" + richTextBox1.Text;
                }

                timer_Loop.Interval = 1000 * 2;
                timer_Loop.Start();
            }
            else if (btn_PGMStart.Text == "다시 시작")
            {
                btn_PGMStart.Text = "작동중";
                ProgressBar1.Style = ProgressBarStyle.Marquee;
                ProgressBar1.MarqueeAnimationSpeed = 50;
                richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "   [재시작]" + "\n" + richTextBox1.Text;

                timer_Loop.Interval = 1000 * 2;
                timer_Loop.Start();
            }
        }






        private void timer_Loop_Tick(object sender, EventArgs e)
        {

            timer_Loop.Stop();
            if (Process_Pause == true)
            {
                _Delay_Pause(10);
            }
            else if (Process_Stop == true)
            {
                goto End_Loop;
            }
            else
            {
                string C_SelText = clib_Channel.SelectedItem.ToString();
                string SelText1 = "";
                SelText1 = clib_Cate1.SelectedItem.ToString();

                if (chk_CateSelect.Checked == false)
                {
                    SQL = " SELECT channel_category_code, channel_category2   FROM PROMOTION_CATEGORY_ALL   ";
                    if (C_SelText == "카카오쇼핑")
                    {
                        SQL += " WHERE channel_name = '카카오쇼핑' AND channel_category1 like '" + SelText1 + "' AND channel_category2 <> '' ";
                    }
                    else
                    {
                        SQL += " WHERE channel_name = '" + C_SelText + "' AND channel_name = '" + C_SelText + "' AND channel_category1 like '" + SelText1 + "' AND channel_category2 <> '' AND channel_category3 LIKE '' ";
                    }                    
                    SQL += " ORDER BY channel_category_no ASC ";
                    ADONet_Conn_Routine();
                    if (RsTbl.Rows.Count > 0)
                    {
                        string[] x_ohoucatecode = new string[RsTbl.Rows.Count + 1];
                        clib_Cate2.Items.Clear();
                        clib_Cate3.Items.Clear();
                        clib_Cate4.Items.Clear();
                        for (int xi = 0; xi < RsTbl.Rows.Count; xi++)
                        {
                            clib_Cate2.Items.Add(RsTbl.Rows[xi].Field<string>("channel_category2"));
                            x_ohoucatecode[xi] = RsTbl.Rows[xi].Field<string>("channel_category_code");
                        }
                        ADONet_Conn.Close();
                        Delay(10);
                        clib_Cate2.SetSelected(0, true);

                        // 반복작업 리스트 02
                        for (int xi = 0; xi < clib_Cate2.Items.Count; xi++)
                        {
                            _Delay_Pause(10);
                            if (Process_Stop == true) { goto End_Loop; }

                            int SelRow2 = xi;
                            clib_Cate2.SetItemChecked(SelRow2, true);
                            clib_Cate2.SetSelected(SelRow2, true);
                            string SelText2 = clib_Cate2.SelectedItem.ToString();
                            txt_CateID.Text = x_ohoucatecode[xi].ToString();
                            //this.clib_N_Cate2_DB_Read();    // DB 읽어오기

                            // 중분류 키워드 작업 -----------------------------------------------------------------------
                            this.Crawling_Market(SelText2);
                            // 중분류 키워드 작업 -----------------------------------------------------------------------

                        }
                    }
                    else
                    {
                        ADONet_Conn.Close();
                    }


                    try
                    {
                        ADONet_Conn.Close();
                    }
                    catch { }

                    // 화면 늘리기 ----------------------------
                    checkBoxHide.Checked = false;
                    timerSliding.Start();
                    Delay(2000);
                    // 화면 늘리기 ----------------------------

                    this.구동종료();

                    string xText = SSplit(clib_Channel.SelectedItem.ToString(), "  [", 0) + "   (대분류키워드)" + clib_Cate1.SelectedItem.ToString();
                    richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "   [" + xText + "]  작업완료" + "\n" + richTextBox1.Text;

                    MessageBox.Show("[" + xText + "]  작업완료");

                }
                else
                {
                    if (clib_Channel.SelectedItem.ToString() == "카카오쇼핑")
                    {
                        string SelText2 = "";
                        string xText = "";
                        if (Key_WorkState == false)
                        {
                            SelText2 = clib_Cate2.SelectedItem.ToString();

                            SQL = " SELECT *   FROM PROMOTION_CATEGORY_ALL   ";
                            SQL += " WHERE channel_name = '" + C_SelText + "' AND channel_category2 like '" + SelText2 + "' ";
                            SQL += " ORDER BY channel_category_no ASC ";
                            ADONet_Conn_Routine();
                            if (RsTbl.Rows.Count > 0)
                            {
                                txt_CateID.Text = RsTbl.Rows[0].Field<string>("channel_category_code").ToString();
                            }

                            this.Crawling_Market(SelText2);
                            xText = SSplit(clib_Channel.SelectedItem.ToString(), "  [", 0) + "   (중분류)" + clib_Cate2.SelectedItem.ToString();
                        }
                        else
                        {
                            Key_WorkState = false;
                        }

                        richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "   [" + xText + "]  작업완료" + "\n" + richTextBox1.Text;
                        richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "   → 잠시만 기다려주세요!" + "\n" + richTextBox1.Text;
                        Key_WorkState = false;

                        // 화면 늘리기 ----------------------------
                        checkBoxHide.Checked = false;
                        timerSliding.Start();
                        // 화면 늘리기 ----------------------------

                        this.구동종료();

                        richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "   [작업 대기중]" + "\n" + richTextBox1.Text;
                        MessageBox.Show("[" + xText + "]  작업완료");
                    }
                    else
                    {
                        string SelText2 = "";
                        string SelText3 = "";
                        string SelText4 = "";
                        string xText = "";
                        if (Key_WorkState == false)
                        {
                            try
                            {
                                SelText4 = clib_Cate4.SelectedItem.ToString();

                                SQL = " SELECT *   FROM PROMOTION_CATEGORY_ALL   ";
                                SQL += " WHERE channel_name = '" + C_SelText + "' AND channel_category4 like '" + SelText4 + "' ";
                                SQL += " ORDER BY channel_category_no ASC ";
                                ADONet_Conn_Routine();
                                if (RsTbl.Rows.Count > 0)
                                {
                                    txt_CateID.Text = RsTbl.Rows[0].Field<string>("channel_category_code").ToString();
                                }
                            }
                            catch
                            {
                                try
                                {
                                    SelText3 = clib_Cate3.SelectedItem.ToString();

                                    SQL = " SELECT *   FROM PROMOTION_CATEGORY_ALL   ";
                                    SQL += " WHERE channel_name = '" + C_SelText + "' AND channel_category3 like '" + SelText3 + "' AND channel_category4 LIKE '' ";
                                    SQL += " ORDER BY channel_category_no ASC ";
                                    ADONet_Conn_Routine();
                                    if (RsTbl.Rows.Count > 0)
                                    {
                                        txt_CateID.Text = RsTbl.Rows[0].Field<string>("channel_category_code").ToString();
                                    }
                                }
                                catch
                                {
                                    SelText2 = clib_Cate2.SelectedItem.ToString();

                                    SQL = " SELECT *   FROM PROMOTION_CATEGORY_ALL   ";
                                    SQL += " WHERE channel_name = '" + C_SelText + "' AND channel_category2 like '" + SelText2 + "' AND channel_category3 LIKE '' AND channel_category4 LIKE '' ";
                                    SQL += " ORDER BY channel_category_no ASC ";
                                    ADONet_Conn_Routine();
                                    if (RsTbl.Rows.Count > 0)
                                    {
                                        txt_CateID.Text = RsTbl.Rows[0].Field<string>("channel_category_code").ToString();
                                    }
                                }
                            }

                            if (SelText4 != "")
                            {
                                this.Crawling_Market(SelText4);
                                xText = SSplit(clib_Channel.SelectedItem.ToString(), "  [", 0) + "   (세분류)" + clib_Cate4.SelectedItem.ToString();
                            }
                            else if (SelText3 != "")
                            {
                                this.Crawling_Market(SelText3);
                                xText = SSplit(clib_Channel.SelectedItem.ToString(), "  [", 0) + "   (소분류)" + clib_Cate3.SelectedItem.ToString();
                            }
                            else if (SelText2 != "")
                            {
                                this.Crawling_Market(SelText2);
                                xText = SSplit(clib_Channel.SelectedItem.ToString(), "  [", 0) + "   (중분류)" + clib_Cate2.SelectedItem.ToString();
                            }

                        }
                        else
                        {
                            Key_WorkState = false;
                        }

                        richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "   [" + xText + "]  완료" + "\n" + richTextBox1.Text;
                        richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "   → 잠시만 기다려주세요!" + "\n" + richTextBox1.Text;
                        Key_WorkState = false;

                        // 화면 늘리기 ----------------------------
                        checkBoxHide.Checked = false;
                        timerSliding.Start();
                        // 화면 늘리기 ----------------------------

                        this.구동종료();

                        richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "   [작업 대기중]" + "\n" + richTextBox1.Text;
                        MessageBox.Show("[" + xText + "]  작업완료");
                    }
                }


            }

        End_Loop:
            Console.WriteLine("작업을 종료합니다.");
        }
        
        private void Crawling_Market(string cText)
        {
            //int index = checkedListBox1.SelectedIndex;
            //string item = checkedListBox1.SelectedItem.ToString();
            
            try
            {
                int SelRow1 = clib_Cate1.SelectedIndex;
                int SelRow2 = clib_Cate2.SelectedIndex;
                int SelRow3 = clib_Cate3.SelectedIndex;
                int SelRow4 = clib_Cate4.SelectedIndex;

                string upSQL = "UPDATE PROMOTION_UserRecord   ";
                upSQL += " SET UserCate1Sel = '" + SelRow1 + "' , UserCate2Sel = '" + SelRow2 + "' , UserCate3Sel = '" + SelRow3 + "' , UserCate4Sel = '" + SelRow4 + "'   ";
                upSQL += " WHERE UserID = '" + p_UserID + "' ";
                ADONet_Conn.Open();
                SqlCommand cmd0_1 = new SqlCommand();
                cmd0_1.Connection = ADONet_Conn;
                cmd0_1.CommandText = upSQL;
                cmd0_1.ExecuteNonQuery();
                ADONet_Conn.Close();
            }
            catch { }

            

            if (Process_Stop == true) { goto End_Loop; }

            //clib_Channel.SelectedIndex = 0;
            string channel_item = clib_Channel.SelectedItem.ToString();

            if (channel_item.IndexOf("오늘의집") > -1)
            {
                db_Market = channel_item;   // 작업정보 가져오기 -------------------------------------
                db_SKeyword = cText;        // 작업정보 가져오기 -------------------------------------
                string xUrl = "https://ohou.se/store/category?category_id=" + txt_CateID.Text;
                //string xUrl = "https://ohou.se/productions/feed?query=" + cText + "&search_affect_type=Typing";
                try { }
                catch { }
                richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "   [오늘의집]  검색어 : " + cText + "\n" + richTextBox1.Text;
                //txt_WorkKeyword.Text = cText;
                txt_WorkKeyword.Text = this.clib_Cate_Check();

                //while (Key_WorkState == false)
                //{
                if (Process_Pause == true)
                {
                    _Delay_Pause(10);
                }
                else if (Process_Stop == true)
                {
                    goto End_Loop;
                }

                Key_WorkState = false;

                if (chk_Error.Checked==true)
                {
                    this.Selenium_오늘의집(xUrl);
                }
                else
                {
                    try
                    {
                        this.Selenium_오늘의집(xUrl);

                        Console.WriteLine(Key_WorkState);
                    }
                    catch
                    {
                        MessageBox.Show(new Form() { TopMost = true }, "일시적인 오류입니다." + "\n" + "마지막작업번호 부터 다시 시작하세요!", "홍보프로그램");
                    }
                }
                //}
            }
            else if (channel_item.IndexOf("카카오선물하기") > -1)
            {

            }
            else if (channel_item.IndexOf("카카오쇼핑") > -1)
            {
                db_Market = channel_item;   // 작업정보 가져오기 -------------------------------------
                db_SKeyword = cText;        // 작업정보 가져오기 -------------------------------------
                string xUrl = txt_CateID.Text;
                try { }
                catch { }
                richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "   [카카오쇼핑]  검색어 : " + cText + "\n" + richTextBox1.Text;
                //txt_WorkKeyword.Text = cText;
                txt_WorkKeyword.Text = this.clib_Cate_Check();

                //while (Key_WorkState == false)
                //{
                if (Process_Pause == true)
                {
                    _Delay_Pause(10);
                }
                else if (Process_Stop == true)
                {
                    goto End_Loop;
                }

                Key_WorkState = false;


                if (chk_Error.Checked == true)
                {
                    this.Selenium_카카오쇼핑(xUrl);
                }
                else
                {
                    try
                    {
                        this.Selenium_카카오쇼핑(xUrl);

                        Console.WriteLine(Key_WorkState);
                    }
                    catch
                    {
                        MessageBox.Show(new Form() { TopMost = true }, "일시적인 오류입니다." + "\n" + "마지막작업번호 부터 다시 시작하세요!", "홍보프로그램");
                    }
                }
                //}
            }
            else if (channel_item.IndexOf("카카오선물하기") > -1)
            {

            }
            else if (channel_item.IndexOf("네이버플레이스") > -1)
            {

            }
            else if (channel_item.IndexOf("에이블리") > -1)
            {

            }
            else if (channel_item.IndexOf("쿠팡") > -1)
            {
                db_Market = channel_item;   // 작업정보 가져오기 -------------------------------------
                db_SKeyword = cText;        // 작업정보 가져오기 -------------------------------------
                string xUrl = txt_CateID.Text;
                try { }
                catch { }
                richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "   [쿠팡]  검색어 : " + cText + "\n" + richTextBox1.Text;
                //txt_WorkKeyword.Text = cText;
                txt_WorkKeyword.Text = this.clib_Cate_Check();

                if (Process_Pause == true)
                {
                    _Delay_Pause(10);
                }
                else if (Process_Stop == true)
                {
                    goto End_Loop;
                }

                Key_WorkState = false;

                if (chk_Error.Checked == true)
                {
                    this.Selenium_쿠팡(xUrl);
                }
                else
                {
                    try
                    {
                        this.Selenium_쿠팡(xUrl);

                        Console.WriteLine(Key_WorkState);
                    }
                    catch
                    {
                        MessageBox.Show(new Form() { TopMost = true }, "일시적인 오류입니다." + "\n" + "마지막작업번호 부터 다시 시작하세요!", "홍보프로그램");
                    }
                }
            }
            else if (channel_item.IndexOf("네이버") > -1)
            {
                db_Market = channel_item;   // 작업정보 가져오기 -------------------------------------
                db_SKeyword = cText;        // 작업정보 가져오기 -------------------------------------
                string xUrl = txt_CateID.Text;
                try { }
                catch { }
                richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "   [네이버]  검색어 : " + cText + "\n" + richTextBox1.Text;
                //txt_WorkKeyword.Text = cText;
                txt_WorkKeyword.Text = this.clib_Cate_Check();

                if (Process_Pause == true)
                {
                    _Delay_Pause(10);
                }
                else if (Process_Stop == true)
                {
                    goto End_Loop;
                }

                Key_WorkState = false;

                if (chk_Error.Checked == true)
                {
                    this.Selenium_네이버(xUrl);
                }
                else
                {
                    try
                    {
                        this.Selenium_네이버(xUrl);

                        Console.WriteLine(Key_WorkState);
                    }
                    catch
                    {
                        MessageBox.Show(new Form() { TopMost = true }, "일시적인 오류입니다." + "\n" + "마지막작업번호 부터 다시 시작하세요!", "홍보프로그램");
                    }
                }
            }

        End_Loop:
            Console.WriteLine("작업을 종료합니다.");

        }

        public string clib_Cate_Check()
        {
            string clib_Cate = "";

            try
            {
                int SelRow1 = clib_Cate1.SelectedIndex;
                string SelText1 = clib_Cate1.SelectedItem.ToString();
                clib_Cate = SelText1;

                try
                {
                    int SelRow2 = clib_Cate2.SelectedIndex;
                    string SelText2 = clib_Cate2.SelectedItem.ToString();
                    clib_Cate += ">" + SelText2;
                }
                catch { }

                if (clib_Channel.SelectedItem.ToString() == "오늘의집" || clib_Channel.SelectedItem.ToString() == "쿠팡")
                {
                    try
                    {
                        int SelRow3 = clib_Cate3.SelectedIndex;
                        string SelText3 = clib_Cate3.SelectedItem.ToString();
                        clib_Cate += ">" + SelText3;

                        try
                        {
                            int SelRow4 = clib_Cate4.SelectedIndex;
                            string SelText4 = clib_Cate4.SelectedItem.ToString();
                            clib_Cate += ">" + SelText4;
                        }
                        catch { }
                    }
                    catch { }
                }
            }
            catch
            {
                clib_Cate = "";
            }

            return clib_Cate;
        }
        





        public void Selenium_오늘의집(string xUrl)
        {
            this.Selenium_Paging(xUrl);

            //// 저사양 페이지 로딩시간 연장 필요
            //if (P_Width < 1500)
            //{
            //    _Delay_Pause(3000);
            //}
            //string xState = OnLoadWait(chromeDriver, xUrl, 1, "production-selling-question");

            WebDriverWait wait = null;
            if (Process_Stop == true) { goto End_Loop; }
            if (chromeDriver.PageSource.IndexOf("회원가입") > -1)
            {
                chromeDriver.Navigate().GoToUrl("https://ohou.se/users/sign_in");
                wait = new WebDriverWait(chromeDriver, new TimeSpan(0, 0, 5));
                Delay(1000);
                _Delay_Pause(10);
                if (Process_Stop == true) { goto End_Loop; }

                //var queryButton = chromeDriver.FindElement(By.XPath("//*[@href='/users/auth/kakao']"));
                //queryButton.Click();
                //wait = new WebDriverWait(chromeDriver, new TimeSpan(0, 0, 5));
                //Delay(5000);
                //_Delay_Pause(10);
                //if (Process_Stop == true) { goto End_Loop; }

                //chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles.Last());
                //wait = new WebDriverWait(chromeDriver, new TimeSpan(0, 0, 5));
                //chromeDriver.Navigate().GoToUrl(xUrl);
                //wait = new WebDriverWait(chromeDriver, new TimeSpan(0, 0, 5));
                //Delay(2000);
                _Delay_Pause(10);
                if (Process_Stop == true) { goto End_Loop; }

                AutoClosingMessageBox.Show("오늘의집 회원가입 후 프로그램을 다시 실행하세요!", "Auto PR  [메세지 자동종료]", 2000);
                richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "     ▶ 오늘의집 로그인안됨" + "\n" + richTextBox1.Text;
                return;
            }
            else if (chromeDriver.PageSource.IndexOf("찾으시는 결과가 없네요.") > -1)
            {
                Key_WorkState = true;
                richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "     → 상품없음" + "\n" + richTextBox1.Text;
                goto End_Loop;
            }

            if (chromeDriver.Url.IndexOf("ohou.se") == -1)
            {
                richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "   [오늘의집 로그인안됨]" + "\n" + richTextBox1.Text;
                Source = "오늘의집 PASS";
            }
            else
            {
                int RowH = 0;
                string LastID = "";
                bool StartPOS = true;
                int Scroll_End = 0;
                //string[] ListID = new string[Convert.ToInt32(txt_Rank_End.Text)];

                while (RowH <= Convert.ToInt32(txt_Rank_End.Text))
                {
                    bool LastPOS = false;

                    _Delay_Pause(10);
                    if (Process_Stop == true) { goto End_Loop; }
                    wait = new WebDriverWait(chromeDriver, TimeSpan.FromSeconds(20));
                    IWebElement _SearchResult = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector("[href$='affect_id&affect_type=ProductCategoryIndex']")));
                    IList<IWebElement> he01 = chromeDriver.FindElements(By.TagName("article"));

                    // 새로 열려는데 엘리먼트가 없으면 리플래시 를 다시 하는 것 검토
                    foreach (IWebElement helem1 in he01)
                    {
                        bool _Prod_Detail = false;
                        // 전체상품 체크
                        if (txt_TTLProdCount.Text == "-1")
                        {
                            string xSource = chromeDriver.PageSource;
                            xSource = SSplit(xSource, "개</p>", 0);
                            xSource = Mid(xSource, xSource.Length - 19, 20);
                            txt_TTLProdCount.Text = SSplit(xSource, ">전체 ", 1).Replace(",", "");
                        }

                        // 페이지 로딩 오류 발ㄹ생시 리로딩 처리하기
                        if (chromeDriver.PageSource.IndexOf("오류가 발생했어요") > -1)
                        {
                            chromeDriver.Navigate().Refresh();
                            Delay(3500);
                            break;
                        }

                        if (helem1.GetAttribute("id") == "")
                        {
                            Console.WriteLine("MD Pick 상품 영역");
                        }
                        else
                        {
                            string SelectID = helem1.GetAttribute("id");
                            _Delay_Pause(10);
                            if (Process_Stop == true) { goto End_Loop; }

                            if (StartPOS == true)
                            {
                                RowH += 1;
                                txt_CheckingRank.Text = RowH.ToString();
                                LastID = helem1.GetAttribute("id");

                                int uxx = helem1.Location.X;
                                WebDoc_Window_ScrollTo(chromeDriver, uxx, helem1.Location.Y + Convert.ToInt32(txt_Scroll.Text));
                                if (RowH % 3 == 0) { _Delay_Pause(Convert.ToInt32(cbx_ScrollSec.Text)); }
                            }
                            else
                            {
                                if (LastPOS == true)        //if (LastID == "" || LastPOS == true)
                                {
                                    RowH += 1;
                                    Scroll_End = 0;

                                    txt_CheckingRank.Text = RowH.ToString();
                                    LastID = helem1.GetAttribute("id");

                                    int uxx = helem1.Location.X;
                                    WebDoc_Window_ScrollTo(chromeDriver, uxx, helem1.Location.Y + Convert.ToInt32(txt_Scroll.Text));
                                    if (RowH % 3 == 0) { _Delay_Pause(Convert.ToInt32(cbx_ScrollSec.Text)); }

                                    if (RowH < Convert.ToInt32(txt_Rank_Start.Text))
                                    {
                                        Console.WriteLine(RowH + "r 시작 순번 이하");
                                    }
                                    else
                                    {

                                        //// 문의하기 처리된 판매자는 제외하는 루틴 =============================================
                                        Recent_Store_YN = false;
                                        string Recent_ProdID = LastID.Replace("product-", "");

                                        //// WinHttp로 상세페이지 판매자정보 가져오기 실패 (내용 못가져옴)
                                        //string SellerUrl = "https://ohou.se/productions/" + Recent_ProdID + "/selling?affect_id&affect_type=ProductCategoryIndex";
                                        //WinHttpRequest WinHttp = new WinHttpRequest();
                                        //WinHttp.Open("GET", SellerUrl);
                                        //WinHttp.SetRequestHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/118.0.0.0 Whale/3.23.214.10 Safari/537.36");
                                        //WinHttp.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded;Charset=euc-kr;");
                                        //WinHttp.SetRequestHeader("Referer", chromeDriver.Url);
                                        //WinHttp.Send();
                                        //WinHttp.WaitForResponse();
                                        //string responseText = WinHttp.ResponseText;
                                        //responseText = responseText.Replace("\u002F", "/");
                                        //responseText = responseText.Replace("\u0026", "&");
                                        //if (responseText.IndexOf("<th>상호</th>") > -1)
                                        //{
                                        //    db_SellerName = "";
                                        //    try
                                        //    {
                                        //        db_SellerName = SSplit(SSplit(SSplit(Source, @"<th>상호</th>", 1), @"<td>", 1), @"</td>", 0);


                                        //        Recent_Store_YN = true;
                                        //    }
                                        //    catch { }
                                        //}


                                        Delay(1000);
                                        IList<IWebElement> heDe_01 = helem1.FindElements(By.TagName("span"));
                                        foreach (IWebElement helemDe1 in heDe_01)
                                        {
                                            if (helemDe1.GetAttribute("class") == "production-item__header__brand")
                                            {
                                                //// 요소에서 스토어명 가져오기
                                                //xStoreName = helemDe1.GetAttribute("innerText");
                                                //// 스토어명을 기존 작업 리스트와 비교
                                                //for (int xxj = 0; xxj < Recent_Store_dno; xxj++)
                                                //{
                                                //    if (Recent_StoreList[xxj] == xStoreName)
                                                //    {
                                                //        Console.WriteLine(RowH + "r  7일 이내 작업 있음 : " + xStoreName);
                                                //        richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "     →      " + RowH + "r  7일내 작업완료 : " + xStoreName + "\n" + richTextBox1.Text;
                                                //        Recent_Store_YN = true;
                                                //        break;
                                                //    }
                                                //}
                                            }
                                            else if (helemDe1.GetAttribute("class") == "production-item__header__name")
                                            {
                                                db_ProductName = helemDe1.GetAttribute("innerText");
                                                db_ProductName = db_ProductName.Replace("'", "");
                                            }
                                        }
                                        // 문의하기 처리된 판매자는 제외하는 루틴 =============================================

                                        //if (Recent_Store_YN == false)   // 7일 이내 해당스토어 작업 없음
                                        //{
                                            //Console.WriteLine(RowH + "r 상품검색 시작");

                                            #region  // 상품검색영역 ====================================================
                                            IList<IWebElement> he02 = helem1.FindElements(By.TagName("a"));

                                            Delay(100);
                                            foreach (IWebElement helem2 in he02)
                                            {
                                                _Delay_Pause(10);
                                                if (Process_Stop == true) { goto End_Loop; }
                                                if (helem2.GetAttribute("href") != null)
                                                {
                                                    //if (helem2.GetAttribute("href").IndexOf("_id=" + RowH + "&") > -1)
                                                    //{

                                                    //_Delay_Pause(200);

                                                    helem2.Click();
                                                    //Console.WriteLine("상품클릭 " + RowH);
                                                    txt_WorkNum.Text = RowH.ToString();
                                                    chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles.Last());
                                                    
                                                    Delay(1000);
                                                    ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 30000)");
                                                    wait = new WebDriverWait(chromeDriver, TimeSpan.FromSeconds(20));

                                                    try
                                                    {
                                                        IWebElement SearchResult = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector("table[class='production-selling-table']")));
                                                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 0)");
                                                        //Console.WriteLine("엘리먼트확인 " + RowH);
                                                    }
                                                    catch
                                                    {
                                                        //Console.WriteLine("상품선택후문의가능 " + RowH);
                                                        try
                                                        {
                                                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 0)");
                                                        }
                                                        catch { }
                                                    }

                                                    // 상세페이지에서 판매자정보 가져와 기존 작업내역이 있는지 비교하여 제외여부 확인 =================================
                                                    // 요소에서 스토어명 가져오기
                                                    Source = chromeDriver.PageSource;
                                                    db_SellerName = "";
                                                    try
                                                    {
                                                        db_SellerName = SSplit(SSplit(SSplit(Source, @"<th>상호</th>", 1), @"<td>", 1), @"</td>", 0);
                                                    }
                                                    catch
                                                    {
                                                        db_SellerName = SSplit(SSplit(Source, @"<span class=""production-item__header__brand"">", 1), @"</span>", 0);
                                                        db_SellerName = db_SellerName.Replace(" ", "");
                                                    }
                                                    // 스토어명을 기존 작업 리스트와 비교
                                                    for (int xxj = 0; xxj < Recent_Store_dno; xxj++)
                                                    {
                                                        if (Recent_StoreList[xxj] == db_SellerName)
                                                        {
                                                            Console.WriteLine(RowH + "r  7일내 홍보됨 : " + db_SellerName);
                                                            Recent_Store_YN = true;
                                                                
                                                            //chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles.Last());
                                                            //wait = new WebDriverWait(chromeDriver, new TimeSpan(0, 0, 5));
                                                            //((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 20000)");
                                                            //_Delay_Pause(2000);

                                                            //_Delay_Pause(8000);
                                                            break;
                                                        }
                                                    }
                                                    // 상세페이지에서 판매자정보 가져와 기존 작업내역이 있는지 비교하여 제외여부 확인 =================================

                                                    if (Recent_Store_YN == false)   // 7일 이내 해당스토어 작업 없음
                                                    {
                                                        _Delay_Pause(10);
                                                        if (Process_Stop == true) { goto End_Loop; }

                                                        if (chromeDriver.PageSource.IndexOf("문의하기") > -1)
                                                        {
                                                            db_Rank = RowH; // 작업정보 가져오기 -------------------------------------
                                                            IList<IWebElement> he11 = chromeDriver.FindElements(By.TagName("button"));
                                                            Delay(100);
                                                            foreach (IWebElement helem11 in he11)
                                                            {
                                                                if (helem11.GetAttribute("innerText").IndexOf("문의") > -1)
                                                                {
                                                                    //Console.WriteLine("문의하기확인 " + RowH);
                                                                    // 작업정보 가져오기 -------------------------------------
                                                                    Source = chromeDriver.PageSource;
                                                                    Source = Source.Replace("&quot;", @"""");
                                                                    Source = Source.Replace("&amp;", @"&");
                                                                    string T_Source = SSplit(Source, @"""loginUser"":", 1);
                                                                    db_StoreName = "";
                                                                    try
                                                                    {
                                                                        db_StoreName = SSplit(SSplit(SSplit(Source, @"<a class=""production-selling-header__title__brand""", 1), @">", 1), @"</a", 0);
                                                                    }
                                                                    catch
                                                                    {
                                                                    }
                                                                    //db_SellerName = "";
                                                                    //try
                                                                    //{
                                                                    //    db_SellerName = SSplit(SSplit(SSplit(Source, @"<th>상호</th>", 1), @"<td>", 1), @"</td>", 0);
                                                                    //}
                                                                    //catch
                                                                    //{
                                                                    //    db_SellerName = SSplit(SSplit(Source, @"<span class=""production-item__header__brand"">", 1), @"</span>", 0);
                                                                    //    db_SellerName = db_SellerName.Replace(" ", "");
                                                                    //}
                                                                    string t_Cate = SSplit(SSplit(T_Source, @"<ol class=""commerce-", 1), @"</ol>", 0);
                                                                    db_ProductCate = "";

                                                                    for (int xl = 0; xl < UBound_int(t_Cate, @"<li"); xl++)
                                                                    {
                                                                        string t_CateBound = SSplit(t_Cate, @"<li", xl + 1);
                                                                        if (xl == UBound_int(t_Cate, @"<li") - 2)
                                                                        {
                                                                            db_ProductCate += SSplit(SSplit(SSplit(t_CateBound, @"<a", 1), @">", 1), @"</a", 0);
                                                                            db_ProductID = SSplit(SSplit(SSplit(t_CateBound, @"<a", 1), @"affect_id=", 1), @"""", 0);
                                                                        }
                                                                        else
                                                                        {
                                                                            db_ProductCate += SSplit(SSplit(SSplit(t_CateBound, @"<a", 1), @">", 1), @"</a", 0) + " > ";
                                                                        }
                                                                        if (xl == UBound_int(t_Cate, @"<li") - 2) { break; }
                                                                        //MessageBox.Show(SSplit(t_Cate, @"<li", xl));
                                                                    }
                                                                    try
                                                                    {
                                                                        db_ProductImage = SSplit(SSplit(Source, @"""og:image"" content=""", 2), @"?", 0);
                                                                    }
                                                                    catch
                                                                    {
                                                                        db_ProductImage = SSplit(SSplit(Source, @"""og:image"" content=""", 1), @"?", 0);
                                                                    }
                                                                    db_RepresentativeName = SSplit(SSplit(SSplit(Source, @"대표자</th>", 1), @"<td>", 1), @"</td", 0);
                                                                    db_CallNumber = SSplit(SSplit(SSplit(Source, @"고객센터 전화번호</th>", 1), @"<td>", 1), @"</td", 0);
                                                                    //db_Address = SSplit(SSplit(SSplit(Source, @"보내실 곳", 1), @"<td>", 1), @"</td>", 0);
                                                                    db_Address = SSplit(SSplit(SSplit(Source, @"사업장소재지", 1), @"<td>", 1), @"</td>", 0);
                                                                    db_Email = SSplit(SSplit(SSplit(Source, @"E-mail", 1), @"<td>", 1), @"</td>", 0);
                                                                    // 작업정보 가져오기 -------------------------------------
                                                                    //Console.WriteLine("판매자정보확인 " + RowH);


                                                                    _Prod_Detail = true;
                                                                    int uxx1 = helem11.Location.X;
                                                                    WebDoc_Window_ScrollTo(chromeDriver, uxx1, helem11.Location.Y - 200);
                                                                    wait = new WebDriverWait(chromeDriver, new TimeSpan(0, 0, 5));
                                                                    Delay(100);

                                                                    helem11.Click();
                                                                    //richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "   → 오늘의집 " + RowH + "r  문의하기 클릭완료" + "\n" + richTextBox1.Text;
                                                                    //Console.WriteLine("문의하기클릭 " + RowH);

                                                                    chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles.Last());
                                                                    wait = new WebDriverWait(chromeDriver, new TimeSpan(0, 0, 5));
                                                                    ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 20000)");
                                                                    Delay(2000);

                                                                    if (chromeDriver.PageSource.IndexOf("문의유형") > -1)
                                                                    {
                                                                        //Console.WriteLine("문의유형확인 " + RowH);

                                                                        // 기타
                                                                        var queryButton = chromeDriver.FindElement(By.XPath("/html/body/div[6]/div/div/div/form/div[4]/div[6]"));
                                                                        queryButton.Click();
                                                                        Delay(500);

                                                                        try
                                                                        {
                                                                            // 선택 안함
                                                                            queryButton = chromeDriver.FindElement(By.XPath("/html/body/div[6]/div/div/div/form/div[6]/div[2]/label"));
                                                                            queryButton.Click();
                                                                            Delay(500);
                                                                        }
                                                                        catch { }

                                                                        // 홍보 내역 입력
                                                                        queryButton = chromeDriver.FindElement(By.XPath("/html/body/div[6]/div/div/div/form/textarea"));
                                                                        queryButton.Click();
                                                                        Delay(500);
                                                                        queryButton.SendKeys(txt_Promotion_Post.Text);
                                                                        Delay(1000);

                                                                        // 비밀글로 문의하기
                                                                        try
                                                                        {
                                                                            queryButton = chromeDriver.FindElement(By.XPath("/html/body/div[6]/div/div/div/form/div[8]/label"));
                                                                        }
                                                                        catch
                                                                        {
                                                                            queryButton = chromeDriver.FindElement(By.XPath("/html/body/div[6]/div/div/div/form/div[6]/label"));
                                                                        }
                                                                        queryButton.Click();
                                                                        Delay(500);

                                                                        // 크게 늘어난 textarea 창을 알맞게 줄여 줌
                                                                        queryButton = chromeDriver.FindElement(By.XPath("/html/body/div[6]/div/div/div/form/textarea"));
                                                                        queryButton.Clear();
                                                                        Delay(1000);

                                                                        _Delay_Pause(10);
                                                                        if (Process_Stop == true) { goto End_Loop; }



                                                                        //// 완료 버튼 클릭
                                                                        //queryButton = chromeDriver.FindElement(By.XPath("/html/body/div[6]/div/div/div/form/div[10]/button"));
                                                                        //queryButton.Click();
                                                                        //_Delay_Pause(500);


                                                                        // 작업 스토어 정보 변수에 저장
                                                                        Recent_StoreList[Recent_Store_dno] = db_SellerName;
                                                                        Recent_Store_dno += 1;
                                                                        // 작업내용 저장 -----------------------------------------
                                                                        try { ADONet_Conn.Close(); }
                                                                        catch { }
                                                                        try
                                                                        {
                                                                            string inSQL = "INSERT PROMOTION_WorkData ";
                                                                            inSQL += "( PACT_UserID, PACT_Market, PACT_Search_Keyword, PACT_Search_Rank, PACT_StoreName, PACT_ProductID, PACT_ProductLink, PACT_ProductCate, PACT_ProductName, PACT_InputTime, PACT_ProductImage, PACT_SellerName, PACT_CallNumber, PACT_Address, PACT_RepresentativeName, PACT_Email ";
                                                                            inSQL += " ) VALUES ( ";
                                                                            inSQL += " '" + p_UserID + "', ";
                                                                            inSQL += " '" + db_Market + "', ";
                                                                            inSQL += " '" + db_SKeyword + "', ";
                                                                            inSQL += "  " + db_Rank + " , ";
                                                                            inSQL += " '" + db_StoreName + "', ";
                                                                            inSQL += " '" + db_ProductID + "', ";
                                                                            inSQL += " 'https://ohou.se/productions/" + db_ProductID + "/selling', ";
                                                                            inSQL += " '" + db_ProductCate + "', ";
                                                                            inSQL += " '" + db_ProductName + "', ";
                                                                            inSQL += " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', ";
                                                                            inSQL += " '" + db_ProductImage + "', ";
                                                                            inSQL += " '" + db_SellerName + "', ";
                                                                            inSQL += " '" + db_CallNumber + "', ";
                                                                            inSQL += " '" + db_Address + "', ";
                                                                            inSQL += " '" + db_RepresentativeName + "', ";
                                                                            inSQL += " '" + db_Email + "'  ";
                                                                            inSQL += "  ) ";
                                                                            ADONet_Conn.Open();
                                                                            SqlCommand cmd0_1 = new SqlCommand();
                                                                            cmd0_1.Connection = ADONet_Conn;
                                                                            cmd0_1.CommandText = inSQL;
                                                                            cmd0_1.ExecuteNonQuery();
                                                                            ADONet_Conn.Close();

                                                                            txt_Work_Count.Text = (Convert.ToInt32(ImportNumbersOnly(txt_Work_Count.Text)) + 1).ToString();
                                                                        }
                                                                        catch
                                                                        {
                                                                            try { ADONet_Conn.Close(); }
                                                                            catch { }
                                                                            string inSQL = "INSERT PROMOTION_WorkData ";
                                                                            inSQL += "( PACT_UserID, PACT_Market, PACT_Search_Keyword, PACT_Search_Rank, PACT_StoreName, PACT_ProductID, PACT_ProductLink, PACT_ProductCate, PACT_ProductName, PACT_InputTime, PACT_ProductImage, PACT_SellerName, PACT_CallNumber, PACT_Address, PACT_RepresentativeName, PACT_Email ";
                                                                            inSQL += " ) VALUES ( ";
                                                                            inSQL += " '" + p_UserID + "', ";
                                                                            inSQL += " '" + db_Market + "', ";
                                                                            inSQL += " '" + db_SKeyword + "', ";
                                                                            inSQL += "  " + db_Rank + " , ";
                                                                            inSQL += " '" + db_StoreName + "', ";
                                                                            inSQL += " '" + db_ProductID + "', ";
                                                                            inSQL += " 'https://ohou.se/productions/" + db_ProductID + "/selling', ";
                                                                            inSQL += " '" + db_ProductCate + "', ";
                                                                            inSQL += " '" + db_ProductName + "', ";
                                                                            inSQL += " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', ";
                                                                            inSQL += " '" + db_ProductImage + "', ";
                                                                            inSQL += " '" + db_SellerName + "', ";
                                                                            inSQL += " '" + db_CallNumber + "', ";
                                                                            inSQL += " '" + db_Address + "', ";
                                                                            inSQL += " '" + db_RepresentativeName + "', ";
                                                                            inSQL += " '" + db_Email + "'  ";
                                                                            inSQL += "  ) ";
                                                                            ADONet_Conn.Open();
                                                                            SqlCommand cmd0_1 = new SqlCommand();
                                                                            cmd0_1.Connection = ADONet_Conn;
                                                                            cmd0_1.CommandText = inSQL;
                                                                            cmd0_1.ExecuteNonQuery();
                                                                            ADONet_Conn.Close();
                                                                        }
                                                                        //Console.WriteLine("작업내용저장 " + RowH);

                                                                        try
                                                                        {
                                                                            richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "     → " + RowH + "r  문의완료 : " + db_SellerName + "_" + db_ProductName.Substring(0, 6) + "\n" + richTextBox1.Text;
                                                                        }
                                                                        catch
                                                                        {
                                                                            richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "     → " + RowH + "r  문의완료 : " + db_SellerName + "_" + db_ProductName + "\n" + richTextBox1.Text;
                                                                        }
                                                                        //Console.WriteLine(db_SellerName + "_" + db_ProductName);

                                                                        // 문의하기 완료 처리가 안되면 '나가기' 처리
                                                                        if (chromeDriver.PageSource.IndexOf("나가기") > -1 && chromeDriver.PageSource.IndexOf("취소") > -1)
                                                                        {
                                                                            // 나가기 버튼 클릭
                                                                            try
                                                                            {
                                                                                queryButton = chromeDriver.FindElement(By.XPath("/html/body/div[2]/div/div/div[2]/div/button[2]"));
                                                                                queryButton.Click();
                                                                            }
                                                                            catch { }

                                                                            AutoClosingMessageBox.Show("문의하기 에서 빠져 나오는 중", "메세지박스 자동종료창", 2000);
                                                                        }



                                                                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("location.reload()");
                                                                        Delay(3000);

                                                                        break;      // 상세페이지 루프 종료
                                                                    }
                                                                }
                                                            }

                                                        }
                                                        else
                                                        {       // 문의하기 없어 빠져나옴

                                                            _Prod_Detail = true;
                                                        }

                                                        if (RowH % 10 == 0)
                                                        {
                                                            try
                                                            {
                                                                string upSQL = "UPDATE PROMOTION_UserRecord   ";
                                                                upSQL += " SET UserLastAct = '" + clib_Channel.SelectedItem.ToString() + "^" + txt_WorkKeyword.Text + "^" + txt_CheckingRank.Text + "'  ";
                                                                upSQL += " WHERE UserID = '" + p_UserID + "' ";
                                                                ADONet_Conn.Open();
                                                                SqlCommand cmd0_1 = new SqlCommand();
                                                                cmd0_1.Connection = ADONet_Conn;
                                                                cmd0_1.CommandText = upSQL;
                                                                cmd0_1.ExecuteNonQuery();
                                                                ADONet_Conn.Close();

                                                                txt_UserLastAct.Text = clib_Channel.SelectedItem.ToString() + "^" + txt_WorkKeyword.Text + "^" + txt_CheckingRank.Text;
                                                            }
                                                            catch { }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        // 작업내역이 있어 빠져나옴
                                                        _Delay_Pause(10);
                                                        if (Process_Stop == true) { goto End_Loop; }

                                                        if (chromeDriver.PageSource.IndexOf("문의하기") > -1)
                                                        {
                                                            db_Rank = RowH; // 작업정보 가져오기 -------------------------------------
                                                            IList<IWebElement> he11 = chromeDriver.FindElements(By.TagName("button"));
                                                            Delay(100);
                                                            foreach (IWebElement helem11 in he11)
                                                            {
                                                                if (helem11.GetAttribute("innerText").IndexOf("문의") > -1)
                                                                {
                                                                    //Console.WriteLine("문의하기확인 " + RowH);
                                                                    // 작업정보 가져오기 -------------------------------------
                                                                    Source = chromeDriver.PageSource;
                                                                    Source = Source.Replace("&quot;", @"""");
                                                                    Source = Source.Replace("&amp;", @"&");
                                                                    string T_Source = SSplit(Source, @"""loginUser"":", 1);
                                                                    db_StoreName = "";
                                                                    try
                                                                    {
                                                                        db_StoreName = SSplit(SSplit(SSplit(Source, @"<a class=""production-selling-header__title__brand""", 1), @">", 1), @"</a", 0);
                                                                    }
                                                                    catch
                                                                    {
                                                                    }
                                                                    string t_Cate = SSplit(SSplit(T_Source, @"<ol class=""commerce-", 1), @"</ol>", 0);
                                                                    db_ProductCate = "";

                                                                    for (int xl = 0; xl < UBound_int(t_Cate, @"<li"); xl++)
                                                                    {
                                                                        string t_CateBound = SSplit(t_Cate, @"<li", xl + 1);
                                                                        if (xl == UBound_int(t_Cate, @"<li") - 2)
                                                                        {
                                                                            db_ProductCate += SSplit(SSplit(SSplit(t_CateBound, @"<a", 1), @">", 1), @"</a", 0);
                                                                            db_ProductID = SSplit(SSplit(SSplit(t_CateBound, @"<a", 1), @"affect_id=", 1), @"""", 0);
                                                                        }
                                                                        else
                                                                        {
                                                                            db_ProductCate += SSplit(SSplit(SSplit(t_CateBound, @"<a", 1), @">", 1), @"</a", 0) + " > ";
                                                                        }
                                                                        if (xl == UBound_int(t_Cate, @"<li") - 2) { break; }
                                                                        //MessageBox.Show(SSplit(t_Cate, @"<li", xl));
                                                                    }
                                                                    try
                                                                    {
                                                                        db_ProductImage = SSplit(SSplit(Source, @"""og:image"" content=""", 2), @"?", 0);
                                                                    }
                                                                    catch
                                                                    {
                                                                        db_ProductImage = SSplit(SSplit(Source, @"""og:image"" content=""", 1), @"?", 0);
                                                                    }
                                                                    db_CallNumber = SSplit(SSplit(SSplit(Source, @"고객센터 전화번호</th>", 1), @"<td>", 1), @"</td", 0);
                                                                    db_Address = SSplit(SSplit(SSplit(Source, @"보내실 곳", 1), @"<td>", 1), @"</td>", 0);
                                                                    // 작업정보 가져오기 -------------------------------------
                                                                    //Console.WriteLine("판매자정보확인 " + RowH);


                                                                    _Prod_Detail = true;
                                                                    int uxx1 = helem11.Location.X;
                                                                    WebDoc_Window_ScrollTo(chromeDriver, uxx1, helem11.Location.Y - 200);
                                                                    wait = new WebDriverWait(chromeDriver, new TimeSpan(0, 0, 5));
                                                                    Delay(100);

                                                                    helem11.Click();
                                                                    //richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "   → 오늘의집 " + RowH + "r  문의하기 클릭완료" + "\n" + richTextBox1.Text;
                                                                    //Console.WriteLine("문의하기클릭 " + RowH);

                                                                    chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles.Last());
                                                                    wait = new WebDriverWait(chromeDriver, new TimeSpan(0, 0, 5));
                                                                    ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 20000)");
                                                                    Delay(2000);

                                                                    if (chromeDriver.PageSource.IndexOf("문의유형") > -1)
                                                                    {
                                                                        //Console.WriteLine("문의유형확인 " + RowH);

                                                                        // 기타
                                                                        var queryButton = chromeDriver.FindElement(By.XPath("/html/body/div[6]/div/div/div/form/div[4]/div[6]"));
                                                                        queryButton.Click();
                                                                        Delay(500);

                                                                        try
                                                                        {
                                                                            // 선택 안함
                                                                            queryButton = chromeDriver.FindElement(By.XPath("/html/body/div[6]/div/div/div/form/div[6]/div[2]/label"));
                                                                            queryButton.Click();
                                                                            Delay(500);
                                                                        }
                                                                        catch { }

                                                                        // 홍보 내역 입력
                                                                        queryButton = chromeDriver.FindElement(By.XPath("/html/body/div[6]/div/div/div/form/textarea"));
                                                                        queryButton.Click();
                                                                        Delay(500);
                                                                        queryButton.SendKeys(RowH + "r  7일내 홍보됨 : " + db_SellerName + "\n" + RowH + "r  7일내 홍보됨 : " + db_SellerName + "\n" + RowH + "r  7일내 홍보됨 : " + db_SellerName + "\n" + RowH + "r  7일내 홍보됨 : " + db_SellerName + "\n" + RowH + "r  7일내 홍보됨 : " + db_SellerName);
                                                                        Delay(1000);

                                                                        // 비밀글로 문의하기
                                                                        try
                                                                        {
                                                                            queryButton = chromeDriver.FindElement(By.XPath("/html/body/div[6]/div/div/div/form/div[8]/label"));
                                                                        }
                                                                        catch
                                                                        {
                                                                            queryButton = chromeDriver.FindElement(By.XPath("/html/body/div[6]/div/div/div/form/div[6]/label"));
                                                                        }
                                                                        queryButton.Click();
                                                                        Delay(500);

                                                                        // 크게 늘어난 textarea 창을 알맞게 줄여 줌
                                                                        queryButton = chromeDriver.FindElement(By.XPath("/html/body/div[6]/div/div/div/form/textarea"));
                                                                        queryButton.Clear();
                                                                        Delay(1000);

                                                                        _Delay_Pause(10);
                                                                        if (Process_Stop == true) { goto End_Loop; }

                                                                        richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "     →      " + RowH + "r  7일내 홍보됨 : " + db_SellerName + "\n" + richTextBox1.Text;


                                                                        // 문의하기 완료 처리가 안되면 '나가기' 처리
                                                                        if (chromeDriver.PageSource.IndexOf("나가기") > -1 && chromeDriver.PageSource.IndexOf("취소") > -1)
                                                                        {
                                                                            // 나가기 버튼 클릭
                                                                            try
                                                                            {
                                                                                queryButton = chromeDriver.FindElement(By.XPath("/html/body/div[2]/div/div/div[2]/div/button[2]"));
                                                                                queryButton.Click();
                                                                            }
                                                                            catch { }

                                                                            AutoClosingMessageBox.Show("문의하기 에서 빠져 나오는 중", "메세지박스 자동종료창", 2000);
                                                                        }


                                                                        break;      // 상세페이지 루프 종료
                                                                    }
                                                                }
                                                            }

                                                        }
                                                        else
                                                        {       // 문의하기 없어 빠져나옴

                                                            _Prod_Detail = true;
                                                        }
                                                    }

                                                    // 상세페이지에서 빠져나오기
                                                    chromeDriver.Navigate().Back();
                                                    Delay(500);
                                                    try
                                                    {
                                                        chromeDriver.Navigate().Refresh();
                                                    }
                                                    catch
                                                    {
                                                        try
                                                        {
                                                            chromeDriver.Navigate().GoToUrl(chromeDriver.Url);
                                                        }
                                                        catch
                                                        {
                                                            try
                                                            {
                                                                ((IJavaScriptExecutor)chromeDriver).ExecuteScript("location.reload()");
                                                            }
                                                            catch
                                                            {
                                                                Console.WriteLine("F5 입력하는 방법");
                                                            }
                                                        }
                                                    }

                                                    //Console.WriteLine("상세에서리스트로이동 " + RowH);
                                                    Delay(3500);
                                                }
                                                break;      // 리스트 페이지로 빠져 나가기
                                            }
                                            #endregion
                                        //}
                                        //Console.WriteLine("상세작업완료 " + RowH);
                                        //break;
                                        //}

                                    }
                                }
                            }
                            if (StartPOS == false)
                            {
                                //Console.WriteLine("상품ID 체크 : " + SelectID.Replace("product-", "") + "__" + LastID.Replace("product-", ""));
                                if (SelectID == LastID)
                                {
                                    //if (RowH >= Convert.ToInt32(txt_Rank_Start.Text))
                                    //{
                                    //}
                                    //else
                                    //{
                                    //    LastPOS = false;
                                    //}
                                    LastPOS = true;

                                    if (Scroll_End >= 1)
                                    {
                                        if (RowH >= Convert.ToInt32(txt_Rank_End.Text) || RowH >= Convert.ToInt32(txt_TTLProdCount.Text))
                                        {
                                            Scroll_End = 0;
                                            Key_WorkState = true;
                                            goto End_Loop;
                                        }
                                    }
                                    else
                                    {
                                        Scroll_End += 1;
                                    }
                                }
                                else
                                {
                                    LastPOS = false;
                                }
                            }
                            else
                            {
                                LastPOS = false;
                            }





                            //if (_Prod_Detail == true)
                            //{
                            //    if (RowH > Convert.ToInt32(txt_Rank_Start.Text))
                            //    {
                            //        ScrollLoop += 1;
                            //        if (ScrollLoop >= 6)
                            //        {
                            //            _Delay_Pause(100);
                            //            //chromeDriver.Navigate().Refresh();
                            //            //_Delay_Pause(1000);
                            //        }
                            //        else
                            //        {
                            //            _Delay_Pause(100);
                            //        }
                            //    }
                            //    break;
                            //}
                        }
                        if (_Prod_Detail == true)
                        {
                            //if (RowH > Convert.ToInt32(txt_Rank_Start.Text))
                            //{
                            //    ScrollLoop += 1;
                            //    if (ScrollLoop >= 6)
                            //    {
                            //        _Delay_Pause(100);
                            //        //chromeDriver.Navigate().Refresh();
                            //        //_Delay_Pause(1000);
                            //    }
                            //    else
                            //    {
                            //        _Delay_Pause(100);
                            //    }
                            //}
                            break;
                        }
                    }
                    //if (RowH >= Convert.ToInt32(txt_Rank_End.Text) || ColH >= 5)
                    //{
                    //    Key_WorkState = true;
                    //    ColH = 1;
                    //}
                    //else
                    //{
                    //    ColH += 1;
                    //}

                    // 리스트의 div는 1개이므로 루틴이 끝나면 종료
                    // 스크롤 리플래쉬 해줘야 오류 미발생
                    StartPOS = false;
                    //chromeDriver.Navigate().Refresh();
                    Delay(1000);
                }
                chromeDriver.Navigate().Back();
                Console.WriteLine("구글페이지로이동 " + RowH);
            }

        End_Loop:
            Console.WriteLine("작업을 종료합니다.");
        }

        public void Selenium_카카오쇼핑(string xUrl)
        {
            this.Selenium_Paging(xUrl);

            WebDriverWait wait = null;
            if (Process_Stop == true) { goto End_Loop; }
            if (chromeDriver.PageSource.IndexOf("로그인") > -1)
            {
                Key_WorkState = true;
                chromeDriver.Navigate().GoToUrl("https://store.kakao.com/");
                wait = new WebDriverWait(chromeDriver, new TimeSpan(0, 0, 5));
                Delay(1000);
                _Delay_Pause(10);
                if (Process_Stop == true) { goto End_Loop; }

                AutoClosingMessageBox.Show("회원가입 후 프로그램을 다시 실행하세요!", "Auto PR  [메세지 자동종료]", 3000);
                richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "     ▶ 카카오쇼핑 로그인안됨" + "\n" + richTextBox1.Text;
                return;
            }
            else if (chromeDriver.PageSource.IndexOf("찾으시는 결과가 없네요.") > -1)
            {
                Key_WorkState = true;
                richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "     → 상품없음" + "\n" + richTextBox1.Text;
                goto End_Loop;
            }

            if (chromeDriver.Url.IndexOf("store.kakao.com") == -1)
            {
                richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "   [카카오쇼핑 로그인안됨]" + "\n" + richTextBox1.Text;
                Source = "카카오쇼핑 PASS";
            }
            else
            {
                int Page_No = 0;
                int SelectNum = 0;
                int CheckNum = 0;
                int Scroll_End = 0;
                int TTL_RowH = 0;
                
                string LastID = "";
                bool StartPOS = true;
                bool Page_End = false;

                int Work_Page = 0;
                if (Convert.ToInt32(txt_Rank_Start.Text) < Convert.ToInt32(txt_WorkNum.Text))
                {
                    Work_Page = 1 + Convert.ToInt32(txt_WorkNum.Text) / 100;
                }
                else
                {
                    Work_Page = 1 + Convert.ToInt32(txt_Rank_Start.Text) / 100;
                }
                #region  // 시작페이지를 100개 단위로 페이지 이동 영역 =============
                if (Work_Page < 6)
                {
                    if (Work_Page == 1)
                    {
                        // PASS
                    }
                    else if (Work_Page % 5 == 2)
                    {
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 50000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='mArticle']/div/cu-pagination-list/div/div[2]/div/button[1]"));
                        pageButton.Click();
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = "101";
                        }
                    }
                    else if (Work_Page % 5 == 3)
                    {
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 50000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='mArticle']/div/cu-pagination-list/div/div[2]/div/button[2]"));
                        pageButton.Click();
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = "201";
                        }
                    }
                    else if (Work_Page % 5 == 4)
                    {
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 50000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='mArticle']/div/cu-pagination-list/div/div[2]/div/button[3]"));
                        pageButton.Click();
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = "301";
                        }
                    }
                    else if (Work_Page % 5 == 0)
                    {
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 50000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='mArticle']/div/cu-pagination-list/div/div[2]/div/button[4]"));
                        pageButton.Click();
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = "401";
                        }
                    }
                }
                else
                {
                    // 5페이지 이상은 페이지 더보기 클릭
                    for (int xyk = 0; xyk < (Work_Page / 5); xyk++)
                    {
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 30000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='mArticle']/div/cu-pagination-list/div/div[2]/button[2]"));
                        pageButton.Click();

                        chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles.Last());
                        Delay(500);
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 30000)");
                        Delay(4000);
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = (Work_Page * 100).ToString() + "1";
                        }
                    }

                    // 추가페이지 필요
                    if (Work_Page % 5 == 2)
                    {
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 50000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='mArticle']/div/cu-pagination-list/div/div[2]/div/button[1]"));
                        pageButton.Click();
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = (Work_Page * 100).ToString() + "1";
                        }
                    }
                    else if (Work_Page % 5 == 3)
                    {
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 50000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='mArticle']/div/cu-pagination-list/div/div[2]/div/button[2]"));
                        pageButton.Click();
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = (Work_Page * 100).ToString() + "1";
                        }
                    }
                    else if (Work_Page % 5 == 4)
                    {
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 50000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='mArticle']/div/cu-pagination-list/div/div[2]/div/button[3]"));
                        pageButton.Click();
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = (Work_Page * 100).ToString() + "1";
                        }
                    }
                    else if (Work_Page % 5 == 0)
                    {
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 50000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='mArticle']/div/cu-pagination-list/div/div[2]/div/button[4]"));
                        pageButton.Click();
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = (Work_Page * 100).ToString() + "1";
                        }
                    }
                }

                chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles.Last());
                Delay(500);
                ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 30000)");
                Delay(3000);
                #endregion
                for (int xyi = Work_Page; xyi <= 50; xyi++)
                {
                    int RowH = 0;
                    Page_No = xyi;
                    Page_End = false;
                    SelectNum = 0;

                    for (int xyj = 1; xyj <= 100; xyj++)
                    {
                        //TTL_RowH = ((xyi - 1) * 100) + xyj;
                        #region  // 리스트 100개 작업 영역 ======================================================
                        bool _Prod_Detail = false;
                        bool LastPOS = false;
                        int Re_RowH = 1;

                        _Delay_Pause(10);
                        if (Process_Stop == true) { goto End_Loop; }
                        wait = new WebDriverWait(chromeDriver, TimeSpan.FromSeconds(20));
                        IWebElement _SearchResult = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector("span[data-tiara-ordnum='100']")));

                        IList<IWebElement> he01 = chromeDriver.FindElements(By.TagName("ul"));
                        // 새로 열려는데 엘리먼트가 없으면 리플래시 를 다시 하는 것 검토
                        foreach (IWebElement helem1 in he01)
                        {
                            if (helem1.GetAttribute("class") != null)
                            {
                                if (helem1.GetAttribute("class") == "list_productcmp")
                                {
                                    Scroll_End = 0;
                                    RowH = 0;
                                    IList<IWebElement> he02 = helem1.FindElements(By.TagName("li"));
                                    // 새로 열려는데 엘리먼트가 없으면 리플래시 를 다시 하는 것 검토
                                    foreach (IWebElement helem2 in he02)
                                    {
                                        RowH += 1;
                                        TTL_RowH = ((xyi - 1) * 100) + RowH;

                                        if (SelectNum == 0)       // if (xyj == 1)
                                        {
                                            _Delay_Pause(20);
                                            if (Process_Stop == true) { goto End_Loop; }

                                            txt_CheckingRank.Text = RowH.ToString();

                                            int uxx = helem2.Location.X;
                                            WebDoc_Window_ScrollTo(chromeDriver, uxx, helem2.Location.Y + Convert.ToInt32(txt_Scroll.Text));
                                            if (RowH % 2 == 0) { _Delay_Pause(Convert.ToInt32(cbx_ScrollSec.Text)); }
                                        }
                                        else
                                        {
                                            if (SelectNum <= RowH)
                                            {
                                                if (Process_Stop == true) { goto End_Loop; }

                                                txt_CheckingRank.Text = RowH.ToString();

                                                int uxx = helem2.Location.X;
                                                WebDoc_Window_ScrollTo(chromeDriver, uxx, helem2.Location.Y + Convert.ToInt32(txt_Scroll.Text));
                                                if (RowH % 2 == 0) { _Delay_Pause(Convert.ToInt32(cbx_ScrollSec.Text)); }
                                            }
                                        }

                                        if (Page_No == 1 && RowH < Convert.ToInt32(txt_Rank_Start.Text))
                                        {
                                            //Console.WriteLine(Page_No + " ^ " + RowH + " ^ " + SelectNum);
                                        }
                                        //else if (Page_No > 1 && (Page_No * 100 + RowH) < Convert.ToInt32(txt_Rank_Start.Text))
                                        //{
                                        //    //Console.WriteLine(Page_No + " ^ " + RowH + " ^ " + SelectNum);
                                        //}
                                        else if (SelectNum >= RowH)
                                        {
                                            //Console.WriteLine(Page_No + " ^ " + RowH + " ^ " + SelectNum);
                                        }
                                        else if (Convert.ToInt32(txt_WorkNum.Text) >= TTL_RowH)
                                        {
                                            //Console.WriteLine(Page_No + " ^ " + RowH + " ^ " + SelectNum);
                                        }
                                        else
                                        {
                                            if (RowH == 100)
                                            {
                                                Page_End = true; 
                                            }

                                            if (helem2.GetAttribute("innerText") != null)
                                            {
                                                _Prod_Detail = false;

                                                #region  // 판매자정보 일부가져오기 -----------------------------------------------
                                                bool sInfoPick = false;
                                                IList<IWebElement> he03 = helem2.FindElements(By.TagName("span"));
                                                _Delay_Pause(100);
                                                foreach (IWebElement helem3 in he03)
                                                {
                                                    if (helem3.GetAttribute("data-tiara-ordnum") != null)
                                                    {
                                                        if (helem3.GetAttribute("data-tiara-ordnum").IndexOf((RowH).ToString()) > -1)
                                                        {
                                                            if (sInfoPick == false)
                                                            {
                                                                //int uxx = helem3.Location.X;
                                                                //WebDoc_Window_ScrollTo(chromeDriver, uxx, helem3.Location.Y + Convert.ToInt32(txt_Scroll.Text) - 300);
                                                                //if (RowH % 2 == 0) { _Delay_Pause(Convert.ToInt32(cbx_ScrollSec.Text)); }

                                                                db_ProductID = helem3.GetAttribute("data-tiara-id");
                                                                LastID = db_ProductID;

                                                                IList<IWebElement> he04 = helem3.FindElements(By.TagName("a"));
                                                                foreach (IWebElement helem4 in he04)
                                                                {
                                                                    if (helem4.GetAttribute("href") != null)
                                                                    {
                                                                        db_ProductLink = helem4.GetAttribute("href");
                                                                        break;
                                                                    }
                                                                }
                                                                db_ProductImage = "";
                                                                db_ProductImage = helem3.GetAttribute("data-tiara-image");
                                                                db_ProductCate = helem3.GetAttribute("data-tiara-category");

                                                                sInfoPick = true;
                                                            }
                                                        }
                                                    }
                                                    if (helem3.GetAttribute("class").IndexOf("store_name") > -1)
                                                    {
                                                        db_StoreName = helem3.GetAttribute("innerText");
                                                        db_StoreName = db_StoreName.Replace("'", "");
                                                    }
                                                    if (helem3.GetAttribute("class").IndexOf("product_name") > -1)
                                                    {
                                                        db_ProductName = helem3.GetAttribute("innerText");
                                                        db_ProductName = db_ProductName.Replace("'", "");
                                                    }
                                                }
                                                #endregion

                                                _Delay_Pause(10);
                                                if (Process_Stop == true) { goto End_Loop; }

                                                if (Page_No == 1 && RowH < Convert.ToInt32(txt_Rank_Start.Text))
                                                {
                                                    Console.WriteLine(RowH + "r 시작 순번 이하");
                                                }
                                                else if (helem2.GetAttribute("innerText").IndexOf("연령 확인") > -1)
                                                {
                                                    Console.WriteLine(RowH + "r 성인인증 상품 작업제외");
                                                    richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "     →      " + TTL_RowH + "r  성인인증 상품 작업제외 : " + db_SellerName + "\n" + richTextBox1.Text;
                                                }
                                                else
                                                {
                                                    SelectNum = RowH;

                                                    #region  // 상품검색 시작 ====================================================
                                                    IList<IWebElement> he05 = helem2.FindElements(By.TagName("a"));
                                                    foreach (IWebElement helem5 in he05)
                                                    {
                                                        if (helem5.GetAttribute("href") != null)
                                                        {
                                                            helem5.Click();

                                                            if (Convert.ToInt32(txt_WorkNum.Text) < TTL_RowH)
                                                            {
                                                                txt_WorkNum.Text = TTL_RowH.ToString();
                                                            }
                                                            chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles.Last());
                                                            break;
                                                        }
                                                    }
                                                    _Prod_Detail = true;
                                                    #endregion

                                                    #region  // 상세페이지에서 판매자정보 가져와 기존 작업내역이 있는지 비교하여 제외여부 확인 =================================
                                                    Delay(2000);

                                                    wait = new WebDriverWait(chromeDriver, TimeSpan.FromSeconds(20));
                                                    _SearchResult = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector("span[class='txt_store']")));

                                                    // 스토어정보 창 열기
                                                    var queryButton = chromeDriver.FindElement(By.CssSelector("span[class='txt_store']"));
                                                    queryButton.Click();
                                                    Delay(3000);

                                                    _Delay_Pause(10);
                                                    if (Process_Stop == true) { goto End_Loop; }

                                                    if (chromeDriver.PageSource.IndexOf("<dt>상호명</dt>") == -1)
                                                    {
                                                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("location.reload()");
                                                        Delay(3000);

                                                        queryButton = chromeDriver.FindElement(By.CssSelector("span[class='txt_store']"));
                                                        queryButton.Click();
                                                        Delay(3000);
                                                    }

                                                    _Delay_Pause(10);
                                                    if (Process_Stop == true) { goto End_Loop; }

                                                    // 작업정보 가져오기 -------------------------------------
                                                    Source = chromeDriver.PageSource;
                                                    Source = Source.Replace("&quot;", @"""");
                                                    Source = Source.Replace("&amp;", @"&");
                                                    //string T_Source = SSplit(Source, @"""loginUser"":", 1);

                                                    db_SellerName = "";
                                                    db_SellerName = SSplit(SSplit(SSplit(Source, @"<dt>상호명</dt>", 1), @"<dd>", 1), @"</dd>", 0);
                                                    db_SellerName = db_SellerName.Replace("'", "");
                                                    db_RepresentativeName = SSplit(SSplit(SSplit(Source, @"<dt>대표자</dt>", 1), @"<dd>", 1), @"</dd>", 0);
                                                    db_CallNumber = SSplit(SSplit(SSplit(Source, @"<dt>대표전화</dt>", 1), @"<dd>", 1), @"</dd>", 0);
                                                    db_Address = SSplit(SSplit(SSplit(Source, @"<dt>사업장 소재지</dt>", 1), @"<dd>", 1), @"</dd>", 0);
                                                    db_Address = db_Address.Replace("'", "");
                                                    db_Email = SSplit(SSplit(SSplit(Source, @"<dt>대표메일</dt>", 1), @"<dd>", 1), @"</dd>", 0);
                                                    // 작업정보 가져오기 -------------------------------------

                                                    Recent_Store_YN = false;
                                                    // 스토어명을 기존 작업 리스트와 비교
                                                    for (int xxj = 0; xxj < Recent_Store_dno; xxj++)
                                                    {
                                                        if (Recent_StoreList[xxj] == db_SellerName)
                                                        {
                                                            Console.WriteLine(RowH + "r  7일내 홍보됨 : " + db_SellerName);
                                                            richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "     →      " + TTL_RowH + "r  7일내 홍보됨 : " + db_SellerName + "\n" + richTextBox1.Text;
                                                            Recent_Store_YN = true;

                                                            break;
                                                        }
                                                    }
                                                    chromeDriver.Navigate().Back();     // 판매자정보 화면 뒤로 가기
                                                    Delay(1500);
                                                    chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles.Last());
                                                    #endregion

                                                    #region  // // 7일 이내 해당스토어 작업 없음 -----------------------------------------------
                                                    if (Recent_Store_YN == false)
                                                    {
                                                        _Delay_Pause(10);
                                                        if (Process_Stop == true) { goto End_Loop; }

                                                        if (chromeDriver.PageSource.IndexOf("문의") == -1)
                                                        {
                                                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("location.reload()");
                                                            Delay(3000);
                                                        }

                                                        _Delay_Pause(10);
                                                        if (Process_Stop == true) { goto End_Loop; }

                                                        if (chromeDriver.PageSource.IndexOf("문의") > -1)
                                                        {
                                                            db_Rank = RowH; // 작업정보 가져오기 -------------------------------------
                                                            IList<IWebElement> he11 = chromeDriver.FindElements(By.TagName("li"));        // IList<IWebElement> he11 = chromeDriver.FindElements(By.TagName("span"));
                                                            foreach (IWebElement helem11 in he11)
                                                            {
                                                                if (helem11.GetAttribute("innerText").IndexOf("문의") > -1)
                                                                {
                                                                    _Prod_Detail = true;
                                                                    int uxx1 = helem11.Location.X;
                                                                    WebDoc_Window_ScrollTo(chromeDriver, uxx1, helem11.Location.Y - 200);
                                                                    wait = new WebDriverWait(chromeDriver, new TimeSpan(0, 0, 5));
                                                                    Delay(500);

                                                                    helem11.Click();

                                                                    chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles.Last());
                                                                    wait = new WebDriverWait(chromeDriver, new TimeSpan(0, 0, 5));
                                                                    Delay(2000);

                                                                    if (chromeDriver.PageSource.IndexOf("문의하기") > -1)
                                                                    {
                                                                        // 문의하기
                                                                        try
                                                                        {
                                                                            queryButton = chromeDriver.FindElement(By.CssSelector("a[class='link_comm link_write ng-star-inserted']"));
                                                                        }
                                                                        catch
                                                                        {
                                                                            queryButton = chromeDriver.FindElement(By.CssSelector("a[class='link_comm link_write']"));
                                                                        }
                                                                        queryButton.Click();
                                                                        Delay(3000);

                                                                        // 문의하기 팝업창 제어 ==============================================
                                                                        string parentHandle = chromeDriver.CurrentWindowHandle;                                 // 현재 창의 핸들을 저장합니다.
                                                                        IEnumerable<string> allHandles = chromeDriver.WindowHandles;                            // 모든 창의 핸들을 가져옵니다.
                                                                        string dialogHandle = allHandles.FirstOrDefault(handle => handle != parentHandle);      // 부모 창을 제외한 다른 창을 찾습니다.
                                                                        if (dialogHandle != null)
                                                                        {
                                                                            chromeDriver.SwitchTo().Window(dialogHandle);                                       // HTML Dialog 창으로 이동합니다.


                                                                            IWebElement confirmButton = chromeDriver.FindElement(By.ClassName("fold_opt"));      // 문의유형 클릭
                                                                            confirmButton.Click();
                                                                            Delay(200);

                                                                            var option = chromeDriver.FindElement(By.XPath("//*[@id='mArticle']/form/div[1]/select/option[7]"));
                                                                            option.Click();

                                                                            confirmButton.Click();
                                                                            Delay(500);
                                                                            confirmButton = chromeDriver.FindElement(By.Id("inpReview1"));      // 비밀글 클릭
                                                                            confirmButton.Click();
                                                                            Delay(200);

                                                                            confirmButton = chromeDriver.FindElement(By.TagName("textarea[title='문의내용']"));     // 홍보 내역 입력
                                                                            confirmButton.Click();
                                                                            Delay(500);
                                                                            confirmButton.SendKeys(txt_Promotion_Post.Text);
                                                                            Delay(1000);



                                                                            //// 등록 버튼 클릭
                                                                            //confirmButton = chromeDriver.FindElement(By.Id("saveBtn"));
                                                                            //confirmButton.Click();
                                                                            //_Delay_Pause(500);

                                                                            // 글 작성시에는 닫기 기능 미사용할 수 있음
                                                                            chromeDriver.SwitchTo().Window(dialogHandle).Close();


                                                                            chromeDriver.SwitchTo().Window(parentHandle);                                       // 부모 창으로 이동합니다.
                                                                        }
                                                                        // 문의하기 팝업창 제어 ==============================================  

                                                                        _Delay_Pause(10);
                                                                        if (Process_Stop == true) { goto End_Loop; }

                                                                        // 작업 스토어 정보 변수에 저장
                                                                        Recent_StoreList[Recent_Store_dno] = db_SellerName;
                                                                        Recent_Store_dno += 1;
                                                                        // 작업내용 저장 -----------------------------------------
                                                                        try { ADONet_Conn.Close(); }
                                                                        catch { }
                                                                        try
                                                                        {
                                                                            string inSQL = "INSERT PROMOTION_WorkData ";
                                                                            inSQL += "( PACT_UserID, PACT_Market, PACT_Search_Keyword, PACT_Search_Rank, PACT_StoreName, PACT_ProductID, PACT_ProductLink, PACT_ProductCate, PACT_ProductName, PACT_InputTime, PACT_ProductImage, PACT_SellerName, PACT_CallNumber, PACT_Address, PACT_RepresentativeName, PACT_Email ";
                                                                            inSQL += " ) VALUES ( ";
                                                                            inSQL += " '" + p_UserID + "', ";
                                                                            inSQL += " '" + db_Market + "', ";
                                                                            inSQL += " '" + db_SKeyword + "', ";
                                                                            inSQL += "  " + db_Rank + " , ";
                                                                            inSQL += " '" + db_StoreName + "', ";
                                                                            inSQL += " '" + db_ProductID + "', ";
                                                                            inSQL += " '" + db_ProductLink + "', ";
                                                                            inSQL += " '" + db_ProductCate + "', ";
                                                                            inSQL += " '" + db_ProductName + "', ";
                                                                            inSQL += " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', ";
                                                                            inSQL += " '" + db_ProductImage + "', ";
                                                                            inSQL += " '" + db_SellerName + "', ";
                                                                            inSQL += " '" + db_CallNumber + "', ";
                                                                            inSQL += " '" + db_Address + "', ";
                                                                            inSQL += " '" + db_RepresentativeName + "', ";
                                                                            inSQL += " '" + db_Email + "'  ";
                                                                            inSQL += "  ) ";
                                                                            ADONet_Conn.Open();
                                                                            SqlCommand cmd0_1 = new SqlCommand();
                                                                            cmd0_1.Connection = ADONet_Conn;
                                                                            cmd0_1.CommandText = inSQL;
                                                                            cmd0_1.ExecuteNonQuery();
                                                                            ADONet_Conn.Close();

                                                                            txt_Work_Count.Text = (Convert.ToInt32(ImportNumbersOnly(txt_Work_Count.Text)) + 1).ToString();
                                                                        }
                                                                        catch
                                                                        {
                                                                            try { ADONet_Conn.Close(); }
                                                                            catch { }
                                                                            string inSQL = "INSERT PROMOTION_WorkData ";
                                                                            inSQL += "( PACT_UserID, PACT_Market, PACT_Search_Keyword, PACT_Search_Rank, PACT_StoreName, PACT_ProductID, PACT_ProductLink, PACT_ProductCate, PACT_ProductName, PACT_InputTime, PACT_ProductImage, PACT_SellerName, PACT_CallNumber, PACT_Address, PACT_RepresentativeName, PACT_Email ";
                                                                            inSQL += " ) VALUES ( ";
                                                                            inSQL += " '" + p_UserID + "', ";
                                                                            inSQL += " '" + db_Market + "', ";
                                                                            inSQL += " '" + db_SKeyword + "', ";
                                                                            inSQL += "  " + db_Rank + " , ";
                                                                            inSQL += " '" + db_StoreName + "', ";
                                                                            inSQL += " '" + db_ProductID + "', ";
                                                                            inSQL += " '" + db_ProductLink + "', ";
                                                                            inSQL += " '" + db_ProductCate + "', ";
                                                                            inSQL += " '" + db_ProductName + "', ";
                                                                            inSQL += " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', ";
                                                                            inSQL += " '" + db_ProductImage + "', ";
                                                                            inSQL += " '" + db_SellerName + "', ";
                                                                            inSQL += " '" + db_CallNumber + "', ";
                                                                            inSQL += " '" + db_Address + "', ";
                                                                            inSQL += " '" + db_RepresentativeName + "', ";
                                                                            inSQL += " '" + db_Email + "'  ";
                                                                            inSQL += "  ) ";
                                                                            ADONet_Conn.Open();
                                                                            SqlCommand cmd0_1 = new SqlCommand();
                                                                            cmd0_1.Connection = ADONet_Conn;
                                                                            cmd0_1.CommandText = inSQL;
                                                                            cmd0_1.ExecuteNonQuery();
                                                                            ADONet_Conn.Close();
                                                                        }

                                                                        try
                                                                        {
                                                                            richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "     → " + TTL_RowH + "r  문의완료 : " + db_SellerName + "_" + db_ProductName.Substring(0, 6) + "\n" + richTextBox1.Text;
                                                                        }
                                                                        catch
                                                                        {
                                                                            richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "     → " + TTL_RowH + "r  문의완료 : " + db_SellerName + "_" + db_ProductName + "\n" + richTextBox1.Text;
                                                                        }

                                                                        break;      // 상세페이지 루프 종료
                                                                    }
                                                                }
                                                            }

                                                        }
                                                        else
                                                        {       // 문의하기 없어 빠져나옴

                                                            _Prod_Detail = true;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        // 작업내역이 있어 빠져나옴
                                                        Console.WriteLine("작업내역이 있어 빠져나옴");
                                                    }
                                                    #endregion

                                                    if (RowH % 10 == 0)
                                                    {
                                                        try
                                                        {
                                                            string upSQL = "UPDATE PROMOTION_UserRecord   ";
                                                            upSQL += " SET UserLastAct = '" + clib_Channel.SelectedItem.ToString() + "^" + txt_WorkKeyword.Text + "^" + TTL_RowH + "'  ";
                                                            upSQL += " WHERE UserID = '" + p_UserID + "' ";
                                                            ADONet_Conn.Open();
                                                            SqlCommand cmd0_1 = new SqlCommand();
                                                            cmd0_1.Connection = ADONet_Conn;
                                                            cmd0_1.CommandText = upSQL;
                                                            cmd0_1.ExecuteNonQuery();
                                                            ADONet_Conn.Close();

                                                            txt_UserLastAct.Text = clib_Channel.SelectedItem.ToString() + "^" + txt_WorkKeyword.Text + "^" + TTL_RowH;
                                                        }
                                                        catch { }
                                                    }

                                                    // 상세페이지에서 빠져나오기
                                                    chromeDriver.Navigate().Back();
                                                    Delay(500);
                                                    //chromeDriver.Navigate().Refresh();
                                                    chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles.Last());
                                                    Delay(500);

                                                }
                                            }
                                        }
                                        if (_Prod_Detail == true)
                                        {
                                            break;  // ul 빠져나가기
                                        }
                                    }
                                    Delay(1000);
                                    if (_Prod_Detail == true)
                                    {
                                        break;  // ul 빠져나가기
                                    }
                                }
                            }
                        }
                        #endregion

                        if (TTL_RowH > Convert.ToInt32(txt_Rank_End.Text))
                        {
                            break;
                        }
                        if (Page_End == true)
                        {
                            break;
                        }
                    }
                    #region  // 100개 단위로 페이지 이동 영역 =============
                    if (xyi % 5 == 0)
                    {
                        // 5페이지 이상은 페이지 더보기 클릭
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 30000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='mArticle']/div/cu-pagination-list/div/div[2]/button[2]"));
                        pageButton.Click();
                    }
                    else if (xyi % 5 == 1)
                    {
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 50000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='mArticle']/div/cu-pagination-list/div/div[2]/div/button[1]"));
                        pageButton.Click();
                    }
                    else if (xyi % 5 == 2)
                    {
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 50000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='mArticle']/div/cu-pagination-list/div/div[2]/div/button[2]"));
                        pageButton.Click();
                    }
                    else if (xyi % 5 == 3)
                    {
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 50000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='mArticle']/div/cu-pagination-list/div/div[2]/div/button[3]"));
                        pageButton.Click();
                    }
                    else if (xyi % 5 == 4)
                    {
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 50000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='mArticle']/div/cu-pagination-list/div/div[2]/div/button[4]"));
                        pageButton.Click();
                    }
                    chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles.Last());
                    Delay(500);
                    ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 30000)");
                    Delay(3000);
                    #endregion

                    if (TTL_RowH > Convert.ToInt32(txt_Rank_End.Text))
                    {
                        break;
                    }
                }
                chromeDriver.Navigate().Back();
                Console.WriteLine("구글페이지로이동 " + TTL_RowH);
            }

        End_Loop:
            Console.WriteLine("작업을 종료합니다.");
        }

        public void Selenium_쿠팡(string xUrl)
        {
            this.Selenium_Paging(xUrl);

            WebDriverWait wait = null;
            if (Process_Stop == true) { goto End_Loop; }
            if (chromeDriver.PageSource.IndexOf("로그인") > -1)
            {
                Key_WorkState = true;
                chromeDriver.Navigate().GoToUrl("https://www.coupang.com/");
                wait = new WebDriverWait(chromeDriver, new TimeSpan(0, 0, 5));
                Delay(1000);
                _Delay_Pause(10);
                if (Process_Stop == true) { goto End_Loop; }

                AutoClosingMessageBox.Show("회원가입 후 프로그램을 다시 실행하세요!", "Auto PR  [메세지 자동종료]", 3000);
                richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "     ▶ 쿠팡 로그인안됨" + "\n" + richTextBox1.Text;
                return;
            }
            else if (chromeDriver.PageSource.IndexOf("찾으시는 결과가 없네요.") > -1)
            {
                Key_WorkState = true;
                richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "     → 상품없음" + "\n" + richTextBox1.Text;
                goto End_Loop;
            }

            if (chromeDriver.Url.IndexOf("store.kakao.com") == -1)
            {
                richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "   [쿠팡 로그인안됨]" + "\n" + richTextBox1.Text;
                Source = "쿠팡 PASS";
            }
            else
            {
                int Page_No = 0;
                int SelectNum = 0;
                int CheckNum = 0;
                int Scroll_End = 0;
                int TTL_RowH = 0;

                string LastID = "";
                bool StartPOS = true;
                bool Page_End = false;

                int Work_Page = 0;
                if (Convert.ToInt32(txt_Rank_Start.Text) < Convert.ToInt32(txt_WorkNum.Text))
                {
                    Work_Page = 1 + Convert.ToInt32(txt_WorkNum.Text) / 60;
                }
                else
                {
                    Work_Page = 1 + Convert.ToInt32(txt_Rank_Start.Text) / 60;
                }
                #region  // 시작페이지를 60개 단위로 페이지 이동 영역 =============
                if (Work_Page < 11)
                {
                    if (Work_Page == 1)
                    {
                        // PASS
                    }
                    else if (Work_Page % 10 == 2)
                    {
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 50000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='product-list-paging']/div/a[3]"));
                        pageButton.Click();
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = "61";
                        }
                    }
                    else if (Work_Page % 10 == 3)
                    {
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 50000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='product-list-paging']/div/a[4]"));
                        pageButton.Click();
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = "121";
                        }
                    }
                    else if (Work_Page % 10 == 4)
                    {
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 50000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='product-list-paging']/div/a[5]"));
                        pageButton.Click();
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = "181";
                        }
                    }
                    else if (Work_Page % 10 == 5)
                    {
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 50000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='product-list-paging']/div/a[6]"));
                        pageButton.Click();
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = "241";
                        }
                    }
                    else if (Work_Page % 10 == 6)
                    {
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 50000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='product-list-paging']/div/a[7]"));
                        pageButton.Click();
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = "301";
                        }
                    }
                    else if (Work_Page % 10 == 7)
                    {
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 50000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='product-list-paging']/div/a[8]"));
                        pageButton.Click();
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = "361";
                        }
                    }
                    else if (Work_Page % 10 == 8)
                    {
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 50000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='product-list-paging']/div/a[9]"));
                        pageButton.Click();
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = "421";
                        }
                    }
                    else if (Work_Page % 10 == 9)
                    {
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 50000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='product-list-paging']/div/a[10]"));
                        pageButton.Click();
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = "481";
                        }
                    }
                    else if (Work_Page % 10 == 0)
                    {
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 50000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='product-list-paging']/div/a[11]"));
                        pageButton.Click();
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = "541";
                        }
                    }
                }
                else
                {
                    // 10페이지 이상은 페이지 더보기 클릭
                    for (int xyk = 0; xyk < (Work_Page / 10); xyk++)
                    {
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 30000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='product-list-paging']/div/a[12]"));
                        pageButton.Click();

                        chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles.Last());
                        Delay(500);
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 30000)");
                        Delay(4000);
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = (Work_Page * 60).ToString() + "1";
                        }
                    }

                    // 추가페이지 필요
                    if (Work_Page % 10 == 2)
                    {
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 50000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='product-list-paging']/div/a[3]"));
                        pageButton.Click();
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = (Work_Page * 60).ToString() + "1";
                        }
                    }
                    else if (Work_Page % 10 == 3)
                    {
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 50000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='product-list-paging']/div/a[4]"));
                        pageButton.Click();
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = (Work_Page * 60).ToString() + "1";
                        }
                    }
                    else if (Work_Page % 10 == 4)
                    {
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 50000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='product-list-paging']/div/a[5]"));
                        pageButton.Click();
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = (Work_Page * 60).ToString() + "1";
                        }
                    }
                    else if (Work_Page % 10 == 5)
                    {
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 50000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='product-list-paging']/div/a[6]"));
                        pageButton.Click();
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = (Work_Page * 60).ToString() + "1";
                        }
                    }
                    else if (Work_Page % 10 == 6)
                    {
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 50000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='product-list-paging']/div/a[7]"));
                        pageButton.Click();
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = (Work_Page * 60).ToString() + "1";
                        }
                    }
                    else if (Work_Page % 10 == 7)
                    {
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 50000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='product-list-paging']/div/a[8]"));
                        pageButton.Click();
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = (Work_Page * 60).ToString() + "1";
                        }
                    }
                    else if (Work_Page % 10 == 8)
                    {
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 50000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='product-list-paging']/div/a[9]"));
                        pageButton.Click();
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = (Work_Page * 60).ToString() + "1";
                        }
                    }
                    else if (Work_Page % 10 == 9)
                    {
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 50000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='product-list-paging']/div/a[10]"));
                        pageButton.Click();
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = (Work_Page * 60).ToString() + "1";
                        }
                    }
                    else if (Work_Page % 10 == 0)
                    {
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 50000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='product-list-paging']/div/a[11]"));
                        pageButton.Click();
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = (Work_Page * 60).ToString() + "1";
                        }
                    }
                }
                chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles.Last());
                Delay(500);
                ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 30000)");
                Delay(3000);
                #endregion

                for (int xyi = Work_Page; xyi <= 83; xyi++)
                {
                    int RowH = 0;
                    Page_No = xyi;
                    Page_End = false;
                    SelectNum = 0;

                    bool asdf = false;
                    if (asdf == true)
                    {

                    for (int xyj = 1; xyj <= 60; xyj++)
                    {
                        //TTL_RowH = ((xyi - 1) * 60) + xyj;
                        #region  // 리스트 60개 작업 영역 ======================================================
                        bool _Prod_Detail = false;
                        bool LastPOS = false;
                        int Re_RowH = 1;

                        _Delay_Pause(10);
                        if (Process_Stop == true) { goto End_Loop; }
                        wait = new WebDriverWait(chromeDriver, TimeSpan.FromSeconds(20));
                        IWebElement _SearchResult = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector("li[class='baby-product renew-badge']")));

                        IList<IWebElement> he01 = chromeDriver.FindElements(By.TagName("ul"));
                        // 새로 열려는데 엘리먼트가 없으면 리플래시 를 다시 하는 것 검토
                        foreach (IWebElement helem1 in he01)
                        {
                            if (helem1.GetAttribute("id") != null)
                            {
                                if (helem1.GetAttribute("id") == "productList")
                                {
                                    Scroll_End = 0;
                                    RowH = 0;
                                    IList<IWebElement> he02 = helem1.FindElements(By.TagName("li"));
                                    // 새로 열려는데 엘리먼트가 없으면 리플래시 를 다시 하는 것 검토
                                    foreach (IWebElement helem2 in he02)
                                    {
                                        RowH += 1;
                                        TTL_RowH = ((xyi - 1) * 100) + RowH;

                                        if (SelectNum == 0)       // if (xyj == 1)
                                        {
                                            _Delay_Pause(20);
                                            if (Process_Stop == true) { goto End_Loop; }

                                            txt_CheckingRank.Text = RowH.ToString();

                                            int uxx = helem2.Location.X;
                                            WebDoc_Window_ScrollTo(chromeDriver, uxx, helem2.Location.Y + Convert.ToInt32(txt_Scroll.Text));
                                            if (RowH % 2 == 0) { _Delay_Pause(Convert.ToInt32(cbx_ScrollSec.Text)); }
                                        }
                                        else
                                        {
                                            if (SelectNum <= RowH)
                                            {
                                                if (Process_Stop == true) { goto End_Loop; }

                                                txt_CheckingRank.Text = RowH.ToString();

                                                int uxx = helem2.Location.X;
                                                WebDoc_Window_ScrollTo(chromeDriver, uxx, helem2.Location.Y + Convert.ToInt32(txt_Scroll.Text));
                                                if (RowH % 2 == 0) { _Delay_Pause(Convert.ToInt32(cbx_ScrollSec.Text)); }
                                            }
                                        }

                                        if (Page_No == 1 && RowH < Convert.ToInt32(txt_Rank_Start.Text))
                                        {
                                            //Console.WriteLine(Page_No + " ^ " + RowH + " ^ " + SelectNum);
                                        }
                                        else if (SelectNum >= RowH)
                                        {
                                            //Console.WriteLine(Page_No + " ^ " + RowH + " ^ " + SelectNum);
                                        }
                                        else if (Convert.ToInt32(txt_WorkNum.Text) >= TTL_RowH)
                                        {
                                            //Console.WriteLine(Page_No + " ^ " + RowH + " ^ " + SelectNum);
                                        }
                                        else
                                        {
                                            if (RowH == 100)
                                            {
                                                Page_End = true;
                                            }

                                            if (helem2.GetAttribute("innerText") != null)
                                            {
                                                _Prod_Detail = false;

                                                #region  // 판매자정보 일부가져오기 -----------------------------------------------
                                                bool sInfoPick = false;
                                                IList<IWebElement> he03 = helem2.FindElements(By.TagName("span"));
                                                _Delay_Pause(100);
                                                foreach (IWebElement helem3 in he03)
                                                {
                                                    if (helem3.GetAttribute("data-tiara-ordnum") != null)
                                                    {
                                                        if (helem3.GetAttribute("data-tiara-ordnum").IndexOf((RowH).ToString()) > -1)
                                                        {
                                                            if (sInfoPick == false)
                                                            {
                                                                //int uxx = helem3.Location.X;
                                                                //WebDoc_Window_ScrollTo(chromeDriver, uxx, helem3.Location.Y + Convert.ToInt32(txt_Scroll.Text) - 300);
                                                                //if (RowH % 2 == 0) { _Delay_Pause(Convert.ToInt32(cbx_ScrollSec.Text)); }

                                                                db_ProductID = helem3.GetAttribute("data-tiara-id");
                                                                LastID = db_ProductID;

                                                                IList<IWebElement> he04 = helem3.FindElements(By.TagName("a"));
                                                                foreach (IWebElement helem4 in he04)
                                                                {
                                                                    if (helem4.GetAttribute("href") != null)
                                                                    {
                                                                        db_ProductLink = helem4.GetAttribute("href");
                                                                        break;
                                                                    }
                                                                }
                                                                db_ProductImage = "";
                                                                db_ProductImage = helem3.GetAttribute("data-tiara-image");
                                                                db_ProductCate = helem3.GetAttribute("data-tiara-category");

                                                                sInfoPick = true;
                                                            }
                                                        }
                                                    }
                                                    if (helem3.GetAttribute("class").IndexOf("store_name") > -1)
                                                    {
                                                        db_StoreName = helem3.GetAttribute("innerText");
                                                        db_StoreName = db_StoreName.Replace("'", "");
                                                    }
                                                    if (helem3.GetAttribute("class").IndexOf("product_name") > -1)
                                                    {
                                                        db_ProductName = helem3.GetAttribute("innerText");
                                                        db_ProductName = db_ProductName.Replace("'", "");
                                                    }
                                                }
                                                #endregion

                                                _Delay_Pause(10);
                                                if (Process_Stop == true) { goto End_Loop; }

                                                if (Page_No == 1 && RowH < Convert.ToInt32(txt_Rank_Start.Text))
                                                {
                                                    Console.WriteLine(RowH + "r 시작 순번 이하");
                                                }
                                                else if (helem2.GetAttribute("innerText").IndexOf("연령 확인") > -1)
                                                {
                                                    Console.WriteLine(RowH + "r 성인인증 상품 작업제외");
                                                    richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "     →      " + TTL_RowH + "r  성인인증 상품 작업제외 : " + db_SellerName + "\n" + richTextBox1.Text;
                                                }
                                                else
                                                {
                                                    SelectNum = RowH;

                                                    #region  // 상품검색 시작 ====================================================
                                                    IList<IWebElement> he05 = helem2.FindElements(By.TagName("a"));
                                                    foreach (IWebElement helem5 in he05)
                                                    {
                                                        if (helem5.GetAttribute("href") != null)
                                                        {
                                                            helem5.Click();

                                                            if (Convert.ToInt32(txt_WorkNum.Text) < TTL_RowH)
                                                            {
                                                                txt_WorkNum.Text = TTL_RowH.ToString();
                                                            }
                                                            chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles.Last());
                                                            break;
                                                        }
                                                    }
                                                    _Prod_Detail = true;
                                                    #endregion

                                                    #region  // 상세페이지에서 판매자정보 가져와 기존 작업내역이 있는지 비교하여 제외여부 확인 =================================
                                                    Delay(2000);
                                                    wait = new WebDriverWait(chromeDriver, TimeSpan.FromSeconds(20));
                                                    _SearchResult = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector("span[class='txt_store']")));

                                                    // 스토어정보 창 열기
                                                    var queryButton = chromeDriver.FindElement(By.CssSelector("span[class='txt_store']"));
                                                    queryButton.Click();
                                                    Delay(3000);

                                                    _Delay_Pause(10);
                                                    if (Process_Stop == true) { goto End_Loop; }

                                                    // 작업정보 가져오기 -------------------------------------
                                                    Source = chromeDriver.PageSource;
                                                    Source = Source.Replace("&quot;", @"""");
                                                    Source = Source.Replace("&amp;", @"&");
                                                    //string T_Source = SSplit(Source, @"""loginUser"":", 1);

                                                    db_SellerName = "";
                                                    db_SellerName = SSplit(SSplit(SSplit(Source, @"<dt>상호명</dt>", 1), @"<dd>", 1), @"</dd>", 0);
                                                    db_RepresentativeName = SSplit(SSplit(SSplit(Source, @"<dt>대표자</dt>", 1), @"<dd>", 1), @"</dd>", 0);
                                                    db_CallNumber = SSplit(SSplit(SSplit(Source, @"<dt>대표전화</dt>", 1), @"<dd>", 1), @"</dd>", 0);
                                                    db_Address = SSplit(SSplit(SSplit(Source, @"<dt>사업장 소재지</dt>", 1), @"<dd>", 1), @"</dd>", 0);
                                                    db_Email = SSplit(SSplit(SSplit(Source, @"<dt>대표메일</dt>", 1), @"<dd>", 1), @"</dd>", 0);
                                                    // 작업정보 가져오기 -------------------------------------

                                                    Recent_Store_YN = false;
                                                    // 스토어명을 기존 작업 리스트와 비교
                                                    for (int xxj = 0; xxj < Recent_Store_dno; xxj++)
                                                    {
                                                        if (Recent_StoreList[xxj] == db_SellerName)
                                                        {
                                                            Console.WriteLine(RowH + "r  7일내 홍보됨 : " + db_SellerName);
                                                            richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "     →      " + TTL_RowH + "r  7일내 홍보됨 : " + db_SellerName + "\n" + richTextBox1.Text;
                                                            Recent_Store_YN = true;

                                                            break;
                                                        }
                                                    }
                                                    chromeDriver.Navigate().Back();     // 판매자정보 화면 뒤로 가기
                                                    Delay(1500);
                                                    chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles.Last());
                                                    #endregion

                                                    #region  // // 7일 이내 해당스토어 작업 없음 -----------------------------------------------
                                                    if (Recent_Store_YN == false)
                                                    {
                                                        _Delay_Pause(10);
                                                        if (Process_Stop == true) { goto End_Loop; }

                                                        if (chromeDriver.PageSource.IndexOf("문의") > -1)
                                                        {
                                                            db_Rank = RowH; // 작업정보 가져오기 -------------------------------------
                                                            IList<IWebElement> he11 = chromeDriver.FindElements(By.TagName("span"));
                                                            Delay(100);
                                                            foreach (IWebElement helem11 in he11)
                                                            {
                                                                if (helem11.GetAttribute("innerText").IndexOf("문의") > -1)
                                                                {
                                                                    _Prod_Detail = true;
                                                                    int uxx1 = helem11.Location.X;
                                                                    WebDoc_Window_ScrollTo(chromeDriver, uxx1, helem11.Location.Y - 200);
                                                                    wait = new WebDriverWait(chromeDriver, new TimeSpan(0, 0, 5));
                                                                    Delay(500);

                                                                    helem11.Click();

                                                                    chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles.Last());
                                                                    wait = new WebDriverWait(chromeDriver, new TimeSpan(0, 0, 5));
                                                                    Delay(2000);

                                                                    if (chromeDriver.PageSource.IndexOf("문의하기") > -1)
                                                                    {
                                                                        // 문의하기
                                                                        try
                                                                        {
                                                                            queryButton = chromeDriver.FindElement(By.CssSelector("a[class='link_comm link_write ng-star-inserted']"));
                                                                        }
                                                                        catch
                                                                        {
                                                                            queryButton = chromeDriver.FindElement(By.CssSelector("a[class='link_comm link_write']"));
                                                                        }
                                                                        queryButton.Click();
                                                                        Delay(3000);

                                                                        // 문의하기 팝업창 제어 ==============================================
                                                                        string parentHandle = chromeDriver.CurrentWindowHandle;                                 // 현재 창의 핸들을 저장합니다.
                                                                        IEnumerable<string> allHandles = chromeDriver.WindowHandles;                            // 모든 창의 핸들을 가져옵니다.
                                                                        string dialogHandle = allHandles.FirstOrDefault(handle => handle != parentHandle);      // 부모 창을 제외한 다른 창을 찾습니다.
                                                                        if (dialogHandle != null)
                                                                        {
                                                                            chromeDriver.SwitchTo().Window(dialogHandle);                                       // HTML Dialog 창으로 이동합니다.


                                                                            IWebElement confirmButton = chromeDriver.FindElement(By.ClassName("fold_opt"));      // 문의유형 클릭
                                                                            confirmButton.Click();
                                                                            Delay(200);

                                                                            var option = chromeDriver.FindElement(By.XPath("//*[@id='mArticle']/form/div[1]/select/option[7]"));
                                                                            option.Click();

                                                                            confirmButton.Click();
                                                                            Delay(500);
                                                                            confirmButton = chromeDriver.FindElement(By.Id("inpReview1"));      // 비밀글 클릭
                                                                            confirmButton.Click();
                                                                            Delay(200);

                                                                            confirmButton = chromeDriver.FindElement(By.TagName("textarea[title='문의내용']"));     // 홍보 내역 입력
                                                                            confirmButton.Click();
                                                                            Delay(500);
                                                                            confirmButton.SendKeys(txt_Promotion_Post.Text);
                                                                            Delay(1000);



                                                                            //// 등록 버튼 클릭
                                                                            //confirmButton = chromeDriver.FindElement(By.Id("saveBtn"));
                                                                            //confirmButton.Click();
                                                                            //_Delay_Pause(500);

                                                                            // 글 작성시에는 닫기 기능 미사용할 수 있음
                                                                            chromeDriver.SwitchTo().Window(dialogHandle).Close();


                                                                            chromeDriver.SwitchTo().Window(parentHandle);                                       // 부모 창으로 이동합니다.
                                                                        }
                                                                        // 문의하기 팝업창 제어 ==============================================  

                                                                        _Delay_Pause(10);
                                                                        if (Process_Stop == true) { goto End_Loop; }

                                                                        // 작업 스토어 정보 변수에 저장
                                                                        Recent_StoreList[Recent_Store_dno] = db_SellerName;
                                                                        Recent_Store_dno += 1;
                                                                        // 작업내용 저장 -----------------------------------------
                                                                        try { ADONet_Conn.Close(); }
                                                                        catch { }
                                                                        try
                                                                        {
                                                                            string inSQL = "INSERT PROMOTION_WorkData ";
                                                                            inSQL += "( PACT_UserID, PACT_Market, PACT_Search_Keyword, PACT_Search_Rank, PACT_StoreName, PACT_ProductID, PACT_ProductLink, PACT_ProductCate, PACT_ProductName, PACT_InputTime, PACT_ProductImage, PACT_SellerName, PACT_CallNumber, PACT_Address, PACT_RepresentativeName, PACT_Email ";
                                                                            inSQL += " ) VALUES ( ";
                                                                            inSQL += " '" + p_UserID + "', ";
                                                                            inSQL += " '" + db_Market + "', ";
                                                                            inSQL += " '" + db_SKeyword + "', ";
                                                                            inSQL += "  " + db_Rank + " , ";
                                                                            inSQL += " '" + db_StoreName + "', ";
                                                                            inSQL += " '" + db_ProductID + "', ";
                                                                            inSQL += " '" + db_ProductLink + "', ";
                                                                            inSQL += " '" + db_ProductCate + "', ";
                                                                            inSQL += " '" + db_ProductName + "', ";
                                                                            inSQL += " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', ";
                                                                            inSQL += " '" + db_ProductImage + "', ";
                                                                            inSQL += " '" + db_SellerName + "', ";
                                                                            inSQL += " '" + db_CallNumber + "', ";
                                                                            inSQL += " '" + db_Address + "', ";
                                                                            inSQL += " '" + db_RepresentativeName + "', ";
                                                                            inSQL += " '" + db_Email + "'  ";
                                                                            inSQL += "  ) ";
                                                                            ADONet_Conn.Open();
                                                                            SqlCommand cmd0_1 = new SqlCommand();
                                                                            cmd0_1.Connection = ADONet_Conn;
                                                                            cmd0_1.CommandText = inSQL;
                                                                            cmd0_1.ExecuteNonQuery();
                                                                            ADONet_Conn.Close();

                                                                            txt_Work_Count.Text = (Convert.ToInt32(ImportNumbersOnly(txt_Work_Count.Text)) + 1).ToString();
                                                                        }
                                                                        catch
                                                                        {
                                                                            try { ADONet_Conn.Close(); }
                                                                            catch { }
                                                                            string inSQL = "INSERT PROMOTION_WorkData ";
                                                                            inSQL += "( PACT_UserID, PACT_Market, PACT_Search_Keyword, PACT_Search_Rank, PACT_StoreName, PACT_ProductID, PACT_ProductLink, PACT_ProductCate, PACT_ProductName, PACT_InputTime, PACT_ProductImage, PACT_SellerName, PACT_CallNumber, PACT_Address, PACT_RepresentativeName, PACT_Email ";
                                                                            inSQL += " ) VALUES ( ";
                                                                            inSQL += " '" + p_UserID + "', ";
                                                                            inSQL += " '" + db_Market + "', ";
                                                                            inSQL += " '" + db_SKeyword + "', ";
                                                                            inSQL += "  " + db_Rank + " , ";
                                                                            inSQL += " '" + db_StoreName + "', ";
                                                                            inSQL += " '" + db_ProductID + "', ";
                                                                            inSQL += " '" + db_ProductLink + "', ";
                                                                            inSQL += " '" + db_ProductCate + "', ";
                                                                            inSQL += " '" + db_ProductName + "', ";
                                                                            inSQL += " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', ";
                                                                            inSQL += " '" + db_ProductImage + "', ";
                                                                            inSQL += " '" + db_SellerName + "', ";
                                                                            inSQL += " '" + db_CallNumber + "', ";
                                                                            inSQL += " '" + db_Address + "', ";
                                                                            inSQL += " '" + db_RepresentativeName + "', ";
                                                                            inSQL += " '" + db_Email + "'  ";
                                                                            inSQL += "  ) ";
                                                                            ADONet_Conn.Open();
                                                                            SqlCommand cmd0_1 = new SqlCommand();
                                                                            cmd0_1.Connection = ADONet_Conn;
                                                                            cmd0_1.CommandText = inSQL;
                                                                            cmd0_1.ExecuteNonQuery();
                                                                            ADONet_Conn.Close();
                                                                        }

                                                                        try
                                                                        {
                                                                            richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "     → " + TTL_RowH + "r  문의완료 : " + db_SellerName + "_" + db_ProductName.Substring(0, 6) + "\n" + richTextBox1.Text;
                                                                        }
                                                                        catch
                                                                        {
                                                                            richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "     → " + TTL_RowH + "r  문의완료 : " + db_SellerName + "_" + db_ProductName + "\n" + richTextBox1.Text;
                                                                        }

                                                                        break;      // 상세페이지 루프 종료
                                                                    }
                                                                }
                                                            }

                                                        }
                                                        else
                                                        {       // 문의하기 없어 빠져나옴

                                                            _Prod_Detail = true;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        // 작업내역이 있어 빠져나옴
                                                        Console.WriteLine("작업내역이 있어 빠져나옴");
                                                    }
                                                    #endregion

                                                    if (RowH % 10 == 0)
                                                    {
                                                        try
                                                        {
                                                            string upSQL = "UPDATE PROMOTION_UserRecord   ";
                                                            upSQL += " SET UserLastAct = '" + clib_Channel.SelectedItem.ToString() + "^" + txt_WorkKeyword.Text + "^" + TTL_RowH + "'  ";
                                                            upSQL += " WHERE UserID = '" + p_UserID + "' ";
                                                            ADONet_Conn.Open();
                                                            SqlCommand cmd0_1 = new SqlCommand();
                                                            cmd0_1.Connection = ADONet_Conn;
                                                            cmd0_1.CommandText = upSQL;
                                                            cmd0_1.ExecuteNonQuery();
                                                            ADONet_Conn.Close();

                                                            txt_UserLastAct.Text = clib_Channel.SelectedItem.ToString() + "^" + txt_WorkKeyword.Text + "^" + TTL_RowH;
                                                        }
                                                        catch { }
                                                    }

                                                    // 상세페이지에서 빠져나오기
                                                    chromeDriver.Navigate().Back();
                                                    Delay(500);
                                                    //chromeDriver.Navigate().Refresh();
                                                    chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles.Last());
                                                    Delay(500);

                                                }
                                            }
                                        }
                                        if (_Prod_Detail == true)
                                        {
                                            break;  // ul 빠져나가기
                                        }
                                    }
                                    Delay(1000);
                                    if (_Prod_Detail == true)
                                    {
                                        break;  // ul 빠져나가기
                                    }
                                }
                            }
                        }
                        #endregion

                        if (TTL_RowH > Convert.ToInt32(txt_Rank_End.Text))
                        {
                            break;
                        }
                        if (Page_End == true)
                        {
                            break;
                        }
                    }

                    }

                    #region  // 60개 단위로 페이지 이동 영역 =============
                    if (xyi % 10 == 0)
                    {
                        // 5페이지 이상은 페이지 더보기 클릭
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 30000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='mArticle']/div/cu-pagination-list/div/div[2]/button[2]"));
                        pageButton.Click();
                    }
                    else if (xyi % 10 == 1)
                    {
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 50000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='mArticle']/div/cu-pagination-list/div/div[2]/div/button[1]"));
                        pageButton.Click();
                    }
                    else if (xyi % 10 == 2)
                    {
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 50000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='mArticle']/div/cu-pagination-list/div/div[2]/div/button[2]"));
                        pageButton.Click();
                    }
                    else if (xyi % 10 == 3)
                    {
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 50000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='mArticle']/div/cu-pagination-list/div/div[2]/div/button[3]"));
                        pageButton.Click();
                    }
                    else if (xyi % 10 == 4)
                    {
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 50000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='mArticle']/div/cu-pagination-list/div/div[2]/div/button[4]"));
                        pageButton.Click();
                    }
                    else if (xyi % 10 == 5)
                    {
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 50000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='mArticle']/div/cu-pagination-list/div/div[2]/div/button[5]"));
                        pageButton.Click();
                    }
                    else if (xyi % 10 == 6)
                    {
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 50000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='mArticle']/div/cu-pagination-list/div/div[2]/div/button[6]"));
                        pageButton.Click();
                    }
                    else if (xyi % 10 == 7)
                    {
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 50000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='mArticle']/div/cu-pagination-list/div/div[2]/div/button[7]"));
                        pageButton.Click();
                    }
                    else if (xyi % 10 == 8)
                    {
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 50000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='mArticle']/div/cu-pagination-list/div/div[2]/div/button[8]"));
                        pageButton.Click();
                    }
                    else if (xyi % 10 == 9)
                    {
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 50000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='mArticle']/div/cu-pagination-list/div/div[2]/div/button[9]"));
                        pageButton.Click();
                    }
                    chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles.Last());
                    Delay(500);
                    ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 30000)");
                    Delay(3000);
                    #endregion

                    if (TTL_RowH > Convert.ToInt32(txt_Rank_End.Text))
                    {
                        break;
                    }
                }
                chromeDriver.Navigate().Back();
                Console.WriteLine("구글페이지로이동 " + TTL_RowH);
            }

        End_Loop:
            Console.WriteLine("작업을 종료합니다.");
        }

        public void Selenium_네이버(string xUrl)
        {
            xUrl = "https://search.shopping.naver.com/search/category/" + xUrl;
            this.Selenium_Paging(xUrl);

            WebDriverWait wait = null;
            if (Process_Stop == true) { goto End_Loop; }
            string LogInfo;
            try
            {
                LogInfo = SSplit(SSplit(chromeDriver.PageSource, "<span class='gnb_name' id='gnb_name1'>", 1), "</span>", 0);
            }
            catch
            {
                LogInfo = "";
            }
            //if (LogInfo.Length == 0)
            //{
            //    Key_WorkState = true;
            //    chromeDriver.Navigate().GoToUrl("https://www.naver.com/");
            //    wait = new WebDriverWait(chromeDriver, new TimeSpan(0, 0, 5));
            //    Delay(1000);
            //    _Delay_Pause(10);
            //    if (Process_Stop == true) { goto End_Loop; }

            //    AutoClosingMessageBox.Show("회원가입 후 프로그램을 다시 실행하세요!", "Auto PR  [메세지 자동종료]", 3000);
            //    richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "     ▶ 네이버 로그인안됨" + "\n" + richTextBox1.Text;
            //    return;
            //}
            //else if (chromeDriver.PageSource.IndexOf("찾으시는 결과가 없네요.") > -1)
            //{
            //    Key_WorkState = true;
            //    richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "     → 상품없음" + "\n" + richTextBox1.Text;
            //    goto End_Loop;
            //}

            if (LogInfo.Length >= 0)
            {
                if (LogInfo.Length == 0)
                {
                    chromeDriver.Navigate().GoToUrl("https://nid.naver.com/nidlogin.login");
                    richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "   [수동으로 로그인 하세요!]" + "\n" + richTextBox1.Text;
                    Source = "네이버 로그인 PASS";

                    Delay(1000 * 20);
                    chromeDriver.Navigate().GoToUrl(xUrl);
                }

                int Page_No = 0;
                int SelectNum = 0;
                int CheckNum = 0;
                int Scroll_End = 0;
                int TTL_RowH = 0;

                string LastID = "";
                bool StartPOS = true;
                bool Page_End = false;

                int Work_Page = 0;
                if (Convert.ToInt32(txt_Rank_Start.Text) < Convert.ToInt32(txt_WorkNum.Text))
                {
                    Work_Page = 1 + Convert.ToInt32(txt_WorkNum.Text) / 40;
                }
                else
                {
                    Work_Page = 1 + Convert.ToInt32(txt_Rank_Start.Text) / 40;
                }
                #region  // 시작페이지를 40개 단위로 페이지 이동 영역 =============
                if (Work_Page < 11)
                {
                    if (Work_Page == 1)
                    {
                        // PASS
                    }
                    else if (Work_Page % 10 == 2)
                    {
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='content']/div[1]/div[4]/div/a[1]"));
                        pageButton.Click();
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = "41";
                        }
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                    }
                    else if (Work_Page % 10 == 3)
                    {
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='content']/div[1]/div[4]/div/a[2]"));
                        pageButton.Click();
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = "81";
                        }
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                    }
                    else if (Work_Page % 10 == 4)
                    {
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='content']/div[1]/div[4]/div/a[3]"));
                        pageButton.Click();
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = "121";
                        }
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                    }
                    else if (Work_Page % 10 == 5)
                    {
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='content']/div[1]/div[4]/div/a[4]"));
                        pageButton.Click();
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = "161";
                        }
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                    }
                    else if (Work_Page % 10 == 6)
                    {
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='content']/div[1]/div[4]/div/a[5]"));
                        pageButton.Click();
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = "201";
                        }
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                    }
                    else if (Work_Page % 10 == 7)
                    {
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='content']/div[1]/div[4]/div/a[6]"));
                        pageButton.Click();
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = "241";
                        }
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                    }
                    else if (Work_Page % 10 == 8)
                    {
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='content']/div[1]/div[4]/div/a[7]"));
                        pageButton.Click();
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = "281";
                        }
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                    }
                    else if (Work_Page % 10 == 9)
                    {
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='content']/div[1]/div[4]/div/a[8]"));
                        pageButton.Click();
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = "321";
                        }
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                    }
                    else if (Work_Page % 10 == 0)
                    {
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='content']/div[1]/div[4]/div/a[9]"));
                        pageButton.Click();
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = "361";
                        }
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                    }
                }
                else
                {
                    // 10페이지 이상은 페이지 더보기 클릭
                    for (int xyk = 0; xyk < (Work_Page / 10); xyk++)
                    {
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='content']/div[1]/div[4]/a"));
                        pageButton.Click();

                        chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles.Last());
                        Delay(500);
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                        Delay(4000);
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = (Work_Page * 40).ToString() + "1";
                        }
                    }

                    // 추가페이지 필요
                    if (Work_Page % 10 == 2)
                    {
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='content']/div[1]/div[4]/div/a[1]"));
                        pageButton.Click();
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = (Work_Page * 60).ToString() + "1";
                        }
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                    }
                    else if (Work_Page % 10 == 3)
                    {
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='content']/div[1]/div[4]/div/a[1]"));
                        pageButton.Click();
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = (Work_Page * 60).ToString() + "1";
                        }
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                    }
                    else if (Work_Page % 10 == 4)
                    {
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='content']/div[1]/div[4]/div/a[1]"));
                        pageButton.Click();
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = (Work_Page * 60).ToString() + "1";
                        }
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                    }
                    else if (Work_Page % 10 == 5)
                    {
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='content']/div[1]/div[4]/div/a[1]"));
                        pageButton.Click();
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = (Work_Page * 60).ToString() + "1";
                        }
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                    }
                    else if (Work_Page % 10 == 6)
                    {
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='content']/div[1]/div[4]/div/a[1]"));
                        pageButton.Click();
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = (Work_Page * 60).ToString() + "1";
                        }
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                    }
                    else if (Work_Page % 10 == 7)
                    {
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='content']/div[1]/div[4]/div/a[1]"));
                        pageButton.Click();
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = (Work_Page * 60).ToString() + "1";
                        }
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                    }
                    else if (Work_Page % 10 == 8)
                    {
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='content']/div[1]/div[4]/div/a[1]"));
                        pageButton.Click();
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = (Work_Page * 60).ToString() + "1";
                        }
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                    }
                    else if (Work_Page % 10 == 9)
                    {
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='content']/div[1]/div[4]/div/a[1]"));
                        pageButton.Click();
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = (Work_Page * 60).ToString() + "1";
                        }
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                    }
                    else if (Work_Page % 10 == 0)
                    {
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='content']/div[1]/div[4]/div/a[1]"));
                        pageButton.Click();
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = (Work_Page * 60).ToString() + "1";
                        }
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                    }
                }
                for (int xi = 0; xi < 5; xi++)
                {
                    ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, -10000)");
                    Delay(new Random().Next(200, 300));
                }
                Delay(new Random().Next(3000, 3500));
                #endregion

                for (int xyi = Work_Page; xyi <= 100; xyi++)
                {
                    int RowH = 0;
                    Page_No = xyi;
                    Page_End = false;
                    SelectNum = 0;

                    for (int xyj = 1; xyj <= 40; xyj++)
                    {
                        //TTL_RowH = ((xyi - 1) * 60) + xyj;
                        #region  // 리스트 40개 작업 영역 ======================================================
                        bool _Prod_Detail = false;
                        bool LastPOS = false;
                        int Re_RowH = 1;

                        RowH = Convert.ToInt32((xyi - 1) * 40) + xyj;

                        _Delay_Pause(10);
                        if (Process_Stop == true) { goto End_Loop; }
                        //wait = new WebDriverWait(chromeDriver, TimeSpan.FromSeconds(20));
                        //IWebElement _SearchResult = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector("a[data-nclick='*']")));

                        string Source = chromeDriver.PageSource;
                        int cxRank = 0;
                        IList<IWebElement> he01 = chromeDriver.FindElements(By.TagName("a"));
                        // 새로 열려는데 엘리먼트가 없으면 리플래시 를 다시 하는 것 검토
                        foreach (IWebElement helem1 in he01)
                        {
                            if (helem1.GetAttribute("data-nclick") != null)
                            {
                                if (helem1.GetAttribute("data-nclick") == "https://adcr.naver.com")
                                {
                                    int uxx = helem1.Location.X;
                                    WebDoc_Window_ScrollTo(chromeDriver, uxx, helem1.Location.Y + Convert.ToInt32(txt_Scroll.Text));
                                    Delay(Convert.ToInt32(cbx_ScrollSec.Text));
                                }
                                else
                                {
                                    //Console.WriteLine(helem1.GetAttribute("data-nclick") + "  //  r:" + RowH);
                                    if (helem1.GetAttribute("data-nclick").IndexOf("r:" + RowH) > -1)
                                    {
                                        string T_Source = @"""rank"":" + SSplit(Source, @"""rank"":", RowH);
                                        if (Process_Stop == true) { goto End_Loop; }

                                        int uxx = helem1.Location.X;
                                        WebDoc_Window_ScrollTo(chromeDriver, uxx, helem1.Location.Y + Convert.ToInt32(txt_Scroll.Text));
                                        Delay(Convert.ToInt32(cbx_ScrollSec.Text));

                                        string cID = helem1.GetAttribute("data-i");
                                        helem1.Click();

                                        db_Rank = RowH;
                                        txt_CheckingRank.Text = RowH.ToString();
                                        txt_WorkNum.Text = TTL_RowH.ToString();
                                        Delay(new Random().Next(5000, 6000));
                                        
                                        chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles.Last());
                                        Source = chromeDriver.PageSource;
                                        if (chromeDriver.Url.IndexOf("catalog") > -1)
                                        {
                                            // 가격비교 상품
                                            // 10개 상점 중 스마트스토어 리스트만 가져오기 (판매량 프로그램 재확인)
                                            // 스마트스토어 리스트 중 상세페이지 로 들어가기
                                            string asd = SSplit(Source, @"""productsPage"":{""totalCount"":", 1);
                                            string asd1 = SSplit(asd, @",", 0);
                                            if (Convert.ToInt32(SSplit(SSplit(Source, @"""productsPage"":{""totalCount"":", 1), @",", 0)) > 0)
                                            {
                                                int mallCount = Convert.ToInt32(SSplit(SSplit(Source, @"""productsPage"":{""totalCount"":", 1), @",", 0));
                                                int ttlPage = 1 + mallCount / 20;
                                                int xPage = 1;
                                                cxRank = 0;

                                                string[] List_ProductLink = new string[50];
                                                int xLRL = 0;
                                                int[] xCRank = new int[50];

                                                for (int xji = 0; xji < ttlPage; xji++)
                                                {

                                                    IList<IWebElement> he02 = chromeDriver.FindElements(By.TagName("li"));
                                                    foreach (IWebElement helem2 in he02)
                                                    {
                                                        bool SS_YN = false;
                                                        IList<IWebElement> he03 = helem2.FindElements(By.TagName("a"));
                                                        foreach (IWebElement helem3 in he03)
                                                        {
                                                            if (helem3.GetAttribute("data-nclick") != null)
                                                            {
                                                                if (helem3.GetAttribute("class").IndexOf("productList_mall_link") > -1)
                                                                {
                                                                    cxRank += 1;

                                                                    int uxx1 = helem3.Location.X;
                                                                    WebDoc_Window_ScrollTo(chromeDriver, uxx1, helem3.Location.Y - 200);
                                                                    wait = new WebDriverWait(chromeDriver, new TimeSpan(0, 0, 5));
                                                                    Delay(500);

                                                                    db_CateRank = cxRank;
                                                                    IList<IWebElement> he04 = helem3.FindElements(By.TagName("img"));
                                                                    if (he04.Count > 0)
                                                                    {
                                                                        // 오픈마켓 은 미처리
                                                                        SS_YN = false;
                                                                        break;
                                                                    }
                                                                    else
                                                                    {
                                                                        // 스마트스토어 나 자사몰 포함일 듯
                                                                        SS_YN = true;
                                                                    }
                                                                }
                                                                else if (helem3.GetAttribute("class").IndexOf("productList_title") > -1)
                                                                {
                                                                    if (SS_YN == true)
                                                                    {
                                                                        helem3.Click();

                                                                        chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles.Last());
                                                                        wait = new WebDriverWait(chromeDriver, new TimeSpan(0, 0, 5));
                                                                        Delay(2000);

                                                                        // 상세정보 가져오기
                                                                        Source = chromeDriver.PageSource;
                                                                        Source = Source.Replace(@"\", "");
                                                                        Source = Source.Replace("u002F", "/");
                                                                        Source = Source.Replace("u0026", "&");
                                                                        Source = Source.Replace("u003E", ">");
                                                                        db_ProductImage = SSplit(SSplit(SSplit(Source, @"""productImages"":[", 1), @"""url"":""", 1), @"""", 0);
                                                                        db_ProductCate = SSplit(SSplit(SSplit(Source, @"""channelProductDisplayStatusType"":""", 1), @"""wholeCategoryName"":""", 1), @"""", 0);
                                                                        db_StoreName = SSplit(SSplit(Source, @"""channelName"":""", 1), @"""", 0);
                                                                        db_ProductName = SSplit(SSplit(SSplit(Source, @"""notice"":{", 1), @"""name"":""", 1), @"""", 0);
                                                                        db_ProductName = db_ProductName.Replace("'", "");
                                                                        db_SellerName = "";
                                                                        db_SellerName = SSplit(SSplit(Source, @"""representName"":""", 1), @"""", 0);
                                                                        string cSUrl = SSplit(SSplit(Source, @"""channelSiteUrl"":""", 1), @"""", 0);
                                                                        db_ProductLink = SSplit(SSplit(Source, @"""productUrl"":""", 1), @"""", 0);
                                                                        db_ProductLink = db_ProductLink.Replace("main", cSUrl);

                                                                        db_CateRank = cxRank;
                                                                        db_RepresentativeName = SSplit(SSplit(Source, @"""representativeName"":""", 1), @"""", 0);
                                                                        db_RepresentativeName = db_RepresentativeName.Replace("'", "");
                                                                        db_CallNumber = "";
                                                                        db_Address = SSplit(SSplit(SSplit(Source, @"보내실 곳</th>", 1), @">", 1), @"</td", 0);
                                                                        db_Address = db_Address.Replace("'", "");
                                                                        db_Email = "";
                                                                        db_ChannelUid = SSplit(SSplit(Source, @"""channelUid"":""", 1), @"""", 0);

                                                                        string channelID = db_ChannelUid;
                                                                        string sURL4 = "https://smartstore.naver.com/i/v2/channels/" + channelID + "/visit";
                                                                        WinHttpRequest WinHttp3 = new WinHttpRequest();
                                                                        WinHttp3.Open("GET", sURL4);
                                                                        WinHttp3.SetRequestHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/118.0.0.0 Whale/3.23.214.10 Safari/537.36");
                                                                        WinHttp3.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded;Charset=euc-kr;");
                                                                        WinHttp3.Send();
                                                                        WinHttp3.WaitForResponse();
                                                                        string responseText3 = WinHttp3.ResponseText;
                                                                        Delay(new Random().Next(1000, 1500));

                                                                        db_Vtoday = Convert.ToInt32(SSplit(SSplit(responseText3, @"today"":", 1), @",", 0));
                                                                        db_Vtotal = Convert.ToInt32(SSplit(SSplit(responseText3, @"total"":", 1), @"}", 0));

                                                                        // 톡톡문의하기 영역 ==================================================================

                                                                        IList<IWebElement> he04 = chromeDriver.FindElements(By.TagName("a"));
                                                                        foreach (IWebElement helem4 in he04)
                                                                        {
                                                                            if (helem4.GetAttribute("innerText") == "톡톡문의")
                                                                            {
                                                                                helem4.Click();
                                                                                Delay(2000);

                                                                                // 문의하기 팝업창 제어 ==============================================
                                                                                string parentHandle = chromeDriver.CurrentWindowHandle;                                 // 현재 창의 핸들을 저장합니다.
                                                                                IEnumerable<string> allHandles = chromeDriver.WindowHandles;                            // 모든 창의 핸들을 가져옵니다.
                                                                                string dialogHandle = allHandles.FirstOrDefault(handle => handle != parentHandle);      // 부모 창을 제외한 다른 창을 찾습니다.
                                                                                if (dialogHandle != null)
                                                                                {
                                                                                    chromeDriver.SwitchTo().Window(dialogHandle);                                       // HTML Dialog 창으로 이동합니다.

                                                                                    
                                                                                    // 홍보 내역 입력
                                                                                    var talkButton = chromeDriver.FindElement(By.XPath("//*[@id='content']/section/footer/div[1]/div[2]/div/div[2]/textarea"));
                                                                                    talkButton.Click();
                                                                                    Delay(500);
                                                                                    talkButton.SendKeys(txt_Promotion_Post.Text);
                                                                                    Delay(1000);

                                                                                    //// 완료 버튼 클릭
                                                                                    //talkButton = chromeDriver.FindElement(By.XPath("//*[@id='content']/section/footer/div[1]/div[2]/div/div[3]/button"));
                                                                                    //talkButton.Click();
                                                                                    //Delay(500);

                                                                                    _Delay_Pause(10);
                                                                                    if (Process_Stop == true) { goto End_Loop; }

                                                                                    // 작업내용 저장 -----------------------------------------
                                                                                    try { ADONet_Conn.Close(); }
                                                                                    catch { }
                                                                                    #region  // 검색정보 저장
                                                                                    try
                                                                                    {
                                                                                        string inSQL = "INSERT PROMOTION_WorkData ";
                                                                                        inSQL += "( PACT_UserID, PACT_Market, PACT_Search_Keyword, PACT_Search_Rank, PACT_Cate_Rank, PACT_StoreName, PACT_ProductID, PACT_ProductLink, PACT_ProductCate, PACT_ProductName, PACT_InputTime, PACT_ProductImage, PACT_SellerName, PACT_CallNumber, PACT_Address, PACT_RepresentativeName, PACT_Email, PACT_ChannelUid, PACT_Visit_Total, PACT_Visit_Today ";
                                                                                        inSQL += " ) VALUES ( ";
                                                                                        inSQL += " '" + p_UserID + "', ";
                                                                                        inSQL += " '" + db_Market + "', ";
                                                                                        inSQL += " '" + db_SKeyword + "', ";
                                                                                        inSQL += "  " + db_Rank + " , ";
                                                                                        inSQL += "  " + db_CateRank + " , ";
                                                                                        inSQL += " '" + db_StoreName + "', ";
                                                                                        inSQL += " '" + db_ProductID + "', ";
                                                                                        inSQL += " '" + db_ProductLink + "', ";
                                                                                        inSQL += " '" + db_ProductCate + "', ";
                                                                                        inSQL += " '" + db_ProductName + "', ";
                                                                                        inSQL += " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', ";
                                                                                        inSQL += " '" + db_ProductImage + "', ";
                                                                                        inSQL += " '" + db_SellerName + "', ";
                                                                                        inSQL += " '" + db_CallNumber + "', ";
                                                                                        inSQL += " '" + db_Address + "', ";
                                                                                        inSQL += " '" + db_RepresentativeName + "', ";
                                                                                        inSQL += " '" + db_Email + "', ";
                                                                                        inSQL += " '" + db_ChannelUid + "', ";
                                                                                        inSQL += "  " + db_Vtotal + " , ";
                                                                                        inSQL += "  " + db_Vtoday + "   ";
                                                                                        inSQL += "  ) ";
                                                                                        ADONet_Conn.Open();
                                                                                        SqlCommand cmd0_1 = new SqlCommand();
                                                                                        cmd0_1.Connection = ADONet_Conn;
                                                                                        cmd0_1.CommandText = inSQL;
                                                                                        cmd0_1.ExecuteNonQuery();
                                                                                        ADONet_Conn.Close();

                                                                                        txt_Work_Count.Text = (Convert.ToInt32(ImportNumbersOnly(txt_Work_Count.Text)) + 1).ToString();
                                                                                    }
                                                                                    catch
                                                                                    {
                                                                                        try { ADONet_Conn.Close(); }
                                                                                        catch { }
                                                                                        string inSQL = "INSERT PROMOTION_WorkData ";
                                                                                        inSQL += "( PACT_UserID, PACT_Market, PACT_Search_Keyword, PACT_Search_Rank, PACT_Cate_Rank, PACT_StoreName, PACT_ProductID, PACT_ProductLink, PACT_ProductCate, PACT_ProductName, PACT_InputTime, PACT_ProductImage, PACT_SellerName, PACT_CallNumber, PACT_Address, PACT_RepresentativeName, PACT_Email, PACT_ChannelUid, PACT_Visit_Total, PACT_Visit_Today ";
                                                                                        inSQL += " ) VALUES ( ";
                                                                                        inSQL += " '" + p_UserID + "', ";
                                                                                        inSQL += " '" + db_Market + "', ";
                                                                                        inSQL += " '" + db_SKeyword + "', ";
                                                                                        inSQL += "  " + db_Rank + " , ";
                                                                                        inSQL += "  " + db_CateRank + " , ";
                                                                                        inSQL += " '" + db_StoreName + "', ";
                                                                                        inSQL += " '" + db_ProductID + "', ";
                                                                                        inSQL += " '" + db_ProductLink + "', ";
                                                                                        inSQL += " '" + db_ProductCate + "', ";
                                                                                        inSQL += " '" + db_ProductName + "', ";
                                                                                        inSQL += " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', ";
                                                                                        inSQL += " '" + db_ProductImage + "', ";
                                                                                        inSQL += " '" + db_SellerName + "', ";
                                                                                        inSQL += " '" + db_CallNumber + "', ";
                                                                                        inSQL += " '" + db_Address + "', ";
                                                                                        inSQL += " '" + db_RepresentativeName + "', ";
                                                                                        inSQL += " '" + db_Email + "', ";
                                                                                        inSQL += " '" + db_ChannelUid + "', ";
                                                                                        inSQL += "  " + db_Vtotal + " , ";
                                                                                        inSQL += "  " + db_Vtoday + "   ";
                                                                                        inSQL += "  ) ";
                                                                                        ADONet_Conn.Open();
                                                                                        SqlCommand cmd0_1 = new SqlCommand();
                                                                                        cmd0_1.Connection = ADONet_Conn;
                                                                                        cmd0_1.CommandText = inSQL;
                                                                                        cmd0_1.ExecuteNonQuery();
                                                                                        ADONet_Conn.Close();
                                                                                    }

                                                                                    try
                                                                                    {
                                                                                        richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "     → " + TTL_RowH + "r  문의완료 : " + db_SellerName + "_" + db_ProductName.Substring(0, 6) + "\n" + richTextBox1.Text;
                                                                                    }
                                                                                    catch
                                                                                    {
                                                                                        richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "     → " + TTL_RowH + "r  문의완료 : " + db_SellerName + "_" + db_ProductName + "\n" + richTextBox1.Text;
                                                                                    }
                                                                                    #endregion

                                                                                    // 글 작성시에는 닫기 기능 미사용할 수 있음
                                                                                    chromeDriver.SwitchTo().Window(dialogHandle).Close();

                                                                                    chromeDriver.SwitchTo().Window(parentHandle);                                       // 부모 창으로 이동합니다.

                                                                                    break;
                                                                                }
                                                                                // 문의하기 팝업창 제어 ======
                                                                            }
                                                                        }
                                                                        // 톡톡문의하기 영역 ==================================================================

                                                                        문의하기 test
                                                                    }
                                                                }
                                                                if (cxRank % 20 == 0)
                                                                {
                                                                    break;
                                                                }
                                                            }
                                                        }



                                                        //string sURL2 = "https://search.shopping.naver.com/catalog/" + cID + "?page=" + xi;

                                                        //WinHttpRequest WinHttp = new WinHttpRequest();
                                                        //WinHttp.Open("GET", sURL2);
                                                        //WinHttp.SetRequestHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/118.0.0.0 Whale/3.23.214.10 Safari/537.36");
                                                        //WinHttp.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded;Charset=euc-kr;");
                                                        ////WinHttp.SetRequestHeader("Referer", chromeDriver.Url);
                                                        //WinHttp.Send();
                                                        //WinHttp.WaitForResponse();
                                                        //string responseText = WinHttp.ResponseText;
                                                        //responseText = responseText.Replace(@"\", "");
                                                        //responseText = responseText.Replace("u002F", "/");
                                                        //responseText = responseText.Replace("u0026", "&");
                                                        //responseText = responseText.Replace("u003E", ">");
                                                        //string ssSource = SSplit(responseText, "서울특별시 강남구 테헤란로 142", 1);
                                                        //Delay(new Random().Next(1000, 1500));

                                                        //ssSource = SSplit(ssSource, @"{""param"":{""nvMid"":""" + cID + @""",""sort"":""LOW_PRICE""", 2);
                                                        ////ssSource = SSplit(ssSource, "{""param"":{""nvMid"":""" + cRowH + """,""sort"":""LOW_PRICE""")(2)

                                                        //Console.WriteLine(Convert.ToInt32(UBound_int(ssSource, @"{""nvMid"":""")));
                                                        //for (int xj = 1; xj < Convert.ToInt32(UBound_int(ssSource, @"{""nvMid"":""")); xj++)
                                                        //{
                                                        //    if (SSplit(ssSource, @"{""nvMid"":""", xj).IndexOf("smartstore.naver.com") > -1)
                                                        //    {
                                                        //        //각 페이지에서 스마트스토어 정보 추출
                                                        //        List_ProductLink[xLRL] = SSplit(SSplit(ssSource, @"""mobileProductUrl"":""", xj), @"""", 0);
                                                        //        xCRank[xLRL] = (1 - xi) * 10 + xj;

                                                        //        // 해당 스토어 상세정보 가져오기
                                                        //        string sURL3 = List_ProductLink[xLRL];

                                                        //        WinHttpRequest WinHttp3 = new WinHttpRequest();
                                                        //        WinHttp3.Open("GET", sURL3);
                                                        //        WinHttp3.SetRequestHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/118.0.0.0 Whale/3.23.214.10 Safari/537.36");
                                                        //        WinHttp3.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded;Charset=euc-kr;");
                                                        //        WinHttp3.Send();
                                                        //        WinHttp3.WaitForResponse();
                                                        //        string responseText3 = WinHttp3.ResponseText;
                                                        //        responseText3 = responseText3.Replace(@"\", "");
                                                        //        responseText3 = responseText3.Replace("u002F", "/");
                                                        //        responseText3 = responseText3.Replace("u0026", "&");
                                                        //        responseText3 = responseText3.Replace("u003E", ">");
                                                        //        string sSource3 = SSplit(responseText3, @"""title"":""묻고 답하기""", 1);
                                                        //        Delay(new Random().Next(1000, 1500));


                                                        //        xLRL += 1;
                                                        //    }
                                                        //}

                                                    }


                                                    if (xPage <= ttlPage)
                                                    {
                                                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='section_price']/div[3]/div[2]/a[" + (xPage + 1) + "]"));
                                                        pageButton.Click();
                                                        Delay(3000);
                                                    }
                                                    else
                                                    {
                                                        break;
                                                    }
                                                }
                                            }

                                            chromeDriver.Close();
                                            chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles.Last());
                                            Delay(new Random().Next(500, 600));


                                        }
                                        else
                                        {
                                            // 단일 상품 상세페이지로 들어가기
                                            // -------------------------------------------------------------------------추가 필요 영역



                                            chromeDriver.Close();
                                            chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles.Last());
                                            Delay(new Random().Next(500, 600));
                                        }






                                        // 상세페이지에 무조건 들어간 뒤
                                        // 방문자수 수집
                                        // 판매자 상호, 대표자명 수집
                                    }
                                }
                            }
                        }
                        #endregion

                        if (TTL_RowH > Convert.ToInt32(txt_Rank_End.Text))
                        {
                            break;
                        }
                        if (Page_End == true)
                        {
                            break;
                        }
                    }

                    #region  // 40개 단위로 페이지 이동 영역 =============
                    if (xyi % 10 == 0)
                    {
                        // 5페이지 이상은 페이지 더보기 클릭
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 30000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='mArticle']/div/cu-pagination-list/div/div[2]/button[2]"));
                        pageButton.Click();
                    }
                    else if (xyi % 10 == 1)
                    {
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 50000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='mArticle']/div/cu-pagination-list/div/div[2]/div/button[1]"));
                        pageButton.Click();
                    }
                    else if (xyi % 10 == 2)
                    {
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 50000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='mArticle']/div/cu-pagination-list/div/div[2]/div/button[2]"));
                        pageButton.Click();
                    }
                    else if (xyi % 10 == 3)
                    {
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 50000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='mArticle']/div/cu-pagination-list/div/div[2]/div/button[3]"));
                        pageButton.Click();
                    }
                    else if (xyi % 10 == 4)
                    {
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 50000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='mArticle']/div/cu-pagination-list/div/div[2]/div/button[4]"));
                        pageButton.Click();
                    }
                    else if (xyi % 10 == 5)
                    {
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 50000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='mArticle']/div/cu-pagination-list/div/div[2]/div/button[5]"));
                        pageButton.Click();
                    }
                    else if (xyi % 10 == 6)
                    {
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 50000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='mArticle']/div/cu-pagination-list/div/div[2]/div/button[6]"));
                        pageButton.Click();
                    }
                    else if (xyi % 10 == 7)
                    {
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 50000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='mArticle']/div/cu-pagination-list/div/div[2]/div/button[7]"));
                        pageButton.Click();
                    }
                    else if (xyi % 10 == 8)
                    {
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 50000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='mArticle']/div/cu-pagination-list/div/div[2]/div/button[8]"));
                        pageButton.Click();
                    }
                    else if (xyi % 10 == 9)
                    {
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 50000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='mArticle']/div/cu-pagination-list/div/div[2]/div/button[9]"));
                        pageButton.Click();
                    }
                    chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles.Last());
                    Delay(500);
                    ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 30000)");
                    Delay(3000);
                    #endregion

                    if (TTL_RowH > Convert.ToInt32(txt_Rank_End.Text))
                    {
                        break;
                    }
                }
                chromeDriver.Navigate().Back();
                Console.WriteLine("구글페이지로이동 " + TTL_RowH);
            }

        End_Loop:
            Console.WriteLine("작업을 종료합니다.");
        }


        public void Selenium_네이버_backup(string xUrl)
        {
            xUrl = "https://search.shopping.naver.com/search/category/" + xUrl;
            this.Selenium_Paging(xUrl);

            WebDriverWait wait = null;
            if (Process_Stop == true) { goto End_Loop; }
            string LogInfo;
            try
            {
                LogInfo = SSplit(SSplit(chromeDriver.PageSource, "<span class='gnb_name' id='gnb_name1'>", 1), "</span>", 0);
            }
            catch
            {
                LogInfo = "";
            }
            //if (LogInfo.Length == 0)
            //{
            //    Key_WorkState = true;
            //    chromeDriver.Navigate().GoToUrl("https://www.naver.com/");
            //    wait = new WebDriverWait(chromeDriver, new TimeSpan(0, 0, 5));
            //    Delay(1000);
            //    _Delay_Pause(10);
            //    if (Process_Stop == true) { goto End_Loop; }

            //    AutoClosingMessageBox.Show("회원가입 후 프로그램을 다시 실행하세요!", "Auto PR  [메세지 자동종료]", 3000);
            //    richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "     ▶ 네이버 로그인안됨" + "\n" + richTextBox1.Text;
            //    return;
            //}
            //else if (chromeDriver.PageSource.IndexOf("찾으시는 결과가 없네요.") > -1)
            //{
            //    Key_WorkState = true;
            //    richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "     → 상품없음" + "\n" + richTextBox1.Text;
            //    goto End_Loop;
            //}

            if (LogInfo.Length >= 0)
            {
                if (LogInfo.Length == 0)
                {
                    chromeDriver.Navigate().GoToUrl("https://nid.naver.com/nidlogin.login");
                    richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "   [수동으로 로그인 하세요!]" + "\n" + richTextBox1.Text;
                    Source = "네이버 로그인 PASS";

                    Delay(1000 * 5);
                    chromeDriver.Navigate().GoToUrl(xUrl);
                }

                int Page_No = 0;
                int SelectNum = 0;
                int CheckNum = 0;
                int Scroll_End = 0;
                int TTL_RowH = 0;

                string LastID = "";
                bool StartPOS = true;
                bool Page_End = false;

                int Work_Page = 0;
                if (Convert.ToInt32(txt_Rank_Start.Text) < Convert.ToInt32(txt_WorkNum.Text))
                {
                    Work_Page = 1 + Convert.ToInt32(txt_WorkNum.Text) / 40;
                }
                else
                {
                    Work_Page = 1 + Convert.ToInt32(txt_Rank_Start.Text) / 40;
                }
                #region  // 시작페이지를 40개 단위로 페이지 이동 영역 =============
                if (Work_Page < 11)
                {
                    if (Work_Page == 1)
                    {
                        // PASS
                    }
                    else if (Work_Page % 10 == 2)
                    {
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='content']/div[1]/div[4]/div/a[1]"));
                        pageButton.Click();
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = "41";
                        }
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                    }
                    else if (Work_Page % 10 == 3)
                    {
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='content']/div[1]/div[4]/div/a[2]"));
                        pageButton.Click();
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = "81";
                        }
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                    }
                    else if (Work_Page % 10 == 4)
                    {
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='content']/div[1]/div[4]/div/a[3]"));
                        pageButton.Click();
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = "121";
                        }
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                    }
                    else if (Work_Page % 10 == 5)
                    {
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='content']/div[1]/div[4]/div/a[4]"));
                        pageButton.Click();
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = "161";
                        }
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                    }
                    else if (Work_Page % 10 == 6)
                    {
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='content']/div[1]/div[4]/div/a[5]"));
                        pageButton.Click();
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = "201";
                        }
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                    }
                    else if (Work_Page % 10 == 7)
                    {
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='content']/div[1]/div[4]/div/a[6]"));
                        pageButton.Click();
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = "241";
                        }
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                    }
                    else if (Work_Page % 10 == 8)
                    {
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='content']/div[1]/div[4]/div/a[7]"));
                        pageButton.Click();
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = "281";
                        }
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                    }
                    else if (Work_Page % 10 == 9)
                    {
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='content']/div[1]/div[4]/div/a[8]"));
                        pageButton.Click();
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = "321";
                        }
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                    }
                    else if (Work_Page % 10 == 0)
                    {
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='content']/div[1]/div[4]/div/a[9]"));
                        pageButton.Click();
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = "361";
                        }
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                    }
                }
                else
                {
                    // 10페이지 이상은 페이지 더보기 클릭
                    for (int xyk = 0; xyk < (Work_Page / 10); xyk++)
                    {
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='content']/div[1]/div[4]/a"));
                        pageButton.Click();

                        chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles.Last());
                        Delay(500);
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                        Delay(4000);
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = (Work_Page * 40).ToString() + "1";
                        }
                    }

                    // 추가페이지 필요
                    if (Work_Page % 10 == 2)
                    {
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='content']/div[1]/div[4]/div/a[1]"));
                        pageButton.Click();
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = (Work_Page * 60).ToString() + "1";
                        }
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                    }
                    else if (Work_Page % 10 == 3)
                    {
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='content']/div[1]/div[4]/div/a[1]"));
                        pageButton.Click();
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = (Work_Page * 60).ToString() + "1";
                        }
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                    }
                    else if (Work_Page % 10 == 4)
                    {
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='content']/div[1]/div[4]/div/a[1]"));
                        pageButton.Click();
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = (Work_Page * 60).ToString() + "1";
                        }
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                    }
                    else if (Work_Page % 10 == 5)
                    {
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='content']/div[1]/div[4]/div/a[1]"));
                        pageButton.Click();
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = (Work_Page * 60).ToString() + "1";
                        }
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                    }
                    else if (Work_Page % 10 == 6)
                    {
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='content']/div[1]/div[4]/div/a[1]"));
                        pageButton.Click();
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = (Work_Page * 60).ToString() + "1";
                        }
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                    }
                    else if (Work_Page % 10 == 7)
                    {
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='content']/div[1]/div[4]/div/a[1]"));
                        pageButton.Click();
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = (Work_Page * 60).ToString() + "1";
                        }
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                    }
                    else if (Work_Page % 10 == 8)
                    {
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='content']/div[1]/div[4]/div/a[1]"));
                        pageButton.Click();
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = (Work_Page * 60).ToString() + "1";
                        }
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                    }
                    else if (Work_Page % 10 == 9)
                    {
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='content']/div[1]/div[4]/div/a[1]"));
                        pageButton.Click();
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = (Work_Page * 60).ToString() + "1";
                        }
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                    }
                    else if (Work_Page % 10 == 0)
                    {
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='content']/div[1]/div[4]/div/a[1]"));
                        pageButton.Click();
                        if (txt_WorkNum.Text == "1")
                        {
                            txt_WorkNum.Text = (Work_Page * 60).ToString() + "1";
                        }
                        for (int xi = 0; xi < 5; xi++)
                        {
                            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 10000)");
                            Delay(new Random().Next(200, 300));
                        }
                    }
                }
                for (int xi = 0; xi < 5; xi++)
                {
                    ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, -10000)");
                    Delay(new Random().Next(200, 300));
                }
                Delay(new Random().Next(3000, 3500));
                #endregion

                for (int xyi = Work_Page; xyi <= 100; xyi++)
                {
                    int RowH = 0;
                    Page_No = xyi;
                    Page_End = false;
                    SelectNum = 0;

                    for (int xyj = 1; xyj <= 40; xyj++)
                    {
                        //TTL_RowH = ((xyi - 1) * 60) + xyj;
                        #region  // 리스트 40개 작업 영역 ======================================================
                        bool _Prod_Detail = false;
                        bool LastPOS = false;
                        int Re_RowH = 1;

                        RowH = Convert.ToInt32((xyi - 1) * 40) + xyj;

                        _Delay_Pause(10);
                        if (Process_Stop == true) { goto End_Loop; }
                        //wait = new WebDriverWait(chromeDriver, TimeSpan.FromSeconds(20));
                        //IWebElement _SearchResult = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector("a[data-nclick='*']")));

                        string Source = chromeDriver.PageSource;
                        int cxRank = 0;
                        IList<IWebElement> he01 = chromeDriver.FindElements(By.TagName("a"));
                        // 새로 열려는데 엘리먼트가 없으면 리플래시 를 다시 하는 것 검토
                        foreach (IWebElement helem1 in he01)
                        {
                            if (helem1.GetAttribute("data-nclick") != null)
                            {
                                if (helem1.GetAttribute("data-nclick") == "https://adcr.naver.com")
                                {
                                    int uxx = helem1.Location.X;
                                    WebDoc_Window_ScrollTo(chromeDriver, uxx, helem1.Location.Y + Convert.ToInt32(txt_Scroll.Text));
                                    Delay(Convert.ToInt32(cbx_ScrollSec.Text));
                                }
                                else
                                {
                                    //Console.WriteLine(helem1.GetAttribute("data-nclick") + "  //  r:" + RowH);
                                    if (helem1.GetAttribute("data-nclick").IndexOf("r:" + RowH) > -1)
                                    {
                                        string T_Source = @"""rank"":" + SSplit(Source, @"""rank"":", RowH);
                                        if (Process_Stop == true) { goto End_Loop; }

                                        int uxx = helem1.Location.X;
                                        WebDoc_Window_ScrollTo(chromeDriver, uxx, helem1.Location.Y + Convert.ToInt32(txt_Scroll.Text));
                                        Delay(Convert.ToInt32(cbx_ScrollSec.Text));

                                        string cID = helem1.GetAttribute("data-i");
                                        helem1.Click();

                                        db_Rank = RowH;
                                        txt_CheckingRank.Text = RowH.ToString();
                                        txt_WorkNum.Text = TTL_RowH.ToString();
                                        Delay(new Random().Next(5000, 6000));

                                        chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles.Last());
                                        Source = chromeDriver.PageSource;
                                        if (chromeDriver.Url.IndexOf("catalog") > -1)
                                        {
                                            // 가격비교 상품
                                            // 10개 상점 중 스마트스토어 리스트만 가져오기 (판매량 프로그램 재확인)
                                            // 스마트스토어 리스트 중 상세페이지 로 들어가기
                                            string asd = SSplit(Source, @"""mallCount"":""", 1);
                                            string asd1 = SSplit(asd, @"""", 0);
                                            if (Convert.ToInt32(SSplit(SSplit(Source, @"""mallCount"":""", 1), @"""", 0)) > 0)
                                            {
                                                int mallCount = Convert.ToInt32(SSplit(SSplit(Source, @"""mallCount"":""", 1), @"""", 0));
                                                int xPage = 1 + mallCount / 10;
                                                cxRank = 0;

                                                string[] List_ProductLink = new string[50];
                                                int xLRL = 0;
                                                int[] xCRank = new int[50];
                                                for (int xi = 1; xi <= xPage; xi++)
                                                {
                                                    string sURL2 = "https://search.shopping.naver.com/catalog/" + cID + "?page=" + xi;

                                                    WinHttpRequest WinHttp = new WinHttpRequest();
                                                    WinHttp.Open("GET", sURL2);
                                                    WinHttp.SetRequestHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/118.0.0.0 Whale/3.23.214.10 Safari/537.36");
                                                    WinHttp.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded;Charset=euc-kr;");
                                                    //WinHttp.SetRequestHeader("Referer", chromeDriver.Url);
                                                    WinHttp.Send();
                                                    WinHttp.WaitForResponse();
                                                    string responseText = WinHttp.ResponseText;
                                                    responseText = responseText.Replace(@"\", "");
                                                    responseText = responseText.Replace("u002F", "/");
                                                    responseText = responseText.Replace("u0026", "&");
                                                    responseText = responseText.Replace("u003E", ">");
                                                    string ssSource = SSplit(responseText, "서울특별시 강남구 테헤란로 142", 1);
                                                    Delay(new Random().Next(1000, 1500));

                                                    ssSource = SSplit(ssSource, @"{""param"":{""nvMid"":""" + cID + @""",""sort"":""LOW_PRICE""", 2);
                                                    //ssSource = SSplit(ssSource, "{""param"":{""nvMid"":""" + cRowH + """,""sort"":""LOW_PRICE""")(2)

                                                    Console.WriteLine(Convert.ToInt32(UBound_int(ssSource, @"{""nvMid"":""")));
                                                    for (int xj = 1; xj < Convert.ToInt32(UBound_int(ssSource, @"{""nvMid"":""")); xj++)
                                                    {
                                                        if (SSplit(ssSource, @"{""nvMid"":""", xj).IndexOf("smartstore.naver.com") > -1)
                                                        {
                                                            //각 페이지에서 스마트스토어 정보 추출
                                                            List_ProductLink[xLRL] = SSplit(SSplit(ssSource, @"""mobileProductUrl"":""", xj), @"""", 0);
                                                            xCRank[xLRL] = (1 - xi) * 10 + xj;

                                                            // 해당 스토어 상세정보 가져오기
                                                            string sURL3 = List_ProductLink[xLRL];

                                                            WinHttpRequest WinHttp3 = new WinHttpRequest();
                                                            WinHttp3.Open("GET", sURL3);
                                                            WinHttp3.SetRequestHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/118.0.0.0 Whale/3.23.214.10 Safari/537.36");
                                                            WinHttp3.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded;Charset=euc-kr;");
                                                            WinHttp3.Send();
                                                            WinHttp3.WaitForResponse();
                                                            string responseText3 = WinHttp3.ResponseText;
                                                            responseText3 = responseText3.Replace(@"\", "");
                                                            responseText3 = responseText3.Replace("u002F", "/");
                                                            responseText3 = responseText3.Replace("u0026", "&");
                                                            responseText3 = responseText3.Replace("u003E", ">");
                                                            string sSource3 = SSplit(responseText3, @"""title"":""묻고 답하기""", 1);
                                                            Delay(new Random().Next(1000, 1500));

                                                            Source = sSource3;
                                                            db_ProductImage = SSplit(SSplit(SSplit(Source, @"""productImages"":[", 1), @"""url"":""", 1), @"""", 0);
                                                            db_ProductCate = SSplit(SSplit(SSplit(Source, @"channelProductDisplayStatusType", 1), @"""wholeCategoryName"":""", 1), @"""", 0);
                                                            db_StoreName = SSplit(SSplit(Source, @"""channelName"":""", 1), @"""", 0);
                                                            db_ProductName = SSplit(SSplit(SSplit(Source, @"""notice"":{", 1), @"""name"":""", 1), @"""", 0);
                                                            db_SellerName = "";
                                                            db_SellerName = SSplit(SSplit(Source, @"""representName"":""", 1), @"""", 0);
                                                            string cSUrl = SSplit(SSplit(Source, @"""channelSiteUrl"":""", 1), @"""", 0);
                                                            db_ProductLink = SSplit(SSplit(Source, @"""productUrl"":""", 1), @"""", 0);
                                                            db_ProductLink = db_ProductLink.Replace("main", cSUrl);

                                                            db_CateRank = xCRank[xLRL];
                                                            db_RepresentativeName = SSplit(SSplit(Source, @"""representativeName"":""", 1), @"""", 0);
                                                            db_CallNumber = "";
                                                            db_Address = "";
                                                            db_Email = "";
                                                            db_ChannelUid = SSplit(SSplit(Source, @"""channelUid"":""", 1), @"""", 0);

                                                            string channelID = db_ChannelUid;
                                                            string sURL4 = "https://smartstore.naver.com/i/v2/channels/" + channelID + "/visit";
                                                            WinHttp3 = new WinHttpRequest();
                                                            WinHttp3.Open("GET", sURL4);
                                                            WinHttp3.SetRequestHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/118.0.0.0 Whale/3.23.214.10 Safari/537.36");
                                                            WinHttp3.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded;Charset=euc-kr;");
                                                            WinHttp3.Send();
                                                            WinHttp3.WaitForResponse();
                                                            responseText3 = WinHttp3.ResponseText;
                                                            Delay(new Random().Next(1000, 1500));

                                                            db_Vtoday = Convert.ToInt32(SSplit(SSplit(responseText3, @"today"":", 1), @",", 0));
                                                            db_Vtotal = Convert.ToInt32(SSplit(SSplit(responseText3, @"total"":", 1), @"}", 0));


                                                            try
                                                            {
                                                                string inSQL = "INSERT PROMOTION_WorkData ";
                                                                inSQL += "( PACT_UserID, PACT_Market, PACT_Search_Keyword, PACT_Search_Rank, PACT_Cate_Rank, PACT_StoreName, PACT_ProductID, PACT_ProductLink, PACT_ProductCate, PACT_ProductName, PACT_InputTime, PACT_ProductImage, PACT_SellerName, PACT_CallNumber, PACT_Address, PACT_RepresentativeName, PACT_Email, PACT_ChannelUid, PACT_Visit_Total, PACT_Visit_Today ";
                                                                inSQL += " ) VALUES ( ";
                                                                inSQL += " '" + p_UserID + "', ";
                                                                inSQL += " '" + db_Market + "', ";
                                                                inSQL += " '" + db_SKeyword + "', ";
                                                                inSQL += "  " + db_Rank + " , ";
                                                                inSQL += "  " + db_CateRank + " , ";
                                                                inSQL += " '" + db_StoreName + "', ";
                                                                inSQL += " '" + db_ProductID + "', ";
                                                                inSQL += " '" + db_ProductLink + "', ";
                                                                inSQL += " '" + db_ProductCate + "', ";
                                                                inSQL += " '" + db_ProductName + "', ";
                                                                inSQL += " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', ";
                                                                inSQL += " '" + db_ProductImage + "', ";
                                                                inSQL += " '" + db_SellerName + "', ";
                                                                inSQL += " '" + db_CallNumber + "', ";
                                                                inSQL += " '" + db_Address + "', ";
                                                                inSQL += " '" + db_RepresentativeName + "', ";
                                                                inSQL += " '" + db_Email + "', ";
                                                                inSQL += " '" + db_ChannelUid + "', ";
                                                                inSQL += "  " + db_Vtotal + " , ";
                                                                inSQL += "  " + db_Vtoday + "   ";
                                                                inSQL += "  ) ";
                                                                ADONet_Conn.Open();
                                                                SqlCommand cmd0_1 = new SqlCommand();
                                                                cmd0_1.Connection = ADONet_Conn;
                                                                cmd0_1.CommandText = inSQL;
                                                                cmd0_1.ExecuteNonQuery();
                                                                ADONet_Conn.Close();

                                                                txt_Work_Count.Text = (Convert.ToInt32(ImportNumbersOnly(txt_Work_Count.Text)) + 1).ToString();
                                                            }
                                                            catch
                                                            {
                                                                try { ADONet_Conn.Close(); }
                                                                catch { }
                                                                string inSQL = "INSERT PROMOTION_WorkData ";
                                                                inSQL += "( PACT_UserID, PACT_Market, PACT_Search_Keyword, PACT_Search_Rank, PACT_Cate_Rank, PACT_StoreName, PACT_ProductID, PACT_ProductLink, PACT_ProductCate, PACT_ProductName, PACT_InputTime, PACT_ProductImage, PACT_SellerName, PACT_CallNumber, PACT_Address, PACT_RepresentativeName, PACT_Email, PACT_ChannelUid, PACT_Visit_Total, PACT_Visit_Today ";
                                                                inSQL += " ) VALUES ( ";
                                                                inSQL += " '" + p_UserID + "', ";
                                                                inSQL += " '" + db_Market + "', ";
                                                                inSQL += " '" + db_SKeyword + "', ";
                                                                inSQL += "  " + db_Rank + " , ";
                                                                inSQL += "  " + db_CateRank + " , ";
                                                                inSQL += " '" + db_StoreName + "', ";
                                                                inSQL += " '" + db_ProductID + "', ";
                                                                inSQL += " '" + db_ProductLink + "', ";
                                                                inSQL += " '" + db_ProductCate + "', ";
                                                                inSQL += " '" + db_ProductName + "', ";
                                                                inSQL += " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', ";
                                                                inSQL += " '" + db_ProductImage + "', ";
                                                                inSQL += " '" + db_SellerName + "', ";
                                                                inSQL += " '" + db_CallNumber + "', ";
                                                                inSQL += " '" + db_Address + "', ";
                                                                inSQL += " '" + db_RepresentativeName + "', ";
                                                                inSQL += " '" + db_Email + "', ";
                                                                inSQL += " '" + db_ChannelUid + "', ";
                                                                inSQL += "  " + db_Vtotal + " , ";
                                                                inSQL += "  " + db_Vtoday + "   ";
                                                                inSQL += "  ) ";
                                                                ADONet_Conn.Open();
                                                                SqlCommand cmd0_1 = new SqlCommand();
                                                                cmd0_1.Connection = ADONet_Conn;
                                                                cmd0_1.CommandText = inSQL;
                                                                cmd0_1.ExecuteNonQuery();
                                                                ADONet_Conn.Close();
                                                            }

                                                            try
                                                            {
                                                                richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "     → " + TTL_RowH + "r  문의완료 : " + db_SellerName + "_" + db_ProductName.Substring(0, 6) + "\n" + richTextBox1.Text;
                                                            }
                                                            catch
                                                            {
                                                                richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "     → " + TTL_RowH + "r  문의완료 : " + db_SellerName + "_" + db_ProductName + "\n" + richTextBox1.Text;
                                                            }

                                                            xLRL += 1;
                                                        }
                                                    }
                                                }

                                                // 링크리스트 받아왔으므로 페이지정보 가져오기
                                                for (int xk = 0; xk < xLRL; xk++)
                                                {


                                                }

                                                // 톡톡 문의하기 클릭



                                            }

                                            chromeDriver.Close();
                                            chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles.Last());
                                            Delay(new Random().Next(500, 600));


                                        }
                                        else
                                        {
                                            // 단일 상품 상세페이지로 들어가기
                                            // -------------------------------------------------------------------------추가 필요 영역



                                            chromeDriver.Close();
                                            chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles.Last());
                                            Delay(new Random().Next(500, 600));
                                        }






                                        // 상세페이지에 무조건 들어간 뒤
                                        // 방문자수 수집
                                        // 판매자 상호, 대표자명 수집
                                    }
                                }
                            }
                        }
                        #endregion

                        if (TTL_RowH > Convert.ToInt32(txt_Rank_End.Text))
                        {
                            break;
                        }
                        if (Page_End == true)
                        {
                            break;
                        }
                    }

                    #region  // 40개 단위로 페이지 이동 영역 =============
                    if (xyi % 10 == 0)
                    {
                        // 5페이지 이상은 페이지 더보기 클릭
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 30000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='mArticle']/div/cu-pagination-list/div/div[2]/button[2]"));
                        pageButton.Click();
                    }
                    else if (xyi % 10 == 1)
                    {
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 50000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='mArticle']/div/cu-pagination-list/div/div[2]/div/button[1]"));
                        pageButton.Click();
                    }
                    else if (xyi % 10 == 2)
                    {
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 50000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='mArticle']/div/cu-pagination-list/div/div[2]/div/button[2]"));
                        pageButton.Click();
                    }
                    else if (xyi % 10 == 3)
                    {
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 50000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='mArticle']/div/cu-pagination-list/div/div[2]/div/button[3]"));
                        pageButton.Click();
                    }
                    else if (xyi % 10 == 4)
                    {
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 50000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='mArticle']/div/cu-pagination-list/div/div[2]/div/button[4]"));
                        pageButton.Click();
                    }
                    else if (xyi % 10 == 5)
                    {
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 50000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='mArticle']/div/cu-pagination-list/div/div[2]/div/button[5]"));
                        pageButton.Click();
                    }
                    else if (xyi % 10 == 6)
                    {
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 50000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='mArticle']/div/cu-pagination-list/div/div[2]/div/button[6]"));
                        pageButton.Click();
                    }
                    else if (xyi % 10 == 7)
                    {
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 50000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='mArticle']/div/cu-pagination-list/div/div[2]/div/button[7]"));
                        pageButton.Click();
                    }
                    else if (xyi % 10 == 8)
                    {
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 50000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='mArticle']/div/cu-pagination-list/div/div[2]/div/button[8]"));
                        pageButton.Click();
                    }
                    else if (xyi % 10 == 9)
                    {
                        ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 50000)");
                        var pageButton = chromeDriver.FindElement(By.XPath("//*[@id='mArticle']/div/cu-pagination-list/div/div[2]/div/button[9]"));
                        pageButton.Click();
                    }
                    chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles.Last());
                    Delay(500);
                    ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollBy(0, 30000)");
                    Delay(3000);
                    #endregion

                    if (TTL_RowH > Convert.ToInt32(txt_Rank_End.Text))
                    {
                        break;
                    }
                }
                chromeDriver.Navigate().Back();
                Console.WriteLine("구글페이지로이동 " + TTL_RowH);
            }

        End_Loop:
            Console.WriteLine("작업을 종료합니다.");
        }




        private void btn_ManualStart_Click(object sender, EventArgs e)
        {
            try { ADONet_Conn.Close(); }
            catch { }

            if (Convert.ToInt32(txt_Rank_Start.Text) > Convert.ToInt32(txt_Rank_End.Text))
            {
                txt_Rank_End.Text = txt_Rank_Start.Text;
            }

            Process_Stop = false;
            PGM_Start_Manual = true;
            btn_ManualStart.Enabled = false;
            if (chk_CateSelect.Checked == false)
            {
                this.Sel_ListView_Reset();
            }

            AutoClosingMessageBox.Show("테스트 작업은 홍보글을 절대 등록하지 않습니다.", "메세지박스 자동종료창", 3000);

            if (btn_PGMStart.Text == "프로그램 시작")
            {
                clib_Channel.BackColor = Color.WhiteSmoke;
                clib_Channel_Num.BackColor = Color.WhiteSmoke;
                clib_Cate1.BackColor = Color.WhiteSmoke;
                clib_Cate2.BackColor = Color.WhiteSmoke;
                clib_Cate3.BackColor = Color.WhiteSmoke;
                clib_Cate4.BackColor = Color.WhiteSmoke;
                //clib_Channel.Enabled = false;
                //clib_Channel_Num.Enabled = false;
                //clib_N_Cate1.Enabled = false; 

                if (chk_CateSelect.Checked == false)
                {
                    clib_Cate2.Items.Clear();
                    clib_Cate3.Items.Clear();
                    clib_Cate4.Items.Clear();
                }
                else
                {

                }

                //clib_N_Cate2.Enabled = false;
                //clib_N_Cate3.Enabled = false;
                //clib_N_Cate4.Enabled = false;

                //btn_ManualStart.Enabled = false;    // enabled=false 는 선택 해제가 되므로 사용하면 안됨

                //Image img = Image.FromFile("");
                //this.picbox_NonSel.Image = ChangeOpacity(img, 30);

                timer_PGMStart_Auto.Interval = 1000 * 2;    // 최초에 2초 후에 내상품 프로그램 실행
                timer_PGMStart_Auto.Start();
            }
            else if (btn_PGMStart.Text == "다시 시작")
            {
                richTextBox1.Text = DateTime.Now.ToString("MM-dd HH:mm:ss") + "   [다시 시작]";
                timer_Loop.Interval = 1000 * 2;    // 최초에 1초 후에 내상품 프로그램 실행
                timer_Loop.Start();
            }
        }

        private void Opacity(ref Bitmap image, int opacity)
        {
            for (int w = 0; w < image.Width; w++)
            {
                for (int h = 0; h < image.Height; h++)
                {
                    Color c = image.GetPixel(w, h);
                    if (c != Color.Transparent) /*<- it only change colours different than transparency color.*/
                    {
                        Color newC = Color.FromArgb(c.A * (opacity / 100), c.R * (opacity / 100), c.G * (opacity / 100), c.B * (opacity / 100)); /*<- this gives real opacity.*/
                        image.SetPixel(w, h, newC);
                    }
                }
            }
        }

        public Bitmap ChangeOpacity(Image img, float opacityvalue)
        {
            Bitmap bmp = new Bitmap(img.Width, img.Height);
            Graphics graphics = Graphics.FromImage(bmp);
            ColorMatrix colormatrix = new ColorMatrix();
            colormatrix.Matrix33 = opacityvalue;
            ImageAttributes imgAttribute = new ImageAttributes();
            imgAttribute.SetColorMatrix(colormatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            graphics.DrawImage(img, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imgAttribute);
            graphics.Dispose();
            return bmp;
        }

        private void chk_KeyTest_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_KeyTest.Checked == true)
            {
                txt_TestKeyword.Enabled = true;
                //txt_TestKeyword.Text = "";
                txt_TestKeyword.Focus();
            }
            else
            {
                txt_TestKeyword.Enabled = false;
                txt_TestKeyword.Text = "티셔츠";
            }
        }


        public static int interval { get; set; }
        public static int checkcount { get; set; }

        public enum SearchType
        {
            Class,
            Name,
            ID,
            XPath,
            TagName
        }
        public static bool OnLoadWait(IWebDriver drv, string url, SearchType type, string name)
        {
            interval = 1000;
            checkcount = 60;

            int i = 0;
            while (i < checkcount)
            {
                if (drv.Url.Contains(url))
                {
                    try
                    {
                        if (type == SearchType.Class)
                            drv.FindElement(By.ClassName(name));
                        else if (type == SearchType.ID)
                            drv.FindElement(By.Id(name));
                        else if (type == SearchType.XPath)
                            drv.FindElement(By.XPath(name));
                        else if (type == SearchType.Name)
                            drv.FindElement(By.Name(name));
                        else if (type == SearchType.TagName)
                            drv.FindElement(By.Name(name));

                        return true;
                    }
                    catch
                    {

                    }
                }
                Thread.Sleep(interval);
                i++;
            }
            return false;

        }

        private void chk_CateSelect_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_CateSelect.Checked == false)
            {
                clib_Cate2.Items.Clear();
                clib_Cate3.Items.Clear();
                clib_Cate4.Items.Clear();

                clib_Cate2.BackColor = Color.WhiteSmoke;
                clib_Cate3.BackColor = Color.WhiteSmoke;
                clib_Cate4.BackColor = Color.WhiteSmoke;

                //clib_N_Cate2.Enabled = false;
                //clib_N_Cate3.Enabled = false;
                //clib_N_Cate4.Enabled = false;
            }
            else
            {
                clib_Cate2.BackColor = Color.White;
                clib_Cate3.BackColor = Color.White;
                clib_Cate4.BackColor = Color.White;

                this.clib_ALL_Cate1_DB_Read();    // DB 읽어오기
                clib_Cate2.SetItemChecked(0, true);
                clib_Cate2.SetSelected(0, true);
                this.clib_ALL_Cate2_DB_Read();    // DB 읽어오기
                clib_Cate3.SetItemChecked(0, true);
                clib_Cate3.SetSelected(0, true);
                this.clib_ALL_Cate3_DB_Read();    // DB 읽어오기
                try
                {
                    clib_Cate4.SetItemChecked(0, true);
                    clib_Cate4.SetSelected(0, true);
                }
                catch { }
            }
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            //this.Selenium_Browser_사이즈자동변경();
            //return;

            //timer_Load.Start();
            string channel_item = clib_Channel.SelectedItem.ToString();
            if (channel_item.IndexOf("오늘의집") > -1)
            {
                string xUrl = "https://ohou.se/store";
                Key_WorkState = false;
                this.Selenium_Paging(xUrl);
                //this.Selenium_오늘의집(xUrl);
            }
            else if (channel_item.IndexOf("카카오쇼핑") > -1)
            {
                string xUrl = "https://store.kakao.com";
                Key_WorkState = false;
                this.Selenium_Paging(xUrl);
            }
            else if (channel_item.IndexOf("쿠팡") > -1)
            {
                string xUrl = "https://www.coupang.com";
                Key_WorkState = false;
                this.Selenium_Paging(xUrl);
            }
            else if (channel_item.IndexOf("네이버") > -1)
            {
                string xUrl = "https://www.naver.com";
                Key_WorkState = false;
                this.Selenium_Paging(xUrl);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string SelText = "";
            try
            {
                SelText = clib_Cate4.SelectedItem.ToString();
            }
            catch
            {
                try
                {
                    SelText = clib_Cate3.SelectedItem.ToString();
                }
                catch
                {
                    try
                    {
                        SelText = clib_Cate2.SelectedItem.ToString();
                    }
                    catch
                    {

                    }
                }
            }
            string xUrl = "https://ohou.se/productions/feed?query=" + SelText + "&search_affect_type=Typing";
            this.Selenium_Paging(xUrl);

            WebDriverWait wait = null;
            int RowH = 1;

            try
            {
                while (RowH <= Convert.ToInt32(txt_Rank_End.Text))
                {
                    //int xRowH = 1;
                    //try
                    //{
                    //    var ProdList = chromeDriver.FindElement(By.CssSelector("[href$='?affect_id=" + RowH + "&affect_type=StoreSearchResult']"));
                    //}
                    //catch
                    //{
                    //    //((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 0)");
                    //    while (xRowH <= RowH)
                    //    {
                    //        try
                    //        {
                    //            var ProdList = chromeDriver.FindElement(By.CssSelector("[href$='?affect_id=" + xRowH + "&affect_type=StoreSearchResult']"));
                    //            int uxx = ProdList.Location.X;
                    //            WebDoc_Window_ScrollTo(chromeDriver, uxx, ProdList.Location.Y + Convert.ToInt32(txt_Scroll.Text));
                    //            _Delay_Pause(100);
                    //        }
                    //        catch { }

                    //        xRowH += 1;
                    //    }
                    //}


                    wait = new WebDriverWait(chromeDriver, TimeSpan.FromSeconds(10));
                    IWebElement _SearchResult = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector("[href$='?affect_id=" + RowH + "&affect_type=StoreSearchResult']")));
                    IList<IWebElement> he01 = chromeDriver.FindElements(By.TagName("article"));
                    if (he01.Count > 3)
                    {
                        foreach (IWebElement helem1 in he01)
                        {
                            if (helem1.GetAttribute("class") == "production-item")
                            {
                                _Delay_Pause(100);
                                int uxx = helem1.Location.X;
                                WebDoc_Window_ScrollTo(chromeDriver, uxx, helem1.Location.Y + Convert.ToInt32(txt_Scroll.Text));
                                if (RowH % 3 == 0)
                                {
                                    _Delay_Pause(Convert.ToInt32(cbx_ScrollSec.Text));
                                }

                                IList<IWebElement> he02 = helem1.FindElements(By.TagName("a"));
                                foreach (IWebElement helem2 in he02)
                                {
                                    if (helem2.GetAttribute("href") != null)
                                    {
                                        if (helem2.GetAttribute("href").IndexOf("_id=" + RowH + "&") > -1)
                                        {


                                        }

                                        if (RowH >= Convert.ToInt32(txt_WorkNum.Text))       //if (RowH >= Convert.ToInt32(txt_Rank_Start.Text))
                                        {
                                            Console.WriteLine("최종작업 위치로 이동완료");
                                            break;
                                        }

                                        RowH += 1;
                                        Console.WriteLine("이동순위 " + RowH);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            catch { }
        }

        //슬라이딩 메뉴의 최대, 최소 폭 크기 -------------------------------------------------
        const int MAX_SLIDING_WIDTH = 1350;
        const int MIN_SLIDING_WIDTH = 790;
        //슬라이딩 메뉴가 보이는/접히는 속도 조절
        const int STEP_SLIDING = 20;
        //최초 슬라이딩 메뉴 크기
        int _posSliding = 1350;
        int _posLeft = 800;        //1283

        private void checkBoxHide_CheckedChanged(object sender, EventArgs e)
        {
            //if (checkBoxHide.Checked == true)
            //{
            //    checkBoxHide.Text = "<";
            //}
            //else
            //{
            //    //슬라이딩 메뉴가 보였을 때, 메뉴 버튼의 표시
            //    checkBoxHide.Text = ">";
            //}

            //타이머 시작

            timerSliding.Start();
        }

        private void timerSliding_Tick(object sender, EventArgs e)
        {
            if (checkBoxHide.Checked == true)
            {
                //슬라이딩 메뉴를 숨기는 동작
                _posSliding -= STEP_SLIDING;
                //_posLeft += STEP_SLIDING;
                if (_posSliding <= MIN_SLIDING_WIDTH)
                {
                    timerSliding.Stop();

                    checkBoxHide.Text = "<";
                }
            }
            else
            {
                //슬라이딩 메뉴를 보이는 동작
                _posSliding += STEP_SLIDING;
                //_posLeft -= STEP_SLIDING;
                if (_posSliding >= MAX_SLIDING_WIDTH)
                {
                    timerSliding.Stop();

                    //슬라이딩 메뉴가 보였을 때, 메뉴 버튼의 표시
                    checkBoxHide.Text = ">";
                }
            }

            this.Width = _posSliding;
            this.Left = Screen.PrimaryScreen.Bounds.Width - this.Width;
            //panelSideMenu.Left = _posLeft;
        }
        //슬라이딩 메뉴의 최대, 최소 폭 크기 -------------------------------------------------


        private void btn_01_Click(object sender, EventArgs e)
        {
            //string _ReadPath = _Path + @"\P_Docu\사용자메뉴얼.txt";
            //System.Diagnostics.Process.Start("Notepad.exe", _ReadPath);

            SQL = " SELECT *   FROM PROMOTION_POST   WHERE PP_UserID = 'PADMIN' AND PP_Market = '사용설명' AND PP_Market_Dno = 0 ";
            ADONet_Conn_Routine();
            if (RsTbl.Rows.Count > 0)
            {
                string textValue = RsTbl.Rows[0].Field<string>("PP_Post");
                textValue = textValue.Replace("<br>", "\r\n");
                txt_Promotion_Post.Text = textValue;
            }
            ADONet_Conn.Close();
        }

        private void btn_Test_Click(object sender, EventArgs e)
        {
            //txt_WorkKeyword.Text = this.clib_Cate_Check();

            frm_Post _frm_Post = new frm_Post();
            _frm_Post.Show();
        }





    }
}
