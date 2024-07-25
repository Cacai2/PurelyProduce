namespace PaginationLearningFietkau.Models.ViewModels
{
    public class ProduceListViewModel
    {
        public List<Perishable> Perishables { get; set; }
        public PaginationInfo PaginationInfo { get; set; } = new PaginationInfo();
    }
}
