using TaskProject.Models;

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
