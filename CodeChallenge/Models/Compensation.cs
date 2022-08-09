using System;

namespace CodeChallenge.Models
{
    public class Compensation
    {
        public String Id { get; set; }
        public UInt64 Salary { get; set; }
        public DateTime EffectiveDate { get; set; }
    }
}
