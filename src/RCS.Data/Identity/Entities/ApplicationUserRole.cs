using Microsoft.AspNetCore.Identity;

namespace RCS.Data.Identity.Entities
{
    public class ApplicationUserRole : IdentityUserRole<Guid>
    {
        protected bool Equals(ApplicationUserRole other) =>
            RoleId == other.RoleId && UserId == other.UserId;

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (obj.GetType() != GetType())
                return false;

            return Equals((ApplicationUserRole)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = 0;
                hashCode = RoleId.GetHashCode();
                hashCode = hashCode * 397 ^ UserId.GetHashCode();
                return hashCode;
            }
        }
    }
}
