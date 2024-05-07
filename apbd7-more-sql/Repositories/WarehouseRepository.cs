using Microsoft.Data.SqlClient;
using Tutorial6.Models.DTOs;

namespace Tutorial6.Repositories;

public class WarehouseRepository : IWarehouseRepository
{
    
    // add IConfiguration for connection string
    private readonly IConfiguration _configuration;
    public WarehouseRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public async Task<bool> DoesProductExist(int idProduct)
    {
        await using var connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "SELECT 1 FROM Product WHERE IdProduct = @Id";
        command.Parameters.AddWithValue("@Id", idProduct);

        await connection.OpenAsync();

        var result = await command.ExecuteScalarAsync();

        return result is not null;
    }

    public async Task<bool> DoesWarehouseExist(int idWarehouse)
    {
        await using var connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "SELECT 1 FROM Warehouse WHERE IdWarehouse = @Id";
        command.Parameters.AddWithValue("@Id", idWarehouse);

        await connection.OpenAsync();

        var result = await command.ExecuteScalarAsync();

        return result is not null;
    }

    public async Task<bool> DoesOrderExist(int IdProcut, int amount, DateTime createdAt)
    {
        await using var connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "SELECT 1 FROM Order WHERE IdProduct = @Id AND Amount = @Amount AND CreatedAt < @CreatedAt AND FullfilledAt is null";
        command.Parameters.AddWithValue("@Id", IdProcut);
        command.Parameters.AddWithValue("@Amount", amount);
        command.Parameters.AddWithValue("@CreatedAt", createdAt);

        await connection.OpenAsync();

        var result = await command.ExecuteScalarAsync();

        return result is not null;
    }

    public async Task<int> getIdOrder(int IdProduct, int amount, DateTime createdAt)
    {
        await using var connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "SELECT 1 FROM Order WHERE IdOrder = @Id";
        command.Parameters.AddWithValue("@Id", idOrder);

        await connection.OpenAsync();

        var result = await command.ExecuteScalarAsync();

        return result
    }

    public async Task<int> AddProduct(ProductDTO product)
    {
        // check if product exists
        if (!await DoesProductExist(product.IdProduct))
        {
            throw new ArgumentException("Product does not exist", nameof(product.IdProduct));
        }

        // check if warehouse exists
        if (!await DoesWarehouseExist(product.IdWarehouse))
        {
            throw new ArgumentException("Warehouse does not exist", nameof(product.IdWarehouse));
        }
        
        // check if amount is greater than 0
        if (!(product.Amount > 0))
        {
            throw new ArgumentOutOfRangeException(nameof(product.Amount), "Amount must be greater than 0");
        }
        
        // check if order exists
        if (!await DoesOrderExist(product.IdProduct, product.Amount, product.CreatedAt))
        {
            throw new ArgumentException("Order does not exists", nameof(product));
        }
        
        // is order fullfilled?
        await using var connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "SELECT 1 FROM Order Product_Warehouse WHERE IdProduct = @Id AND Amount = @amount AND FullfilledAt is not null";
        command.Parameters.AddWithValue("@Id", product.IdProduct);
        command.Parameters.AddWithValue("@amount", product.Amount);

        await connection.OpenAsync();

        var result = await command.ExecuteScalarAsync();
        if (result is not null)
        {
            throw new ArgumentException("Order is fullfilled", nameof(product));
        }

        // add product

        
    }

    public Task updateOrder(DateTime date, int idOrder)
    {
        throw new NotImplementedException();
    }
}