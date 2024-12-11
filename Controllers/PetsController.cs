using Microsoft.AspNetCore.Mvc;
using PetClinicAPI.Data;
using PetClinicAPI.Models;

namespace PetClinicAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PetsController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Pet>> GetAllPets()
    {
        return Ok(InMemoryData.Pets);
    }

    [HttpGet("{id}")]
    public ActionResult<Pet> GetPetById(int id)
    {
        var pet = InMemoryData.Pets.FirstOrDefault(p => p.Id == id);
        if (pet == null)
        {
            return NotFound();
        }
        return Ok(pet);
    }

    [HttpPost]
    public ActionResult<Pet> AddPet([FromBody] Pet pet)
    {
        if (InMemoryData.Owners.Any(o => o.Id == pet.Id))
        {
            return Ok("Pet already exists.");
        }
        if (!InMemoryData.Owners.Any(owner => owner.Id == pet.OwnerId))
        {
            return NotFound("Owner not found.");
        }
        InMemoryData.Pets.Add(pet);
        return CreatedAtAction(nameof(AddPet), new { id = pet.Id }, pet);
    }

    [HttpPut("{id}")]
    public IActionResult UpdatePet(int id, Pet updatedPet)
    {
        var pet = InMemoryData.Pets.FirstOrDefault(p => p.Id == id);
        if (pet == null)
        {
            return NotFound();
        }
        if (!InMemoryData.Owners.Any(owner => owner.Id == updatedPet.OwnerId))
        {
            return NotFound("Owner not found.");
        }
        pet.Name = updatedPet.Name;
        pet.Species = updatedPet.Species;
        pet.Breed = updatedPet.Breed;
        pet.Age = updatedPet.Age;
        pet.Gender = updatedPet.Gender;
        pet.OwnerId = updatedPet.OwnerId;
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeletePet(int id)
    {
        var pet = InMemoryData.Pets.FirstOrDefault(p => p.Id == id);
        if (pet == null)
        {
            return NotFound();
        }
        InMemoryData.Pets.Remove(pet);
        return NoContent();
    }
}
