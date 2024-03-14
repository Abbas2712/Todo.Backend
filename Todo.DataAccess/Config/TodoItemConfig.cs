using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.Models;

namespace Todo.DataAccess.Config
{
    public class TodoItemConfig : IEntityTypeConfiguration<TodoItem>
    {
       public void Configure(EntityTypeBuilder<TodoItem> builder) {

            builder.ToTable("TodoItems");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).UseIdentityColumn();
            builder.Property(t => t.Title).HasMaxLength(80).IsRequired();
            builder.Property(t => t.Description).HasMaxLength(150);
            builder.Property(t => t.IsCompleted).HasDefaultValue(false);
            builder.Property(t => t.IsImportant).HasDefaultValue(false);
            builder.Property(t => t.IsTodayTodo).HasDefaultValue(false);
            builder.Property(t => t.CreatedAt).HasDefaultValue(DateTime.Now);
            builder.Property(t => t.ListId).HasDefaultValue(null);
            builder.Property(t => t.IsTasked).HasDefaultValue(true);

            builder.HasOne(l => l.TodoList) //model name you want to connet to
                .WithMany(i => i.Items) // attribute to which you want to connet to in other models table
                .HasForeignKey(k => k.ListId) // attribute present in the current table 
                .HasConstraintName("FK_TodoItem_TodoList"); // Name of the foreign key
        }
    }
}
