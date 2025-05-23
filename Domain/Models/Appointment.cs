using System.Text.Json.Serialization;

namespace TaskProject.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentDatetime { get; set; }
        public string ReasonForVisit { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }

        [JsonIgnore]
        public Patient? Patient { get; set; }
        [JsonIgnore]
        public Doctor? Doctor { get; set; }
        public Payment Payment { get; set; }
    }

}
