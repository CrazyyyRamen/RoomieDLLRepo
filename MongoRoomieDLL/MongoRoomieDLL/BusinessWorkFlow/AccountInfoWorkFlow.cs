using MongoRoomieDLL.BusinessDAO;
using MongoRoomieDLL.BusinessEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoRoomieDLL.BusinessWorkFlow
{
    public static class AccountInfoWorkFlow
    {
        #region insert / update / delete
        public static async Task CreateMemberAccount(AccountInfo accountInfo)
        {
            AccountInfoDAO maiDAO = new AccountInfoDAO();
            await maiDAO.CreateMemberAccount(accountInfo);
        }

        public static async Task<long> UpdateMemberAccount(AccountInfo accountInfo)
        {
            AccountInfoDAO maiDAO = new AccountInfoDAO();
            return await maiDAO.UpdateMemberAccount(accountInfo);
        }

        public static async Task<long> RecordLoginDay(string id, string loginDay)
        {
            AccountInfoDAO maiDAO = new AccountInfoDAO();
            return await maiDAO.RecordLoginDay(id, loginDay);
        }

        public static async Task<long> ActivateAccount(string id)
        {
            AccountInfoDAO maiDAO = new AccountInfoDAO();
            return await maiDAO.ActivateAccount(id);
        }

        public static async Task<long> DeactivateAccount(string id)
        {
            AccountInfoDAO maiDAO = new AccountInfoDAO();
            return await maiDAO.DeactivateAccount(id);
        }

        public static async Task<long> ArchiveAccount(string id)
        {
            AccountInfoDAO maiDAO = new AccountInfoDAO();
            return await maiDAO.ArchiveAccount(id);
        }

        public static async Task<long> DeleteMemberAccountById(string id)
        {
            AccountInfoDAO maiDAO = new AccountInfoDAO();
            return await maiDAO.DeleteMemberAccountById(id);
        }
        #endregion

        #region select
        public static List<AccountInfo> GetMemberById(string id)
        {
            AccountInfoDAO maiDAO = new AccountInfoDAO();
            return maiDAO.GetMemberById(id);
        }

        public static List<AccountInfo> GetActiveMemberAccountExcludeId(string id, string role, string neighbourhood, string cityName, string provinceCode, string countryName)
        {
            AccountInfoDAO maiDAO = new AccountInfoDAO();
            return maiDAO.GetActiveMemberAccountExcludeId(id, role, neighbourhood, cityName, provinceCode, countryName);
        }

        public static List<AccountInfo> GetActiveMemberAccountByLocation(int count, string neighbourhood, string cityName, string provinceCode, string countryName)
        {
            AccountInfoDAO maiDAO = new AccountInfoDAO();
            return maiDAO.GetActiveMemberAccountByLocation(count, neighbourhood, cityName, provinceCode, countryName);
        }

        public static List<AccountInfo> GetMemberAccountLoginInfo(string email, string password)
        {
            AccountInfoDAO maiDAO = new AccountInfoDAO();
            return maiDAO.GetMemberAccountLoginInfo(email, password);
        }

        public static bool CheckEmail(string email)
        {
            AccountInfoDAO maiDAO = new AccountInfoDAO();
            return maiDAO.CheckEmail(email);
        }
        #endregion
    }
}
