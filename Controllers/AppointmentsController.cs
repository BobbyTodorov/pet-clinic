using Microsoft.AspNetCore.Mvc;
using PetClinicAPI.Data;
using PetClinicAPI.Models;

namespace PetClinicAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Appointment>> GetAllAppointments()
    {
        return Ok(InMemoryData.Appointments);
    }

    [HttpGet("{id}")]
    public ActionResult<Appointment> GetAppointmentById(int id)
    {
        var appointment = InMemoryData.Appointments.FirstOrDefault(a => a.Id == id);
        if (appointment == null)
        {
            return NotFound();
        }
        return Ok(appointment);
    }

    [HttpPost]
    public ActionResult<Appointment> AddAppointment(Appointment appointment)
    {
        if (InMemoryData.Owners.Any(a => a.Id == appointment.Id))
        {
            return Ok("Appointment already exists.");
        }
        if (!InMemoryData.Pets.Any(pet => pet.Id == appointment.PetId))
        {
            return NotFound("Pet not found.");
        }
        InMemoryData.Appointments.Add(appointment);
        return CreatedAtAction(nameof(GetAppointmentById), new { id = appointment.Id }, appointment);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateAppointment(int id, Appointment updatedAppointment)
    {
        var appointment = InMemoryData.Appointments.FirstOrDefault(a => a.Id == id);
        if (appointment == null)
        {
            return NotFound();
        }
        if (!InMemoryData.Pets.Any(pet => pet.Id == updatedAppointment.PetId))
        {
            return NotFound("Pet not found.");
        }
        appointment.PetId = updatedAppointment.PetId;
        appointment.Date = updatedAppointment.Date;
        appointment.Duration = updatedAppointment.Duration;
        appointment.Reason = updatedAppointment.Reason;
        appointment.Status = updatedAppointment.Status;
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteAppointment(int id)
    {
        var appointment = InMemoryData.Appointments.FirstOrDefault(a => a.Id == id);
        if (appointment == null)
        {
            return NotFound();
        }
        InMemoryData.Appointments.Remove(appointment);
        return NoContent();
    }
}
