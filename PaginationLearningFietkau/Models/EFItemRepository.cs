using Microsoft.EntityFrameworkCore;

namespace PaginationLearningFietkau.Models
{
    public class EFItemRepository : IItemRepository
    {
        private ItemCtx _context;

        public EFItemRepository(ItemCtx context)
        {
            _context = context;
        }

        public IQueryable<Perishable> Perishables => _context.Perishables;

        public void Add<T>(T entity) where T : class
        {
            _context.Set<T>().Add(entity);
        }

        public void Remove<T>(T entity) where T : class
        {
            _context.Set<T>().Remove(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            // Get the DbSet for the entity type
            var dbSet = _context.Set<T>();

            // Get the current entry for the entity
            var entry = _context.Entry(entity);

            // Check if the entity is detached (not being tracked by the context)
            if (entry.State == EntityState.Detached)
            {
                // Get the primary key properties for the entity type
                var key = _context.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties
                             .Select(p => p.Name)
                             .ToArray();

                // Extract the values of the primary key properties from the entity
                var keyValues = key.Select(k => entry.Property(k).CurrentValue).ToArray();

                // Find the existing entity in the database using the primary key values
                var attachedEntity = dbSet.Find(keyValues);

                if (attachedEntity != null)
                {
                    // If the entity is found in the database, update its values with the new entity's values
                    _context.Entry(attachedEntity).CurrentValues.SetValues(entity);
                }
                else
                {
                    // If the entity is not found, attach it and mark it as modified
                    dbSet.Attach(entity);
                    //entry.State = EntityState.Modified;
                }
            }
            else
            {
                // If the entity is already being tracked, simply mark it as modified
                entry.State = EntityState.Modified;
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
