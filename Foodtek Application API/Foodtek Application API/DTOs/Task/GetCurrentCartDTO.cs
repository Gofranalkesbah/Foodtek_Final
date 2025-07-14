namespace Foodtek_Application_API.DTOs.Task
{
    public class GetCurrentCartDTO
    {
        public int Id { get; set; }

        public string NameEn { get; set; } = null!;

        public string NameAr { get; set; } = null!;

        public string? DescriptionAr { get; set; }

        public string? DescriptionEn { get; set; }

        public int? Quantity { get; set; }


    }
}
