using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace quan_ly_rung_wpf_2.Models
{
    public class AnimalSpeciesModel
    {
        private string connectionString = "Server=localhost;Database=quan_li_rung;Uid=root;Pwd=123456;";

        public DataTable GetAllAnimalSpecies()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM animal_species";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi tải dữ liệu: " + ex.Message);
            }

            return dataTable;
        }

        public void AddAnimalSpecies(string name, string food, string disease, string quantity, string description)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO animal_species (name, main_food, main_disease, longevity, description) VALUES (@name, @food, @disease, @quantity, @description)";
                    using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@food", food);
                        cmd.Parameters.AddWithValue("@disease", disease);
                        cmd.Parameters.AddWithValue("@quantity", quantity);
                        cmd.Parameters.AddWithValue("@description", description);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm dữ liệu: " + ex.Message);
            }
        }

        public void DeleteAnimalSpecies(int id)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM animal_species WHERE id = @id";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xóa dữ liệu: " + ex.Message);
            }
        }
    }
}
