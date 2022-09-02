using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PermissionManagerAPI.Models;

namespace PermissionManagerAPI.Configurations
{
    public class PermissionTypeConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.PermissionType);
        }
    }
}
