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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace quan_ly_rung_wpf_2
{
    /// <summary>
    /// Interaction logic for ctrl_thong_tin.xaml
    /// </summary>
    public partial class ctrl_thong_tin : UserControl
    {
        
        public ctrl_thong_tin(string username)
        {
            InitializeComponent();
            // Lấy chuỗi kết nối từ App.config
            // Lấy chuỗi kết nối từ App.config
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            // Tạo kết nối
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // Truy vấn lấy tất cả các cột
                    string query = "SELECT * FROM auth_user WHERE Username = @username";

                    // Sử dụng MySqlCommand để thực hiện truy vấn
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);

                        // Đọc dữ liệu
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    // Lấy giá trị cột Name và Phone
                                    user_name.Text = reader["username"].ToString();
                                    phone.Text = reader["phone"].ToString();
                                    ten.Text = reader["last_name"].ToString();
                                    ho.Text = reader["first_name"].ToString();
                                    email.Text = reader["email"].ToString();
                                    dia_chi.Text = reader["address"].ToString();
                                    ngay_tham_gia.Text = reader["date_joined"].ToString();


                                }
                            }
                            else
                            {
                                MessageBox.Show("No user found.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }
    }
    
}
