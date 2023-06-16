using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Npgsql;
using System.Data;
using FashionApi;

namespace BooksService
{
    public class Fash
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
        public bool active { get; set; } = true;
        public bool delete { get; set; } = false;
        public int ReqID { get; set; } = 300;


        // Functions

        public NpgsqlConnection connect()
        {
            string con = Startup.strconBook;
            return new NpgsqlConnection(con);
        }
        public int GetFashCount() {

            NpgsqlConnection con = connect();

            try
            {
                con.Open();
                string Query = "select count(fashid) from Fash;";
                NpgsqlCommand cmd = new NpgsqlCommand(Query, con);
                object dt = cmd.ExecuteScalar();

                long res = (Int64)dt;

                return Convert.ToInt32(res);
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

        public List<Fash> GetFashList()
        {
            List<Fash> lst = new List<Fash>();
            DataSet dt = new DataSet();
            
            NpgsqlConnection con = connect();

            try
            {
                con.Open();
                string Query = $"select GetFash('ref1');fetch all from \"ref1\";";
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(Query, con);
                adapter.Fill(dt);

                lst = dt.Tables[1].AsEnumerable()
                .Select(dataRow => new Fash
                {
                    ID = dataRow.Field<int>("fashid"),
                    Title = dataRow.Field<string>("fashtitle"),
                    Image = dataRow.Field<string>("fashpath"),
                    Description = dataRow.Field<string>("fashdetail"),
                    Price = dataRow.Field<int>("fashprice"),
                    active = dataRow.Field<bool>("isactive"),
                    delete = dataRow.Field<bool>("isdelete"),
                    ReqID = dataRow.Field<int>("reqid")

                }).ToList();

                return lst;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error :  ", e.Message);
                return lst;
            }
            finally
            {
                con.Close();
            }
        }

        public int InsertInToDB(Fash product)
        {
            NpgsqlConnection con = connect();

            try
            {
                con.Open();
                string Query = @$"select AddFash('{product.Title}','{product.Image}','{product.Description}','{product.Price}','{product.active}','{product.delete}','{product.ReqID}');";
                NpgsqlCommand cmd = new NpgsqlCommand(Query, con);
                int res = cmd.ExecuteNonQuery();
                
                return 1;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error :  " , e.Message);
                return -1;
            }
            finally
            {
                con.Close();
            }
        }
    
    
        public bool AddFash(Fash Product)
        {
            try
            {
                int ID = GetFashCount() + 1;

                // Get Extension
                string[] Split1 = Product.Image.Split(",");
                string[] Split2 = Split1[0].Split("/");
                string[] Split3 = Split2[1].Split(";");

                string Extension = Split3[0];
                // Get Extension

                string ImageName = "ProductImg" + ID + "." + Extension;

                string Path = @"C:/Prashant/MicroServices/Project/MicroServices/Website/src/assets/Images/Fashion/" + ImageName;
                string result = Regex.Replace(Product.Image, @"^data:image\/[a-zA-Z]+;base64,", string.Empty);
                File.WriteAllBytes(Path, Convert.FromBase64String(result));

                Product.Image = ImageName;

                int res = InsertInToDB(Product);

                if(res == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : ",e.Message);
                return false;
            }
        }
    }
}
