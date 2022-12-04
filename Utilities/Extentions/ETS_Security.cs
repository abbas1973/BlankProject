using System.Security.Cryptography;
using System.Text;

namespace Utilities
{
    /// <summary>
    /// Powered By Abbas Mohammadnezhad
    /// </summary>
    public static class ETS_Security
    {
        public static string GetHash(this string pass)
        {
            pass += "@7^e{3x#";
            byte[] data = Encoding.ASCII.GetBytes(pass);

            #region هش با استفاده از MD5
            //using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            //{
            //    data = md5.ComputeHash(data);
            //}
            #endregion


            #region هش با استفاده از SHA512
            using (SHA512 shaM = new SHA512Managed())
            {
                data = shaM.ComputeHash(data);
            }
            #endregion

            string encPass = Encoding.ASCII.GetString(data);
            return encPass;
        }
    }
}