using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoPromotion
{
    public partial class Login : Form
    {
        SqlConnection ADONet_Conn = new SqlConnection(AutoPromotion.cfg.DBConnString);
        public string SQL;
        public SqlDataAdapter adapter = null;
        public SqlCommand RsCmd = null;
        public DataTable RsTbl = null;
        public DataSet customers = null;

        public string AdminYN = "";
        string p_UserID = AutoPromotion.cfg.p_UserID;
        string tVer = AutoPromotion.cfg.tVersion;


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





        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            try { this.exeClear("chromedriver"); }
            catch { }

            System.Diagnostics.Process.GetCurrentProcess().Kill();
            System.Diagnostics.Process[] mProcess = System.Diagnostics.Process.GetProcessesByName("AutoPromotion");
            foreach (System.Diagnostics.Process p in mProcess)
            {
                p.Kill();
            }
            try
            {
                Application.Exit();
            }
            catch { }
        }

        public Login()
        {
            InitializeComponent();

            //this.Text += tVer;
            lblVer.Text = tVer;

            timer1.Start();

            try { this.exeClear("chromedriver"); }
            catch { }

            // ID 가져오기
            RegistryKey reg = Registry.CurrentUser;
            reg = reg.OpenSubKey("Software\\SellerHelper\\", true);
            if (reg != null)
            {
                Object val = reg.GetValue("UserId");
                if (null != val)
                {
                    p_UserID = Convert.ToString(val);
                    txt_ID.Text = p_UserID;
                    if (p_UserID.ToUpper().IndexOf("GEUST") > -1)
                    {
                        txt_PW.Text = "1215";
                    }
                    else if (p_UserID.ToUpper().IndexOf("PADMIN") > -1)
                    {
                        txt_PW.Text = "1215";
                        chk_DB_Handler.Visible = true;
                    }
                    else if (p_UserID.ToUpper().IndexOf("UNIVENT") > -1)
                    {
                        txt_PW.Text = "1234";
                    }
                }
            }
            // 라벨 투명화 처리 ----------------
            lblVer.Parent = pictureBox2;
            lblVer.BackColor = Color.Transparent;
            
            // 불필요한 소스 초기화 ----------------------------------
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
            // 불필요한 소스 초기화 ----------------------------------
        }

        private void btn_Log_Click(object sender, EventArgs e)
        {
            Login_Check();
        }

        private void txt_PW_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Login_Check();
            }
        }


        private void Login_Check()
        {
            AutoPromotion.cfg.p_UserID = txt_ID.Text;

            SQL = "SELECT *   FROM PROMOTION_UserRecord   WHERE UserID = '" + txt_ID.Text + "' ";
            ADONet_Conn_Routine();
            int RowH = RsTbl.Rows.Count;

            if (RowH > 0)
            {
                string[] zUser;
                zUser = new string[3];
                zUser[1] = RsTbl.Rows[0].Field<string>("UserID");
                zUser[2] = RsTbl.Rows[0].Field<string>("UserPassword");

                AutoPromotion.cfg.p_User_KakaoID = RsTbl.Rows[0].Field<string>("User_KakaoID");
                AutoPromotion.cfg.p_User_KakaoPW = RsTbl.Rows[0].Field<string>("User_KakaoPW");

                ADONet_Conn.Close();

                if (zUser[2] == txt_PW.Text)
                {
                    RegistryKey reg;
                    reg = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Software\\SellerHelper");          //레지스트리 생성 ,폴더생성
                    ////키 생성하기
                    //RegistryKey regKey = Registry.LocalMachine.CreateSubKey("Software\\SellerHelper", RegistryKeyPermissionCheck.ReadWriteSubTree);
                    //값 저장하기
                    reg.SetValue("UserId", txt_ID.Text.ToString());           //레지스틀리 파일  생성

                    if (chk_DB_Handler.Checked == false)
                    {
                        frm_PROMOTION _frm_PROMOTION = new frm_PROMOTION();
                        _frm_PROMOTION.Show(); // Form2 를 보이게 한다 위치는 조정

                        //Form1 _Form1 = new Form1();
                        //_Form1.Show(); // 체크리스트박스 검증용
                    }
                    else
                    {
                        _UserSales _UserSales = new _UserSales();
                        _UserSales.Show();
                    }

                    this.Hide();
                }
                else
                {
                    MessageBox.Show("비밀번호가 잘못 되었습니다.");
                    return;
                }
            }
            else
            {
                ADONet_Conn.Close();
                MessageBox.Show("아이디가 없습니다.");
                return;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            if (txt_PW.Text == "")
            {
                txt_PW.Focus();
            }
            else
            {
                btn_Log.Focus();
            }

            System.Windows.Forms.Application.DoEvents();

            Thread myThread = new Thread(Func);
            myThread.Start();
        }


        private static void Func()
        {
            // 크롬드라이버 자동업데이트 기능 ---------------------------------------------------------------------
            AutoPromotion.ChromeDriverAutoUpdater _CDAU = new AutoPromotion.ChromeDriverAutoUpdater();
            _CDAU.DownloadLatestVersionOfChromeDriver();
            // 크롬드라이버 자동업데이트 기능 ---------------------------------------------------------------------
        }

        private void txt_ID_TextChanged(object sender, EventArgs e)
        {
            if (txt_ID.Text == "PADMIN")
            {
                chk_DB_Handler.Visible = true;
            }
            else
            {
                chk_DB_Handler.Visible = false;
            }
        }
    }
}
