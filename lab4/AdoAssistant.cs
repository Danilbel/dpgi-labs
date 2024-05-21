using System;
using System.Data;
using Npgsql;
using System.Windows;

namespace lab4;

public class AdoAssistant
{
    private readonly string _connectionString = System.Configuration.ConfigurationManager
        .ConnectionStrings["connectionStringsAdo"].ConnectionString;

    public DataTable TableLoad()
    {
        var dt = new DataTable();
        
        try
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            const string command = "select id, article, name, unit_of_measure, quantity, price from products";
            using var adapter = new NpgsqlDataAdapter(command, connection);
            adapter.Fill(dt);
        }
        catch (Exception)
        {
            MessageBox.Show("Помилка БД");
        }

        return dt;
    }
    
    public void UpdateTable(int id, string article, string name, string unitOfMeasure, double? quantity, double? price)
    {
        try
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            const string command = "update products set article = @article, name = @name, unit_of_measure = @unitOfMeasure, quantity = @quantity, price = @price where id = @id";
            using var adapter = new NpgsqlDataAdapter();
            adapter.UpdateCommand = new NpgsqlCommand(command, connection);
            adapter.UpdateCommand.Parameters.AddWithValue("@id", id);
            adapter.UpdateCommand.Parameters.AddWithValue("@article", article);
            adapter.UpdateCommand.Parameters.AddWithValue("@name", name);
            adapter.UpdateCommand.Parameters.AddWithValue("@unitOfMeasure", unitOfMeasure.Equals("") ? DBNull.Value : unitOfMeasure);
            adapter.UpdateCommand.Parameters.AddWithValue("@quantity", quantity == null ? DBNull.Value : quantity);
            adapter.UpdateCommand.Parameters.AddWithValue("@price", price == null ? DBNull.Value : price);
            adapter.UpdateCommand.ExecuteNonQuery();
        }
        catch (Exception)
        {
            MessageBox.Show("Помилка БД");
        }
    }
    
    public void InsertIntoTable(string article, string name, string unitOfMeasure, double? quantity, double? price)
    {
        try
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            const string command = "insert into products (article, name, unit_of_measure, quantity, price) values (@article, @name, @unitOfMeasure, @quantity, @price)";
            using var adapter = new NpgsqlDataAdapter();
            adapter.InsertCommand = new NpgsqlCommand(command, connection);
            adapter.InsertCommand.Parameters.AddWithValue("@article", article);
            adapter.InsertCommand.Parameters.AddWithValue("@name", name);
            adapter.InsertCommand.Parameters.AddWithValue("@unitOfMeasure", unitOfMeasure.Equals("") ? DBNull.Value : unitOfMeasure);
            adapter.InsertCommand.Parameters.AddWithValue("@quantity", quantity == null ? DBNull.Value : quantity);
            adapter.InsertCommand.Parameters.AddWithValue("@price", price == null ? DBNull.Value : price);
            adapter.InsertCommand.ExecuteNonQuery();
        }
        catch (Exception)
        {
            MessageBox.Show("Помилка БД");
        }
    }
    
    public void DeleteFromTable(int id)
    {
        try
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            const string command = "delete from products where id = @id";
            using var adapter = new NpgsqlDataAdapter();
            adapter.DeleteCommand = new NpgsqlCommand(command, connection);
            adapter.DeleteCommand.Parameters.AddWithValue("@id", id);
            adapter.DeleteCommand.ExecuteNonQuery();
        }
        catch (Exception)
        {
            MessageBox.Show("Помилка БД");
        }
    }
}