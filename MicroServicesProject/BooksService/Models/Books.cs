using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Npgsql;
using System.Data;

namespace BooksService
{
    public class Books
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
        public bool active { get; set; } = true;
        public bool delete { get; set; } = false;
        public int ReqID { get; set; } = 100;


        // Functions

        public NpgsqlConnection connect()
        {
            string con = Startup.strconBook;
            return new NpgsqlConnection(con);
        }
        public int GetBookCount() {

            NpgsqlConnection con = connect();

            try
            {
                con.Open();
                string Query = "select count(bookid) from Books;";
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

        public List<Books> GetBookList()
        {
            List<Books> lst = new List<Books>();
            DataSet dt = new DataSet();
            
            NpgsqlConnection con = connect();

            try
            {
                con.Open();
                string Query = $"select GetBooks('ref1');fetch all from \"ref1\";";
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(Query, con);
                adapter.Fill(dt);

                lst = dt.Tables[1].AsEnumerable()
                .Select(dataRow => new Books
                {
                    ID = dataRow.Field<int>("bookid"),
                    Title = dataRow.Field<string>("booktitle"),
                    Image = dataRow.Field<string>("bookpath"),
                    Description = dataRow.Field<string>("bookdetail"),
                    Price = dataRow.Field<int>("bookprice"),
                    active = dataRow.Field<bool>("isactive"),
                    delete = dataRow.Field<bool>("isdelete")

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

        public int InsertInToDB(Books product)
        {
            NpgsqlConnection con = connect();

            try
            {
                con.Open();
                string Query = @$"select AddBook('{product.Title}','{product.Image}','{product.Description}','{product.Price}','{product.active}','{product.delete}','{product.ReqID}');";
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
    
    
        public bool AddBooks(Books Product)
        {
            try
            {
                int ID = GetBookCount() + 1;

                // Get Extension
                string[] Split1 = Product.Image.Split(",");
                string[] Split2 = Split1[0].Split("/");
                string[] Split3 = Split2[1].Split(";");

                string Extension = Split3[0];
                // Get Extension

                string ImageName = "ProductImg" + ID + "." + Extension;

                string Path = @"C:/Prashant/MicroServices/Project/MicroServices/Website/src/assets/Images/Books/" + ImageName;
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
