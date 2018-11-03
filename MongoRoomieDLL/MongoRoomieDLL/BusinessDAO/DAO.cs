using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoRoomieDLL.BusinessDAO
{
    public abstract class DAO<T>
    {
        protected MongoClient mongoClient;
        protected IMongoDatabase databaseMongo;
        protected IMongoCollection<T> collection;
        protected string clusterName;

        protected DAO()
        {
            mongoClient = new MongoClient(ConfigurationManager.ConnectionStrings["mongoDBURL"].ConnectionString.ToString());
            databaseMongo = mongoClient.GetDatabase(ConfigurationManager.ConnectionStrings["mongoDB"].ConnectionString.ToString());
        }
    }
}
