namespace StudentRegApi.Model.Entities
{
    public class Student
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string FatherName { get; set; }
        public required string Grade { get; set; }
        public required string Address { get; set; }
    }
}
