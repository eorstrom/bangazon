using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace Bangazon
{
    public class DatabaseActions
    {
        // create a new connection to the database location where information is being retrieved from and stored in 
        const string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB; AttachDbFilename=\'C:\\Users\\Erin\\Documents\\Visual Studio 2015\\Projects\\Invoices\\Invoices\\Invoices.mdf'; Integrated Security= True";

        public static void executeNonQuery(string command)  // use this for INSERTs
        {
            SqlConnection sqlConnection1 = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = command;
            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
        }

        public static List<CustomerInfo> loadCustomers()
        // return list of all customers in DB
        {
            List<CustomerInfo> customerList = new List<CustomerInfo>();
            string query = @"SELECT c.CustomerId,c.FirstName,c.LastName,c.Address1,c.Address2,c.City,c.State,c.ZipCode,c.Phone FROM Customer c";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                connection.Open();
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    // Check if the reader has any rows at all before starting to read.
                    if (r.HasRows)
                    {
                        // If it has rows, then read advances to the next row.
                        int index = 0;
                        while (r.Read())
                        {
                            CustomerInfo c = new CustomerInfo(index++, r[0] as string, r[1] as string, r[2] as string, r[3] as string, r[4] as string, r[5] as string, r[6] as string, r[7] as string, r[8] as string);
                            customerList.Add(c);
                        }
                    }
                }
                connection.Close();
            }
            return customerList;
        }

        public static List<Product> loadProducts()
        // return list of all products in the Database
        {
            List<Product> productList = new List<Product>();
            string query = @"SELECT p.productId, p.name, p.price FROM Product p";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                connection.Open();
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    // Check if the reader has any rows at all before starting to read.
                    if (r.HasRows)
                    {
                        // Read advances to the next row, if the reader has rows.
                        int index = 0;
                        while (r.Read())
                        {
                            Product p = new Product(index++, r[0] as int? ?? 0, r[1] as string, r[2] as decimal? ?? 0);
                            productList.Add(p);
                        }
                    }
                }
                connection.Close();
            }
            return productList;
        }

        public static List<PaymentType> loadPaymentTypes()
        // return list of all payment types in Database
        {
            List<PaymentType> paymentTypeList = new List<PaymentType>();
            string query = @"SELECT pt.paymentTypeId, pt.name FROM PaymentType pt";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                connection.Open();
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    // Check if the reader has any rows before starting to read.
                    if (r.HasRows)
                    {
                        // Read advances to the next row if there are rows.
                        int index = 0;
                        while (r.Read())
                        {
                            PaymentType pt = new PaymentType(index++, r[0] as int? ?? 0, r[1] as string);
                            paymentTypeList.Add(pt);
                        }
                    }
                }
                connection.Close();
            }
            return paymentTypeList;
        }

        public static List<PaymentType> loadPaymentTypesAvailable(string customerId)
        // return list of payment types available for this customer
        {
            List<PaymentType> paymentTypesAvailable = new List<PaymentType>();
            string query = @"SELECT pt.paymentTypeId, pt.name FROM PaymentType pt INNER JOIN PaymentTypesAvailable pyt ON pyt.paymentTypeId = pt.paymentTypeId WHERE pyt.customerId = '" + customerId + "'";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                connection.Open();
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    // Check if the reader has any rows before starting to read.
                    if (r.HasRows)
                    {
                        // Read advances to the next row if there are rows.
                        int index = 0;
                        while (r.Read())
                        {
                            PaymentType pyt = new PaymentType(index++, r[0] as int? ?? 0, r[1] as string);
                            paymentTypesAvailable.Add(pyt);
                        }
                    }
                }
                connection.Close();
            }
            return paymentTypesAvailable;
        }

        public static void createOrder(string customerId, int paymentTypeId, List<Product> lineItems)
        // this executes the database updates associated with MainMenu.CloseOrder
        {
            // determine the next Invoice ID number
            
            int maxInvoiceId = 1000; // use 1000 if Invoices table is empty
            string query = @"SELECT MAX(invoiceId) FROM Invoices";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                connection.Open();
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    if (r.HasRows)
                    {
                        // Read advances to the next row if the reader has rows.
                        while (r.Read())
                        {
                            maxInvoiceId = r[0] as int? ?? 0;
                        }
                    }
                }
                connection.Close();
            }

            // add new row to Invoices table
            StringBuilder command5 = new StringBuilder();
            command5.Append("INSERT INTO Invoices ");
            command5.Append("(invoiceId, customerId, paymentTypeId) ");
            command5.Append("VALUES (");
            command5.Append((maxInvoiceId + 1).ToString() + ", ");
            command5.Append("'" + customerId + "', ");
            command5.Append(paymentTypeId.ToString());
            command5.Append(")");
            DatabaseActions.executeNonQuery(command5.ToString());

            // add new rows to LineItems table
            foreach (Product p in lineItems)
            {
                StringBuilder command4 = new StringBuilder();
                command4.Append("INSERT INTO LineItems");
                command4.Append("(invoiceId, productId) ");
                command4.Append("VALUES (");
                command4.Append((maxInvoiceId + 1).ToString() + ",");
                command4.Append(p.productId);
                command4.Append(")");
                DatabaseActions.executeNonQuery(command4.ToString());
            }
        }

        public static List<string> getPopularProducts(List<Product> productList)
        {
            // for each product, report how many were sold and number of customers who bought it
            List<string> report = new List<string>();
            foreach (Product p in productList)
            {
                string query = @"SELECT COUNT(DISTINCT y.foo) as customers, count(y.bar) as units
    FROM (SELECT i.customerId as foo, li.productId as bar from Invoices i 
    INNER JOIN LineItems li ON li.invoiceId = i.invoiceId WHERE li.productId = '" + p.productId + "') y";
                using (SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB; AttachDbFilename=\"C:\\Users\\shu\\workspace\\cs\\Bangazon\\Bangazon\\Bangazon.mdf\"; Integrated Security= True"))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Check if the reader has any rows before starting to read.
                        if (reader.HasRows)
                        {
                            // Read advances to the next row if reader has rows.
                            while (reader.Read())
                            {
                                int customersWhoBought = reader[0] as int? ?? 0;
                                int unitsSold = reader[1] as int? ?? 0;
                                var regex = new Regex("[ ]+$");  // for eliminating trailing white space
                                string reportLine = String.Format("{0} ordered {1} times by {2} customers for total revenue of ${3:0.00}", regex.Replace(p.name, ""), unitsSold, customersWhoBought, unitsSold * p.price);
                                report.Add(reportLine);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            return report;
        }
    }
}
