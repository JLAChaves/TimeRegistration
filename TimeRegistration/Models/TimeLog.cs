namespace TimeRegistration.Models
{
    public class TimeLog
    {
        public int Id { get; set; }
        public int? ContractId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double Hours { get; set; }

    }
}
