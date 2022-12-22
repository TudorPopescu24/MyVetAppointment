using VetExpert.Domain;

namespace VetExpert.UI.Services.Interfaces
{
	public interface IPetService
	{
		Task InsertPet(Pet pet);
	}
}