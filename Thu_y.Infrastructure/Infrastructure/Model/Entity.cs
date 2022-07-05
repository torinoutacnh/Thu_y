using System.ComponentModel.DataAnnotations;

namespace Thu_y.Infrastructure.Model
{
    public abstract class Entity
    {
        [Key]
        public string Id { get; set; }

        public int Status { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset DateUpdated { get; set; }
        public DateTimeOffset? DateDeleted { get; set; }

        protected Entity()
        {
            this.Id = Guid.NewGuid().ToString("D");

            this.DateCreated = this.DateUpdated = DateTimeOffset.UtcNow;
        }
    }
}
