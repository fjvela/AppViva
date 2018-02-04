using System;
using System.Collections.Generic;
using System.Text;

namespace AppViva.Models
{


    public class LoginInfoModel
    {
        public int UserIdentityId { get; set; }
        public int ContractPersonId { get; set; }
        public int ContractNo { get; set; }
        public int ClubNo { get; set; }
        public string ClubDesc { get; set; }
        public string Username { get; set; }
        public int UserTypeId { get; set; }
        public Role[] Roles { get; set; }
        public bool FirstTimeLogin { get; set; }
        public string FirstTimeLoginToken { get; set; }
        public bool FirstTimeLoginExpired { get; set; }
        public object CardId { get; set; }
        public string UserCode { get; set; }
        public string FirstName { get; set; }
        public bool IsUnpaidCancel { get; set; }
        public bool IsSuspended { get; set; }
        public bool IsFrozen { get; set; }
        public string Token { get; set; }
        public bool IsOneTime { get; set; }
        public string Error { get; set; }
        public string ErrorCode { get; set; }
    }

    public class Role
    {
        public int id { get; set; }
        public string description { get; set; }
    }


}
