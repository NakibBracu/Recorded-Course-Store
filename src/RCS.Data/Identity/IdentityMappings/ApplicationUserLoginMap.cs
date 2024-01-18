using NHibernate;
using NHibernate.Mapping.ByCode.Conformist;
using RCS.Data.Identity.Entities;

namespace RCS.Data.Identity.IdentityMappings
{
    public class ApplicationUserLoginMap : ClassMapping<ApplicationUserLogin>
    {
        public ApplicationUserLoginMap()
        {
            Schema("dbo");
            Table("ApplicationUserLogins");
            ComposedId(id =>
            {
                id.Property(e => e.LoginProvider, prop =>
                {
                    prop.Column("LoginProvider");
                    prop.Type(NHibernateUtil.String);
                    prop.Length(32);
                });
                id.Property(e => e.ProviderKey, prop =>
                {
                    prop.Column("ProviderKey");
                    prop.Type(NHibernateUtil.String);
                    prop.Length(32);
                });
            });
            Property(e => e.ProviderDisplayName, prop =>
            {
                prop.Column("ProviderDisplayName");
                prop.Type(NHibernateUtil.String);
                prop.Length(32);
                prop.NotNullable(true);
            });
            Property(e => e.UserId, prop =>
            {
                prop.Column("UserId");
                prop.Type(NHibernateUtil.Guid);
                prop.NotNullable(true);
            });
        }
    }
}
