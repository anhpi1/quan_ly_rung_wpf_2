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
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace quan_ly_rung_wpf_2
{
    /// <summary>
    /// Interaction logic for ctrl_dang_nhap.xaml
    /// </summary>
    
    public partial class ctrl_dang_nhap : UserControl
    {
        public ContentControl UserControlContainer { get; set; }
        public Window newscreen { get; set; }
        public ctrl_dang_nhap()
        {
            InitializeComponent();
        }
        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            // Tạo DropShadowEffect màu đen
            var shadowEffect_black = new DropShadowEffect
            {
                Color = Colors.Black,
                Direction = -45,
                ShadowDepth = 6,
                BlurRadius = 10,
                Opacity = 0.2 // Bắt đầu với opacity nhỏ
            };

            button1.Effect = shadowEffect_black;

            // Tạo animation tăng dần opacity
            var opacityAnimation_black = new DoubleAnimation
            {
                From = 0.2,
                To = 0, // Độ trong suốt tối đa
                Duration = TimeSpan.FromSeconds(0.3),
                EasingFunction = new QuadraticEase()
            };
            shadowEffect_black.BeginAnimation(DropShadowEffect.OpacityProperty, opacityAnimation_black);

            // Tạo DropShadowEffect màu trắng
            var shadowEffect_white = new DropShadowEffect
            {
                Color = Colors.White,
                Direction = 135,
                ShadowDepth = 6,
                BlurRadius = 10,
                Opacity = 0.8 // Bắt đầu với opacity nhỏ
            };

            button1_trang.Effect = shadowEffect_white;

            // Tạo animation tăng dần opacity
            var opacityAnimation_white = new DoubleAnimation
            {
                From = 0.8,
                To = 0, // Độ trong suốt tối đa
                Duration = TimeSpan.FromSeconds(0.3),
                EasingFunction = new QuadraticEase()
            };
            shadowEffect_white.BeginAnimation(DropShadowEffect.OpacityProperty, opacityAnimation_white);

            var thicknessAnimation = new ThicknessAnimation
            {
                To = new Thickness(1),
                Duration = TimeSpan.FromSeconds(0.3), // Thời gian chuyển đổi
                EasingFunction = new QuadraticEase() // Hiệu ứng chuyển động mượt
            };
            button1.BeginAnimation(BorderThicknessProperty, thicknessAnimation);

            // Tạo hiệu ứng thay đổi BorderBrush
            var colorAnimation = new ColorAnimation
            {
                To = Color.FromArgb(0xFF, 0xB4, 0xB4, 0xB4),
                Duration = TimeSpan.FromSeconds(0.3)
            };
            var brush = new SolidColorBrush();
            brush.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);
            button1.BorderBrush = brush;
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            if (button1.Effect is DropShadowEffect shadowEffect_black)
            {
                // Tạo animation giảm dần opacity
                var opacityAnimation_black = new DoubleAnimation
                {
                    From = shadowEffect_black.Opacity,
                    To = 0.2, // Mất bóng dần
                    Duration = TimeSpan.FromSeconds(0.3),
                    EasingFunction = new QuadraticEase()
                };

                // Bắt đầu animation
                shadowEffect_black.BeginAnimation(DropShadowEffect.OpacityProperty, opacityAnimation_black);

                // Xóa hiệu ứng sau khi animation kết thúc
                opacityAnimation_black.Completed += (s, a) => button1.Effect = null;
            }
            if (button1_trang.Effect is DropShadowEffect shadowEffect_white)
            {
                // Tạo animation giảm dần opacity
                var opacityAnimation_white = new DoubleAnimation
                {
                    From = shadowEffect_white.Opacity,
                    To = 0.8, // Mất bóng dần
                    Duration = TimeSpan.FromSeconds(0.3),
                    EasingFunction = new QuadraticEase()
                };

                // Bắt đầu animation
                shadowEffect_white.BeginAnimation(DropShadowEffect.OpacityProperty, opacityAnimation_white);

                // Xóa hiệu ứng sau khi animation kết thúc
                opacityAnimation_white.Completed += (s, a) => button1_trang.Effect = null;

            }
            // Tạo hiệu ứng chuyển đổi BorderThickness về 0
            var thicknessAnimation = new ThicknessAnimation
            {
                To = new Thickness(0),
                Duration = TimeSpan.FromSeconds(0.3),
                EasingFunction = new QuadraticEase()
            };
            button1.BeginAnimation(BorderThicknessProperty, thicknessAnimation);

            // Loại bỏ BorderBrush bằng hiệu ứng
            var colorAnimation = new ColorAnimation
            {
                To = Colors.Transparent,
                Duration = TimeSpan.FromSeconds(0.3)
            };
            var brush = new SolidColorBrush(Color.FromArgb(0xFF, 0xB4, 0xB4, 0xB4)); // Giá trị khởi tạo
            brush.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);
            button1.BorderBrush = brush;
        }

        private void LoginButton_Click(object sender, MouseButtonEventArgs e)
        {
            // Lấy dữ liệu từ TextBox và PasswordBox
            string username = used_name.Text;
            string password = pass.Password;

            // Chuỗi kết nối đến cơ sở dữ liệu MySQL
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Truy vấn kiểm tra tài khoản
                    string query = "SELECT COUNT(*) FROM auth_user WHERE username = @username AND password = @password";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Thêm tham số để tránh SQL Injection
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@password", password);

                        // Thực thi truy vấn
                        int result = Convert.ToInt32(command.ExecuteScalar());

                        if (result > 0)
                        {
                            man_hinh_menu screen = new man_hinh_menu();
                            screen.Name = username;
                            screen.Show();
                            newscreen.Close();
                            // Hiển thị cửa sổ khác hoặc thực hiện hành động khác
                            // Ví dụ: mở cửa sổ chính
                        }
                        else
                        {
                            MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi kết nối cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Chua_co_tai_khoan(object sender, MouseButtonEventArgs e)
        {
            var myControl = new ctrl_dang_ki_1();

            myControl.UserControlContainer = UserControlContainer;
            myControl.newscreen = newscreen;
            UserControlContainer.Content = myControl;



        }

        private void Chua_co_tai_khoan_enter(object sender, MouseEventArgs e)
        {
            if (chua_co_tai_khoan != null)
            {
                // Thay đổi màu nền khi chuột di vào
                chua_co_tai_khoan.Foreground = new SolidColorBrush(Colors.Blue);
            }
        }

        private void Chua_co_tai_khoan_leave(object sender, MouseEventArgs e)
        {
            if (chua_co_tai_khoan != null)
            {
                // Đặt lại màu nền khi chuột rời khỏi
                chua_co_tai_khoan.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void Quen_mat_khau(object sender, MouseButtonEventArgs e)
        {
            var myControl = new ctrl_quen_mat_khau();

            myControl.UserControlContainer = UserControlContainer;
            myControl.newscreen = newscreen;
            UserControlContainer.Content = myControl;

        }

        private void Quen_mat_khau_enter(object sender, MouseEventArgs e)
        {
            if (quen_mat_khau != null)
            {
                // Thay đổi màu nền khi chuột di vào
                quen_mat_khau.Foreground = new SolidColorBrush(Colors.Blue);
            }
        }

        private void Quen_mat_khau_leave(object sender, MouseEventArgs e)
        {
            if (quen_mat_khau != null)
            {
                // Đặt lại màu nền khi chuột rời khỏi
                quen_mat_khau.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

    }
}
