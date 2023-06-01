using AbstractedRabbitMQ.Publishers;

namespace OrderSagaOchestrator.Services
{
    public class CreateOrderService : ICreateOrderService
    {
        private readonly HttpClient httpClient;
        private readonly IPublisher publisher;

        public CreateOrderService(HttpClient httpClient, IPublisher publisher)
        {
            this.httpClient = httpClient;
            this.publisher = publisher;
        }
        public async Task<bool> CreateOrder(Order order)
        {
           var resp= await httpClient.PatchAsJsonAsync<Order>("https://localhost:7275/api/order", order);
            return resp.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteOrder(Order order)
        {
            var resp = await httpClient.DeleteAsync($"https://localhost:7275/api/order?id={order.Id}");
            return resp.IsSuccessStatusCode;
        }

        public async Task<bool> RolledBackInventoty(Inventory inventory)
        {
            inventory.Quantity=-inventory.Quantity;
            var resp = await httpClient.PutAsJsonAsync<Inventory>("https://localhost:7172/ap/inventory", inventory);
            return resp.IsSuccessStatusCode;
        }

        public void SendNotication(string message)
        {
            publisher.Publish(message, "order-routingKey",null,null);
        }

        public async Task<bool> UpdateInventoty(Inventory inventory)
        {
            var resp = await httpClient.PutAsJsonAsync<Inventory>("https://localhost:7172/ap/inventory", inventory);
            return resp.IsSuccessStatusCode;
        }
    }
}
