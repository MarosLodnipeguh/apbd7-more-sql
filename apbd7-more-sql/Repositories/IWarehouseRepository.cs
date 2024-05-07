using Tutorial6.Models.DTOs;

namespace Tutorial6.Repositories;

public interface IWarehouseRepository
{
   Task<bool> DoesProductExist(int idProduct);
   Task<bool> DoesWarehouseExist(int idWarehouse);
   Task<bool> DoesOrderExist(int IdProcut, int amount, DateTime createdAt);
   Task UpdateOrder(int IdProcut, int amount, DateTime createdAt);
   Task<double> GetProductPrice(int idProduct);
   Task<int> AddProduct(ProductDTO product);
   
   
    
}