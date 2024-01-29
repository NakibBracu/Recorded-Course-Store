using NHibernate;
using NHibernate.Mapping.ByCode.Conformist;
using RCS.Data.Identity.Entities;

namespace RCS.Data.Identity.IdentityMappings
{
    public class ApplicationUserRoleMap : ClassMapping<ApplicationUserRole>
    {
        public ApplicationUserRoleMap()
        {
            Schema("dbo");
            Table("ApplicationUserRoles");
            ComposedId(id =>
            {
                id.Property(e => e.UserId, prop =>
                {
                    prop.Column("UserId");
                    prop.Type(NHibernateUtil.Guid);
                });
                id.Property(e => e.RoleId, prop =>
                {
                    prop.Column("RoleId");
                    prop.Type(NHibernateUtil.Guid);
                });
            });
        }
    }
}
