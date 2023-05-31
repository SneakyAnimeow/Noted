using Microsoft.EntityFrameworkCore;

namespace Noted.Data;

/// <summary>
/// Database context generated from the database schema using EF Core
/// </summary>
public partial class NotedContext : DbContext {
    /// <summary>
    /// Default constructor
    /// </summary>
    public NotedContext() { }


    /// <summary>
    /// Constructor with options
    /// </summary>
    /// <param name="options"> The options to use for this context </param>
    public NotedContext(DbContextOptions<NotedContext> options)
        : base(options) { }

    /// <summary>
    /// Categories table
    /// </summary>
    public virtual DbSet<Category> Categories { get; set; } = null!;

    /// <summary>
    /// Notes table
    /// </summary>
    public virtual DbSet<Note> Notes { get; set; } = null!;

    /// <summary>
    /// Tokens table
    /// </summary>
    public virtual DbSet<Token> Tokens { get; set; } = null!;

    /// <summary>
    /// Users table
    /// </summary>
    public virtual DbSet<User> Users { get; set; } = null!;

    // Unused OnConfiguring method, due to the use of dependency injection, but kept for reference
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //     => optionsBuilder.UseSqlServer("");

    /// <summary>
    /// Configure the database schema
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Category>(entity => {
            entity.ToTable("Category");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(32)
                .HasColumnName("name");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Categories)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Category_User");
        });

        modelBuilder.Entity<Note>(entity => {
            entity.ToTable("Note");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Content)
                .HasMaxLength(2048)
                .HasColumnName("content");
            entity.Property(e => e.Name)
                .HasMaxLength(32)
                .HasColumnName("name");

            entity.HasOne(d => d.Category).WithMany(p => p.Notes)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Note_Category");
        });

        modelBuilder.Entity<Token>(entity => {
            entity.ToTable("Token");

            entity.HasIndex(e => e.Token1, "IX_Token").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ExpireDate)
                .HasColumnType("smalldatetime")
                .HasColumnName("expire_date");
            entity.Property(e => e.Token1)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("token");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Tokens)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Token_User");
        });

        modelBuilder.Entity<User>(entity => {
            entity.ToTable("User");

            entity.HasIndex(e => e.Name, "IX_User").IsUnique();

            entity.HasIndex(e => e.Email, "IX_User_1").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreationDate)
                .HasColumnType("smalldatetime")
                .HasColumnName("creation_date");
            entity.Property(e => e.Email)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Hash)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("hash");
            entity.Property(e => e.Name)
                .HasMaxLength(32)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    /// <summary>
    /// Partial method for configuring the database schema
    /// </summary>
    /// <param name="modelBuilder"> The model builder to use </param>
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}