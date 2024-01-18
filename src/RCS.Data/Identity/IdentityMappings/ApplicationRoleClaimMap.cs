using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using RCS.Data.Identity.Entities;

namespace RCS.Data.Identity.IdentityMappings
{
    public class ApplicationRoleClaimMap : ClassMapping<ApplicationRoleClaim>
    {
        public ApplicationRoleClaimMap()
        {
            Schema("dbo");
            Table("ApplicationRoleClaims");
            Id(e => e.Id, id =>
            {
                id.Column("Id");
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
            Property(e => e.RoleId, prop =>
            {
                prop.Column("RoleId");
                prop.Type(NHibernateUtil.Guid);
                prop.NotNullable(true);
            });
        }
    }
}
