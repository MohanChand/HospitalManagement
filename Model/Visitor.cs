namespace HospitalManagementAPI.Model
{
    public class Visitor
    {
        public int Id {  get; set; }
        public string PatientID { get; set; }
        public DateTime VisitDate { get; set; }
        //public List<ProgressNote>? ProgressNotes { get; set;}
    }
}
