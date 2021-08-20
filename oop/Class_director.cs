using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop
{
    public class Director : Class_people
    {
        public Director()
        { }
        public Director(string familyname, string name, string patronymic, DateTime datebirth, string contactPhone) :
           base(familyname, name, patronymic, datebirth, contactPhone)
        {
            typeOfPeople = "director";
        }
    }
}
