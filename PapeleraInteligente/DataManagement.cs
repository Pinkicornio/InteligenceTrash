using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;

namespace PapeleraInteligente
{
    class DataManagement
    {

        public static string currentUser;
        public static string currentStorage;
        public static string currentUser_Id;

        private string server = "sql4.freesqldatabase.com";
        private string user = "sql4415325";
        private string password = "kG5qWmg8qi";
        private string port = "3306";
        private string cs;
        private string dbName = "sql4415325";
        private static MySqlConnection conn = null;
        private static MySqlCommand cmd;
        private static MySqlDataReader rdr = null;

        public void startConnection()
        {
            cs = "server=" + server + ";user=" + user + ";port=" + port + ";password=" + password + ";";

            try
            {
                conn = new MySqlConnection(cs);
                conn.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        internal void updateStock(StorageItem item)
        {
            string sql = "";
            cmd = new MySqlCommand();
            MySqlTransaction trans = conn.BeginTransaction(); ;
            try
            {
                sql = "UPDATE " + dbName + ".STORAGE_DETAIL SET STOCK = @stock WHERE PRODUCT_ID = @product_id AND HOUSE_ID = @house_id";
                cmd = new MySqlCommand(sql, conn);
                cmd.Transaction = trans;
                cmd.Parameters.AddWithValue("@product_id", item.Id);
                cmd.Parameters.AddWithValue("@house_id", currentStorage);
                cmd.Parameters.AddWithValue("@stock", item.Stock);
                cmd.ExecuteNonQuery();
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
            }
        }

        public StorageItem selectProduct(string product_id)
        {
            StorageItem item = new StorageItem();
            string sql = "";
            cmd = new MySqlCommand();

            try
            {
                sql = "SELECT NAME, BRAND, STOCK FROM " + dbName + ".STORAGE_DETAIL WHERE PRODUCT_ID=@product_id AND HOUSE_ID=@house_id";
                cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@product_id", product_id);
                cmd.Parameters.AddWithValue("@house_id", DataManagement.currentStorage);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    item.Name = rdr["NAME"].ToString();
                    item.Brand = rdr["BRAND"].ToString();
                    item.Id = Int32.Parse(product_id);
                    item.Stock = Int32.Parse(rdr["STOCK"].ToString()) - 1;

                    if (item.Stock < 0)
                    {
                        item.Stock = 0;
                        item.Name = "Item not found";
                        item.Brand = "";
                    }

                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                rdr.Close();
                return item;
            }
            return item;
        }



        public List<String> getLoginInfo(string username)
        {
            string sql = "";
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader rdr = null;
            List<String> infoList = new List<String>();

            sql = "SELECT USERNAME, PWD, CREATION_DATE, ROL, USER_ID FROM " + dbName + ".USERS WHERE USERNAME = @username";

            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@username", username);

            rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                infoList.Add(rdr["USERNAME"].ToString());
                infoList.Add(rdr["PWD"].ToString());
                infoList.Add(rdr["CREATION_DATE"].ToString());
                infoList.Add(rdr["ROL"].ToString());
                infoList.Add(rdr["USER_ID"].ToString());
            }
            rdr.Close();

            return infoList;
        }

        public DataTable Storage_Selection(int user_id)
        {
            string sql = "";
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader rdr = null;
            DataTable selectTable = new DataTable();


            sql = "SELECT * FROM " + dbName + ".STORAGE WHERE USER_ID = @user_id";
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@user_id", user_id);
            rdr = cmd.ExecuteReader();
            selectTable.Load(rdr);
            rdr.Close();


            return selectTable;
        }

        public string hashpwd(string contra, string fecha)
        {

            string fechaHash = fecha;

            fechaHash = fechaHash.Trim();
            fechaHash = fechaHash.Replace(":", "");
            fechaHash = fechaHash.Replace("/", "");
            string hashDate = mD5(fechaHash);
            string hashpass = mD5(contra);
            string salContra = mD5(hashDate + hashpass);

            string pebre = salContra;
            string sSubCadena = pebre.Substring(0, (int)(pebre.Length / 2));
            string sSubCadena2 = pebre.Substring((int)(pebre.Length / 2), (int)(pebre.Length / 2));
            string Contrapebre = sSubCadena2 + sSubCadena;


            string contraFinal = mD5(salContra + Contrapebre);
            return contraFinal;

        }

        private string mD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

    }
}
