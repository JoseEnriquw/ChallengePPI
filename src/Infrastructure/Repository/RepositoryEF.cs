using Core.Common.Interfaces;
using Core.Common.Models;
using Core.Domain.Dto;
using Core.Domain.Entities;
using Core.UseCase.V1.OrderOperations.Queries.GetAll;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Infrastructure.Repository
{
    public class RepositoryEF : IRepositoryEF
    {
        private readonly ILogger<RepositoryEF> _logger;
        private readonly ApplicationDbContext _dbContext;
        private readonly IPaginationService _pagination;
        public RepositoryEF(ILogger<RepositoryEF> logger, ApplicationDbContext dbContext,IPaginationService pagination)
        {
            _logger = logger;
            _dbContext = dbContext;
            _pagination = pagination;

        }

        public void Insert<T>(T entity) where T : class
        {
            try
            {
                _dbContext.Add(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inserting entity {entity}", entity);
                throw;
            }
        }

        public void Update<T>(T entity) where T : class
        {
            try
            {
                _dbContext.Attach(entity).State = EntityState.Modified;
                _dbContext.Update(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating entity {entity}", entity);
                throw;
            }
        }

        public void Delete<T>(T entity) where T : class
        {
            try
            {
                _dbContext.Remove(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting entity {entity}", entity);
                throw;
            }
        }

        public async Task<List<T>> GetAllAsync<T>() where T : class
        {
            try
            {
                return await _dbContext.Set<T>().ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all ");
                throw;
            }
        }

        public async Task SaveChangesAsync()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving changes");
                throw;
            }
        }

        public async Task<T> FindAsync<T>(Expression<Func<T, bool>> func) where T : class
        {
            try
            {
                return await _dbContext.Set<T>().FirstAsync(func);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error finding entity ");
                throw;
            }
        }      
        


        public async Task<PaginatedList<OrderDto>> GetOrdersByFiltersAsync(GetOrdersByFilters filter)
        {
            var query = _dbContext.Orders
            .AsQueryable();

            if (filter.Operation.HasValue)
            {
                query = query.Where(o => o.Operation == filter.Operation.GetValueOrDefault());
            }

            if (filter.State!=null)
            {
                
                query = query.Where(o => o.Status == filter.State);
            }

            var ordersDto= query.Select(x => new OrderDto
            {
                Id = x.Id,
                AccountId = x.AccountId,
                AssetId = x.AssetId,
                Operation = x.Operation,
                Price = x.Price,
                Quantity = x.Quantity,
                AssetName = x.AssetName,
                Status = x.Status,
                TotalAmount = x.TotalAmount
            });

            return await _pagination.CreateAsync(ordersDto, filter.Page, filter.Size);
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _dbContext.Orders.
                Include(x=> x.Asset).
                ThenInclude(x=> x.AssetType).
                FirstOrDefaultAsync(o => o.Id == id);
        }
    }
}
