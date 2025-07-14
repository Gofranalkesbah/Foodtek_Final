namespace Foodtek_Application_API.DTOs.Task
{
    public class AddAddressDTO
    {
        public string? Title { get; set; }
        public string? BuildingName { get; set; }

        public string? BuildingNumber { get; set; }
        public string? Floor { get; set; }

        public string? AdditionalDetails { get; set; } 
        public decimal? Latitude { get; set; }  
        public decimal? Longitude { get; set; }
        public string? ApartmentNumber { get; set; }

        public string? Province { get; set; }

        public string? Region { get; set; }

        public int UsersId { get; set; }

    }
}
