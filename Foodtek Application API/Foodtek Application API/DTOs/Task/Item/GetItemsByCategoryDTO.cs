namespace Foodtek_Application_API.DTOs.Task.Item
{
    public class GetItemsByCategoryDTO
    {
        public int Id { get; set; }

        public string NameEn { get; set; } = null!;

        public string NameAr { get; set; } = null!;

        public string? DescriptionAr { get; set; }

        public string? DescriptionEn { get; set; }

        public double? Price { get; set; }

        public string Image { get; set; } = null!;


    }
}
