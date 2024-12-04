using quan_ly_rung_wpf_2.Controllers;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace quan_ly_rung_wpf_2
{
    public partial class ctrl_dong_vat : UserControl
    {
        private AnimalSpeciesController controller;
        private bool isAdding;

        public ctrl_dong_vat()
        {
            InitializeComponent();
            controller = new AnimalSpeciesController();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                DataTable data = controller.LoadAllAnimalSpecies();
                dataGrid.ItemsSource = data.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAddSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (isAdding)
            {
                string name = txtName.Text;
                string food = txtFood.Text;
                string disease = txtDisease.Text;
                string quantity = txtQuantity.Text;
                string description = txtDiscription.Text;

                if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(food) &&
                    !string.IsNullOrWhiteSpace(disease) && !string.IsNullOrWhiteSpace(quantity) &&
                    !string.IsNullOrWhiteSpace(description))
                {
                    try
                    {
                        controller.AddAnimalSpecies(name, food, disease, quantity, description);
                        MessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                ResetInputGrid();
            }
            else
            {
                inputGrid.Visibility = Visibility.Visible;
                btnAddSubmit.Content = "Submit";
                isAdding = true;
            }
        }

        private void Xoa_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null && int.TryParse(button.Tag.ToString(), out int id))
            {
                if (MessageBox.Show($"Bạn có chắc chắn muốn xóa ID: {id}?", "Xác nhận", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        controller.DeleteAnimalSpecies(id);
                        MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void ResetInputGrid()
        {
            inputGrid.Visibility = Visibility.Collapsed;
            btnAddSubmit.Content = "Add";
            isAdding = false;

            txtName.Clear();
            txtFood.Clear();
            txtDisease.Clear();
            txtQuantity.Clear();
            txtDiscription.Clear();
        }
    }
}
