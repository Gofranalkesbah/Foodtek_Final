namespace Foodtek_Application_API.DTOs.Task
{
    public class GetAllCategoriesDTO
    {
        public int Id { get; set; }

        public string NameEn { get; set; } = null!;

        public string NameAr { get; set; } = null!;

        public string Image { get; set; } = null!;
    }
}
