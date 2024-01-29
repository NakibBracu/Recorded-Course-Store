using RCS.Data.Enums;

namespace RCS.Data.Entities
{
    public class Course : IEntity<Guid>
    {
        public virtual Guid Id { get; set; }
        public virtual string Title { get; set; }

        public virtual string? Description { get; set; }
        public virtual string? ThumbnailImageName { get; set; }
        public virtual decimal Price { get; set; }

        public virtual DifficultyLevel DifficultyLevel { get; set; }

    }
}
