﻿using MongoRoomieDLL.BusinessDAO;
using MongoRoomieDLL.BusinessEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoRoomieDLL.BusinessWorkFlow
{
    /**
     * This is an old version of member account class data access layer
     * No longer use
     * 
     * **/
    public static class MemberAccountInfoWorkFlow
    {
        #region insert / update / delete
        public static async Task CreateMemberAccount(MemberAccountInfo mai)
        {
            MemberAccountInfoDAO maiDAO = new MemberAccountInfoDAO();
            await maiDAO.CreateMemberAccount(mai);
        }

        public static async Task<long> UpdateMemberAccount(MemberAccountInfo mai)
        {
            MemberAccountInfoDAO maiDAO = new MemberAccountInfoDAO();
            return await maiDAO.UpdateMemberAccount(mai);
        }

        public static async Task<long> RecordLoginDay(string id, string loginDay)
        {
            MemberAccountInfoDAO maiDAO = new MemberAccountInfoDAO();
            return await maiDAO.RecordLoginDay(id, loginDay);
        }

        public static async Task<long> ActivateAccount(string id)
        {
            MemberAccountInfoDAO maiDAO = new MemberAccountInfoDAO();
            return await maiDAO.ActivateAccount(id);
        }

        public static async Task<long> DeactivateAccount(string id)
        {
            MemberAccountInfoDAO maiDAO = new MemberAccountInfoDAO();
            return await maiDAO.DeactivateAccount(id);
        }

        public static async Task<long> PendingAccount(string id)
        {
            MemberAccountInfoDAO maiDAO = new MemberAccountInfoDAO();
            return await maiDAO.PendingAccount(id);
        }

        public static async Task<long> DeleteMemberAccountById(string id)
        {
            MemberAccountInfoDAO maiDAO = new MemberAccountInfoDAO();
            return await maiDAO.DeleteMemberAccountById(id);
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
