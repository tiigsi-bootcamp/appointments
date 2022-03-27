using Microsoft.EntityFrameworkCore;

using Models;

namespace Data;

public class AppointmentsDbContext : DbContext
{
	public AppointmentsDbContext(DbContextOptions<AppointmentsDbContext> options)
		: base(options)
	{
	}
	
	 protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
    }

	public DbSet<User> Users { get; set; }

	public DbSet<Doctor> Doctors { get; set; }

	public DbSet<Schedule> Schedules { get; set; }

	public DbSet<TimeSlot> TimeSlots { get; set; }

	public DbSet<Booking> Bookings { get; set; }

	public DbSet<BookingNote> Notes { get; set; }

	public DbSet<Review> Reviews { get; set; }
}
