using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallenge.Models
{
    public class ReportingStructure
    {
        // TODO get rid of setters and put into ctor
        public Employee Employee { get; set; }
        public int NumberOfReports { get; set; }
    }
}
