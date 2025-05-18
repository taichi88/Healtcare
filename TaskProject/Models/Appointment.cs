namespace TaskProject.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public string ReasonForVisit { get; set; }

        public Patient? Patient { get; set; }
        public Doctor? Doctor { get; set; }
    }

}
