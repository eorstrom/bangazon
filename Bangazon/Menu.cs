using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Bangazon
{
    public class Menu
    {
        public static void main()
        {
            List<Product> productList = DatabaseActions.loadProducts();
            List<CustomerInfo> customerList = DatabaseActions.loadCustomers();
            List<Product> lineItems = new List<Product>();

            MainMenu:
            Console.WriteLine("\n******************************************************");
            Console.WriteLine("** Welcome to Bangazon Command Line Ordering System **");
            Console.WriteLine("******************************************************");
            List<string> displayList = new List<string>
                    { "Create an Account",
                      "Create a Payment Option",
                      "Order a Product",
                      "Complete an Order",
                      "See Product Popularity",
                      "Leave Bangazon" };
            InternalOperations.displayMenu(displayList);
            // switch statement to handle user's input indicating a choice 
            switch (InternalOperations.getUserChoice())
            {
                // Create an Account/New Customer
                case 0:
                    customerList = MenuOptions.AddCustomer();
                    break;
                // Create a Payment Option
                case 1:
                    MenuOptions.AddPaymentType(customerList);
                    break;
                // Order a Product
                case 2:
                    lineItems = MenuOptions.ChooseProducts(productList, lineItems);
                    break;
                // Complete an Order
                case 3:
                    lineItems = MenuOptions.CloseOrder(lineItems, customerList);
                    break;
                // See Product Popularity
                case 4:
                    MenuOptions.ReportPopularProducts(productList);
                    break;
                // Leave Bangazon
                case 5:
                    goto End;
                // Exit switch statement
                default:
                    break;
            }
            goto MainMenu;
        End:
            Console.WriteLine("Come back soon!");
        }
    }
}
