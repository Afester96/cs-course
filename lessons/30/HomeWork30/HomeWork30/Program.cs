using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Test
{
    class Program
    {
        public class InsertOrderCommand
        {
            public int CustomerId { get; set; }
            public DateTimeOffset OrderDate { get; set; }
            public double? Discount { get; set; }
            public List<(int productId, int count)> Lines { get; set; }

            public InsertOrderCommand(int customerId, double? discount = default)
            {
                CustomerId = customerId;
                OrderDate = DateTimeOffset.UtcNow;
                Discount = discount;
                Lines = new List<(int productId, int count)>();
            }
        }

        public class Order
        {
            public int Id { get; }
            public string Customer { get; }
            public DateTimeOffset OrderDate { get; }
            public double? Discount { get; }

            public Order(int id, string customer, DateTimeOffset orderDate, double discount)
            {
                Id = id;
                Customer = customer;
                OrderDate = orderDate;
                Discount = discount;
            }

            public override string ToString()
            {
                return $"Product with id: {Id} {Customer} {OrderDate} {Discount}";
            }
        }
        public interface IOrderRepository
        {
            Task<int> GetCount();
            Task<Order> GetById(int id);
            Task<List<Order>> GetAll();
            Task<int> Insert(InsertOrderCommand dto);
        }
        public class OrderRepository : IOrderRepository
        {
            private readonly string _connection;

            public OrderRepository(string connection)
            {
                _connection = connection;
            }

            public async Task<List<Order>> GetAll()
            {
                await using var connection = await GetConnection();

                var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT " +
                    "O.Id, " +
                    "C.Name, " +
                    "O.OrderDate, " +
                    "O.Discount " +
                    "FROM [Order] AS O " +
                    "JOIN [Customer] AS C " +
                    "ON O.CustomerId = C.Id ";

                await using var reader = await command.ExecuteReaderAsync();

                var products = new List<Order>();
                var idIndex = reader.GetOrdinal("Id");
                var customerIndex = reader.GetOrdinal("Name");
                var orderDateIndex = reader.GetOrdinal("OrderDate");
                var discountIndex = reader.GetOrdinal("Discount");

                if (!reader.HasRows)
                {
                    return new List<Order>();
                }

                while (await reader.ReadAsync())
                {
                    var product = new Order(
                    reader.GetInt32(idIndex),
                    reader.GetString(customerIndex),
                    reader.GetDateTimeOffset(orderDateIndex),
                    reader.IsDBNull(discountIndex) ? 0 : reader.GetDouble(discountIndex)
                    );
                    products.Add(product);
                }

                return products;
            }

            public async Task<Order> GetById(int id)
            {
                await using var connection = await GetConnection();

                var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT " +
                    "O.Id, " +
                    "C.Name, " +
                    "O.OrderDate, " +
                    "O.Discount " +
                    "FROM [Order] AS O " +
                    "JOIN [Customer] AS C " +
                    "ON O.CustomerId = C.Id " +
                    "WHERE O.Id = @id";
                command.Parameters.AddWithValue("id", id);

                await using var reader = await command.ExecuteReaderAsync();

                if (!reader.HasRows)
                {
                    throw new ArgumentException($"Product with id {id} not found");
                }

                var idIndex = reader.GetOrdinal("Id");
                var customerIndex = reader.GetOrdinal("Name");
                var orderDateIndex = reader.GetOrdinal("OrderDate");
                var discountIndex = reader.GetOrdinal("Discount");

                await reader.ReadAsync();

                var product = new Order(
                    reader.GetInt32(idIndex),
                    reader.GetString(customerIndex),
                    reader.GetDateTimeOffset(orderDateIndex),
                    reader.GetDouble(discountIndex)
                );
                return product;
            }

            public async Task<int> Insert(InsertOrderCommand dto)
            {
                await using var connection = await GetConnection();
                await using var transaction = (SqlTransaction)await connection.BeginTransactionAsync(IsolationLevel.ReadCommitted);

                var command = connection.CreateCommand();
                command.CommandText = "InsertOrder";
                command.CommandType = CommandType.StoredProcedure;
                command.Transaction = transaction;
                command.Parameters.AddWithValue("p_customerId", dto.CustomerId);
                command.Parameters.AddWithValue("p_orderDate", dto.OrderDate);
                command.Parameters.AddWithValue("p_discount", dto.Discount.HasValue ? (object)dto.Discount.Value : DBNull.Value);
                var orderId = command.Parameters.Add("p_id", SqlDbType.Int);
                orderId.Direction = ParameterDirection.Output;

                try
                {
                    await command.ExecuteNonQueryAsync();

                    foreach (var (productId, count) in dto.Lines)
                    {
                        command = connection.CreateCommand();
                        command.CommandText = "INSERT INTO [OrderLine] (OrderId, ProductId, Count) VALUES(@orderId, @productId, @count)";
                        command.CommandType = CommandType.Text;
                        command.Transaction = transaction;
                        command.Parameters.AddWithValue("orderId", (int)orderId.Value);
                        command.Parameters.AddWithValue("productId", productId);
                        command.Parameters.AddWithValue("count", count);

                        await command.ExecuteNonQueryAsync();
                    }

                    await transaction.CommitAsync();
                }
                catch (SqlException)
                {
                    await transaction.RollbackAsync();
                    throw;
                }

                return (int)orderId.Value;
            }

            public async Task<int> GetCount()
            {
                await using var connection = await GetConnection();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT Count(*) FROM [Order] AS O JOIN [Customer] AS C ON O.CustomerId = C.Id";

                return (int)command.ExecuteScalar();
            }
            private async Task<SqlConnection> GetConnection()
            {
                var connection = new SqlConnection(_connection);
                await connection.OpenAsync();
                return connection;
            }

            Task<List<Order>> IOrderRepository.GetAll()
            {
                throw new NotImplementedException();
            }
        }

        private const string ConnectionString =
            "Server=tcp:shadow-art.database.windows.net,1433;" +
            "Initial Catalog=reminder; " +
            "Persist Security Info=False;" +
            "User ID=k_gvozdev@shadow-art;" +
            "Password=Wuz74u6MZy7EYB2eZws8;" +
            "Encrypt=True;";

        private static async Task Main(string[] args)
        {
            var orderRepository = new OrderRepository(ConnectionString);
            Console.WriteLine(await orderRepository.GetCount());
            Console.WriteLine(await orderRepository.GetById(2));
            var test = await orderRepository.GetAll();
            foreach (var item in test)
            {
                Console.WriteLine(item);
            }

        }
    }
}
