namespace PaginationLearningFietkau.Models
{
    public interface IItemRepository
    {
        IQueryable<Perishable> Perishables { get; }

        void Add<T>(T entity) where T : class;

        void Remove<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void SaveChanges();
    }
}
