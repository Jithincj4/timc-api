namespace TimcApi.Application.DTOs
{
    public class PatientServiceDto
    {
        public int PatientServiceId { get; set; }
        public int PatientId { get; set; }
        public int ServiceId { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class AssignServiceDto
    {
        public int PatientId { get; set; }
        public int ServiceId { get; set; }
        public string Status { get; set; }
    }
}
