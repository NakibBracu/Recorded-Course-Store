using NHibernate;
using NHibernate.Mapping.ByCode.Conformist;
using RCS.Data.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCS.Data.Identity.IdentityMappings
{
    public class ApplicationUserTokenMap : ClassMapping<ApplicationUserToken>
    {
        public ApplicationUserTokenMap()
        {
            Schema("dbo");
            Table("ApplicationUserTokens");
            ComposedId(id =>
            {
                id.Property(e => e.UserId, prop =>
                {
                    prop.Column("UserId");
                    prop.Type(NHibernateUtil.Guid);
                });
                id.Property(e => e.LoginProvider, prop =>
                {
                    prop.Column("LoginProvider");
                    prop.Type(NHibernateUtil.String);
                    prop.Length(32);
                });
                id.Property(e => e.Name, prop =>
                {
                    prop.Column("Name");
                    prop.Type(NHibernateUtil.String);
                    prop.Length(32);
                });
            });
            Property(e => e.Value, prop =>
            {
                prop.Column("Value");
                prop.Type(NHibernateUtil.String);
                prop.Length(256);
                prop.NotNullable(true);
            });
        }
    }
}
