﻿using AppViva.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppViva
{
    public class GlobalSetting
    {
        private static readonly GlobalSetting _instance = new GlobalSetting();
        public string APIEndPoint { get; private set; }
        public LoginInfoModel LoginInfo { get; set; }


        public static GlobalSetting Instance
        {
            get { return _instance; }
        }

        public GlobalSetting()
        {
            APIEndPoint = "https://localhost:1921/";
        }

        public static DateTime MaxDateTimeBook
        {
            get { return DateTime.Now.AddDays(2).Date; }
        }

        public static DateTime MinDateTimeBook
        {
            get { return DateTime.Now.AddHours(-1); }
        }
    }
}
