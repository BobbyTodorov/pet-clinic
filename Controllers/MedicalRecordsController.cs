using Microsoft.AspNetCore.Mvc;
using PetClinicAPI.Data;
using PetClinicAPI.Models;

namespace PetClinicAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MedicalRecordsController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<MedicalRecord>> GetAllMedicalRecords()
    {
        return Ok(InMemoryData.MedicalRecords);
    }

    [HttpGet("{id}")]
    public ActionResult<MedicalRecord> GetMedicalRecordById(int id)
    {
        var record = InMemoryData.MedicalRecords.FirstOrDefault(m => m.Id == id);
        if (record == null)
        {
            return NotFound();
        }
        return Ok(record);
    }

    [HttpPost]
    public ActionResult<MedicalRecord> AddMedicalRecord(MedicalRecord medicalRecord)
    {
        if (InMemoryData.Owners.Any(r => r.Id == medicalRecord.Id))
        {
            return Ok("Medical record already exists.");
        }
        medicalRecord.Id = InMemoryData.MedicalRecords.Count + 1;
        InMemoryData.MedicalRecords.Add(medicalRecord);
        return CreatedAtAction(nameof(GetMedicalRecordById), new { id = medicalRecord.Id }, medicalRecord);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateMedicalRecord(int id, MedicalRecord updatedMedicalRecord)
    {
        var record = InMemoryData.MedicalRecords.FirstOrDefault(m => m.Id == id);
        if (record == null)
        {
            return NotFound();
        }
        if (!InMemoryData.Owners.Any(pet => pet.Id == updatedMedicalRecord.PetId))
        {
            return NotFound("Pet not found.");
        }
        record.PetId = updatedMedicalRecord.PetId;
        record.AppointmentId = updatedMedicalRecord.AppointmentId;
        record.Description = updatedMedicalRecord.Description;
        record.Treatment = updatedMedicalRecord.Treatment;
        record.Medications = updatedMedicalRecord.Medications;
        record.DateOfRecord = updatedMedicalRecord.DateOfRecord;
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteMedicalRecord(int id)
    {
        var record = InMemoryData.MedicalRecords.FirstOrDefault(m => m.Id == id);
        if (record == null)
        {
            return NotFound();
        }
        InMemoryData.MedicalRecords.Remove(record);
        return NoContent();
    }
}
