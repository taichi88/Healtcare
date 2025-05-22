namespace TaskProject.Models
{
    public class Mergedmodels
    {
        public class Person
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string PersonalNumber { get; set; }
            public DateTime? DateOfBirth { get; set; }
            public string Phone { get; set; }
            public string Address { get; set; }
            public string Role { get; set; } = "Patient";

            public Patient Patient { get; set; }
            public Doctor Doctor { get; set; }
        }

        public class Patient
        {
            public int Id { get; set; }
            public string InsuranceNumber { get; set; }
            public string EmergencyContactName { get; set; }
            public string EmergencyContactPhone { get; set; }
            public string BloodType { get; set; }
            public string Allergies { get; set; }

            public Person Person { get; set; }
            public ICollection<Appointment> Appointments { get; set; }
            public ICollection<Diagnosis> Diagnoses { get; set; }
        }

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

        public class Appointment
        {
            public int AppointmentId { get; set; }
            public int PatientId { get; set; }
            public int DoctorId { get; set; }
            public DateTime AppointmentDatetime { get; set; }
            public string ReasonForVisit { get; set; }
            public string Status { get; set; }
            public string Notes { get; set; }

            public Patient Patient { get; set; }
            public Doctor Doctor { get; set; }
            public Payment Payment { get; set; }
        }

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

        public class Payment
        {
            public int PaymentId { get; set; }
            public int AppointmentId { get; set; }
            public decimal Amount { get; set; }
            public DateTime PaymentDate { get; set; }
            public string PaymentMethod { get; set; }
            public string Status { get; set; }
            public string Notes { get; set; }

            public Appointment Appointment { get; set; }
        }

        public class DailyReport
        {
            public int ReportId { get; set; }
            public DateTime ReportDate { get; set; }
            public decimal TotalAmountPaid { get; set; }
            public int TotalPatientsServed { get; set; }
        }

    }
}
