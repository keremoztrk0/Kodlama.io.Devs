using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq;

namespace Kodlama.io.Devs.Persistence.Configurations
{
    public class UserOperationClaimConfiguration : IEntityTypeConfiguration<UserOperationClaim>
    {
        public void Configure(EntityTypeBuilder<UserOperationClaim> builder)
        {
            builder.ToTable("UserOperationClaims").HasKey(k => k.Id);
            builder.Property(p => p.Id).HasColumnName("Id");
            builder.Property(p => p.UserId).HasColumnName("UserId");
            builder.Property(p => p.OperationClaimId).HasColumnName("OperationClaimId");
            builder.HasOne(p => p.OperationClaim);
            builder.HasOne(p => p.User);
        }
    }
}
