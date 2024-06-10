
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementFunctions
{
    public class Person
    {
        private string _name; // Encapsulated private field

        public string Name  // Public property with getter and setter
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _address;
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }
        public Person(string name, string address) // Constructor to initialize name and address
        {
            Name = name;
            Address = address;
        }
    }
}
