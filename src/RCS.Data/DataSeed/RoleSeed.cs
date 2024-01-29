using RCS.Data.Identity.Entities;

namespace RCS.Data.DataSeed
{
    public static class RoleSeed
    {
        public static ApplicationRole[] GetRoles
        {
            get
            {
                return new ApplicationRole[]
                {
                new ApplicationRole
                {
                    Id = Guid.Parse("24A79338-30A4-4827-94C9-BB84B615D4FE"),
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                }
                };
            }
        }
    }
}
