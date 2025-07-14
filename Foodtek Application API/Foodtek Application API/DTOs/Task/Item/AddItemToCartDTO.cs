namespace Foodtek_Application_API.DTOs.Task.Item
{
    public class AddItemToCartDTO
    {
        public int UserId { get; set; }

        public int ItemId { get; set; }

        public int Quantity { get; set; }
    }
}
