using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Hashing
{
    public class HashingHelper
    {

        // out 
        public static void CreatePasswordHash(string password,out byte[] passwordHash,out byte[] passwordSalt)
        {

            using (var hmac = new System.Security.Cryptography.HMACSHA512()) // class oldugu ıcın hmac ı newlıyoruz ()
            {
                // hmac key ile salt artı güvenlik kodu olusturuyoruz
                passwordSalt = hmac.Key;
                // hash byte ile veriliyor byte bulmak için utf8 encoding ile buluyoruz getbytes komutu ıle de getiriypruz.
                passwordHash = hmac.ComputeHash((Encoding.UTF8.GetBytes(password)));
            }
        }
        // sisteme gırmek ısteyen passwordun bızım hash kod ıle eşleşşip eşleşmediğini buldugumuz yer
        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt)) // class oldugu ıcın hmac ı newlıyoruz ()
            {
               var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }

                }
                return true;    
            }

        }
    }
}
