using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using RCS.Data.Entities;

namespace RCS.Data.Mappings
{
    public class OrderMap : ClassMapping<Order>
    {
        public OrderMap()
        {
            Schema("dbo");
            Table("Orders");
            Id(x => x.Id, x =>
            {
                x.Generator(Generators.Guid);
                x.Type(NHibernateUtil.Guid);
                x.Column("Id");
                x.UnsavedValue(Guid.Empty);
            });
            Property(x => x.Name, m =>
            {
                m.Length(256);
                m.Type(NHibernateUtil.StringClob);
                m.NotNullable(true);
                m.Column("Name");
            });
            Property(x => x.Line1, m =>
            {
                m.Length(500);
                m.Type(NHibernateUtil.StringClob);
                m.NotNullable(true);
                m.Column("Line1");
            });
            Property(x => x.Line2, m =>
            {
                m.Length(500);
                m.Type(NHibernateUtil.StringClob);
                m.NotNullable(false);
                m.Column("Line2");
            });
            Property(x => x.Line3, m =>
            {
                m.Length(500);
                m.Type(NHibernateUtil.StringClob);
                m.NotNullable(false);
                m.Column("Line3");
            });
            Property(x => x.City, m =>
            {
                m.Length(100);
                m.Type(NHibernateUtil.StringClob);
                m.NotNullable(true);
                m.Column("City");
            });
            Property(x => x.State, m =>
            {
                m.Length(100);
                m.Type(NHibernateUtil.StringClob);
                m.NotNullable(true);
                m.Column("State");
            });
            Property(x => x.Zip, m =>
            {
                m.Length(20);
                m.Type(NHibernateUtil.StringClob);
                m.NotNullable(true);
                m.Column("Zip");
            });
            Property(x => x.Country, m =>
            {
                m.Length(100);
                m.Type(NHibernateUtil.StringClob);
                m.NotNullable(true);
                m.Column("Country");
            });
            ManyToOne(x => x.User, m =>
            {
                m.Column("UserId"); 
                m.Cascade(Cascade.Persist);
            });

            Bag(x => x.Lines, colmap => { }, map => map.OneToMany());
        }
    }

}
