using Microsoft.AspNetCore.Identity;

namespace RCS.Data.Identity.Entities
{
    public class ApplicationUserToken : IdentityUserToken<Guid>
    {
        protected bool Equals(ApplicationUserToken other) =>
            UserId == other.UserId && LoginProvider == other.LoginProvider && Name == other.Name;

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (obj.GetType() != GetType())
                return false;

            return Equals((ApplicationUserToken)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = 0;
                hashCode = UserId.GetHashCode();
                hashCode = hashCode * 397 ^ LoginProvider.GetHashCode();
                hashCode = hashCode * 397 ^ Name.GetHashCode();
                return hashCode;
            }
        }
    }
}
