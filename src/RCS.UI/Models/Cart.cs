using RCS.Data.Entities;

namespace RCS.UI.Models
{
    public class Cart
    {

        public List<CartLine> Lines { get; set; } = new List<CartLine>();

        public virtual void AddItem(Course course, int quantity)
        {
            CartLine line = Lines
                .Where(c => c.CourseId.Id == course.Id)
                .FirstOrDefault();

            if (line == null)
            {
                Lines.Add(new CartLine
                {
                    CourseId = course,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public virtual void RemoveLine(Course course) =>
            Lines.RemoveAll(l => l.CourseId.Id == course.Id);

        public decimal ComputeTotalValue() =>
            Lines.Sum(e => e.CourseId.Price * e.Quantity);

        public virtual void Clear() => Lines.Clear();
    }
}
