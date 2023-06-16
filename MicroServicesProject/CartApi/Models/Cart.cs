using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CartApi.Models
{
    public class Cart
    {
        public int UserID { get; set; } 
        public int ProductID { get; set; }
        public int Price { get; set; }
        public int RequestID { get; set; }


        // Functions

        public NpgsqlConnection connect()
        {
            string con = Startup.strconBook;
            return new NpgsqlConnection(con);
        }

        public int GetCartCount(int UserIDforCount)
        {

            NpgsqlConnection con = connect();

            try
            {
                con.Open();
                string Query = $"select count(reqid) from cart where userid={UserIDforCount};";
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


        public List<Products> GetCartList(int User)
        {
            List<Products> lst = new List<Products>();
            DataSet dt = new DataSet();

            NpgsqlConnection con = connect();

            try
            {
                con.Open();
                string Query = $"select GetCartData('ref1',{User});fetch all from \"ref1\";";
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(Query, con);
                adapter.Fill(dt);

                lst = dt.Tables[1].AsEnumerable()
                .Select(dataRow => new Products
                {
                    ID = dataRow.Field<int>("bookid"),
                    Title = dataRow.Field<string>("title"),
                    Image = dataRow.Field<string>("imgpath"),
                    Price = dataRow.Field<int>("price"),
                    Quantity = dataRow.Field<int>("bqty"),
                    ReqID = dataRow.Field<int>("reqid"),
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

        public bool AddToBookCart(Products product)
        {
            NpgsqlConnection con = connect();

            try
            {
                con.Open();
                string Query = @$"select AddToBookCart('{product.UserID}','{product.ID}','{product.Title}','{product.Image}','{product.Price}');";
                NpgsqlCommand cmd = new NpgsqlCommand(Query, con);
                int res = cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error :  ", e.Message);
                return false;
            }
            finally
            {
                con.Close();
            }
        }


        public bool AddToCompCart(Products product)
        {
            NpgsqlConnection con = connect();

            try
            {
                con.Open();
                string Query = @$"select AddToCompCart('{product.UserID}','{product.ID}','{product.Title}','{product.Image}','{product.Price}');";
                NpgsqlCommand cmd = new NpgsqlCommand(Query, con);
                int res = cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error :  ", e.Message);
                return false;
            }
            finally
            {
                con.Close();
            }
        }


        public bool AddToFashCart(Products product)
        {
            NpgsqlConnection con = connect();

            try
            {
                con.Open();
                string Query = @$"select AddToFashCart('{product.UserID}','{product.ID}','{product.Title}','{product.Image}','{product.Price}');";
                NpgsqlCommand cmd = new NpgsqlCommand(Query, con);
                int res = cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error :  ", e.Message);
                return false;
            }
            finally
            {
                con.Close();
            }
        }


        public bool UpdateQuantity(Products product)
        {
            NpgsqlConnection con = connect();

            try
            {
                con.Open();
                string Query = @$"select UpdateQuantity('{product.UserID}','{product.ID}','{product.ReqID}','{product.Quantity}');";
                NpgsqlCommand cmd = new NpgsqlCommand(Query, con);
                int res = cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error :  ", e.Message);
                return false;
            }
            finally
            {
                con.Close();
            }
        }


        public bool RemoveFromCart(Products product)
        {
            NpgsqlConnection con = connect();

            try
            {
                con.Open();
                string Query = @$"select DeleteCart('{product.UserID}','{product.ID}','{product.ReqID}');";
                NpgsqlCommand cmd = new NpgsqlCommand(Query, con);
                int res = cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error :  ", e.Message);
                return false;
            }
            finally
            {
                con.Close();
            }
        }

    }
}
