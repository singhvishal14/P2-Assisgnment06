using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_06
{
    internal class Program
    {
        static SqlDataReader reader;
        static SqlCommand cmd;
        static SqlConnection con;
        static string conStr = "server=DESKTOP-FUGQNF4;database=ProductInventoryDB;trusted_connection=true;";
        static void Main(string[] args)
        {
            try
            {
                con = new SqlConnection(conStr);
                cmd = new SqlCommand("select * from Products", con);
                Console.WriteLine("Which Opertion You want Perform\n1.Display All products\n2. Insert the data\n3. Update the data\n4. Remove the data\n Press Keys to Perform Opertion");
                int op = int.Parse(Console.ReadLine());
                switch (op)
                {
                    case 1:
                        {
                            con.Open();
                            reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                Console.WriteLine("ProductID : " + reader["ProductId"]);
                                Console.WriteLine("Product Name : " + reader["ProductName"]);
                                Console.WriteLine("Price : " + reader["Price"]);
                                Console.WriteLine("Quantity: " + reader["Quantity"]);
                                Console.WriteLine("MfDate: " + reader["MfDate"]);
                                Console.WriteLine("ExpDate: " + reader["ExpDate"]);

                                Console.WriteLine("--------------------------------------------------------");


                            }
                            con.Close();
                            break;

                        }
                    case 2:
                        {
                            con = new SqlConnection(conStr);
                            cmd = new SqlCommand()
                            {
                                CommandText = "insert into Products(ProductID,ProductName,Price,Quantity,MfDate,ExpDate)values(@id,@name,@price,@quantity,@mfdate,@expdate)",
                                Connection = con
                            };
                            Console.WriteLine("Enter Product Id");
                            cmd.Parameters.AddWithValue("@id", int.Parse(Console.ReadLine()));
                            Console.WriteLine("Enter Product Name");
                            cmd.Parameters.AddWithValue("@name", Console.ReadLine());
                            Console.WriteLine("Enter Price");
                            cmd.Parameters.AddWithValue("@price", Console.ReadLine());
                            Console.WriteLine("Enter Quantity");
                            cmd.Parameters.AddWithValue("@quantity", double.Parse(Console.ReadLine()));
                            Console.WriteLine("Enter MfDate");
                            cmd.Parameters.AddWithValue("@mfdate", Console.ReadLine());
                            Console.WriteLine("Enter ExpDate");
                            cmd.Parameters.AddWithValue("@expdate", Console.ReadLine());
                            con.Open();
                            int nor = cmd.ExecuteNonQuery();
                            if (nor >= 1)
                            {
                                Console.WriteLine("Record Inserted!!!");
                            }
                            con.Close();
                            break;
                        }
                    case 3:
                        {

                            int id;
                            Console.WriteLine("Enter Product ID to update details ");
                            id = int.Parse(Console.ReadLine());
                            con = new SqlConnection(conStr);
                            cmd = new SqlCommand()
                            {
                                CommandText = "select * from Products where ProductId=@id ",
                                Connection = con
                            };
                            cmd.Parameters.AddWithValue("@id", id);
                            con.Open();
                            reader = cmd.ExecuteReader();
                            if (reader.HasRows)
                            {
                                con.Close();
                                con.Open();
                                cmd.CommandText = "update Products set Quantity=@qty where ProductId=@pid";
                                Console.WriteLine("Enter New Quantity ");
                                cmd.Parameters.AddWithValue("@qty", Console.ReadLine());
                                cmd.Parameters.AddWithValue("@pid", id);
                                cmd.ExecuteNonQuery(); Console.WriteLine("Record Updated on Database");
                            }
                            else
                            {
                                Console.WriteLine($"No Such ProductId {id} exist in our database");
                            }
                            break;
                        }
                    case 4:
                        {
                            con = new SqlConnection(conStr);
                            cmd = new SqlCommand()
                            {
                                CommandText = "Delete from Products where  ProductId=@id",
                                Connection = con
                            };
                            Console.WriteLine("Enter Product Id to Delete");
                            cmd.Parameters.AddWithValue("@id", int.Parse(Console.ReadLine()));

                            con.Open();
                            int nor = cmd.ExecuteNonQuery();
                            if (nor >= 1)
                            {
                                Console.WriteLine("Record Deleted from Database!!!");
                            }
                            else
                            {
                                Console.WriteLine("No such Id  not exist");
                            }
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Invalid operation choice");
                            return;
                        }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex.Message);
            }
            finally
            {
                //con.Close();
                Console.ReadKey();
            }
        }
    }
}