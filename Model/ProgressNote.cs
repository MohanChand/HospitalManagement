namespace HospitalManagementAPI.Model
{
    public class ProgressNote
    {
        public int Id { get; set; }
        public string PatientId { get; set; }
        public string? Height { get; set; }
        public string? Weight { get; set; }
        public string? Temperature { get; set; }
    }
}
