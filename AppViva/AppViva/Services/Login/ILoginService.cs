using AppViva.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppViva.Services
{
    public interface ILoginService
    {
        Task<LoginInfoModel> Login(string login, string password);
    }
}
