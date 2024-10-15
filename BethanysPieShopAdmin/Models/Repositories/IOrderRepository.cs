namespace BethanysPieShopAdmin.Models.Repositories
{
    public interface IOrderRepository
    {
        public Task<Order> GetOrderDetailsAsync(int? orderId);
        public Task<IEnumerable<Order>> GetAllOrdersWithDetailsAsync();
    }
}
