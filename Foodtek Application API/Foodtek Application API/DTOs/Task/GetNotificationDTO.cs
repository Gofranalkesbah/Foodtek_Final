namespace Foodtek_Application_API.DTOs.Task
{
    public class GetNotificationDTO
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Content { get; set; }

        public DateTime? CreationDate { get; set; }

        public bool IsRead { get; set; }

    }
}
