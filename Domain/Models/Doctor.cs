namespace TaskProject.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Specialty { get; set; }
        public string LicenseNumber { get; set; }
        public int YearsOfExperience { get; set; }
        public string Phone { get; set; }

        public Person Person { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<Diagnosis> Diagnoses { get; set; }
    }

}
