namespace Foodtek_Application_API.DTOs.Task
{
    public class GetAllOffersDTO
    {
        public int Id { get; set; }

        public string TitleEn { get; set; } = null!;

        public string TitleAr { get; set; } = null!;

        public string? DescriptionEn { get; set; }

        public string? DescriptionAr { get; set; }

        public string? Image { get; set; }
    }
}
