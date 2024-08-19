using ApplicationLayer;
using EnterpriseLayer;
using InterfaceAdapters_Data;
using InterfaceAdpaters_Models;
using Microsoft.EntityFrameworkCore;

namespace InterfaceAdapter_Repository
{
    public class Repository : IRepository<Beer>
    {
        private readonly AppDbContext _dbContext;

        public Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Beer beer)
        {
            var beerModel = new BeerModel()
            {
                Name = beer.Name,
                Style = beer.Style,
                Alcohol = beer.Alcohol,
            };
            await _dbContext.Beers.AddAsync(beerModel);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Beer>> GetAllAsync()
        {
            return await _dbContext.Beers
                .Select( b => new Beer { 
                    Id = b.Id,
                    Name = b.Name,
                    Style = b.Style,
                    Alcohol = b.Alcohol,
                })
                .ToListAsync();
        }

        public async Task<Beer> GetByIdAsync(int id)
        {
            var beerModel = await _dbContext.Beers.FindAsync(id);
            return new Beer
            {
                Id = beerModel.Id,
                Name = beerModel.Name,
                Style = beerModel.Style,
                Alcohol = beerModel.Alcohol,
            };
        }
    }
}
