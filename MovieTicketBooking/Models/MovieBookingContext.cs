using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MovieTicketBooking.Models
{
    public partial class MovieBookingContext : DbContext
    {
        public MovieBookingContext()
        {
        }

        public MovieBookingContext(DbContextOptions<MovieBookingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Actor> Actors { get; set; } = null!;
        public virtual DbSet<Booking> Bookings { get; set; } = null!;
        public virtual DbSet<BookingDetail> BookingDetails { get; set; } = null!;
        public virtual DbSet<Genre> Genres { get; set; } = null!;
        public virtual DbSet<Movie> Movies { get; set; } = null!;
        public virtual DbSet<Room> Rooms { get; set; } = null!;
        public virtual DbSet<Seat> Seats { get; set; } = null!;
        public virtual DbSet<Showtime> Showtimes { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var ConnectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("DB");
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor>(entity =>
            {
                entity.Property(e => e.ActorId).HasColumnName("actor_id");

                entity.Property(e => e.BirthDate)
                    .HasColumnType("date")
                    .HasColumnName("birth_date");

                entity.Property(e => e.Gender)
                    .HasMaxLength(50)
                    .HasColumnName("gender");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.Property(e => e.BookingId).HasColumnName("booking_id");

                entity.Property(e => e.BookDate)
                    .HasColumnType("datetime")
                    .HasColumnName("book_date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PaymentMethod).HasColumnName("payment_method");

                entity.Property(e => e.ShowtimeId).HasColumnName("showtime_id");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TotalPrice)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("total_price");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Showtime)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.ShowtimeId)
                    .HasConstraintName("FK__Bookings__showti__3B75D760");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Bookings__user_i__3C69FB99");
            });

            modelBuilder.Entity<BookingDetail>(entity =>
            {
                entity.Property(e => e.BookingDetailId).HasColumnName("booking_detail_id");

                entity.Property(e => e.BookingId).HasColumnName("booking_id");

                entity.Property(e => e.SeatId).HasColumnName("seat_id");

                entity.HasOne(d => d.Booking)
                    .WithMany(p => p.BookingDetails)
                    .HasForeignKey(d => d.BookingId)
                    .HasConstraintName("FK__BookingDe__booki__398D8EEE");

                entity.HasOne(d => d.Seat)
                    .WithMany(p => p.BookingDetails)
                    .HasForeignKey(d => d.SeatId)
                    .HasConstraintName("FK__BookingDe__seat___3A81B327");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.HasIndex(e => e.Name, "UQ__Genres__72E12F1B3B5BAFF5")
                    .IsUnique();

                entity.Property(e => e.GenreId).HasColumnName("genre_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.Property(e => e.MovieId).HasColumnName("movie_id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Director)
                    .HasMaxLength(100)
                    .HasColumnName("director");

                entity.Property(e => e.Duration).HasColumnName("duration");

                entity.Property(e => e.ImgUrl)
                    .HasMaxLength(255)
                    .HasColumnName("img_url");

                entity.Property(e => e.Language)
                    .HasMaxLength(100)
                    .HasColumnName("language");

                entity.Property(e => e.ReleaseDate)
                    .HasColumnType("date")
                    .HasColumnName("release_date");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .HasColumnName("title");

                entity.HasMany(d => d.Actors)
                    .WithMany(p => p.Movies)
                    .UsingEntity<Dictionary<string, object>>(
                        "MovieActor",
                        l => l.HasOne<Actor>().WithMany().HasForeignKey("ActorId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Movie_Actor_Actors"),
                        r => r.HasOne<Movie>().WithMany().HasForeignKey("MovieId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Movie_Actor_Movies"),
                        j =>
                        {
                            j.HasKey("MovieId", "ActorId").HasName("PK__Movie_Ac__DB7FB332309F2B98");

                            j.ToTable("Movie_Actor");

                            j.IndexerProperty<int>("MovieId").HasColumnName("movie_id");

                            j.IndexerProperty<int>("ActorId").HasColumnName("actor_id");
                        });

                entity.HasMany(d => d.Genres)
                    .WithMany(p => p.Movies)
                    .UsingEntity<Dictionary<string, object>>(
                        "MovieGenre",
                        l => l.HasOne<Genre>().WithMany().HasForeignKey("GenreId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Movie_Gen__genre__3D5E1FD2"),
                        r => r.HasOne<Movie>().WithMany().HasForeignKey("MovieId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Movie_Gen__movie__3E52440B"),
                        j =>
                        {
                            j.HasKey("MovieId", "GenreId").HasName("PK__Movie_Ge__B249DF9DEACCADAF");

                            j.ToTable("Movie_Genre");

                            j.IndexerProperty<int>("MovieId").HasColumnName("movie_id");

                            j.IndexerProperty<int>("GenreId").HasColumnName("genre_id");
                        });
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.Property(e => e.RoomId).HasColumnName("room_id");

                entity.Property(e => e.Capacity).HasColumnName("capacity");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Seat>(entity =>
            {
                entity.Property(e => e.SeatId).HasColumnName("seat_id");

                entity.Property(e => e.RoomId).HasColumnName("room_id");

                entity.Property(e => e.SeatNumber)
                    .HasMaxLength(10)
                    .HasColumnName("seat_number");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Seats)
                    .HasForeignKey(d => d.RoomId)
                    .HasConstraintName("FK__Seats__room_id__3F466844");
            });

            modelBuilder.Entity<Showtime>(entity =>
            {
                entity.Property(e => e.ShowtimeId).HasColumnName("showtime_id");

                entity.Property(e => e.MovieId).HasColumnName("movie_id");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("price");

                entity.Property(e => e.RoomId).HasColumnName("room_id");

                entity.Property(e => e.StartTime)
                    .HasColumnType("datetime")
                    .HasColumnName("start_time");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.Showtimes)
                    .HasForeignKey(d => d.MovieId)
                    .HasConstraintName("FK__Showtimes__movie__403A8C7D");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Showtimes)
                    .HasForeignKey(d => d.RoomId)
                    .HasConstraintName("FK__Showtimes__room___412EB0B6");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email, "UQ__Users__AB6E61644496DC48")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Dob)
                    .HasColumnType("date")
                    .HasColumnName("dob");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .HasColumnName("password");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .HasColumnName("phone");

                entity.Property(e => e.Role).HasColumnName("role");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
