using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Data.SqlClient;

namespace AutoPromotion
{
    public partial class _UserSales : Form
    {
        int P_Width = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width;
        int P_Height = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height;

        SqlConnection ADONet_Conn = new SqlConnection(AutoPromotion.cfg.DBConnString);
        public string SQL;
        public SqlDataAdapter adapter = null;
        public SqlCommand RsCmd = null;
        public DataTable RsTbl = null;
        public DataSet customers = null;

        string[,] xData = new string[130000, 15];


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

        #region UBoundSplit Function    // UBound / WeekDay  ==============================================
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

        public string Week_str(DateTime nowDt)
        {
            string T_Dt = "";
            if (nowDt.DayOfWeek == DayOfWeek.Monday)
            {
                //T_Dt = "월";
            }
            else if (nowDt.DayOfWeek == DayOfWeek.Tuesday)
            {
                //T_Dt = "화";
            }
            else if (nowDt.DayOfWeek == DayOfWeek.Wednesday)
            {
                //T_Dt = "수";
            }
            else if (nowDt.DayOfWeek == DayOfWeek.Thursday)
            {
                //T_Dt = "목";
            }
            else if (nowDt.DayOfWeek == DayOfWeek.Friday)
            {
                //T_Dt = "금";
            }
            else if (nowDt.DayOfWeek == DayOfWeek.Saturday)
            {
                T_Dt = "토";
            }
            else if (nowDt.DayOfWeek == DayOfWeek.Sunday)
            {
                T_Dt = "일";
            }
            return T_Dt;
        }
        #endregion


        public _UserSales()
        {
            InitializeComponent();

            Sel_ListView_Reset();
        }

        private void Sel_ListView_Reset()
        {
            liv_Sales_O.Items.Clear();
            liv_Sales_O.Columns.Clear();
            liv_Sales_O.Columns.Add("No", 30, HorizontalAlignment.Center);
            liv_Sales_O.Columns.Add("카테고리", 60, HorizontalAlignment.Center);
            liv_Sales_O.Columns.Add("쇼핑몰명", 60, HorizontalAlignment.Center);
            liv_Sales_O.Columns.Add("URL", 30, HorizontalAlignment.Center);
            liv_Sales_O.Columns.Add("쇼핑몰소개", 30, HorizontalAlignment.Center);
            liv_Sales_O.Columns.Add("몰등급", 50, HorizontalAlignment.Center);
            liv_Sales_O.Columns.Add("상품개수", 50, HorizontalAlignment.Left);
            liv_Sales_O.Columns.Add("사이트연결", 5, HorizontalAlignment.Left);
            liv_Sales_O.Columns.Add("상호명", 100, HorizontalAlignment.Center);
            liv_Sales_O.Columns.Add("대표자명", 100, HorizontalAlignment.Center);
            liv_Sales_O.Columns.Add("사업자정보", 120, HorizontalAlignment.Right);
            liv_Sales_O.Columns.Add("주소", 200, HorizontalAlignment.Center);
            liv_Sales_O.Columns.Add("고객센터", 100, HorizontalAlignment.Center);
            liv_Sales_O.Columns.Add("이메일", 120, HorizontalAlignment.Right);
        }

        private void sListBox_Update()
        {
            DateTime start;             // 시작한 시간
            DateTime end;               // 끝난 시간
            DateTime end_el;            // 경과 시간용
            start = DateTime.Now;       // 시작한 시간 체크

            lbl_Start.Text = DateTime.Now.ToString("HH:mm:ss");
            string xStart = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            lbl_End.Text = "";
            //Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Start();


            Excel.Application ExcelApp = new Excel.Application();
            Excel.Workbook workbook = ExcelApp.Workbooks.Open(Filename: System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\" + txt_A.Text);
            Excel.Worksheet wsheet = workbook.Worksheets.get_Item(1) as Excel.Worksheet;

            ExcelApp.DisplayAlerts = false;
            ExcelApp.Visible = false;
            ExcelApp.ScreenUpdating = false;
            ExcelApp.DisplayStatusBar = false;
            ExcelApp.Calculation = Excel.XlCalculation.xlCalculationManual;
            ExcelApp.EnableEvents = false;

            try
            {
                Excel.Range range = wsheet.UsedRange;    // 사용중인 셀 범위를 가져오기
                int xxi = 0;
                txt_End.Text = range.Rows.Count.ToString("#,##0");
                for (int row = Convert.ToInt32(txt_Start.Text); row <= range.Rows.Count; row++) // 가져온 행 만큼 반복
                {
                    _Delay_Pause(10);
                    if (Process_Stop == true) { goto End_Loop; }

                    int str1 = (int)(range.Cells[row, 1] as Excel.Range).Value2;  // 셀 데이터 가져옴
                    string str2 = "";
                    try
                    {
                        str2 = (string)(range.Cells[row, 2] as Excel.Range).Value2;  // 셀 데이터 가져옴
                        if (str2 == null)
                        {
                            str2 = " ";  // 셀 데이터 가져옴
                        }
                    }
                    catch
                    {
                        str2 = (string)(range.Cells[row, 2] as Excel.Range).Value2.ToString();  // 셀 데이터 가져옴
                    }
                    str2 = str2.Replace("'", "");

                    string str3 = "";
                    try
                    {
                        str3 = (string)(range.Cells[row, 3] as Excel.Range).Value2;  // 셀 데이터 가져옴
                        if (str3 == null)
                        {
                            str3 = " ";  // 셀 데이터 가져옴
                        }
                    }
                    catch
                    {
                        str3 = (string)(range.Cells[row, 3] as Excel.Range).Value2.ToString();  // 셀 데이터 가져옴
                    }
                    str3 = str3.Replace("'", "");

                    string str4 = "";
                    try
                    {
                        str4 = (string)(range.Cells[row, 4] as Excel.Range).Value2;  // 셀 데이터 가져옴
                        if (str4 == null)
                        {
                            str4 = " ";  // 셀 데이터 가져옴
                        }
                    }
                    catch
                    {
                        str4 = (string)(range.Cells[row, 4] as Excel.Range).Value2.ToString();  // 셀 데이터 가져옴
                    }
                    str4 = str4.Replace("'", "");

                    string str5 = "";
                    try
                    {
                        str5 = (string)(range.Cells[row, 5] as Excel.Range).Value2;  // 셀 데이터 가져옴
                        if (str5 == null)
                        {
                            str5 = " ";  // 셀 데이터 가져옴
                        }
                    }
                    catch
                    {
                        str5 = (string)(range.Cells[row, 5] as Excel.Range).Value2.ToString();  // 셀 데이터 가져옴
                    }
                    str5 = str5.Replace("'", "");

                    string str6 = "";
                    try
                    {
                        str6 = (string)(range.Cells[row, 6] as Excel.Range).Value2;  // 셀 데이터 가져옴
                        if (str6 == null)
                        {
                            str6 = " ";  // 셀 데이터 가져옴
                        }
                    }
                    catch
                    {
                        str6 = (string)(range.Cells[row, 6] as Excel.Range).Value2.ToString();  // 셀 데이터 가져옴
                    }
                    str6 = str6.Replace("'", "");

                    int str7 = (int)(range.Cells[row, 7] as Excel.Range).Value2;  // 셀 데이터 가져옴

                    string str8 = "";
                    try
                    {
                        str8 = (string)(range.Cells[row, 8] as Excel.Range).Value2;  // 셀 데이터 가져옴
                        if (str8 == null)
                        {
                            str8 = " ";  // 셀 데이터 가져옴
                        }
                    }
                    catch
                    {
                        str8 = (string)(range.Cells[row, 8] as Excel.Range).Value2.ToString();  // 셀 데이터 가져옴
                    }
                    str8 = str8.Replace("'", "");

                    string str9 = "";
                    try
                    {
                        str9 = (string)(range.Cells[row, 9] as Excel.Range).Value2;  // 셀 데이터 가져옴
                        if (str9 == null)
                        {
                            str9 = " ";  // 셀 데이터 가져옴
                        }
                    }
                    catch
                    {
                        str9 = (string)(range.Cells[row, 9] as Excel.Range).Value2.ToString();  // 셀 데이터 가져옴
                    }
                    str9 = str9.Replace("'", "");
                    string str10 = "";
                    try
                    {
                        str10 = (string)(range.Cells[row, 10] as Excel.Range).Value2;  // 셀 데이터 가져옴
                        if (str10 == null)
                        {
                            str10 = " ";  // 셀 데이터 가져옴
                        }
                    }
                    catch
                    {
                        str10 = (string)(range.Cells[row, 10] as Excel.Range).Value2.ToString();  // 셀 데이터 가져옴
                    }
                    str10 = str10.Replace("'", "");
                    string str11 = "";
                    try
                    {
                        str11 = (string)(range.Cells[row, 11] as Excel.Range).Value2;  // 셀 데이터 가져옴
                        if (str11 == null)
                        {
                            str11 = " ";  // 셀 데이터 가져옴
                        }
                    }
                    catch
                    {
                        str11 = (string)(range.Cells[row, 11] as Excel.Range).Value2.ToString();  // 셀 데이터 가져옴
                    }
                    str11 = str11.Replace("'", "");
                    string str12 = "";
                    try
                    {
                        str12 = (string)(range.Cells[row, 12] as Excel.Range).Value2;  // 셀 데이터 가져옴
                        if (str12 == null)
                        {
                            str12 = " ";  // 셀 데이터 가져옴
                        }
                    }
                    catch
                    {
                        str12 = (string)(range.Cells[row, 12] as Excel.Range).Value2.ToString();  // 셀 데이터 가져옴
                    }
                    str12 = str12.Replace("'", "");
                    string str13 = "";
                    try
                    {
                        str13 = (string)(range.Cells[row, 13] as Excel.Range).Value2;  // 셀 데이터 가져옴
                        if (str13 == null)
                        {
                            str13 = " ";  // 셀 데이터 가져옴
                        }
                    }
                    catch
                    {
                        str13 = (string)(range.Cells[row, 13] as Excel.Range).Value2.ToString();  // 셀 데이터 가져옴
                    }
                    str13 = str13.Replace("'", "");
                    string str14 = "";
                    try
                    {
                        str14 = (string)(range.Cells[row, 14] as Excel.Range).Value2;  // 셀 데이터 가져옴
                        if (str14 == null)
                        {
                            str14 = " ";  // 셀 데이터 가져옴
                        }
                    }
                    catch
                    {
                        str14 = (string)(range.Cells[row, 14] as Excel.Range).Value2.ToString();  // 셀 데이터 가져옴
                    }
                    str14 = str14.Replace("'", "");

                    xData[xxi, 1] = str1.ToString();
                    xData[xxi, 2] = str2.ToString();
                    xData[xxi, 3] = str3.ToString();
                    xData[xxi, 4] = str4.ToString();
                    xData[xxi, 5] = str5.ToString();
                    xData[xxi, 6] = str6.ToString();
                    xData[xxi, 7] = str7.ToString();
                    xData[xxi, 8] = str8.ToString();
                    xData[xxi, 9] = str9.ToString();
                    xData[xxi, 10] = str10.ToString();
                    xData[xxi, 11] = str11.ToString();
                    xData[xxi, 12] = str12.ToString();
                    xData[xxi, 13] = str13.ToString();
                    xData[xxi, 14] = str14.ToString();

                    this.DB_Save(xxi, xStart);
                    //liv_Sales_O.BeginUpdate();
                    //ListViewItem item;
                    //item = new ListViewItem((xxi + 1).ToString());
                    //item.SubItems.Add(str2);
                    //item.SubItems.Add(str3);
                    //item.SubItems.Add(str4);
                    //item.SubItems.Add(str5);
                    //item.SubItems.Add(str6);
                    //item.SubItems.Add(str7.ToString());
                    //item.SubItems.Add(str8);
                    //item.SubItems.Add(str9);
                    //item.SubItems.Add(str10);
                    //item.SubItems.Add(str11);
                    //item.SubItems.Add(str12);
                    //item.SubItems.Add(str13);
                    //item.SubItems.Add(str14);
                    //liv_Sales_O.Items.Add(item);
                    //liv_Sales_O.EndUpdate();
                    xxi += 1;

                    txt_Sales_Excel.Text = "작업행: " + row + "  /  출력: " + xxi.ToString();

                    end_el = DateTime.Now;       // 끝난 시간 체크
                    txt_ElapsedTime.Text = ((TimeSpan)(end_el - start)).ToString();
                    if (xxi % 10 == 0)
                    {
                        System.Windows.Forms.Application.DoEvents();
                        Delay(Convert.ToInt32(txt_Delay.Text));
                        //Delay(1500);
                    }
                }


                ExcelApp.DisplayAlerts = true;
                ExcelApp.ScreenUpdating = true;
                ExcelApp.DisplayStatusBar = true;
                ExcelApp.Calculation = Excel.XlCalculation.xlCalculationAutomatic;
                ExcelApp.EnableEvents = true;
                System.Windows.Forms.Application.DoEvents();

                //workbook.Close();
                //ExcelApp.Quit();
                ExcelApp.Visible = true;
            }
            finally
            {
                ReleaseExcelObject(wsheet);
                ReleaseExcelObject(workbook);
                ReleaseExcelObject(ExcelApp);
            }

            lbl_End.Text = DateTime.Now.ToString("HH:mm:ss");
            //stopwatch.Stop();
            //txt_ElapsedTime.Text = stopwatch.ElapsedMilliseconds.ToString();


            end = DateTime.Now;       // 끝난 시간 체크
            txt_ElapsedTime.Text = ((TimeSpan)(end - start)).Ticks.ToString();  // 총 건린 초 로 표시
            txt_ElapsedTime.Text = ((TimeSpan)(end - start)).ToString();  // 시:분:초 로 표시


        End_Loop:
            Console.WriteLine("작업을 종료합니다.");

            lbl_End.Text = DateTime.Now.ToString("HH:mm:ss");
            //stopwatch.Stop();
            //txt_ElapsedTime.Text = stopwatch.ElapsedMilliseconds.ToString();


            end = DateTime.Now;       // 끝난 시간 체크
            txt_ElapsedTime.Text = ((TimeSpan)(end - start)).Ticks.ToString();  // 총 건린 초 로 표시
            txt_ElapsedTime.Text = ((TimeSpan)(end - start)).ToString();  // 시:분:초 로 표시

        }

        private void DB_Save(int xxi, string xStart)
        {
            string inSQL = "INSERT PROMOTION_WorkData_NaverMall ";
            inSQL += "( PNM_StoreCate, PNM_StoreName, PNM_StoreLink, PNM_StoreDescript, PNM_StoreGrade, PNM_ProdCount, PNM_t1, PNM_SellerName, PNM_RepresentativeName, PNM_SellerComp, PNM_Address, PNM_CallNumber, PNM_Email, PNM_InputTime ";
            inSQL += " ) VALUES ( ";
            inSQL += " '" + xData[xxi, 2] + "', ";
            inSQL += " '" + xData[xxi, 3] + "', ";
            inSQL += " '" + xData[xxi, 4] + "', ";
            inSQL += " '" + xData[xxi, 5] + "', ";
            inSQL += " '" + xData[xxi, 6] + "', ";
            inSQL += " '" + xData[xxi, 7] + "', ";
            inSQL += " '" + xData[xxi, 8] + "', ";
            inSQL += " '" + xData[xxi, 9] + "', ";
            inSQL += " '" + xData[xxi, 10] + "', ";
            inSQL += " '" + xData[xxi, 11] + "', ";
            inSQL += " '" + xData[xxi, 12] + "', ";
            inSQL += " '" + xData[xxi, 13] + "', ";
            inSQL += " '" + xData[xxi, 14] + "', ";
            inSQL += " '" + xStart + "'  ";
            inSQL += "  ) ";
            ADONet_Conn.Open();
            SqlCommand cmd0_1 = new SqlCommand();
            cmd0_1.Connection = ADONet_Conn;
            cmd0_1.CommandText = inSQL;
            cmd0_1.ExecuteNonQuery();
            ADONet_Conn.Close();
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            this.sListBox_Update();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int xxi = 0; xxi < liv_Sales_O.Items.Count; xxi++)
            {
                string inSQL = "INSERT PROMOTION_WorkData_NaverMall ";
                inSQL += "( PNM_StoreCate, PNM_StoreName, PNM_StoreLink, PNM_StoreDescript, PNM_StoreGrade, PNM_ProdCount, PNM_t1, PNM_SellerName, PNM_RepresentativeName, PNM_SellerComp, PNM_Address, PNM_CallNumber, PNM_Email ";
                inSQL += " ) VALUES ( ";
                inSQL += " '" + liv_Sales_O.Items[xxi].SubItems[1].Text + "', ";
                inSQL += " '" + liv_Sales_O.Items[xxi].SubItems[2].Text + "', ";
                inSQL += " '" + liv_Sales_O.Items[xxi].SubItems[3].Text + "', ";
                inSQL += " '" + liv_Sales_O.Items[xxi].SubItems[4].Text + "', ";
                inSQL += " '" + liv_Sales_O.Items[xxi].SubItems[5].Text + "', ";
                inSQL += " '" + liv_Sales_O.Items[xxi].SubItems[6].Text + "', ";
                inSQL += " '" + liv_Sales_O.Items[xxi].SubItems[7].Text + "', ";
                inSQL += " '" + liv_Sales_O.Items[xxi].SubItems[8].Text + "', ";
                inSQL += " '" + liv_Sales_O.Items[xxi].SubItems[9].Text + "', ";
                inSQL += " '" + liv_Sales_O.Items[xxi].SubItems[10].Text + "', ";
                inSQL += " '" + liv_Sales_O.Items[xxi].SubItems[11].Text + "', ";
                inSQL += " '" + liv_Sales_O.Items[xxi].SubItems[12].Text + "', ";
                inSQL += " '" + liv_Sales_O.Items[xxi].SubItems[13].Text + "' ";
                inSQL += "  ) ";
                ADONet_Conn.Open();
                SqlCommand cmd0_1 = new SqlCommand();
                cmd0_1.Connection = ADONet_Conn;
                cmd0_1.CommandText = inSQL;
                cmd0_1.ExecuteNonQuery();
                ADONet_Conn.Close();

                if (xxi % 10 == 0)
                {
                    Delay(1500);
                }
            }
        }

        private static void ReleaseExcelObject(object obj)
        {
            try
            {
                if (obj != null)
                {
                    Marshal.ReleaseComObject(obj);
                    obj = null;
                }
            }
            catch (Exception ex)
            {
                obj = null;
                throw ex;
            }
            finally
            {
                GC.Collect();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            liv_Sales_O.Items.Clear();
        }

        private void btn_ReStart_Click(object sender, EventArgs e)
        {
            Process_Pause = false;
        }

        private void btn_Pause_Click(object sender, EventArgs e)
        {
            Process_Pause = true;
            _Delay_Pause(10);
        }

        private void btn_PGMEnd_Click(object sender, EventArgs e)
        {

            Process_Pause = false;
            Process_Stop = true;
        }
    }
}
