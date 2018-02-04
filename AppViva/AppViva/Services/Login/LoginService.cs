using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AppViva.Models;

namespace AppViva.Services
{
    public class LoginService : ILoginService
    {
        private readonly IHttpRequestProvider requestProvider;


        public async Task<LoginInfoModel> Login(string login, string pass)
        {
            UriBuilder uribuilder = new UriBuilder(GlobalSetting.Instance.APIEndPoint)
            {
                Path = "//User/Login",
            };

            return await requestProvider.PostAsync<LoginInfoModel>(
                uribuilder.ToString(),
                new LoginModel() { Username = login, Password = pass});
        }

        public LoginService(IHttpRequestProvider requestProvider)
        {
            this.requestProvider = requestProvider;
        }


    }
}
