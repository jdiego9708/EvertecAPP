using Microsoft.EntityFrameworkCore;
using SISEvertec.APP.Services.Interfaces;
using SISEvertec.Entities.Models;

namespace SISEvertec.APP.Services
{
    public class Configuration_evertecService : IConfiguration_evertecService
    {
        private readonly EvertecDBContext _context;
        public Configuration_evertecService(EvertecDBContext context)
        {
            _context = context;
        }
        public async Task<List<Configuration_evertec>> GetAll()
        {
            return await _context.Configurations_evertec.ToListAsync();
        }
        public async Task<Configuration_evertec> GetById(int id)
        {
            return await _context.Configurations_evertec.FindAsync(id);
        }
        public async Task Add(Configuration_evertec item)
        {
            await _context.Configurations_evertec.AddAsync(item);
            await _context.SaveChangesAsync();
        }
        public async Task Update(Configuration_evertec item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            var item = await GetById(id);
            _context.Configurations_evertec.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}
