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
    /// Логика взаимодействия для messagebox.xaml
    /// </summary>
    public partial class messagebox : Window
    {
        public messagebox(string tittle, string content)
        {
            InitializeComponent();
            this.Title = tittle;
            this.tb_content.Text = content;
        }

        private void btn_ok_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
