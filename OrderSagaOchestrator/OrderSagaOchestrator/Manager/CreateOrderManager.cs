using OrderSagaOchestrator.Constants;
using OrderSagaOchestrator.Services;
using static OrderSagaOchestrator.Constants.Enums;

namespace OrderSagaOchestrator.Manager
{
    public class CreateOrderManager : IOrderManager
    {
        private readonly ICreateOrderService createOrder;

        public CreateOrderManager(ICreateOrderService createOrder)
        {
            this.createOrder = createOrder;
        }

        public bool CreateOrder(Order order)
        {
            var machine = new Stateless.StateMachine<TransactionState, Actions>(TransactionState.NotStarted);

            machine.Configure(TransactionState.NotStarted)
                .PermitDynamic(Actions.CreateOrder, () => createOrder.CreateOrder(order).Result ? 
                TransactionState.OrderCreated : TransactionState.OrderCreatedFailed);

            var invent = new Inventory
            {
                Id = order.Id,
                Quantity = order.Quantiy,
                Name = order.Name,
                ProductId = order.ProductId,
            };
            machine.Configure(TransactionState.OrderCreated)
                .PermitDynamic(Actions.UpdateInventory, () => createOrder.UpdateInventoty(invent).Result ?
                TransactionState.InventoryUpdated : TransactionState.InventoryRolledBack
                ).OnEntry(()=> machine.Fire(Actions.UpdateInventory));

            machine.Configure(TransactionState.InventoryUpdated)
                .PermitDynamic(Actions.SendNotification, () =>
                {
                    createOrder.SendNotication($"{order.Quantiy} order of {order.Name} made.");
                    return TransactionState.NotificationSent;
                });
                

            machine.Configure(TransactionState.InventoryUpdatedFailed)
               .PermitDynamic(Actions.DeleteOder, () => {
                   createOrder.DeleteOrder(order);
                   return TransactionState.InventoryRolledBack;
                   } 
               )
               .OnEntry(() => machine.Fire(Actions.SendNotification));

        }

    }
}
