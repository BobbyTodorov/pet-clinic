namespace PetClinicAPI.Models;

public class Appointment
{
    public int Id { get; set; }
    public int PetId { get; set; }
    public DateTime Date { get; set; }
    public double Duration { get; set; }
    public string Reason { get; set; }
    public string Status { get; set; }
}
