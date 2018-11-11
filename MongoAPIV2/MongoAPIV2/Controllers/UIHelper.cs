using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace MongoAPIV2.Controllers
{
    public static class UIHelper
    {
        public static string EncryptDataMD5(string data)
        {
            var md5 = new MD5CryptoServiceProvider();
            var md5data = md5.ComputeHash(Encoding.ASCII.GetBytes(data));
            return new ASCIIEncoding().GetString(md5data);
        }

        public static string EncryptDataSha1(string data)
        {
            var sha1 = new SHA1CryptoServiceProvider();
            var sha1data = sha1.ComputeHash(Encoding.ASCII.GetBytes(data));
            return new ASCIIEncoding().GetString(sha1data);
        }
    }
}