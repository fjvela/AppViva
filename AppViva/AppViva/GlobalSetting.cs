using AppViva.Models;
using System;

namespace AppViva
{
    public class GlobalSetting
    {
        private static readonly GlobalSetting _instance = new GlobalSetting();

        public GlobalSetting()
        {
            APIEndPoint = "https://localhost:1921/";
        }

        public static GlobalSetting Instance
        {
            get { return _instance; }
        }

        public static DateTime MaxDateTimeBook
        {
            get
            {
                switch (Instance.LoginInfo.FeeTypeId)
                {
                    case 2:
                        return DateTime.Now.AddDays(3).Date;

                    default:
                        return DateTime.Now.AddDays(2).Date;
                }
            }
        }

        public static DateTime MinDateTimeBook
        {
            get { return DateTime.Now.AddHours(1); }
        }

        public string APIEndPoint { get; private set; }
        public LoginInfoModel LoginInfo { get; set; }
    }
}