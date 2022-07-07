namespace GacDirectory.Models
{
    public class Employee
    { 
        public int EmployeeID { get; set; }
        public string AttenCode { get; set; }
        public string ContactFav { get; set; }
    }

    public class EmployeeFav
    {        
        public string Code { get; set; }
        public string Favs { get; set; }
    }
    public class EmployeeData
    {
        public Int64 Id { get; set; }
        public string Code { get; set; }
        public string FirstName { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string Branch { get; set; }
        public string Department { get; set; }
        public string JobTitle { get; set; }
        public string? Extension { get; set; }
        public byte[]? Photo { get; set; }
    }
    public class EmployeeContact
    {
        public Int64 Id { get; set; }
        public string Code { get; set; }      
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string Company { get; set; }        
        public string Branch { get; set; }
        public string Department { get; set; }
        public string JobTitle { get; set; }
        public string? Extension { get; set; }
        public string? DirectPhone { get; set; }        
        public string? Mobile1 { get; set; }
        public string? Mobile2 { get; set; }
        public string? OfficialEmail{ get; set; }
        public string? PersonalEmail { get; set; }
        public byte[]? Photo { get; set; }
    }
}
