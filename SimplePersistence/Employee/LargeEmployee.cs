using System;
using System.Collections.Generic;
using System.Text;

namespace SimplePersistence.Employee
{
    public class LargeEmployee : Employee
    {
        public byte[] someData { get; set; }

    }
}
