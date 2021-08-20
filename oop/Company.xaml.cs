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
    /// Логика взаимодействия для Company.xaml
    /// </summary>
    public partial class Company : Window
    {
        MainWindow mw;
        nodes node;
        public Company(MainWindow mw, nodes Node)
        {
            InitializeComponent();
            this.mw = mw;
            this.node = Node;
            if(node != null)
            {
                AddInformationIntoWindow((Class_organization)node.ReturnNotNullItem());
            }
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            
                if (tb_name_organization.Text != "" || tb_about.Text != "")
                {
                        if (node == null)
                        {
                            Class_organization organization = new Class_organization(
                                tb_name_organization.Text,
                                tb_about.Text,
                                tb_telnumber.Text == "" ? "" : tb_telnumber.Text,
                                tb_mail.Text == "" ? "" : tb_mail.Text,
                                tb_site.Text == "" ? "" : tb_site.Text,
                                tb_adress.Text == "" ? "" : tb_adress.Text
                                );

                            mw.AddNodesIntoTree(organization, "organization", "organization");
                        }
                        else
                        {
                            SaveChanges(((Class_organization)((nodes)(mw.tv_company.SelectedItem)).ReturnNotNullItem()));
                        }
                    this.Close();
                }
                else
                {
                    messagebox msb = new messagebox("Внимание", "Не все поля заполнены");
                    msb.ShowDialog();
                }
            
        }
        private void AddInformationIntoWindow(Class_organization organization)
        {
            tb_name_organization.Text = organization.name;
            tb_about.Text = organization.about;
            tb_telnumber.Text = organization.telnumber;
            tb_mail.Text = organization.mail;
            tb_site.Text = organization.site;
            tb_adress.Text = organization.adress;
        }
        private void SaveChanges(Class_organization organization)
        {
            organization.name = tb_name_organization.Text;
            organization.about = tb_about.Text;
            organization.telnumber = (tb_telnumber.Text == "" ? "" : tb_telnumber.Text);
            organization.mail = (tb_mail.Text == "" ? "" : tb_mail.Text);
            organization.site = (tb_site.Text == "" ? "" : tb_site.Text);
            organization.adress = (tb_adress.Text == "" ? "" : tb_adress.Text);
            mw.Namechanged(tb_name_organization.Text);
        }
    }
}
