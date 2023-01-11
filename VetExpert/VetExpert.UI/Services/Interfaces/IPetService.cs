using VetExpert.Domain;

namespace VetExpert.UI.Services.Interfaces
{
	public interface IPetService
	{
		Task InsertPet(Pet pet);
		Task<IEnumerable<Pet>> GetAllPets();
		Task<IEnumerable<Pet>> GetClientPets(Guid userId);
		Task UpdatePet(Pet pet);
		Task DeletePet(Guid petId);
	}
}