namespace GacDirectory.Models
{
    public class EmployeeModels
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int Year { get; set; }
        public string LeaveType { get; set; }
        public decimal DaysAllowed { get; set; }
        public decimal CarriedForward { get; set; }
        public decimal Refill { get; set; }
        public decimal DaysUsed { get; set; }
        public decimal Balance { get; set; }
    }

    public class LeaveReport
    {
        public int Id { get; set; }
        public string AppliedDate { get; set; }
        public string LeaveType { get; set; }
        public string Description { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public decimal LeaveDays { get; set; }
        public decimal Total { get; set; }
    }

    public class DepartmentChart
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public int? Level { get; set; }
        public byte[] Photo { get; set; }
    }
}
