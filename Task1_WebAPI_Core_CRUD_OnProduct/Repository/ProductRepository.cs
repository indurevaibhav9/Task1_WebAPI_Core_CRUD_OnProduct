using Microsoft.Data.SqlClient;
using Task1_WebAPI_Core_CRUD_OnProduct.Models;
using System.Data;

namespace Task1_WebAPI_Core_CRUD_OnProduct.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connString;

        public ProductRepository(IConfiguration configuration)
        {
            _connString = configuration.GetConnectionString("DefaultConnectionString");
        }

        public List<Product> GetAll()
        {
            List<Product> products = new List<Product>();
            string query = "SELECT Id, Name, Price FROM Products";

            using (SqlConnection conn = new SqlConnection(_connString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    products.Add(new Product
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["Name"]?.ToString(),
                        Price = reader["Price"] != DBNull.Value ? Convert.ToDecimal(reader["Price"]) : 0
                    });
                }
            }

            return products;
        }

        public Product GetById(int id)
        {
            Product product = null;
            string query = "SELECT Id, Name, Price FROM Products WHERE Id = @Id";

            using (SqlConnection conn = new SqlConnection(_connString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    product = new Product
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["Name"]?.ToString(),
                        Price = reader["Price"] != DBNull.Value ? Convert.ToDecimal(reader["Price"]) : 0
                    };
                }
            }

            return product;
        }

        public int InsertProduct(Product product)
        {
            string query = "INSERT INTO Products (Name, Price) VALUES (@Name, @Price)";

            using (SqlConnection conn = new SqlConnection(_connString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Name", product.Name);
                cmd.Parameters.AddWithValue("@Price", product.Price);

                conn.Open();
                return cmd.ExecuteNonQuery(); // returns number of rows affected
            }
        }

        public Product UpdateById(int id, Product product)
        {
            string query = "UPDATE Products SET Name = @Name, Price = @Price WHERE Id = @Id";

            using (SqlConnection conn = new SqlConnection(_connString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Name", product.Name);
                cmd.Parameters.AddWithValue("@Price", product.Price);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    return GetById(id);
                }
                return null;
            }
        }

        public bool DeleteById(int id)
        {
            string query = "DELETE FROM Products WHERE Id = @Id";

            using (SqlConnection conn = new SqlConnection(_connString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }
    }
}
