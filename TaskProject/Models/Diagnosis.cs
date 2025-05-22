namespace TaskProject.Models
{
    public class Diagnosis
    {
        public int DiagnosisId { get; set; }
        public int PatientId { get; set; }
        public DateTime DiagnosisDate { get; set; }
        public string Description { get; set; }
        public string PrescribedTreatment { get; set; }
        public int DoctorId { get; set; }

        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
    }
}
