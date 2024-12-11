using PetClinicAPI.Models;

namespace PetClinicAPI.Data;

public static class InMemoryData
{
    public static List<Pet> Pets = [];
    public static List<Owner> Owners = [];
    public static List<Appointment> Appointments = [];
    public static List<MedicalRecord> MedicalRecords = [];
}
