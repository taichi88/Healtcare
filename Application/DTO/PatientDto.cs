

namespace HealthcareApi.Application.DTO
{
    public class PatientDto
    {
        public int PersonId { get; set; }
        public string InsuranceNumber { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactPhone { get; set; }
        public string BloodType { get; set; }
        public string Allergies { get; set; }
    }

}
