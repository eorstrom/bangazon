﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bangazon
{
    public class MenuOptions
    {
        public static List<CustomerInfo> AddCustomer()
        {
            // gather customer info through user input
            Console.Write("\nEnter new customer ID\n> ");
            string customerId = Console.ReadLine();
            Console.Write("\nEnter customer name\n> ");
            string customerName = Console.ReadLine();
            Console.Write("\nEnter street address\n> ");
            string streetAddress = Console.ReadLine();
            Console.Write("\nEnter city\n> ");
            string city = Console.ReadLine();
            Console.Write("\nEnter state\n> ");
            string state = Console.ReadLine();
            Console.Write("\nEnter postal code\n> ");
            string zip = Console.ReadLine();
            Console.Write("\nEnter phone number\n> ");
            string phone = Console.ReadLine();

            // add customer info gathered from user input to Customer table in database
            StringBuilder command = new StringBuilder();
            command.Append("INSERT INTO Customer ");
            command.Append("(IdCustomer, FirstName, LastName, StreetAddress, City, StateProvince, PostalCode, PhoneNumber) ");
            command.Append("VALUES (");
            command.Append("'" + customerId + "',");
            // split first and last name in order to put them in the appropriate columns in the Customer table
            command.Append("'" + customerName.Split(' ')[0] + "',");
            command.Append("'" + customerName.Split(' ')[1] + "',");
            command.Append("'" + streetAddress + "',");
            command.Append("'" + city + "',");
            command.Append("'" + state + "',");
            command.Append("'" + zip + "',");
            command.Append("'" + phone + "'");
            command.Append(")");

            DatabaseActions.executeNonQuery(command.ToString());  // do SQL INSERT
            Console.WriteLine("Added new customer");
            return DatabaseActions.loadCustomers();  // return list that includes newly added customer info (row)
        }

        public static void AddPaymentType(List<CustomerInfo> customerList)
        {
            Console.WriteLine("Which customer?");
            // better: instead of loop, use LINQ to create display list
            List<string> displayList = new List<string>();
            foreach (CustomerInfo c in customerList)
            {
                displayList.Add(c.FirstName + c.LastName);
            }
            InternalOperations.displayMenu(displayList);
            int customerIndexChosen = InternalOperations.getUserChoice();
            string customerIdChosen = customerList[customerIndexChosen].CustomerId;

            List<PaymentType> paymentTypeList = DatabaseActions.loadPaymentTypes();
            Console.Write("\nEnter payment type:\n");
            // better: instead of loop, use LINQ to create display list
            displayList = new List<String>();
            foreach (PaymentType pt in paymentTypeList)
            {
                displayList.Add(pt.name);
            }
            InternalOperations.displayMenu(displayList);
            int paymentTypeIndexChosen = InternalOperations.getUserChoice();
            int paymentTypeIdChosen = paymentTypeList[paymentTypeIndexChosen].paymentTypeId;

            Console.Write("\nEnter account number:\n> ");
            string accountNumber = Console.ReadLine();

            StringBuilder command = new StringBuilder();
            command.Append("INSERT INTO PaymentTypesAvailable ");
            command.Append("(customerId, paymentTypeId, accountNumber) ");
            command.Append("VALUES ('" + customerIdChosen + "', " + paymentTypeIdChosen + ", '" + accountNumber + "')");

            DatabaseActions.executeNonQuery(command.ToString());  // do SQL INSERT
            Console.WriteLine("Added payment type");
        }

        public static List<Product> ChooseProducts(List<Product> productList, List<Product> lineItems)
        {
        PickAProduct:
            Console.WriteLine("Which product?");
            // better: instead of loop, use LINQ to create display list
            List<string> displayList = new List<string>();
            foreach (Product p in productList)
            {
                displayList.Add(p.name);
            }
            InternalOperations.displayMenu(displayList);
            Console.WriteLine("9. Back to Main Menu");
            int productIndexChosen = InternalOperations.getUserChoice();
            if (productIndexChosen == 8) return lineItems;
            lineItems.Add(productList[productIndexChosen]);
            Console.WriteLine("Added line item: {0}", productList[productIndexChosen].name);
            goto PickAProduct;
        }

        public static List<Product> CloseOrder(List<Product> lineItems, List<CustomerInfo> customerList)
        {
            if (lineItems.Count == 0)
            {
                Console.WriteLine("Please add some products to your order first. Press enter to return to main menu.");
                Console.ReadLine();
                return lineItems;
            }

            decimal totalPrice = 0;
            foreach (Product p in lineItems)
            {
                totalPrice += p.price;
            }
            Console.WriteLine("Your order total is ${0:0.00}. Ready to purchase", totalPrice);
            Console.Write("(Y/N)? ");
            string yesOrNo = Console.ReadLine();
            if (yesOrNo == "N" || yesOrNo == "n") return lineItems;

            // get customerId
            Console.WriteLine("\nWhich customer is placing the order?");
            // better: instead of loop, use LINQ to create display list
            List<string> displayListC = new List<string>();
            foreach (CustomerInfo c in customerList)
            {
                displayListC.Add(c.FirstName + c.LastName);
            }
            InternalOperations.displayMenu(displayListC);
            int customerIndexChosen = InternalOperations.getUserChoice();
            string customerIdChosen = customerList[customerIndexChosen].CustomerId;

            // get paymentTypes available for this customer
            List<PaymentType> paymentTypesAvailable = DatabaseActions.loadPaymentTypesAvailable(customerIdChosen);
            Console.WriteLine("Which payment type?");
            // better: instead of loop, use LINQ to create display list
            List<string> displayListPTA = new List<string>();
            foreach (PaymentType pt in paymentTypesAvailable)
            {
                displayListPTA.Add(pt.name);
            }
            InternalOperations.displayMenu(displayListPTA);
            int paymentTypeIndexChosen = InternalOperations.getUserChoice();
            int paymentTypeIdChosen = paymentTypesAvailable[paymentTypeIndexChosen].paymentTypeId;

            DatabaseActions.createOrder(customerIdChosen, paymentTypeIdChosen, lineItems);
            Console.WriteLine("Invoice added.\n");
            lineItems = new List<Product>(); // clear lineItems list
            return lineItems;
        }

        public static void ReportPopularProducts(List<Product> productList)
        {
            Console.WriteLine("\n** Product Popularity and Revenue **\n");
            List<string> report = DatabaseActions.getPopularProducts(productList);
            foreach (string s in report)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine("\nPress enter to return to main menu.");
            Console.ReadLine();
        }
    }
}
