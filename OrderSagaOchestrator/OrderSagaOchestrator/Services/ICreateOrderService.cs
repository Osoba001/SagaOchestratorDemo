namespace OrderSagaOchestrator.Services
{
    public interface ICreateOrderService
    {
        public Task<bool> CreateOrder(Order order);
        public Task<bool> DeleteOrder(Order order);
        public Task<bool> UpdateInventoty(Inventory inventory);
        public Task<bool> RolledBackInventoty(Inventory inventory);
        public void SendNotication(string message);
    }
}
