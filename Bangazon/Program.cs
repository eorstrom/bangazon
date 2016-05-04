using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Bangazon
{
    class Program
    {
        static void Main()
        {
            Menu.main();

            switch (userChoice)
            {
                case "1":
                    Console.WriteLine("Please enter customer information.");
                    Console.WriteLine("Name: ");
                    string customerName = Console.ReadLine();
                    Console.WriteLine("Street Address: ");
                    string customerStreet = Console.ReadLine();
                    Console.WriteLine("City: ");
                    string customerCity = Console.ReadLine();
                    Console.WriteLine("State: ");
                    string customerState = Console.ReadLine();
                    Console.WriteLine("Postal Code (Zip): ");
                    string customerZip = Console.ReadLine();
                    Console.WriteLine("Phone Number: ");
                    string customerPhone = Console.ReadLine();
                    break;
                case "2":
                    Console.WriteLine("Enter a payment type: ");
                    string paymentType = Console.ReadLine();
                    Console.WriteLine("Enter an account number: ");
                    string accountNumber = Console.ReadLine();
                    break;
                case "3":
                    break;
                case "4":
                    break;
                case "5":
                    break;
                case "6":
                    Console.WriteLine("Come back soon!");
                    break;
                default:
                    break;
            }

        }

        static int doSql(string command)
        {
            System.Data.SqlClient.SqlConnection sqlConnection1 =
                new System.Data.SqlClient.SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB; AttachDbFilename=\'C:\\Users\\Erin\\Documents\\Visual Studio 2015\\Projects\\Invoices\\Invoices\\Invoices.mdf'; Integrated Security= True");

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = command;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();

            return 0;
        }
    }
}
