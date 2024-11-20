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
    /// Interaction logic for ctrl_dang_ki_2.xaml
    /// </summary>
    
    public partial class ctrl_dang_ki_2 : UserControl
    {
        public ContentControl UserControlContainer { get; set; }
        public Window newscreen { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string phone { get; set; }
        public ctrl_dang_ki_2()
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
            // Lấy thông tin từ các trường nhập liệu
            string last_name_o = last_name.Text.Trim();
            string fist_name_o = fist_name.Text.Trim();
            string address_o = address.Text.Trim();
            string email_o = email.Text.Trim();

            // Kiểm tra điều kiện
            int c = 0;

            // 1. Kiểm tra tên người dùng hoặc số điện thoại đã tồn tại
            if (last_name_o != "")
            {
                {
                    log_last_name.Text = "Tên:";
                    log_last_name.Foreground = new SolidColorBrush(Colors.Black);
                }
            }
            else
            {
                log_last_name.Text = "Tên trống.";
                log_last_name.Foreground = new SolidColorBrush(Colors.Red); ;
            }
            if (fist_name_o != "")
            {

                {
                    log_fist_name.Text = "Họ:";
                    log_fist_name.Foreground = new SolidColorBrush(Colors.Black);
                }
            }
            else
            {
                log_fist_name.Text = "Họ trống.";
                log_fist_name.Foreground = new SolidColorBrush(Colors.Red); ;
            }
            if (address_o != "")
            {

                {
                    log_address.Text = "Địa chỉ:";
                    log_address.Foreground = new SolidColorBrush(Colors.Black);
                }
            }
            else
            {
                log_address.Text = "Địa chỉ trống.";
                log_address.Foreground = new SolidColorBrush(Colors.Red); ;
            }
            if (email_o != "")
            {

                {
                    log_email.Text = "Email:";
                    log_email.Foreground = new SolidColorBrush(Colors.Black);
                }
            }
            else
            {
                log_email.Text = "Email trống.";
                log_email.Foreground = new SolidColorBrush(Colors.Red); ;
            }


            if (last_name_o == "" || fist_name_o == "" || address_o == "" || email_o == "") return;
            if (c > 0) return;

            // Nếu không có lỗi, ghi vào MySQL
            if (SaveUserToDatabase(last_name_o, fist_name_o, address_o, email_o, username, phone, password))
            {

                var myControl = new ctrl_dang_ki_thanh_cong();

                myControl.UserControlContainer = UserControlContainer;
                myControl.newscreen = newscreen;
                UserControlContainer.Content = myControl;


            }

        }



        // Hàm lưu người dùng vào cơ sở dữ liệu
        private bool SaveUserToDatabase(string last_name, string first_name, string address, string email, string username, string phone, string password)
        {
            string connectionString = "server=localhost;user=root;database=quan_li_rung;password=123456;";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                    INSERT INTO auth_user (first_name, last_name, address, email, username, phone, password, is_superuser, is_staff, date_joined, last_login, is_active)
                    VALUES (@first_name, @last_name, @address, @email, @username, @phone, @password, 0, 0, NOW(), NULL, 1)";


                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        // Gán giá trị cho các tham số
                        cmd.Parameters.AddWithValue("@first_name", first_name);
                        cmd.Parameters.AddWithValue("@last_name", last_name);
                        cmd.Parameters.AddWithValue("@address", address);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@phone", phone);
                        cmd.Parameters.AddWithValue("@password", password);

                        // Thực thi lệnh
                        cmd.ExecuteNonQuery();
                        return true;
                    }

                }

                catch (Exception ex)
                {
                    // Tùy chọn ghi log lỗi
                    ShowErrorMessage($"Đã xảy ra lỗi: {ex.Message}");
                    return false;
                }
            }
        }
        private void ShowErrorMessage(string errorMessage)
        {
            // Tạo một cửa sổ mới để hiển thị lỗi
            Window errorWindow = new Window
            {
                Title = "Lỗi",
                Width = 400,
                Height = 200,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };

            // Tạo TextBox để hiển thị lỗi
            TextBox errorTextBox = new TextBox
            {
                Text = errorMessage,
                IsReadOnly = true,  // Để không thể chỉnh sửa, chỉ có thể sao chép
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Auto,
                Margin = new Thickness(10),
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch
            };

            // Thêm TextBox vào cửa sổ
            errorWindow.Content = errorTextBox;

            // Hiển thị cửa sổ lỗi
            errorWindow.ShowDialog();
        }
    }
}
