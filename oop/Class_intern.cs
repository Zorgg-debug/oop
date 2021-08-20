using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop
{
    public class Intern : Class_people
    {
        public Intern()
        { }
        public Intern(string position, string familyname, string name, string patronymic, DateTime datebirth, string contactPhone, int money) :
            base(position, familyname, name, patronymic, datebirth,
             contactPhone, money)
        {
            typeOfPeople = "Intern";
        }
    }
}
