using MongoDB.Bson;
using MongoDB.Driver;
using MongoRoomieDLL.BusinessEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoRoomieDLL.BusinessDAO
{
    public class MemberAccountInfoDAO : DAO<MemberAccountInfo>
    {
        private string clusterName = "MemberAccountInfo";

        public MemberAccountInfoDAO()
        {
            collection = databaseMongo.GetCollection<MemberAccountInfo>(clusterName);
        }

        #region insert / update / delete
        public async Task CreateMemberAccount(MemberAccountInfo mai)
        {
            await collection.InsertOneAsync(mai);
        }

        public async Task<long> UpdateMemberAccount(MemberAccountInfo mai)
        {
            var result = await collection.UpdateOneAsync(Builders<MemberAccountInfo>.Filter.Eq("_id", mai._id), Builders<MemberAccountInfo>.Update
                                            .Set("user_name", mai.user_name)
                                            .Set("_email", mai._email)
                                            .Set("_birthday", mai._birthday)
                                            .Set("_cell", mai._cell)
                                            .Set("update_day", mai.update_day)
                                            .Set("smoke_habit", mai.smoke_habit)
                                            .Set("has_pet", mai.has_pet)
                                            .Set("_gender", mai._gender)
                                            .Set("current_status", mai.current_status)
                                            .Set("offer_place", mai.offer_place)
                                            .Set("home_type", mai.home_type)
                                            .Set("room_type", mai.room_type)
                                            .Set("have_locker", mai.have_locker)
                                            .Set("have_gym", mai.have_gym)
                                            .Set("have_parking", mai.have_parking)
                                            .Set("have_swim_pool", mai.have_swim_pool)
                                            .Set("have_furniture", mai.have_furniture)
                                            .Set("rent_fee", mai.rent_fee)
                                            .Set("interested_place", mai.interested_place)
                                            .Set("min_rent_fee", mai.min_rent_fee)
                                            .Set("max_rent_fee", mai.max_rent_fee)
                                            .Set("move_in_date", mai.move_in_date));

            return result.ModifiedCount;
        }

        public async Task<long> RecordLoginDay(string id, string loginDay)
        {
            var result = await collection.UpdateOneAsync(Builders<MemberAccountInfo>.Filter.Eq("_id", id), Builders<MemberAccountInfo>.Update.Set("login_day", loginDay));

            return result.ModifiedCount;
        }

        public async Task<long> ActivateAccount(string id)
        {
            var result = await collection.UpdateOneAsync(Builders<MemberAccountInfo>.Filter.Eq("_id", id), Builders<MemberAccountInfo>.Update.Set("account_status", "A"));

            return result.ModifiedCount;
        }

        public async Task<long> DeactivateAccount(string id)
        {
            var result = await collection.UpdateOneAsync(Builders<MemberAccountInfo>.Filter.Eq("_id", id), Builders<MemberAccountInfo>.Update.Set("account_status", "I"));

            return result.ModifiedCount;
        }

        public async Task<long> PendingAccount(string id)
        {
            var result = await collection.UpdateOneAsync(Builders<MemberAccountInfo>.Filter.Eq("_id", id), Builders<MemberAccountInfo>.Update.Set("account_status", "P"));

            return result.ModifiedCount;
        }

        public async Task<long> DeleteMemberAccountById(string id)
        {
            var result = await collection.DeleteOneAsync(Builders<MemberAccountInfo>.Filter.Eq("_id", id));
            return result.DeletedCount;
        }
        #endregion

        #region select
        public List<MemberAccountInfo> GetMemberById(string id)
        {
            List<MemberAccountInfo> lstMember = new List<MemberAccountInfo>();

            lstMember = collection.AsQueryable<MemberAccountInfo>().Where(m => m._id == id).ToList();

            return lstMember;
        }

        public List<MemberAccountInfo> GetActiveMemberAccountExcludeId(string id, string offerPlace, string neighbourhood, string cityName, string provinceCode, string countryName)
        {
            List<MemberAccountInfo> lstMember = new List<MemberAccountInfo>();

            if("N".Equals(offerPlace))
            {
                lstMember = collection.AsQueryable<MemberAccountInfo>().Where(m => m._id != id && m.account_status == "A" 
                                            && (m.interested_place[0].interested_neighbourhood == neighbourhood
                                            && m.interested_place[0].interested_city == cityName
                                            && m.interested_place[0].interested_province == provinceCode
                                            && m.interested_place[0].interested_country == countryName)).OrderBy(m => m.create_day).ToList();

            }
            else if("Y".Equals(offerPlace))
            {
                lstMember = collection.AsQueryable<MemberAccountInfo>().Where(m => m._id != id && m.offer_place == "N" && m.account_status == "A" 
                                            && (m.interested_place[0].interested_neighbourhood == neighbourhood
                                            && m.interested_place[0].interested_city == cityName
                                            && m.interested_place[0].interested_province == provinceCode
                                            && m.interested_place[0].interested_country == countryName)).OrderBy(m => m.create_day).ToList();

            }

            return lstMember; 
        }

        public List<MemberAccountInfo> GetActiveMemberAccountByLocation(int count, string neighbourhood, string cityName, string provinceCode, string countryName)
        {
            List<MemberAccountInfo> lstMember = new List<MemberAccountInfo>();

            if(count > 0)
            {
                lstMember = collection.AsQueryable<MemberAccountInfo>().Where(m => m.account_status == "A" 
                                            && (m.interested_place[0].interested_neighbourhood == neighbourhood
                                            && m.interested_place[0].interested_city == cityName
                                            && m.interested_place[0].interested_province == provinceCode
                                            && m.interested_place[0].interested_country == countryName)).OrderBy(m => m.create_day).Take(count).ToList();

            }
            else
            {
                lstMember = collection.AsQueryable<MemberAccountInfo>().Where(m => m.account_status == "A" 
                                            && (m.interested_place[0].interested_neighbourhood == neighbourhood
                                            && m.interested_place[0].interested_city == cityName
                                            && m.interested_place[0].interested_province == provinceCode
                                            && m.interested_place[0].interested_country == countryName)).OrderBy(m => m.create_day).ToList();

            }

            return lstMember;
        }

        public List<MemberAccountInfo> GetMemberAccountLoginInfo(string userName, string password)
        {
            List<MemberAccountInfo> lstMember = new List<MemberAccountInfo>();

            lstMember = collection.AsQueryable<MemberAccountInfo>().Where(m => m.user_name == userName && m._password == password).ToList();

            return lstMember;
        }

        public bool CheckUserName(string userName)
        {
            bool isExisted = false;

            var result = collection.AsQueryable<MemberAccountInfo>().Where(m => m.user_name == userName).ToList();

            if(result.Count > 0)
            {
                isExisted = true;
            }

            return isExisted;
        }

        public bool CheckEmail(string email)
        {
            bool isDuplicateEmail = false;

            var result = collection.AsQueryable<MemberAccountInfo>().Where(m => m._email == email).ToList();

            if(result.Count > 0)
            {
                isDuplicateEmail = true;
            }

            return isDuplicateEmail;
        }
        #endregion
    }
}
