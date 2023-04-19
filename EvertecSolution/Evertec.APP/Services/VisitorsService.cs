using Microsoft.EntityFrameworkCore;
using SISEvertec.APP.Services.Interfaces;
using SISEvertec.Entities.Models;

namespace SISEvertec.APP.Services
{
    public class VisitorsService : IVisitorsService
    {
        private readonly EvertecDBContext _context;
        public VisitorsService(EvertecDBContext context)
        {
            _context = context;
        }
        public async Task<List<Visitors>> GetAll()
        {
            var result = await _context.Visitors.ToListAsync();

            if (result == null)
                return new List<Visitors>();

            return result;
        }
        public async Task<Visitors> GetById(int id)
        {
            return await _context.Visitors.FindAsync(id);
        }
        public async Task Add(Visitors item)
        {
            await _context.Visitors.AddAsync(item);
            await _context.SaveChangesAsync();
        }
        public async Task Update(Visitors item)
        {
            var visitor = _context.Visitors.FirstOrDefault(v => v.Id == item.Id);

            if (visitor == null)
                return;

            visitor.Name_visitor = item.Name_visitor;
            visitor.Image_visitor = item.Image_visitor;

            _context.Update(visitor);

            await _context.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            var item = await GetById(id);
            _context.Visitors.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}
