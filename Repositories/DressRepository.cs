using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class DressRepository : IDressRepository
    {
        private readonly EventDressRentalContext _eventDressRentalContext;
        public DressRepository(EventDressRentalContext eventDressRentalContext)
        {
            _eventDressRentalContext = eventDressRentalContext;
        }
        public async Task<Dress> GetDressById(int id)
        {
            return await _eventDressRentalContext.Dresses.FirstOrDefaultAsync(o => o.Id == id);
        }
        public async Task<int> GetCountByIdAndSize(int id, string size)
        {
            return await _eventDressRentalContext.Dresses.Where(m => m.Id == id && m.Size == size).CountAsync();
        }
        public async Task<Dress> addDress(Dress dress)
        {
            await _eventDressRentalContext.Dresses.AddAsync(dress);
            await _eventDressRentalContext.SaveChangesAsync();
            return dress;
        }
    }
}
