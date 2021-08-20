using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace oop
{
    /// <summary>
    /// Логика взаимодействия для peoples.xaml
    /// </summary>
    public partial class peoples : Window
    {
        MainWindow mw;
        string TypePeople;
        nodes Node;
        public peoples(MainWindow mw,string TypePeople, nodes Node)
        {
            InitializeComponent();
            DIsableMoney(TypePeople);
            this.mw = mw;
            this.TypePeople = TypePeople;
            TittleWindow(TypePeople);
            this.Node = Node;
            if(Node != null)
            {
                AddInformationIntoWindow((Class_people)Node.ReturnNotNullItem());
            }
        }
        private void DIsableMoney(string TypePeople)
        {
            switch(TypePeople)
            {
                case "worker":
                    lb_money.Content = "Часовой оклад";
                    break;
                case "manager":
                    lb_position.Visibility = Visibility.Collapsed;
                    lb_money.Visibility = Visibility.Collapsed;
                    tb_position.Visibility = Visibility.Collapsed;
                    tb_money.Visibility = Visibility.Collapsed;
                    break;
                case "director":
                    lb_position.Visibility = Visibility.Collapsed;
                    lb_money.Visibility = Visibility.Collapsed;
                    tb_position.Visibility = Visibility.Collapsed;
                    tb_money.Visibility = Visibility.Collapsed;
                    break;
            }
        }
        private void TittleWindow(string TypePeople)
        {
            switch (TypePeople)
            {
                case "Intern":
                    this.Title = "Добавление интерна";
                    break;
                case "manager":
                    this.Title = "Добавление менеджера";
                    break;
                case "worker":
                    this.Title = "Добавление рабочего";
                    break;
                case "director":
                    this.Title = "Добавление директора";
                    break;

            }
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            if (CheckCorrectInformationInElemet())
            {
                if (Node == null)
                    switch (TypePeople)
                    {
                        case "Intern":
                            mw.AddNodesIntoTree(new Intern(tb_position.Text, tb_familyname.Text, tb_name.Text,
                                tb_patronymic.Text, dp_datebirth.SelectedDate.Value, tb_contactPhone.Text
                                , Convert.ToInt32(tb_money.Text)), "people", "Intern");
                            break;
                        case "manager":
                            if (CheckStatusLeader())
                            {
                                mw.AddNodesIntoTree(new Manager("Руководитель подразделения", tb_familyname.Text, tb_name.Text,
                                tb_patronymic.Text, dp_datebirth.SelectedDate.Value, tb_contactPhone.Text
                                , 1300), "people", "manager");
                            }
                            else
                            {
                                messagebox msb = new messagebox("Внимание!", "Не возможно добавить еще одного менеджера");
                            }
                            break;
                        case "worker":
                            mw.AddNodesIntoTree(new Worker(tb_position.Text, tb_familyname.Text, tb_name.Text,
                                tb_patronymic.Text, dp_datebirth.SelectedDate.Value, tb_contactPhone.Text
                                , Convert.ToInt32(tb_money.Text)), "people", "worker");
                            break;
                        case "director":
                            if (CheckStatusLeader())
                            {
                                mw.AddNodesIntoTree(new Director(tb_familyname.Text, tb_name.Text,
                                tb_patronymic.Text, dp_datebirth.SelectedDate.Value, tb_contactPhone.Text),
                                "people", "director");
                            }
                            else
                            {
                                messagebox msb = new messagebox("Внимание!", "Не возможно добавить еще одного директора");
                                msb.ShowDialog();
                                this.Close();
                            }
                            break;
                    }
                else
                    SaveChanges((Class_people)mw.tv_company.SelectedItem);
                this.Close();
            }
            else
            {
                messagebox msb = new messagebox("Внимание", "Не все поля заполнены!");
                msb.ShowDialog();
            }
        }
        private bool CheckStatusLeader()
        {
            var _tmpList = from nodeEl in ((nodes)mw.tv_company.SelectedItem).ReturnCollectionNodesElement()
                       where nodeEl.ReturnTypeNode() == "director" || nodeEl.ReturnTypeNode() == "manager"
                       select nodeEl.ReturnTypeNode();
            if (_tmpList.Count() > 0)
                return false;
            else
                return true;
        }
        private bool CheckCorrectInformationInElemet()
        {
            switch (TypePeople)
            {
                case "Intern":
                    if (tb_position.Text == "")
                        return false;
                    break;
                case "worker":
                    if (tb_position.Text == "")
                        return false;
                    break;
            }
            if (tb_familyname.Text == "")
                return false;
            else if (tb_name.Text == "")
                return false;
            else if (tb_patronymic.Text == "")
                return false;
            else if (dp_datebirth.Text == "")
                return false;
            else
                return true;
        }
        private void AddInformationIntoWindow(Class_people node)
        {
            tb_position.Text = node.position;
            tb_familyname.Text = node.familyname;
            tb_name.Text = node.name;
            tb_patronymic.Text = node.patronymic;
            dp_datebirth.SelectedDate = node.datebirth;
            tb_contactPhone.Text = node.contactPhone;
            tb_money.Text = node.money.ToString();
        }
        private void SaveChanges(Class_people node)
        {
            node.position = tb_position.Text;
            node.familyname = tb_familyname.Text;
            node.name = tb_name.Text;
            node.patronymic = tb_patronymic.Text;
            node.datebirth = dp_datebirth.SelectedDate.Value;
            node.contactPhone = tb_contactPhone.Text;
            node.money = Convert.ToInt32(tb_money.Text);
            mw.Namechanged(tb_position.Text);
        }
    }
}
