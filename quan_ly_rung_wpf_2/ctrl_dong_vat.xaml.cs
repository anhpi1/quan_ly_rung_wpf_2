using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace quan_ly_rung_wpf_2
{
    /// <summary>
    /// Interaction logic for ctrl_dong_vat.xaml
    /// </summary>
    public partial class ctrl_dong_vat : UserControl
    {
        public ctrl_dong_vat()
        {
            InitializeComponent();
            LoadData();
        }

        private string connectionString = "Server=localhost;Database=quan_li_rung;Uid=root;Pwd=123456;";
        private bool isAdding;

        private void LoadData()
        {

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * from animal_species ";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGrid.ItemsSource = dataTable.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }

        private void ChiTiet_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is object data)
            {
                // Tìm dòng tương ứng
                var row = dataGrid.ItemContainerGenerator.ContainerFromItem(data) as DataGridRow;

                if (row != null)
                {
                    // Chuyển đổi trạng thái ẩn/hiện
                    row.DetailsVisibility = row.DetailsVisibility == Visibility.Visible
                        ? Visibility.Collapsed
                        : Visibility.Visible;
                }
            }
        }




        // Xóa sản phẩm
        private void Xoa_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string id = button.Tag.ToString(); // Lấy ID của sản phẩm

            // Hỏi xác nhận trước khi xóa
            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa sản phẩm ID: {id}?", "Xác nhận", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = "DELETE FROM users WHERE id = @id";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Xóa sản phẩm thành công!");

                        // Tải lại dữ liệu sau khi xóa
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa sản phẩm: " + ex.Message);
                }
            }
        }


        private void btnAddSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (isAdding)
            {
                // Xử lý khi nhấn nút Submit
                string name = txtName.Text;
                string category = txtCategory.Text;
                string price = txtPrice.Text;

                if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(category) && !string.IsNullOrWhiteSpace(price))
                {
                    try
                    {
                        // Kết nối tới MySQL
                        using (MySqlConnection connection = new MySqlConnection(connectionString))
                        {
                            connection.Open();

                            // Câu lệnh SQL để chèn dữ liệu
                            string sql = "INSERT INTO users (username, phone, password) VALUES (@name, @category, @price)";
                            using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                            {
                                cmd.Parameters.AddWithValue("@name", name);
                                cmd.Parameters.AddWithValue("@category", category);
                                cmd.Parameters.AddWithValue("@price", price);

                                // Thực thi câu lệnh
                                cmd.ExecuteNonQuery();
                            }
                        }

                        MessageBox.Show("Thêm sản phẩm thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Cập nhật DataGrid (chỉ hiển thị, không ảnh hưởng dữ liệu trong database)
                        var newItem = new
                        {
                            id = dataGrid.Items.Count + 1, // STT tự động tăng
                            name,
                            category,
                            price
                        };
                        var itemsSource = dataGrid.ItemsSource as System.Collections.IList;
                        itemsSource?.Add(newItem);

                        // Reset trạng thái và ẩn Grid nhập liệu
                        inputGrid.Visibility = Visibility.Collapsed;
                        btnAddSubmit.Content = "Add";
                        isAdding = false;

                        // Xóa các giá trị nhập liệu
                        txtName.Clear();
                        txtCategory.Clear();
                        txtPrice.Clear();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                // Hiển thị Grid nhập liệu
                inputGrid.Visibility = Visibility.Visible;
                btnAddSubmit.Content = "Submit";
                isAdding = true;
            }

            LoadData();
        }

        private void CloseInputGrid_Click(object sender, RoutedEventArgs e)
        {
            // Ẩn Grid nhập thông tin
            inputGrid.Visibility = Visibility.Collapsed;

            // Đặt lại trạng thái nút Add/Submit
            btnAddSubmit.Content = "Add";
            isAdding = false;

            // Xóa dữ liệu nhập liệu nếu có
            txtName.Clear();
            txtCategory.Clear();
            txtPrice.Clear();
        }

        private void dataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            // Cập nhật Header của hàng theo chỉ số (index)
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

    }
}
