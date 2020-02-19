using Checkout.PaymentGateway.DataModel.Models;
using Checkout.PaymentGateway.Domain;

using Microsoft.EntityFrameworkCore;

namespace Checkout.PaymentGateway.DataModel
{
    /// <summary>
    /// Database context
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public partial class PaymentGatewayContext : DbContext
    {
        /// <summary>
        /// Gets or sets the payment.
        /// </summary>
        /// <value>
        /// The payment.
        /// </value>
        public virtual DbSet<Payment> Payment { get; set; }

        /// <summary>
        /// Gets or sets the payment status.
        /// </summary>
        /// <value>
        /// The payment status.
        /// </value>
        public virtual DbSet<PaymentStatus> PaymentStatus { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentGatewayContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public PaymentGatewayContext(DbContextOptions<PaymentGatewayContext> options) : base(options)
        {
        }

        /// <summary>
        /// Override this method to further configure the model that was discovered by convention from the entity types
        /// exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived context. The resulting model may be cached
        /// and re-used for subsequent instances of your derived context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically
        /// define extension methods on this object that allow you to configure aspects of the model that are specific
        /// to a given database.</param>
        /// <remarks>
        /// If a model is explicitly set on the options for this context (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
        /// then this method will not be run.
        /// </remarks>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PaymentStatus>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne(e => e.PaymnentStatusCodeNavigation)
                            .WithMany(e => e.Payment)
                            .HasPrincipalKey(e => e.Id)
                            .HasForeignKey(e => e.CodeStatus)
                            .OnDelete(DeleteBehavior.ClientSetNull);
            });

            //seed data
            modelBuilder.Entity<PaymentStatus>()
                .HasData(new PaymentStatus
                {
                    Id = 1,
                    Code = (int)PaymentStatusTypes.Successful,
                    Name = "Successful"
                });

            modelBuilder.Entity<PaymentStatus>()
                .HasData(new PaymentStatus
                {
                    Id = 2,
                    Code = (int)PaymentStatusTypes.Unsuccessful,
                    Name = "Unsuccessful"
                });

            modelBuilder.Entity<PaymentStatus>()
               .HasData(new PaymentStatus
               {
                   Id = 3,
                   Code = (int)PaymentStatusTypes.Pending,
                   Name = "Pending"
               });
        }
    }
}