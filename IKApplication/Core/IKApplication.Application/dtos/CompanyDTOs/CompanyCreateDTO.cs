namespace IKApplication.Application.dtos.CompanyDTOs
{
    public class CompanyCreateDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Sector { get; set; }
        public int NumberOfEmployees { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        //public List<AppUser>? CompanyManagers { get; set; }
    }
}
