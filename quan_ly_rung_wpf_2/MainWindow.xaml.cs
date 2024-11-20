using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace quan_ly_rung_wpf_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Button_OpenManhinh_Click(object sender, RoutedEventArgs e)
        {
            // Tạo đối tượng Manhinh và mở cửa sổ mới
            man_hinh_dang_nhap manhinhWindow = new man_hinh_dang_nhap();
            manhinhWindow.Show();  // Hiển thị cửa sổ Manhinh
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            test test1 = new test();    
            test1.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            man_hinh_menu main = new man_hinh_menu();
            main.Show();
        }
    }
}