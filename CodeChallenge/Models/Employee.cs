using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallenge.Models
{
    public class Employee
    {
        public String EmployeeId { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Position { get; set; }
        public String Department { get; set; }
        public List<Employee> DirectReports { get; set; }

        public void Dump(String prefix)
        {
            Console.WriteLine("{0}: {1} {2}", prefix, FirstName, LastName);
            if (DirectReports == null)
            {
                Console.WriteLine("{0}: no directs", prefix);
            }
            else
            {
                Console.WriteLine("{0}: {1} directs:", prefix, DirectReports.Count);
                foreach(Employee dr in DirectReports)
                {
                    Console.WriteLine("{0}->>   {1}", prefix, dr);
                }
            }
            Console.WriteLine("-----");
        }
    }
}
