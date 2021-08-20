using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace oop
{
    public class nodes: INotifyPropertyChanged
    {
        public string nameNode { get; set; }
        public Class_subdivision subdivision;
        public Class_people people;
        public Class_organization organization;
        public nodes OwnerNode;

        public event PropertyChangedEventHandler PropertyChanged;

        public string TypeNode { get; set; }
        public ObservableCollection<nodes> node { get; set; }
        public nodes()
        {}
        public nodes(string name, Class_subdivision subdivision, string TypeNode,nodes OwnerNode)
        {
            this.OwnerNode = OwnerNode;
            this.TypeNode = TypeNode;
            this.nameNode = name;
            this.subdivision = subdivision;
            this.node = new ObservableCollection<nodes>();
        }
        public nodes(string name, Class_people people, string TypeNode, nodes OwnerNode)
        {
            this.OwnerNode = OwnerNode;
            this.TypeNode = TypeNode;
            this.nameNode = name;
            this.people = people;
            this.node = new ObservableCollection<nodes>();
        }
        public nodes(string name, Class_organization organization)
        {
            this.TypeNode = "organization";
            this.nameNode = name;
            this.organization = organization;
            this.node = new ObservableCollection<nodes>();
        }
        public void addElementIntoNodes(nodes node)
        {
            this.node.Add(node);
        }
        public void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public object ReturnNotNullItem()
        {
             if (subdivision != null)
                return subdivision;
            else if (organization != null)
                return organization;
            else if (people != null)
                return people;
            else
                return null;
        }
        public string InformationNode()
        {
            if (subdivision != null)
                return subdivision.ReturnInformation();
            else if (organization != null)
                return organization.ReturnInformation();
            else if (people != null)
                return people.ReturnInformation();
            else
                return string.Empty;
        }
        public ObservableCollection<nodes> ReturnCollectionNodesElement()
        {
            return node;
        }
        public string ReturnTypeNode()
        {
            return TypeNode;
        }
        public nodes ReturnOwnerNodes()
        {
            return OwnerNode;
        }
        public double ReturnMoneyInPeoplesNodes()
        {
            double money = 0;
            foreach(var elem in ReturnCollectionNodesElement())
            {
                if(elem.ReturnTypeNode()!="manager")
                    money += ((Class_people)elem.ReturnNotNullItem()).ReturnMoney();
            }
            return money;
        }
        

    }
}
