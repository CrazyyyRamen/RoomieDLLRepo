using MongoRoomieDLL.BusinessDAO;
using MongoRoomieDLL.BusinessEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoRoomieDLL.BusinessWorkFlow
{
    public static class MemberAccountInfoWorkFlow
    {
        #region insert / update / delete
        public static void CreateMemberAccount(MemberAccountInfo mai)
        {
            MemberAccountInfoDAO maiDAO = new MemberAccountInfoDAO();
            maiDAO.CreateMemberAccount(mai);
        }

        public static long UpdateMemberAccount(MemberAccountInfo mai)
        {
            MemberAccountInfoDAO maiDAO = new MemberAccountInfoDAO();
            return maiDAO.UpdateMemberAccount(mai);
        }

        public static long RecordLoginDay(string id, string loginDay)
        {
            MemberAccountInfoDAO maiDAO = new MemberAccountInfoDAO();
            return maiDAO.RecordLoginDay(id, loginDay);
        }

        public static long ActivateAccount(string id)
        {
            MemberAccountInfoDAO maiDAO = new MemberAccountInfoDAO();
            return maiDAO.ActivateAccount(id);
        }

        public static long DeactivateAccount(string id)
        {
            MemberAccountInfoDAO maiDAO = new MemberAccountInfoDAO();
            return maiDAO.DeactivateAccount(id);
        }

        public static long PendingAccount(string id)
        {
            MemberAccountInfoDAO maiDAO = new MemberAccountInfoDAO();
            return maiDAO.PendingAccount(id);
        }

        public static long DeleteMemberAccountById(string id)
        {
            MemberAccountInfoDAO maiDAO = new MemberAccountInfoDAO();
            return maiDAO.DeleteMemberAccountById(id);
        }
        #endregion

        #region select
        public static List<MemberAccountInfo> GetMemberById(string id)
        {
            MemberAccountInfoDAO maiDAO = new MemberAccountInfoDAO();
            return maiDAO.GetMemberById(id);
        }

        public static List<MemberAccountInfo> GetActiveMemberAccountExcludeId(string id, string offerPlace, string neighbourhood, string cityName, string provinceCode, string countryName)
        {
            MemberAccountInfoDAO maiDAO = new MemberAccountInfoDAO();
            return maiDAO.GetActiveMemberAccountExcludeId(id, offerPlace, neighbourhood, cityName, provinceCode, countryName);
        }

        public static List<MemberAccountInfo> GetActiveMemberAccountByLocation(int count, string neighbourhood, string cityName, string provinceCode, string countryName)
        {
            MemberAccountInfoDAO maiDAO = new MemberAccountInfoDAO();
            return maiDAO.GetActiveMemberAccountByLocation(count, neighbourhood, cityName, provinceCode, countryName);
        }

        public static List<MemberAccountInfo> GetMemberAccountLoginInfo(string userName, string password)
        {
            MemberAccountInfoDAO maiDAO = new MemberAccountInfoDAO();
            return maiDAO.GetMemberAccountLoginInfo(userName, password);
        }

        public static bool CheckUserName(string userName)
        {
            MemberAccountInfoDAO maiDAO = new MemberAccountInfoDAO();
            return maiDAO.CheckUserName(userName);
        }

        public static bool CheckEmail(string email)
        {
            MemberAccountInfoDAO maiDAO = new MemberAccountInfoDAO();
            return maiDAO.CheckEmail(email);
        }
        #endregion
    }
}
