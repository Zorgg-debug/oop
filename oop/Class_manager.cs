using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop
{
    public class Manager : Class_people
    {
        public Manager()
        { }
        public Manager(string position, string familyname, string name, string patronymic, DateTime datebirth, string contactPhone, int money) :
           base(position, familyname, name, patronymic, datebirth,
             contactPhone, money)
        {
            typeOfPeople = "manager";
        }
        public void AddMoneyLeader(double money)
        {
            if (money * 0.15 > 1300)
                this.money = money * 0.15;
            else
                this.money = 1300;
        }

    }
}
