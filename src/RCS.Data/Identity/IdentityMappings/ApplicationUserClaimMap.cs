using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using RCS.Data.Identity.Entities;

namespace RCS.Data.Identity.IdentityMappings
{
    public class ApplicationUserClaimMap : ClassMapping<ApplicationUserClaim>
    {
        public ApplicationUserClaimMap()
        {
            Schema("dbo");
            Table("ApplicationUserClaims");
            Id(e => e.Id, id =>
            {
                id.Column("id");
                id.Type(NHibernateUtil.Guid);
                id.Generator(Generators.Guid);
                id.UnsavedValue(Guid.Empty);
            });
            Property(e => e.ClaimType, prop =>
            {
                prop.Column("ClaimType");
                prop.Type(NHibernateUtil.String);
                prop.Length(1024);
                prop.NotNullable(true);
            });
            Property(e => e.ClaimValue, prop =>
            {
                prop.Column("ClaimValue");
                prop.Type(NHibernateUtil.String);
                prop.Length(1024);
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
