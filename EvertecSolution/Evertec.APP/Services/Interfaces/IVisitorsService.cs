using SISEvertec.Entities.Models;

namespace SISEvertec.APP.Services.Interfaces
{
    public interface IVisitorsService
    {
        Task<List<Visitors>> GetAll();
        Task<Visitors> GetById(int id);
        Task Add(Visitors item);
        Task Update(Visitors item);
        Task Delete(int id);
    }
}
