namespace Repository.Configuration
{
    using Entities.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class UrlManagementConfiguration : IEntityTypeConfiguration<UrlManagement>
    {
        public void Configure(EntityTypeBuilder<UrlManagement> builder)
        {
            builder.HasData
                (
                new UrlManagement
                {
                    ShortUrl = "HtyU83f",
                    OriginalUrl = "https://code-maze.com/ultimate-aspnetcore-webapi-second-edition/?source=nav"
                },
                new UrlManagement
                {
                    ShortUrl = "YtyB45f",
                    OriginalUrl = "https://stackoverflow.com/questions/tagged/c%23"
                }
                );
        }
    }
}
