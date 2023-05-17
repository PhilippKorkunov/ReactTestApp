using DataLayer;
using DataLayer.Entities;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BusinessLayer.Implementations
{
    public class EFRepository<T> : IAsyncDisposable
        where T : Entity
    {
        private protected readonly Context context;

        public EFRepository(Context context)
        {
            this.context = context;
        }


        public async Task<IQueryable<T>?> GetAsync(int takeNumber = 0, Expression<Func<T, bool>>? predicate = null)
        {
            if (takeNumber == 0) 
            {
                return predicate is null ? (await context.Set<T>().ToListAsync()).AsQueryable() :
                   (await context.Set<T>().Where(predicate).ToListAsync()).AsQueryable();
            }
            
            return predicate is null ? (await context.Set<T>().Take(takeNumber).ToListAsync()).AsQueryable() :
                    (await context.Set<T>().Where(predicate).Take(takeNumber).ToListAsync()).AsQueryable();
        }

        public async Task InsertAsync(T entry)
        {
            if (entry is null) { throw new ArgumentNullException(nameof(entry)); }
            await context.AddAsync(entry);
            await context.SaveChangesAsync();
        }

        public async Task InsertRangeAsync(params T[] entries) //EF Core + BULK SQL
        {
            if (entries is null) { throw new ArgumentNullException(nameof(entries)); }
            using (var transaction = context.Database.BeginTransaction())
            {
                context.BulkInsert(entries);
                await transaction.CommitAsync();
            }
        }



        public async ValueTask DisposeAsync()
        {
            await context.DisposeAsync();
            GC.SuppressFinalize(this);
        }
    }
}
