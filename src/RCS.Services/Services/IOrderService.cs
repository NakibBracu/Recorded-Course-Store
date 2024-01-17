namespace RCS.Services.Services
{
    public interface IOrderService
    {
        Task AddOrderAsync(string name, string line1,string line2,
            string line3,string city,string state,string zip,string country,IList<Guid> CourseIDs);
    }
}
