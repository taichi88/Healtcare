
namespace HealthcareApi.Application.DTO;

public class AppointmentsDto
{
    
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public DateTime AppointmentDateTime { get; set; }
    public string ReasonForVisit { get; set; }
}
  
