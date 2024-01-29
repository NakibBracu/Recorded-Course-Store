namespace RCS.Data.Entities
{
    public class CartLine : IEntity<Guid>
    {
        public virtual Guid Id { get; set; }
        public virtual Course CourseId { get; set; }
        public virtual int Quantity { get; set; }
        public virtual Order OrderId { get; set; }
    }
}
