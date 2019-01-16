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
    public class AccountInfoDAO : DAO<AccountInfo>
    {
        private string clusterName = "AccountInfo";

        public AccountInfoDAO()
        {
            collection = databaseMongo.GetCollection<AccountInfo>(clusterName);
        }

        #region insert / update / delete
        public async Task CreateMemberAccount(AccountInfo accountInfo)
        {
            await collection.InsertOneAsync(accountInfo);
        }

        public async Task<long> UpdateMemberAccount(AccountInfo accountInfo)
        {
            var result = await collection.UpdateOneAsync(Builders<AccountInfo>.Filter.Eq("_id", accountInfo._id), Builders<AccountInfo>.Update
                                            .Set("user_image", accountInfo.user_image)
                                            .Set("_firstname", accountInfo._firstname)
                                            .Set("_lastname", accountInfo._lastname)
                                            .Set("_email", accountInfo._email)
                                            .Set("_birthday", accountInfo._birthday)
                                            .Set("_cell", accountInfo._cell)
                                            .Set("update_day", accountInfo.update_day)
                                            .Set("smoke_habit", accountInfo.smoke_habit)
                                            .Set("has_pet", accountInfo.has_pet)
                                            .Set("_gender", accountInfo._gender)
                                            .Set("_property", accountInfo._property)
                                            .Set("current_status", accountInfo.current_status)
                                            .Set("currently_live_in", accountInfo.currently_live_in)
                                            .Set("_rrent", accountInfo._rrent)
                                            .Set("move_in_day", accountInfo.move_in_day)
                                            .Set("interested_place", accountInfo.interested_place)
                                            .Set("_preference", accountInfo._preference));

            return result.ModifiedCount;
        }

        public async Task<long> RecordLoginDay(string id, string loginDay)
        {
            var result = await collection.UpdateOneAsync(Builders<AccountInfo>.Filter.Eq("_id", id), Builders<AccountInfo>.Update.Set("login_day", loginDay));

            return result.ModifiedCount;
        }

        public async Task<long> ActivateAccount(string id)
        {
            var result = await collection.UpdateOneAsync(Builders<AccountInfo>.Filter.Eq("_id", id), Builders<AccountInfo>.Update.Set("account_status", "A"));

            return result.ModifiedCount;
        }

        public async Task<long> DeactivateAccount(string id)
        {
            var result = await collection.UpdateOneAsync(Builders<AccountInfo>.Filter.Eq("_id", id), Builders<AccountInfo>.Update.Set("account_status", "I"));

            return result.ModifiedCount;
        }

        public async Task<long> ArchiveAccount(string id)
        {
            var result = await collection.UpdateOneAsync(Builders<AccountInfo>.Filter.Eq("_id", id), Builders<AccountInfo>.Update.Set("account_status", "AR"));

            return result.ModifiedCount;
        }

        public async Task<long> DeleteMemberAccountById(string id)
        {
            var result = await collection.DeleteOneAsync(Builders<AccountInfo>.Filter.Eq("_id", id));
            return result.DeletedCount;
        }

        #endregion

        #region select
        public List<AccountInfo> GetMemberById(string id)
        {
            List<AccountInfo> lstMember = new List<AccountInfo>();

            lstMember = collection.AsQueryable<AccountInfo>().Where(m => m._id == ObjectId.Parse(id)).ToList();

            return lstMember;
        }

        // Match members based on their location and the role
        public List<AccountInfo> GetActiveMemberAccountExcludeId(string id, string role, string neighbourhood, string cityName, string provinceCode, string countryName)
        {
            List<AccountInfo> lstMember = new List<AccountInfo>();

            if ("R".Equals(role))
            {
                lstMember = collection.AsQueryable<AccountInfo>().Where(m => m._id != ObjectId.Parse(id) && m.account_status == "A"
                                            && (m.interested_place.interested_neighbour == neighbourhood
                                            && m.interested_place.interested_city == cityName
                                            && m.interested_place.interested_province == provinceCode
                                            && m.interested_place.interested_country == countryName)).OrderBy(m => m.create_day).ToList();

            }
            else if ("O".Equals(role))
            {
                lstMember = collection.AsQueryable<AccountInfo>().Where(m => m._id != ObjectId.Parse(id) && m._role == "R" && m.account_status == "A"
                                            && (m.interested_place.interested_neighbour == neighbourhood
                                            && m.interested_place.interested_city == cityName
                                            && m.interested_place.interested_province == provinceCode
                                            && m.interested_place.interested_country == countryName)).OrderBy(m => m.create_day).ToList();

            }

            return lstMember;
        }

        // Match members based on their location
        public List<AccountInfo> GetActiveMemberAccountByLocation(int count, string neighbourhood, string cityName, string provinceCode, string countryName)
        {
            List<AccountInfo> lstMember = new List<AccountInfo>();

            if (count > 0)
            {
                lstMember = collection.AsQueryable<AccountInfo>().Where(m => m.account_status == "A"
                                            && (m.interested_place.interested_neighbour == neighbourhood
                                            && m.interested_place.interested_city == cityName
                                            && m.interested_place.interested_province == provinceCode
                                            && m.interested_place.interested_country == countryName)).OrderBy(m => m.create_day).Take(count).ToList();

            }
            else
            {
                lstMember = collection.AsQueryable<AccountInfo>().Where(m => m.account_status == "A"
                                            && (m.interested_place.interested_neighbour == neighbourhood
                                            && m.interested_place.interested_city == cityName
                                            && m.interested_place.interested_province == provinceCode
                                            && m.interested_place.interested_country == countryName)).OrderBy(m => m.create_day).ToList();

            }

            return lstMember;
        }

        // Login
        public List<AccountInfo> GetMemberAccountLoginInfo(string email, string password)
        {
            List<AccountInfo> lstMember = new List<AccountInfo>();

            lstMember = collection.AsQueryable<AccountInfo>().Where(m => m._email == email && m._password == password).ToList();

            return lstMember;
        }

        public bool CheckEmail(string email)
        {
            bool isDuplicateEmail = false;

            var result = collection.AsQueryable<AccountInfo>().Where(m => m._email == email).ToList();

            if (result.Count > 0)
            {
                isDuplicateEmail = true;
            }

            return isDuplicateEmail;
        }
        #endregion
    }
}
