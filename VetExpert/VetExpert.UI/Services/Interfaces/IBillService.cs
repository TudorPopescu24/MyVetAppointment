using VetExpert.Domain;

namespace VetExpert.UI.Services.Interfaces;

public interface IBillService
{
    Task InsertBill(Bill bill);
    Task UpdateBill(Bill bill);
    Task<IEnumerable<Bill>> GetAllBills();
    Task<IEnumerable<Bill>> GetClinicBills(Guid clinicId);
    Task DeleteBill(Guid billId);
}