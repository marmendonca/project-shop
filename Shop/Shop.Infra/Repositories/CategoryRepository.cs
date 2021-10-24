using Microsoft.Extensions.Configuration;
using Shop.Domain.Entities;
using Shop.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Shop.Infra.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IConfiguration _configuration;

        public CategoryRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Category> GetAll()
        {
            var categories = new List<Category>();
            var connectionString = GetConnectionString();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var sqlCommand = "GetCategoryList";

                using (var command = new SqlCommand(sqlCommand, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var category = new Category();
                        category.Id = Convert.ToInt32(reader["Id"]);
                        category.Title = reader["Title"].ToString();
                        categories.Add(category);
                    }
                }
            }

            return categories;
        }

        public Category GetCategoryById(int id)
        {
            var category = new Category();
            var connectionString = GetConnectionString();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var sqlCommand = "GetCategoryById";

                using (var command = new SqlCommand(sqlCommand, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@paramId", id);

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        category.Id = Convert.ToInt32(reader["Id"]);
                        category.Title = reader["Title"].ToString();
                    }
                }
            }

            return category;
        }

        public void Save(Category category)
        {
            var connectionString = GetConnectionString();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = $"INSERT INTO [dbo].[Category]([Title], [Price], [CategoryId]) VALUES({category.Title})";

                using (var command = new SqlCommand(query, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;

                    var rows = command.ExecuteNonQuery();
                    if (rows != 1)
                    {
                        throw new Exception();
                    }
                }
            }
        }

        public void Update(Category category, int id)
        {
            var connectionString = GetConnectionString();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = $"UPDATE [Category] SET [Title] = {category.Title} WHERE [Id] = {id}";

                using (var command = new SqlCommand(query, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;

                    var rows = command.ExecuteNonQuery();
                    if (rows != 1)
                    {
                        throw new Exception();
                    }
                }
            }
        }

        public void Delete(int id)
        {
            var connectionString = GetConnectionString();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = $"DELETE FROM [Category] WHERE [Id] = {id}";

                using (var command = new SqlCommand(query, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;

                    var rows = command.ExecuteNonQuery();
                    if (rows != 1)
                    {
                        throw new Exception();
                    }
                }
            }
        }

        private string GetConnectionString()
        {
            return _configuration.GetConnectionString("connectionString");
        }
    }
}
