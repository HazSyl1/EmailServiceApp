using OnionEmailApp.Domain.Entities;
using OnionEmailApp.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnionEmailApp.Infrastructure.Data;

namespace OnionEmailApp.Infrastructure.Repositories
{
    public class EmailRepository : IRepository<Email>
    {
        private readonly AppDbContext _context;

        public EmailRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Email>> GetAllAsync()
        {
            return await _context.Emails.ToListAsync();
        }

        public async Task AddAsync(Email entity)
        {
            await _context.Emails.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
    }
}