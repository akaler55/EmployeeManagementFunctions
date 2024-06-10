using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace EmployeeManagementFunctions
{
    
    public class Employee : Person
    {
        private string _Id; // Encapsulated private field
        public string id // Public property with getter and setter
        {
            get { return _Id; }
            set
            {
                _Id = value;
            }
        }

        private string _position; // Encapsulated private field
        public string Position// Public property with getter and setter
        {
            get { return _position; }
            set 
            {
                _position = value;
            }
        }

        private double _salary; // Encapsulated private field
        public double Salary// Public property with getter and setter
        {
            get { return _salary; }
            set
            {
               _salary = value;
            }
        }

        private string _department; // Encapsulated private field
        public string Department// Public property with getter and setter
        {
            get { return _department; }
            set { _department = value; }
        }

        public Employee(string name, string id, string position, double salary, string department, string address)
            : base(name,address) // Call base class constructor
        {
            // Validate and set properties
            this.id = id;
            Position = position;
            Salary = salary;
            Department = department;
        }
    }

    
}
