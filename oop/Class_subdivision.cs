using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;


namespace oop
{
    public abstract class Class_subdivision
    {
        public string name;
        public string about;
        public Class_people Leader;
        public Class_subdivision()
        { }
        public Class_subdivision(string name, string about)
        {
            this.name = name;
            this.about = about;
        }
        internal void AddLeaderSubdivision(Class_people Leader)
        {
            this.Leader = Leader;
        }

        public Class_people ReturnLeader()
        {
            if (Leader is null)
                return null;
            else
                return Leader;
        }
        public string ReturnInformation()
        {
            string Inf = $"Название: {name} \n";
            Inf += about == string.Empty ? string.Empty : $"Информация: {about}";
            return Inf;
        }
    }
   
   

}

