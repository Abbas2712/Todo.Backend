using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.Models;

namespace Todo.DataAccess.Config
{
    internal class TodoListConfig: IEntityTypeConfiguration<TodoList>
    {
        public void Configure(EntityTypeBuilder<TodoList> builder)
        {
            builder.HasKey(l=> l.Id);
            builder.Property(l=> l.Id).UseIdentityColumn();
            builder.Property(l => l.ListName).IsRequired();
        }
    }
}
