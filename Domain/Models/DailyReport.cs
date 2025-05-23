namespace TaskProject.Models
{
    public class DailyReport
{
    public int ReportId { get; set; }
    public DateTime ReportDate { get; set; }
    public decimal TotalAmountPaid { get; set; }
    public int TotalPatientsServed { get; set; }
}
}
