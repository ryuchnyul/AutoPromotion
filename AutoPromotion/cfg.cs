using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPromotion
{
    class cfg
    {
        public static string DBConnString = "Password=qaz159632!@;Persist Security Info=True;User ID=ehdqkswk21c;" +
                                     "Data Source=dbehdqkswk21c;Network Library=DBMSSOCN;Network Address=db.pointgo.co.kr;Connection TimeOut =30;";

        public static string tVersion = "[업데이트 : 2024-01-01]";
        public cfg() { }

        public static string p_UserID { get; set; }
        public static string p_UserPW { get; set; }
        public static string p_UserIP { get; set; }
        public static string p_UserName { get; set; }
        public static string p_UserPhone { get; set; }
        public static string p_JoinDate { get; set; }
        public static string p_HDD { get; set; }
        public static string p_ConnState { get; set; }
        public static string p_ConnTime { get; set; }
        public static string p_StartDate { get; set; }
        public static string p_EndDate { get; set; }
        public static string p_AdminCheck { get; set; }

        public static string p_User_KakaoID { get; set; }
        public static string p_User_KakaoPW { get; set; }

        public static bool p_WorkTimer { get; set; }
        public static string ChromeDownState { get; set; }
    }
}
