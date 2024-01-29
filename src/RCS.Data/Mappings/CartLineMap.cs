using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using RCS.Data.Entities;

namespace RCS.Data.Mappings
{
    public class CartLineMap : ClassMapping<CartLine>
    {
        public CartLineMap()
        {
            Schema("dbo");
            Table("CartLines");
            Id(x => x.Id, x =>
            {
                x.Generator(Generators.Guid);
                x.Type(NHibernateUtil.Guid);
                x.Column("Id");
                x.UnsavedValue(Guid.Empty);
            });
            ManyToOne(x => x.CourseId, m =>
            {
                m.Column("CourseId"); // Assuming you have a foreign key column named "CourseId" in CartLines table
                m.Cascade(Cascade.Persist);
            });
            Property(x => x.Quantity, m =>
            {
                m.NotNullable(true);
                m.Column("Quantity");
            });
            ManyToOne(x => x.OrderId, m =>
            {
                m.Column("OrderId"); // Assuming you have a foreign key column named "OrderId" in CartLines table
                m.Cascade(Cascade.Persist);
            });
        }
    }


}
