using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop
{
    public class Class_organization
    {
        public string name;
        public string about;
        public string telnumber;
        public string mail;
        public string site;
        public string adress;
        Class_organization()
        { }
        public Class_organization(string name,string about,string telnumber,string mail, string site, string adress)
        {
            this.name = name;
            this.about = about;
            this.telnumber = telnumber;
            this.mail = mail;
            this.site = site;
            this.adress = adress;
        }

        public string ReturnInformation()
        {
            string Inf = "Имя: " + name + "\n";
            Inf += about != string.Empty ? "О компании: " + about + "\n": string.Empty;
            Inf += !telnumber.Contains("_") ? "Телефонный номер: " + telnumber + "\n" : string.Empty;
            Inf += mail != string.Empty ? "Электронная почта: " + mail + "\n" : string.Empty;
            Inf += site != string.Empty ? "Сайт: " + site + "\n" : string.Empty;
            Inf += adress != string.Empty ? "Адрес: " + adress + "\n" : string.Empty;
            return Inf;
        }


       
    }
}
