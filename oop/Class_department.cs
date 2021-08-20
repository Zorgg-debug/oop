using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop
{
    public class Class_department : Class_subdivision
    {
        Class_department() : base()
        { }
        public Class_department(string name, string about) : base(name, about)
        {
        }
    }
}
