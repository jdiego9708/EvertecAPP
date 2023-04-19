using SISEvertec.Entities.Models;

namespace SISEvertec.APP.Services.Interfaces
{
    public interface IConfiguration_evertecService
    {
        Task<List<Configuration_evertec>> GetAll();
        Task<Configuration_evertec> GetById(int id);
        Task Add(Configuration_evertec item);
        Task Update(Configuration_evertec item);
        Task Delete(int id);
    }
}
