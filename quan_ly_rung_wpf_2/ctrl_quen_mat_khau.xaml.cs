using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for ctrl_quen_mat_khau.xaml
    /// </summary>
    public partial class ctrl_quen_mat_khau : UserControl
    {
        public ctrl_quen_mat_khau()
        {
            InitializeComponent();
        }
        public ContentControl UserControlContainer { get; set; }
        public Window newscreen { get; set; }


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



        private void Da_co_tai_khoan(object sender, MouseButtonEventArgs e)
        {

            var myControl = new ctrl_dang_nhap();


            myControl.UserControlContainer = UserControlContainer;
            myControl.newscreen = newscreen;
            UserControlContainer.Content = myControl;




        }

        private void Da_co_tai_khoan_enter(object sender, DragEventArgs e)
        {
            if (da_co_tai_khoan != null)
            {
                // Thay đổi màu nền khi chuột di vào
                da_co_tai_khoan.Foreground = new SolidColorBrush(Colors.Blue);
            }
        }

        private void Da_co_tai_khoan_leave(object sender, DragEventArgs e)
        {
            if (da_co_tai_khoan != null)
            {
                // Đặt lại màu nền khi chuột rời khỏi
                da_co_tai_khoan.Foreground = new SolidColorBrush(Colors.Black);
            }
        }





        public class MainViewModel : INotifyPropertyChanged
        {
            private string _numberInput;

            public string NumberInput
            {
                get => _numberInput;
                set
                {
                    if (_numberInput != value)
                    {
                        // Kiểm tra chỉ nhập số
                        if (string.IsNullOrEmpty(value) || int.TryParse(value, out _))
                        {
                            _numberInput = value;
                            OnPropertyChanged(nameof(NumberInput));
                        }
                    }
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;
            protected void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private void LoginButton_Click(object sender, MouseButtonEventArgs e)
        {

            // Lấy thông tin từ các trường nhập liệu
            string username = used_name.Text.Trim();
            string phone = so_dien_thoai.Text.Trim();
            string password = pass.Password;
            string rePassword = re_pass.Password;

            // Kiểm tra điều kiện
            int c = 0;

            // 1. Kiểm tra tên người dùng hoặc số điện thoại đã tồn tại
            if (username != "")
            {
                if (IsNoExistsUsername(username))
                {
                    log_used_name.Text = "Tên người dùng không tồn tại.";
                    log_used_name.Foreground = new SolidColorBrush(Colors.Red);
                    c++;

                }
                else
                {
                    log_used_name.Text = "Tên đăng nhập:";
                    log_used_name.Foreground = new SolidColorBrush(Colors.Black);
                }
            }
            else
            {
                log_used_name.Text = "Tên người dùng trống.";
                log_used_name.Foreground = new SolidColorBrush(Colors.Red); ;
            }
            if (phone != "")
            {
                if (!IsUserExistsPhone(phone, username))
                {
                    log_so_dien_thoai.Text = "Sai số điện thoại";
                    log_so_dien_thoai.Foreground = new SolidColorBrush(Colors.Red); ;
                    c++;
                }
                else
                {
                    log_so_dien_thoai.Text = "Số điện thoại:";
                    log_so_dien_thoai.Foreground = new SolidColorBrush(Colors.Black); ;
                }
            }
            else
            {
                log_so_dien_thoai.Text = "Số điện thoại trống.";
                log_so_dien_thoai.Foreground = new SolidColorBrush(Colors.Red); ;
            }


            if (password != "")
            {
                if (password.Length < 8 //||
                //!Regex.IsMatch(password, @"[a-z]") ||
                //!Regex.IsMatch(password, @"[A-Z]")
                )
                {
                    log_pass.Text = "Mật khẩu mới có ít nhất 8 ký tự.";
                    log_pass.Foreground = new SolidColorBrush(Colors.Red); ;
                    c++;
                }
                else
                {
                    log_pass.Text = "Mật khẩu mới:";
                    log_pass.Foreground = new SolidColorBrush(Colors.Black); ;
                }
            }
            else
            {
                log_pass.Text = "Mật khẩu trống.";
                log_pass.Foreground = new SolidColorBrush(Colors.Red);
            }


            if (rePassword != "")
            {             // 3. Kiểm tra mật khẩu nhập lại
                if (password != rePassword)
                {
                    log_re_pass.Text = "Mật khẩu không trùng khớp.";
                    log_re_pass.Foreground = new SolidColorBrush(Colors.Red);
                    c++;
                }
                else
                {
                    log_re_pass.Text = "Nhập lại mật khẩu:";
                    log_re_pass.Foreground = new SolidColorBrush(Colors.Black);
                }
            }
            else
            {
                log_re_pass.Text = "Mật khẩu không trống.";
                log_re_pass.Foreground = new SolidColorBrush(Colors.Red);
            }

            if (username == "" || phone == "" || password == "" || rePassword == "") return;
            if (c > 0) return;

            // Nếu không có lỗi, ghi vào MySQL

            if (UpdateUserPassword(username, password))
            {

                var myControl = new ctrl_lay_lai_mat_khau_thanh_cong();

                myControl.UserControlContainer = UserControlContainer;
                myControl.newscreen = newscreen;
                UserControlContainer.Content = myControl;


            }



        }

        private bool UpdateUserPassword(string username, string newPassword)
        {
            string connectionString = "server=localhost;user=root;database=quan_li_rung;password=123456;";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Câu truy vấn để cập nhật mật khẩu cho người dùng dựa trên username
                    string query = "UPDATE auth_user SET password = @newPassword WHERE username = @username";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        // Gán giá trị cho các tham số
                        cmd.Parameters.AddWithValue("@newPassword", newPassword);
                        cmd.Parameters.AddWithValue("@username", username);

                        // Thực thi lệnh
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Kiểm tra xem có bản ghi nào được cập nhật không
                        return rowsAffected > 0; // Trả về true nếu cập nhật thành công
                    }
                }
                catch (Exception ex)
                {
                    // Ghi log hoặc hiển thị lỗi
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

        // Hàm kiểm tra người dùng đã tồn tại
        private bool IsNoExistsUsername(string username)
        {
            string connectionString = "server=localhost;user=root;database=quan_li_rung;password=123456;";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM auth_user WHERE username = @username";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count == 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);

                    return false;
                }
            }
        }
        private bool IsUserExistsPhone(string phone, string username)
        {
            string connectionString = "server=localhost;user=root;database=quan_li_rung;password=123456;";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // Điều chỉnh query để kiểm tra phone của một username cụ thể
                    string query = "SELECT COUNT(*) FROM auth_user WHERE username = @username AND phone = @phone";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@phone", phone);

                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0; // Nếu count > 0, nghĩa là có tồn tại phone cho username

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false; // Trả về false nếu có lỗi
                }
            }
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

        private void Chua_co_tai_khoan(object sender, MouseButtonEventArgs e)
        {
            var myControl = new ctrl_dang_ki_1();

            myControl.UserControlContainer = UserControlContainer;
            myControl.newscreen = newscreen;
            UserControlContainer.Content = myControl;
        }
    }
}
