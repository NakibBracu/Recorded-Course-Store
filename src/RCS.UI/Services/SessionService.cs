using Newtonsoft.Json;
using RCS.Data.Entities;
namespace RCS.UI.Services
{
    public class SessionService : ISessionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public T Get<T>(string key)
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var value = session.GetString(key);

            if (value != null)
            {
                return JsonConvert.DeserializeObject<T>(value);
            }

            return default(T);
        }

        public void Set<T>(string key, T value)
        {
            var session = _httpContextAccessor.HttpContext.Session;
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public List<CartLine> CartLines
        {
            get
            {
                return Get<List<CartLine>>("Cart") ?? new List<CartLine>();
            }
            set
            {
                Set("Cart", value);
            }
        }
    }
}
