using RCS.Data.Entities;

namespace RCS.UI.Services
{
    public interface ISessionService
    {
        T Get<T>(string key);
        void Set<T>(string key, T value);

        List<CartLine> CartLines { get; set; }
    }
}
