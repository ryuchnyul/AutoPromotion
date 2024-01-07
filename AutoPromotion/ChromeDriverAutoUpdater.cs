using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Windows.Forms;
using WinHttp;

namespace AutoPromotion
{
    class ChromeDriverAutoUpdater
    {

        #region UBoundSplit Function    // UBound  ==============================================
        public int UBound_int(string target, string split)
        {
            string[] target_result = target.Split(new string[] { split }, StringSplitOptions.None);
            return target_result.Length - 1;
        }

        public string UBound_str(string target, string split, int index)
        {
            string[] target_result = target.Split(new string[] { split }, StringSplitOptions.None);
            return target_result[index];
        }
        // 엑셀 VBA 함수 Split 처럼 함수로 만들어서 사용
        public static string SSplit(string _body, string _parseString, int index)
        {
            string varTemp = "";
            try
            {
                varTemp = _body.Split(new string[] { _parseString }, StringSplitOptions.None)[index];
            }
            catch
            {
                varTemp = "";
            }
            return varTemp;
        }
        #endregion
                
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
        #endregion

        public void DownloadLatestVersionOfChromeDriver()
        {
            // 압축해제 디렉토리 삭제 오류 방지용 사전 폴더 삭제 기능 추가
            try
            {
                string _dPath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\chromedriver-win32";
                DirectoryInfo _directory = new DirectoryInfo(_dPath);
                _directory.Delete(true);
            }
            catch { }
            // 압축해제 디렉토리 삭제 오류 방지용 사전 폴더 삭제 기능 추가

            string path = DownloadLatestVersionOfChromeDriverGetVersionPath();
            var version = DownloadLatestVersionOfChromeDriverGetChromeVersion(path);
            var urlToDownload = DownloadLatestVersionOfChromeDriverGetURLToDownload(version);
            DownloadLatestVersionOfChromeDriverKillAllChromeDriverProcesses();
            DownloadLatestVersionOfChromeDriverDownloadNewVersionOfChrome(urlToDownload);
        }

        public string DownloadLatestVersionOfChromeDriverGetVersionPath()
        {
            //Path originates from here: https://chromedriver.chromium.org/downloads/version-selection            
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\chrome.exe"))
            {
                if (key != null)
                {
                    Object o = key.GetValue("");
                    if (!String.IsNullOrEmpty(o.ToString()))
                    {
                        return o.ToString();
                    }
                    else
                    {
                        throw new ArgumentException("Unable to get version because chrome registry value was null");
                    }
                }
                else
                {
                    throw new ArgumentException("Unable to get version because chrome registry path was null");
                }
            }
        }

        public string DownloadLatestVersionOfChromeDriverGetChromeVersion(string productVersionPath)
        {
            if (String.IsNullOrEmpty(productVersionPath))
            {
                throw new ArgumentException("Unable to get version because path is empty");
            }

            if (!File.Exists(productVersionPath))
            {
                throw new FileNotFoundException("Unable to get version because path specifies a file that does not exists");
            }

            var versionInfo = FileVersionInfo.GetVersionInfo(productVersionPath);
            if (versionInfo != null && !String.IsNullOrEmpty(versionInfo.FileVersion))
            {
                return versionInfo.FileVersion;
            }
            else
            {
                throw new ArgumentException("Unable to get version from path because the version is either null or empty: " + productVersionPath);
            }
        }

        public string DownloadLatestVersionOfChromeDriverGetURLToDownload(string version)
        {
            if (String.IsNullOrEmpty(version))
            {
                throw new ArgumentException("Unable to get url because version is empty");
            }

            string xUrl = "https://googlechromelabs.github.io/chrome-for-testing";
            //string xUrl = "https://edgedl.me.gvt1.com/edgedl/chrome/chrome-for-testing/119.0.6045.105/win32/chromedriver-win32.zip";

            WinHttpRequest WinHttp = new WinHttpRequest();
            WinHttp.Open("GET", xUrl);
            WinHttp.SetRequestHeader("User-Agent", "Mozilla/5.0 (iPhone; CPU iPhone OS 6_0 like Mac OS X) AppleWebKit/536.26 (KHTML, like Gecko) Version/6.0 Mobile/10A5376e Safari/8536.25");
            WinHttp.Send();
            WinHttp.WaitForResponse();
            string responseText = WinHttp.ResponseText;
            if (responseText.IndexOf(String.Join(".", version.Split('.').Take(3))) > -1)
            {
                string html = string.Empty;
                html = "https://edgedl.me.gvt1.com/edgedl/chrome/chrome-for-testing/" + String.Join(".", version.Split('.').Take(3));
                string Stable_Ver = SSplit(responseText, html, 1);
                Stable_Ver = SSplit(Stable_Ver, "/linux64", 0);
                html = "";

                //string urlToPathLocation = @"https://edgedl.me.gvt1.com/edgedl/chrome/chrome-for-testing/" + String.Join(".", version.Split('.').Take(3)) + Stable_Ver + "/win32/chromedriver-win32.zip";

                //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlToPathLocation);
                //request.AutomaticDecompression = DecompressionMethods.GZip;

                //using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                //using (Stream stream = response.GetResponseStream())
                //using (StreamReader reader = new StreamReader(stream))
                //{
                //    html = reader.ReadToEnd();
                //}

                //if (String.IsNullOrEmpty(html))
                //{
                //    throw new WebException("Unable to get version path from website");
                //}

                //return "https://chromedriver.storage.googleapis.com/" + html + "/chromedriver_win32.zip";



                string urlToPathLocation = @"https://edgedl.me.gvt1.com/edgedl/chrome/chrome-for-testing/" + String.Join(".", version.Split('.').Take(3)) + Stable_Ver + @"/win32/chromedriver-win32.zip";
                return urlToPathLocation;
            }
            else
            {
                MessageBox.Show("크롬브라우저 설정에서 크롬브라우저를 최신 버전으로 업데이트 하세요!");
                return "";
            }
        }

        public void DownloadLatestVersionOfChromeDriverKillAllChromeDriverProcesses()
        {
            //It's important to kill all processes before attempting to replace the chrome driver, because if you do not you may still have file locks left over
            var processes = Process.GetProcessesByName("chromedriver");
            foreach (var process in processes)
            {
                try
                {
                    process.Kill();
                }
                catch
                {
                    //We do our best here but if another user account is running the chrome driver we may not be able to kill it unless we run from a elevated user account + various other reasons we don't care about
                }
            }
        }

        public void DownloadLatestVersionOfChromeDriverDownloadNewVersionOfChrome(string urlToDownload)
        {
            if (String.IsNullOrEmpty(urlToDownload))
            {
                throw new ArgumentException("Unable to get url because urlToDownload is empty");
            }
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            //Downloaded files always come as a zip, we need to do a bit of switching around to get everything in the right place
            using (var client = new WebClient())
            {
                if (File.Exists(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\chromedriver.zip"))
                {
                    File.Delete(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\chromedriver.zip");
                }
                //client.DownloadFile(urlToDownload, "chromedriver-win32.zip");
                client.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                client.DownloadFileAsync(new Uri(urlToDownload), "chromedriver.zip");

                for (int xi = 0; xi < 100; xi++)
                {
                    var fi = new FileInfo(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\chromedriver.zip");
                    Console.WriteLine(fi.Length.ToString());
                    Delay(2000);

                    if (fi.Length > 7000000)
                    {
                        Delay(2000);
                        break;
                    }
                }
                client.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
            }
        }

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            if (File.Exists(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\chromedriver.zip"))
            {
                //Delay(2000);

                // 다운로드 완료후 기존파일 삭제
                if (File.Exists(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\chromedriver.zip") && File.Exists(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\chromedriver.exe"))
                {
                    //File.Delete(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\chromedriver.exe");
                }
                // 다운로드 완료후 압축해제
                System.IO.Compression.ZipFile.ExtractToDirectory(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\chromedriver.zip", System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
                // 파일 이동
                string source_folder = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\chromedriver-win32\\chromedriver.exe";
                //MessageBox.Show(source_folder);
                string target_folder = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\chromedriver.exe";
                // 일반 파일 복사시 : File.Copy(원본파일, 복사파일)  // 파일 덮어쓰기 : File.Copy(원본파일, 복사파일, true)
                //System.IO.File.Move(source_folder, target_folder);
                System.IO.File.Copy(source_folder, target_folder, true);
                // 압축해제 폴더 삭제
                string dPath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\chromedriver-win32";
                DirectoryInfo directory = new DirectoryInfo(dPath);
                directory.Delete(true);

                AutoPromotion.cfg.ChromeDownState = "OK";
                //MessageBox.Show("크롬드라이버 다운로드 완료");
            }
        }

        // 정상루틴
        //private void btnDownload_Click(object sender, EventArgs e)
        //{
        //    WebClient webClient = new WebClient();
        //    webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
        //    webClient.DownloadFileAsync(new Uri("http://---/file.zip"), @"c:\file.zip");
        //}   
        //private void Completed(object sender, AsyncCompletedEventArgs e)
        //{
        //    MessageBox.Show("Download completed!");
        //}
    }
}
