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
    /// Логика взаимодействия для departments.xaml
    /// </summary>
    public partial class departments : Window
    {
        MainWindow mw;
        nodes Node;
        string typeSubdivision;
        public departments(MainWindow mw, string typeSubdivision, nodes Node)
        {
            InitializeComponent();
            this.mw = mw;
            this.typeSubdivision = typeSubdivision;
            this.Node = Node;
            if(Node != null)
            {
                AddInformationIntoWindow((Class_institution)(mw.tv_company.SelectedItem));
            }
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            if (tb_nameSubdivision.Text != "")
            {
                if (typeSubdivision == "institution")
                {
                    mw.AddNodesIntoTree(new Class_institution(tb_nameSubdivision.Text, tb_about.Text),
                        "subdivision", "institution");
                }
                else if (Node != null)
                {
                    SaveChanges((Class_institution)(((nodes)mw.tv_company.SelectedItem).ReturnNotNullItem()));
                }
                else
                {
                    mw.AddNodesIntoTree(new Class_department(tb_nameSubdivision.Text, tb_about.Text), "subdivision", "department");
                }
                this.Close();
            }
            else
            {
                messagebox msb = new messagebox("Внимание","Не все поля заполнены!");
                msb.ShowDialog();
            }
        }

        private void AddInformationIntoWindow(Class_institution node)
        {
            tb_nameSubdivision.Text = node.name;
            tb_about.Text = node.about; 
        }
        private void SaveChanges(Class_institution institution)
        {
            institution.name = tb_nameSubdivision.Text;
            institution.about = tb_about.Text;
            mw.Namechanged(tb_nameSubdivision.Text);
        }


    }
}
