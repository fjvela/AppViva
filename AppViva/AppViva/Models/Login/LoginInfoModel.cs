namespace AppViva.Models
{
    public class LoginInfoModel
    {
        public object CardId { get; set; }
        public string ClubDesc { get; set; }
        public int ClubNo { get; set; }
        public int ContractNo { get; set; }
        public int ContractPersonId { get; set; }
        public string Error { get; set; }
        public string ErrorCode { get; set; }
        public int FeeTypeId { get; set; }
        public string FirstName { get; set; }
        public bool FirstTimeLogin { get; set; }
        public bool FirstTimeLoginExpired { get; set; }
        public string FirstTimeLoginToken { get; set; }
        public bool IsFrozen { get; set; }
        public bool IsOneTime { get; set; }
        public bool IsSuspended { get; set; }
        public bool IsUnpaidCancel { get; set; }
        public Role[] Roles { get; set; }
        public string Token { get; set; }
        public string UserCode { get; set; }
        public int UserIdentityId { get; set; }
        public string Username { get; set; }
        public int UserTypeId { get; set; }
    }

    public class Role
    {
        public string description { get; set; }
        public int id { get; set; }
    }
}