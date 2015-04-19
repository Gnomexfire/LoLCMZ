using LoLBaseReleaseBExternal;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace LoLCMZ_Release_B.Class
{
    public class LoL_Utilidade
    {

        public string GetMD5HashOfAFile(string file)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            FileStream stream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read, 8192);
            md5.ComputeHash(stream);
            stream.Close();
            byte[] hash = md5.Hash;
            StringBuilder sb = new StringBuilder();
            foreach (byte b in hash)
            {
                sb.Append(string.Format("{0:X2}", b));
            }
            return sb.ToString();
        }

        void Load_File_XML()
        {

        }

        public bool _Checar_Nova_Atualizao_()
        {


            return false;
        }

    }
}
