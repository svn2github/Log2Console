using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace SerilogTest
{
    class Person
    {
        Serilog.ILogger Log = Serilog.Log.ForContext<Person>();

        public Person(string name, string ssn, string prop1)
        {
            Name = name;
            Ssn = ssn;
            property1 = prop1;
            Log.Verbose("ctor {@Person}", this);
        }

        public string Name { get; set; }
        private string Ssn { get; set; }
        private string property1;
    }
}
