using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms.Integration;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Xml;

namespace oop
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public ObservableCollection<nodes> NodesCollection = new ObservableCollection<nodes>();
        List<string> contentForCreatedOrganozation = new List<string>() { "Организация" };
        List<string> contentAfterCreatedOrganization = new List<string>() { "Директор", "Ведомство" };
        List<string> contentForinstitution = new List<string>() { "Руководитель", "Департамент" };
        List<string> contentForCreatedDepartment = new List<string>() { "Руководитель","Рабочий", "Интерн" };
        List<string> contentLeader = new List<string>() {"organization", "institution", "department" };
        Dictionary<string, string> Dic = new Dictionary<string, string>() {
            { "organization", "Организация" }, { "institution", "Ведомство" }, {"department","Департамент" }, {"director","Директор" },{"manager","Руководитель"},
            {"Intern", "Интерн"}, {"worker","Рабочий" } };
        public MainWindow()
        {
            InitializeComponent();
            ContentChanged();
            tv_company.ItemsSource = NodesCollection;
            ChamgeFileNodes();
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            if (tv_company.SelectedItem != null)
            {
                if (contentLeader.Contains(((nodes)tv_company.SelectedItem).ReturnTypeNode()) &&
                    (cb_content.Text != "Директор" && cb_content.Text != "Руководитель") && !CorrectAddNewNode(false))
                {
                    messagebox msb = new messagebox("Внимание", "Необходимо добавить руководителя");
                    msb.ShowDialog();
                }
                else if (((nodes)tv_company.SelectedItem).ReturnOwnerNodes()!=null)
                {
                    if(!CorrectAddNewNode(true))
                    {
                        messagebox msb = new messagebox("Внимание", "Необходимо добавить руководителя в вышестоящем элементе");
                        msb.ShowDialog();
                    }
                    else
                    OpenNewWindow(null);
                }
                else
                    OpenNewWindow(null); 
            }
            else OpenNewWindow(null);
        }

    private void OpenNewWindow(nodes Node)
        {
            
                switch (Node == null ? cb_content.Text:Dic[Node.ReturnTypeNode()])
                {
                    case "Организация":
                        Company org_window = new Company(this,Node);
                        org_window.ShowDialog();
                        break;
                    case "Ведомство":
                        departments InstitutionWindow = new departments(this, "institution", Node);
                        InstitutionWindow.ShowDialog();
                        break;
                    case "Департамент":
                        departments DepartmentsWindow = new departments(this, "department", Node);
                        DepartmentsWindow.ShowDialog();
                        break;
                    case "Директор":
                        peoples DirectorWindow = new peoples(this, "director", Node);
                        DirectorWindow.ShowDialog();
                        break;
                    case "Руководитель":
                        peoples ManagerWindow = new peoples(this, "manager", Node);
                        ManagerWindow.ShowDialog();
                        break;
                    case "Интерн":
                        peoples InternWindow = new peoples(this, "Intern", Node);
                        InternWindow.ShowDialog();
                        break;
                    case "Рабочий":
                        peoples WorkerWindow = new peoples(this, "worker", Node);
                        WorkerWindow.ShowDialog();
                        break;
                }
        }
        private bool CorrectAddNewNode(bool OwnerStatus)
        {
            object node;
            if (OwnerStatus)
                node = ((nodes)tv_company.SelectedItem).ReturnOwnerNodes();
            else
                node = (nodes)tv_company.SelectedItem;
            bool statusLeader = false;
            if(node != null)
            foreach (var el in ((nodes)node).ReturnCollectionNodesElement())
            {
                if(el.ReturnTypeNode() == "manager"|| el.ReturnTypeNode() == "director")
                {
                    statusLeader = true;
                    break;
                }
            }
            return statusLeader;
        }
        
        public void AddNodesIntoTree(object NewNode,string TypeClass, string NameClass)
        {
            switch(TypeClass)
            {
                case "organization":
                    nodes NodeOfOrganization = new nodes(((Class_organization)NewNode).name, (Class_organization)NewNode);
                    NodesCollection.Add(NodeOfOrganization);
                    break;
                case "subdivision":
                    nodes NodeOfSubdivision = new nodes(((Class_subdivision)NewNode).name, (Class_subdivision)NewNode, NameClass, 
                        (nodes)tv_company.SelectedItem);
                    ((nodes)tv_company.SelectedItem).addElementIntoNodes(NodeOfSubdivision);
                    break;
                case "people":
                    nodes NodeOFPeople = new nodes(((Class_people)NewNode).ReturnFIO(), (Class_people)NewNode, NameClass, 
                        (nodes)tv_company.SelectedItem);
                    ((nodes)tv_company.SelectedItem).addElementIntoNodes(NodeOFPeople);
                    if(NameClass == "manager")
                    {
                        ((Class_subdivision)((nodes)tv_company.SelectedItem).ReturnNotNullItem()).AddLeaderSubdivision((Class_people)NewNode);
                    }
                    ChangeMoney(NameClass);
                    break;
            }
        }
        private void ContentChanged()
        {
                cb_content.ItemsSource = contentForCreatedOrganozation;
        }
        private void tv_company_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (((nodes)tv_company.SelectedItem) != null)
            {
                tb_InformationNode.Clear();
                tb_InformationNode.Text = ((nodes)tv_company.SelectedItem).InformationNode();
                switch (((nodes)tv_company.SelectedItem).ReturnTypeNode())
                {
                    case "institution":
                        cb_content.IsEnabled = true;
                        cb_content.ItemsSource = contentForinstitution;
                        break;
                    case "department":
                        cb_content.IsEnabled = true;
                        cb_content.ItemsSource = contentForCreatedDepartment;
                        break;
                    case "organization":
                        cb_content.IsEnabled = true;
                        cb_content.ItemsSource = contentAfterCreatedOrganization;
                        break;
                    default:
                        cb_content.IsEnabled = false;
                        break;
                }
            }
            else
            {
                cb_content.IsEnabled = true;
                cb_content.ItemsSource = contentForCreatedOrganozation;
            }
            }
        

        private void ChangeMoney(string NameClass)
        {
            switch (((nodes)tv_company.SelectedItem).ReturnTypeNode())
            {
                case "department":
                    CorrectMoney(((nodes)tv_company.SelectedItem), ((nodes)tv_company.SelectedItem).ReturnMoneyInPeoplesNodes());
                    CorrectMoneyInstitution(((nodes)tv_company.SelectedItem).ReturnOwnerNodes());
                    break;
                case "institution":
                    CorrectMoneyInstitution((nodes)tv_company.SelectedItem);
                    break;
            }
        }
        private void CorrectMoney(nodes Node, double money)
        {
            ((Manager)((Class_subdivision)Node.ReturnNotNullItem()).ReturnLeader()).AddMoneyLeader(money);
        }
        private void CorrectMoneyInstitution(nodes Node)
        {
            double money = 0;
            foreach (var elem in Node.ReturnCollectionNodesElement())
            {
                if (elem.ReturnTypeNode() == "department")
                {
                    money += ((Manager)((Class_subdivision)elem.ReturnNotNullItem()).ReturnLeader()).ReturnMoney();
                    money += elem.ReturnMoneyInPeoplesNodes();
                }
            }
            CorrectMoney(Node,money);
        }

        private void MIOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter= "(*.xml)|*.xml";
            ofd.FileOk += Ofd_FileOk;
            ofd.ShowDialog();

        }

        private void Ofd_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            using(FileStream fs = new FileStream(sender is OpenFileDialog ?((OpenFileDialog)sender).FileName: System.Windows.Forms.Application.StartupPath + "\\Nodes.xml", FileMode.Open))
            {
                XmlDictionaryReader reader =
                XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());
                var dcss = new DataContractSerializerSettings
                {
                    PreserveObjectReferences = true,
                    KnownTypes = new[] { typeof(Class_department), typeof(Class_institution), typeof(Class_organization), typeof(Intern), typeof(Manager), typeof(Worker), typeof(Director) }
                };
                var dcs = new DataContractSerializer(typeof(ObservableCollection<nodes>), dcss);
                NodesCollection.Clear();
                foreach (var el in (ObservableCollection<nodes>)dcs.ReadObject(reader, true))
                {
                    NodesCollection.Add(el);
                }
                reader.Close();
            }
        }

        private void MISaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileOk += Sfd_FileOk;
            sfd.Filter = "(*.xml)|*.xml";
            sfd.ShowDialog();
        }

        private void Sfd_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var dcss = new DataContractSerializerSettings { PreserveObjectReferences = true, 
                KnownTypes = new[] { typeof(Class_department), typeof(Class_institution), typeof(Class_organization), typeof(Intern), typeof(Manager), typeof(Worker), typeof(Director) } };
            var dcs = new DataContractSerializer(typeof(ObservableCollection<nodes>), dcss);

            using (Stream fStream = new FileStream(sender is SaveFileDialog ? ((SaveFileDialog)sender).FileName: System.Windows.Forms.Application.StartupPath + "\\Nodes.xml",
                FileMode.Create, FileAccess.Write, FileShare.None))
            {
                dcs.WriteObject(fStream, NodesCollection);
            }   
        }

        private void MIClear_Click(object sender, RoutedEventArgs e)
        {
            NodesCollection.Clear();
            tb_InformationNode.Clear();
        }
        private void MIExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MenuItem_Click_Delete(object sender, RoutedEventArgs e)
        {
            if(tv_company.SelectedItem!=null)
            {
                tb_InformationNode.Clear();
                if (((nodes)tv_company.SelectedItem).ReturnTypeNode() == "organization")
                    NodesCollection.Clear();
                else
                ((ObservableCollection<nodes>)((nodes)((nodes)tv_company.SelectedItem).ReturnOwnerNodes()).ReturnCollectionNodesElement()).Remove(((nodes)tv_company.SelectedItem));
            }
            else
            {
                messagebox messagebox = new messagebox("Внимание","Не выбрано не одного элемента");
                messagebox.Show();
            }    
        }
        private void MenuItem_Click_Correct(object sender, RoutedEventArgs e)
        {
            if (tv_company.SelectedItem != null)
            {
                OpenNewWindow(((nodes)tv_company.SelectedItem));
            }
            else
            {
                messagebox messagebox = new messagebox("Внимание", "Не выбрано не одного элемента");
                messagebox.Show();
            }
        }
        public void Namechanged(string NameNode)
        {
            ((nodes)tv_company.SelectedItem).nameNode = NameNode;
            ((nodes)tv_company.SelectedItem).NotifyPropertyChanged("nameNode");
        }

        private void MISave_Click(object sender, RoutedEventArgs e)
        {
            Sfd_FileOk(this, null);
        }
        private void ChamgeFileNodes()
        {
            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Nodes.xml"))
                Ofd_FileOk(this,null);
        }
    }

}
