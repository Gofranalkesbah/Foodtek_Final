namespace Foodtek_Application_API.DTOs.Task
{
    public class GetOrderHistoryDTO
    {
        public int Id {  get; set; }

        public decimal TotalPrice { get; set; }

        public int? AddressId { get; set; }

        public DateTime? CreationDate { get; set; }


    }
}
