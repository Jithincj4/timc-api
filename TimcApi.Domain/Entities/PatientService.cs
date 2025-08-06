namespace TimcApi.Domain.Entities
{
    public class PatientService
    {
        public int PatientServiceId { get; set; }
        public int PatientId { get; set; }
        public int ServiceId { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
