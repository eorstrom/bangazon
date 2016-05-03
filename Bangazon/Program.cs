using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bangazon
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> customers = new List<string>();
            customers.Add("Lily Owens");

            List<string> lineItems = new List<string>();

            Console.WriteLine("*********************************************************");
            Console.WriteLine("**  Welcome to Bangazon! Command Line Ordering System  **");
            Console.WriteLine("*********************************************************");
            Console.WriteLine("1. Create and Account");
            Console.WriteLine("2. Create a Payment Option");
            Console.WriteLine("3. Order a Product");
            Console.WriteLine("4. Complete an Order");
            Console.WriteLine("5. See Product Popularity");
            Console.WriteLine("6. Leave Bangazon!");

            Console.WriteLine("What would you like to do? (Enter a number choice)");
            string userChoice = Console.ReadLine();

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
    }
}
