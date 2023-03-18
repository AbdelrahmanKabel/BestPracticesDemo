using BestPracticesDemo.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BestPracticesDemo.Persistence.Configurations
{
    public class EventConfiguration
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
