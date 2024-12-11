namespace PetClinicAPI.Models;

public class MedicalRecord
{
    public int Id { get; set; }
    public int PetId { get; set; }
    public int AppointmentId { get; set; }
    public string Description { get; set; }
    public string Treatment { get; set; }
    public string Medications { get; set; }
    public DateTime DateOfRecord { get; set; }
}
