using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.DAL
{
    public static class Crypto
    {
        public static string Secret = "G1g1Sch00lOfC0d1ng";

        public static string HashWith(this string username, string password)
        {
            byte[] result = new byte[18];
            int len = Secret.Length;
            while (username.Length < len) username += password + username;
            byte[] bKey = Encoding.ASCII.GetBytes(Secret);
            byte[] bStr = Encoding.ASCII.GetBytes(username.Substring(0, len));
            for (int i = 0; i < len; i++)
            {
                result[i] = (byte)(((bKey[i] + bStr[i]) * (i + 1)) % 19);
            }
            return Convert.ToBase64String(result);
        }
    }
}


            //if (id == -1)
            //{
            //    var users = await Unit.Users.Get();
            //    foreach(User u in users)
            //    {
            //        u.Password = u.Username.HashWith(u.Password);
            //        Unit.Context.Entry(u).CurrentValues.SetValues(u);
            //    }
            //    await Unit.Save();
            //    return Ok("haman uradijo");
            //}
