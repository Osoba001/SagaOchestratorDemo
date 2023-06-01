namespace OrderSagaOchestrator.Constants
{
    public static class Enums
    {
         public enum Actions
        {
            CreateOrder, 
            DeleteOder,
            UpdateInventory,
            RolledBackInventory,
            SendNotification
        }
        public enum TransactionState
        {
            NotStarted,
            OrderCreated,
            OrderCancelled,
            OrderCreatedFailed,
            InventoryUpdated,
            InventoryUpdatedFailed,
            InventoryRolledBack,
            NotificationSent,
            NotificationSendFailed
        }
    }
}
