using Microsoft.AspNetCore.Mvc;
using PetClinicAPI.Data;
using PetClinicAPI.Models;

namespace PetClinicAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OwnersController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Owner>> GetAllOwners()
    {
        return Ok(InMemoryData.Owners);
    }

    [HttpGet("{id}")]
    public ActionResult<Owner> GetOwnerById(int id)
    {
        var owner = InMemoryData.Owners.FirstOrDefault(o => o.Id == id);
        if (owner == null)
        {
            return NotFound();
        }
        return Ok(owner);
    }

    [HttpPost]
    public ActionResult<Owner> AddOwner(Owner owner)
    {
        if (InMemoryData.Owners.Any(o => o.Id == owner.Id))
        {
            return Ok("Owner already exists.");
        }
        InMemoryData.Owners.Add(owner);
        return CreatedAtAction(nameof(GetOwnerById), new { id = owner.Id }, owner);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateOwner(int id, Owner updatedOwner)
    {
        var owner = InMemoryData.Owners.FirstOrDefault(o => o.Id == id);
        if (owner == null)
        {
            return NotFound();
        }
        owner.Name = updatedOwner.Name;
        owner.Email = updatedOwner.Email;
        owner.PhoneNumber = updatedOwner.PhoneNumber;
        owner.Address = updatedOwner.Address;
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteOwner(int id)
    {
        var owner = InMemoryData.Owners.FirstOrDefault(o => o.Id == id);
        if (owner == null)
        {
            return NotFound();
        }
        InMemoryData.Owners.Remove(owner);
        return NoContent();
    }
}
