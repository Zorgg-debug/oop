using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace oop
{
    public abstract class Class_people
    {
        public string position;
        public string familyname;
        public string name;
        public string patronymic;
        public DateTime datebirth;
        public string contactPhone;
        public double money;
        public string typeOfPeople;

        public Class_people()
        { }
        public Class_people(string position, string familyname, string name, string patronymic, DateTime datebirth,
            string contactPhone, int money)
        {

            this.position = position;
            this.familyname = familyname;
            this.name = name;
            this.patronymic = patronymic;
            this.datebirth = datebirth;
            this.contactPhone = contactPhone;
            this.money = money;
        }

        public Class_people(string familyname, string name, string patronymic, DateTime datebirth, string contactPhone)
        {
            this.familyname = familyname;
            this.name = name;
            this.patronymic = patronymic;
            this.datebirth = datebirth;
            this.contactPhone = contactPhone;

        }
        public string ReturnFIO()
        {
            return familyname + " " + name + " " + patronymic;
        }
        public double ReturnMoney()
        {
            if (typeOfPeople == "worker")
                return money * 240;
            else
                return money;
        }
        public string ReturnInformation()
        {
            string Inf = position == null ? string.Empty : $"Должность: {position}\n";
            Inf += $"Фамилия: {familyname} \n";
            Inf += $"Имя: {name} \n";
            Inf += $"Отчество: {patronymic} \n";
            Inf += $"Дата рождения: {datebirth.ToShortDateString()}\n";
            Inf += $"Контактный телефон: {contactPhone}\n";
            Inf += money == 0  ? string.Empty : $"Заработная плата: {money}";
            return Inf;
        }

    }
   
    
    
   
}
    
