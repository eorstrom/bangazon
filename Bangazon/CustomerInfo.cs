using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Bangazon
{
    public class CustomerInfo
    {   // allow customer info to be get and set
        public int listIndex { get; set; }
        public string CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }

        // Customer constructor
        public CustomerInfo(int listIndex, string CustomerId, string FirstName, string LastName, string AddressLine1, string AddressLine2, string City, string State, string Zip, string Phone)
        {
            this.listIndex = listIndex;
            this.CustomerId = CustomerId;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.AddressLine1 = AddressLine1;
            this.AddressLine2 = AddressLine2;
            this.City = City;
            this.State = State;
            this.Zip = Zip;
            this.Phone = Phone;
        }
    }
}
