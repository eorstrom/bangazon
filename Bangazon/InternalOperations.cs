using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bangazon
{
    public class InternalOperations
    {
        // code to determine which menu option path to display and follow
        public static void displayMenu(List<string> linesToDisplay)
        {
            int index = 1;
            foreach (string line in linesToDisplay)
            {
                Console.WriteLine("{0}. {1}", index++, line);
            }
        }

        public static int getUserChoice()
        {
            Console.Write("> ");
            string choice = Console.ReadLine();
            // future improvement: validate input!
            return Int32.Parse(choice) - 1;
        }
    }
}
