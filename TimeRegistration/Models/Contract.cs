namespace TimeRegistration.Models
{
    public class Contract
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double ValuePerHour { get; set; }
        public double? TotalValue { get; set; }
        public double TotalHours { get; set; }
        public ICollection<TimeLog>? TimeLogs { get; set; }
    }
}