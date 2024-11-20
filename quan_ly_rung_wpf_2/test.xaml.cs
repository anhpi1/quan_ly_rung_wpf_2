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

namespace quan_ly_rung_wpf_2
{
    /// <summary>
    /// Interaction logic for test.xaml
    /// </summary>
    public partial class test : Window
    {
        
        public test()
        {
            InitializeComponent();
        }

        

        private void btn1_Click(object sender, MouseButtonEventArgs e)
        {
            var myctrl = new ctrl_test();
            myctrl.myctrl = content1;
            content1.Content = myctrl;
        }
    }
}
