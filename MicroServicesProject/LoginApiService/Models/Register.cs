using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace LoginApiService.Models
{
    public class Register
    {
        public string name { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public bool isactive { get; set; }
        public bool isdeleted { get; set; }


        public NpgsqlConnection connect()
        {
            string con = Startup.strconBook;
            return new NpgsqlConnection(con);
        }

        public int RegisterUser(Register product)
        {
            NpgsqlConnection con = connect();

            try
            {
                con.Open();
                string Query = @$"insert into micrologin(name,email,username,password,isactive,isdeleted) values('{product.name}','{product.email}','{product.username}','{product.password}',true,false);";
                NpgsqlCommand cmd = new NpgsqlCommand(Query, con);
                int res = cmd.ExecuteNonQuery();

                return 1;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error :  ", e.Message);
                return -1;
            }
            finally
            {
                con.Close();
            }
        }

        public int LoginUser(Register product)
        {
            NpgsqlConnection con = connect();

            try
            {
                con.Open();
                string Query = @$"select isLoggedIn('{product.username}','{product.password}');";
                NpgsqlCommand cmd = new NpgsqlCommand(Query, con);
                int res = (int)cmd.ExecuteScalar();

                return res;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error :  ", e.Message);
                return 0;
            }
            finally
            {
                con.Close();
            }
        }


        public int GetUserID(Register product)
        {
            NpgsqlConnection con = connect();

            try
            {
                con.Open();
                string Query = @$"select getUserid('{product.username}','{product.password}');";
                NpgsqlCommand cmd = new NpgsqlCommand(Query, con);
                int res = (int)cmd.ExecuteScalar();

                return res;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error :  ", e.Message);
                return 0;
            }
            finally
            {
                con.Close();
            }
        }
    }
}
