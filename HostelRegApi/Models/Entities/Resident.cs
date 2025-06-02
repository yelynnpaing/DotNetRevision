namespace HostelRegApi.Models.Entities
{
    public class Resident
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Address { get; set; }    
    }
}