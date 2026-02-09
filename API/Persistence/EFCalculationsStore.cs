using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using thangi_calucator;
using thangi_calucator.Persistence;
namespace API.Persistence
{
    public class EFCalculationsStore : ICalculationStore
    {
        private readonly CalculatorDbContext _context;

        public EFCalculationsStore(CalculatorDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<IReadOnlyList<Calculation>> LoadAllAsync()
        {
          var calculations = _context.Calculations.OrderByDescending(c => c.CreatedAt).ToList();
          return calculations;
        }

        public async Task SaveAsync(Calculation calculation)
        {
            _context.Calculations.Add(calculation);
            await _context.SaveChangesAsync();

            
        }
    }
}