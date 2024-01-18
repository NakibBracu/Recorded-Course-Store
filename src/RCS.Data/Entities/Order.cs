using RCS.Data.Identity.Entities;

namespace RCS.Data.Entities
{
    public class Order : IEntity<Guid>
    {
        public virtual Guid Id { get; set; }
        public virtual ICollection<CartLine> Lines { get; set; }
        public virtual string Name { get; set; }
        public virtual string Line1 { get; set; }
        public virtual string Line2 { get; set; }
        public virtual string Line3 { get; set; }
        public virtual string City { get; set; }
        public virtual string State { get; set; }
        public virtual string Zip { get; set; }
        public virtual string Country { get; set; }

        public virtual ApplicationUser User { get; set; }

    }
}
