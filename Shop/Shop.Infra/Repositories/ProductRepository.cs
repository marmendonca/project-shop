using Microsoft.Extensions.Configuration;
using Shop.Domain.Entities;
using Shop.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Shop.Infra.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IConfiguration _configuration;

        public ProductRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Product> GetAll()
        {
            var products = new List<Product>();
            var connectionString = GetConnectionString();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var sqlCommand = "GetProductList";

                using (var command = new SqlCommand(sqlCommand, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var product = new Product();
                        product.Id = Convert.ToInt32(reader["Id"]);
                        product.Title = reader["Title"].ToString();
                        product.Price = Convert.ToDecimal(reader["Price"]);
                        product.CategoryId = Convert.ToInt32(reader["CategoryId"]);
                        products.Add(product);
                    }
                }
            }

            return products;
        }

        public Product GetProductById(int id)
        {
            var product = new Product();
            var connectionString = GetConnectionString();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var sqlCommand = "GetProductById";

                using (var command = new SqlCommand(sqlCommand, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@paramId", id);

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        product.Id = Convert.ToInt32(reader["Id"]);
                        product.Title = reader["Title"].ToString();
                        product.Price = Convert.ToDecimal(reader["Price"]);
                        product.CategoryId = Convert.ToInt32(reader["CategoryId"]);
                    }
                }
            }

            return product;
        }

        public void Save(Product product)
        {
            var connectionString = GetConnectionString();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = $"INSERT INTO [dbo].[Product]([Title], [Price], [CategoryId]) VALUES('{product.Title}', {product.Price}, {product.CategoryId})";

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

        public void Update(Product product, int id)
        {
            var connectionString = GetConnectionString();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = $"UPDATE Product SET [Title] = '{product.Title}', [Price] = {product.Price}, [CategoryId] = {product.CategoryId} WHERE [Id] = {id}";

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
                var query = $"DELETE FROM PRODUCT WHERE [Id] = {id}";

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
