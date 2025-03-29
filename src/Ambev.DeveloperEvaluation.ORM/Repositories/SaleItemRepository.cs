using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    /// <summary>
    /// Implementation of ISaleItemRepository using Entity Framework Core
    /// </summary>
    public class SaleItemRepository : ISaleItemRepository
    {
        private readonly DefaultContext _context;

        /// <summary>
        /// Initializes a new instance of SaleItemRepository
        /// </summary>
        /// <param name="context">The database context</param>
        public SaleItemRepository(DefaultContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new sale item in the database
        /// </summary>
        /// <param name="saleItem">The sale item to create</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The created sale item</returns>
        public async Task<SaleItem> CreateAsync(SaleItem saleItem, CancellationToken cancellationToken = default)
        {
            await _context.SaleItems.AddAsync(saleItem, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return saleItem;
        }

        /// <summary>
        /// Retrieves a sale item by its unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the sale item</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The sale item if found, null otherwise</returns>
        public async Task<SaleItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.SaleItems.FirstOrDefaultAsync(item => item.Id == id, cancellationToken);
        }

        /// <summary>
        /// Retrieves all sale items from the database
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>A list of all sale items</returns>
        public async Task<IEnumerable<SaleItem>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaleItems.ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Retrieves all items of a specific sale
        /// </summary>
        /// <param name="saleId">The unique identifier of the sale</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>A list of sale items for the given sale</returns>
        public async Task<IEnumerable<SaleItem>> GetBySaleIdAsync(Guid saleId, CancellationToken cancellationToken = default)
        {
            return await _context.SaleItems
                .Where(item => item.SaleId == saleId)
                .ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Updates an existing sale item in the database
        /// </summary>
        /// <param name="saleItem">The sale item to update</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>True if the sale item was updated, false if not found</returns>
        public async Task<bool> UpdateAsync(SaleItem saleItem, CancellationToken cancellationToken = default)
        {
            var existingSaleItem = await GetByIdAsync(saleItem.Id, cancellationToken);
            if (existingSaleItem == null)
                return false;

            _context.Entry(existingSaleItem).CurrentValues.SetValues(saleItem);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        /// <summary>
        /// Deletes a sale item from the database
        /// </summary>
        /// <param name="id">The unique identifier of the sale item to delete</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>True if the sale item was deleted, false if not found</returns>
        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var saleItem = await GetByIdAsync(id, cancellationToken);
            if (saleItem == null)
                return false;

            _context.SaleItems.Remove(saleItem);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
