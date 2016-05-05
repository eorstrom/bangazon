# Bangazon!

The Command Line Ordering System
In this exercise, you will be allowing a user to interact with a basic product ordering database.

Setup
Create a new Visual Studio console solution named Bangazon.
Instructions
You will create a series of prompts that will allow the user to create various types of data in your Invoices database.

Main Menu
*********************************************************
**  Welcome to Bangazon! Command Line Ordering System  **
*********************************************************
1. Create an account
2. Create a payment option
3. Order a product
4. Complete an order
5. See product popularity
6. Leave Bangazon!
>
Create Customer
Enter customer name
>

Enter street address
>

Enter city
>

Enter state
>

Enter postal code
>

Enter phone number
>
Create Payment Option
Which customer?
1. John Q. Public
2. Svetlana Z. Herevazena
> 

Enter payment type (e.g. AmEx, Visa, Checking)
>

Enter account number
>
Order a Product
Note: These are examples. Add your own products to the Product table.
To make it easier to add multiple products, when the user selects a product to order, display the menu of products again. Make sure the last option of Back to main menu so the user can specify that no more products are needed.

1. Diapers
2. Case of Cracking Cola
3. Bicycle
4. AA Batteries
...
9. Back to main menu
Complete an Order
If no products have been selected yet
Please add some products to your order first. Press any key to return to main menu.
If there are current products in an order
Your order total is $149.54. Ready to purchase
(Y/N) >

# If user entered Y
Which customer is placing the order?
1. John Q. Public
2. Svetlana Z. Herevazena
> 

Choose a payment option
1. Amex
2. Visa
>

Your order is complete! Press any key to return to main menu.

# If user entered N, display the main menu again
Once the order is complete, show the main menu again, where the user can start creating another order.

See product popularity
AA Batteries ordered 10 times by 2 customers for total revenue of $99.90
Diapers ordered 5 times by 1 customers for total revenue of $64.95
Case of Cracking Cola ordered 4 times by 3 customers for total revenue of $27.96

-> Press any key to return to main menu
References

Inserting into table with SQL
string command = @"
INSERT INTO Customer
    (FirstName, LastName, StreetAddress)
VALUES
    ('Sophia', 'Vargas', '801 Kilgore Street')
";

System.Data.SqlClient.SqlConnection sqlConnection1 = 
    new System.Data.SqlClient.SqlConnection("YOUR CONNECTION STRING");

System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
cmd.CommandType = System.Data.CommandType.Text;
cmd.CommandText = command;
cmd.Connection = sqlConnection1;

sqlConnection1.Open();
cmd.ExecuteNonQuery();
sqlConnection1.Close();
External reference: How to: Insert New Records into a Database
