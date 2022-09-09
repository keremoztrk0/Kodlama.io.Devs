using Kodlama.io.Devs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Persistence.Configurations
{
    public class DeveloperConfiguration : IEntityTypeConfiguration<Developer>
    {
        public void Configure(EntityTypeBuilder<Developer> builder)
        {

            builder.ToTable("Developers");

            builder.HasMany(p => p.GitHubProfiles);

        }
    }
    public class GitHubProfileConfiguration : IEntityTypeConfiguration<GitHubProfile>
    {
        public void Configure(EntityTypeBuilder<GitHubProfile> builder)
        {
            builder.ToTable("GitHubProfiles").HasKey(k => k.Id);

            builder.Property(p => p.Id).HasColumnName("Id");
            builder.Property(p => p.DeveloperId).HasColumnName("DeveloperId");
            builder.Property(p => p.ProfileUrl).HasColumnName("ProfileUrl");

            builder.HasOne(p => p.Developer);
        }
    }
}
