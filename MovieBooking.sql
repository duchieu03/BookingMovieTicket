USE [master]
GO
/****** Object:  Database [MovieBooking]    Script Date: 7/6/2024 5:03:09 PM ******/
CREATE DATABASE [MovieBooking]
GO
ALTER DATABASE [MovieBooking] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MovieBooking].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MovieBooking] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MovieBooking] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MovieBooking] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MovieBooking] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MovieBooking] SET ARITHABORT OFF 
GO
ALTER DATABASE [MovieBooking] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MovieBooking] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MovieBooking] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MovieBooking] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MovieBooking] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MovieBooking] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MovieBooking] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MovieBooking] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MovieBooking] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MovieBooking] SET  ENABLE_BROKER 
GO
ALTER DATABASE [MovieBooking] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MovieBooking] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MovieBooking] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MovieBooking] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MovieBooking] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MovieBooking] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MovieBooking] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MovieBooking] SET RECOVERY FULL 
GO
ALTER DATABASE [MovieBooking] SET  MULTI_USER 
GO
ALTER DATABASE [MovieBooking] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MovieBooking] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MovieBooking] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MovieBooking] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MovieBooking] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MovieBooking] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'MovieBooking', N'ON'
GO
ALTER DATABASE [MovieBooking] SET QUERY_STORE = OFF
GO
USE [MovieBooking]
GO
/****** Object:  Table [dbo].[Actors]    Script Date: 7/6/2024 5:03:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Actors](
	[actor_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NOT NULL,
	[birth_date] [date] NULL,
	[gender] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[actor_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BookingDetails]    Script Date: 7/6/2024 5:03:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookingDetails](
	[booking_detail_id] [int] IDENTITY(1,1) NOT NULL,
	[booking_id] [int] NULL,
	[seat_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[booking_detail_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bookings]    Script Date: 7/6/2024 5:03:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bookings](
	[booking_id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NULL,
	[showtime_id] [int] NULL,
	[book_date] [datetime] NULL,
	[total_price] [decimal](10, 2) NULL,
	[status] [int] NULL,
	[payment_method] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[booking_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Genres]    Script Date: 7/6/2024 5:03:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Genres](
	[genre_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[genre_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Movie_Actor]    Script Date: 7/6/2024 5:03:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Movie_Actor](
	[movie_id] [int] NOT NULL,
	[actor_id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[movie_id] ASC,
	[actor_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Movie_Genre]    Script Date: 7/6/2024 5:03:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Movie_Genre](
	[movie_id] [int] NOT NULL,
	[genre_id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[movie_id] ASC,
	[genre_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Movies]    Script Date: 7/6/2024 5:03:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Movies](
	[movie_id] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](100) NOT NULL,
	[description] [nvarchar](max) NULL,
	[duration] [int] NULL,
	[release_date] [date] NULL,
	[director] [nvarchar](100) NULL,
	[language] [nvarchar](100) NULL,
	[status] [int] NULL,
	[img_url] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[movie_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rooms]    Script Date: 7/6/2024 5:03:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rooms](
	[room_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NULL,
	[capacity] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[room_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Seats]    Script Date: 7/6/2024 5:03:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Seats](
	[seat_id] [int] IDENTITY(1,1) NOT NULL,
	[room_id] [int] NULL,
	[seat_number] [nvarchar](10) NULL,
	[seat_status] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[seat_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Showtimes]    Script Date: 7/6/2024 5:03:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Showtimes](
	[showtime_id] [int] IDENTITY(1,1) NOT NULL,
	[movie_id] [int] NULL,
	[room_id] [int] NULL,
	[start_time] [datetime] NULL,
	[price] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[showtime_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 7/6/2024 5:03:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[email] [nvarchar](100) NOT NULL,
	[password] [nvarchar](255) NOT NULL,
	[name] [nvarchar](100) NULL,
	[phone] [nvarchar](20) NULL,
	[role] [int] NOT NULL,
	[status] [int] NULL,
	[dob] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Actors] ON 

INSERT [dbo].[Actors] ([actor_id], [name], [birth_date], [gender]) VALUES (1, N'Tom Cruise', CAST(N'1962-07-03' AS Date), N'Male')
INSERT [dbo].[Actors] ([actor_id], [name], [birth_date], [gender]) VALUES (2, N'Scarlett Johansson', CAST(N'1984-11-22' AS Date), N'Female')
INSERT [dbo].[Actors] ([actor_id], [name], [birth_date], [gender]) VALUES (3, N'Leonardo DiCaprio', CAST(N'1974-11-11' AS Date), N'Male')
INSERT [dbo].[Actors] ([actor_id], [name], [birth_date], [gender]) VALUES (4, N'Jennifer Lawrence', CAST(N'1990-08-15' AS Date), N'Female')
INSERT [dbo].[Actors] ([actor_id], [name], [birth_date], [gender]) VALUES (5, N'Robert Downey Jr.', CAST(N'1965-04-04' AS Date), N'Male')
INSERT [dbo].[Actors] ([actor_id], [name], [birth_date], [gender]) VALUES (6, N'Emma Stone', CAST(N'1988-11-06' AS Date), N'Female')
INSERT [dbo].[Actors] ([actor_id], [name], [birth_date], [gender]) VALUES (7, N'Chris Hemsworth', CAST(N'1983-08-11' AS Date), N'Male')
SET IDENTITY_INSERT [dbo].[Actors] OFF
GO
SET IDENTITY_INSERT [dbo].[BookingDetails] ON 

INSERT [dbo].[BookingDetails] ([booking_detail_id], [booking_id], [seat_id]) VALUES (1, 1, 1)
INSERT [dbo].[BookingDetails] ([booking_detail_id], [booking_id], [seat_id]) VALUES (2, 1, 2)
INSERT [dbo].[BookingDetails] ([booking_detail_id], [booking_id], [seat_id]) VALUES (3, 2, 3)
INSERT [dbo].[BookingDetails] ([booking_detail_id], [booking_id], [seat_id]) VALUES (4, 2, 4)
SET IDENTITY_INSERT [dbo].[BookingDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[Bookings] ON 

INSERT [dbo].[Bookings] ([booking_id], [user_id], [showtime_id], [book_date], [total_price], [status], [payment_method]) VALUES (1, 1, 1, CAST(N'2024-06-27T14:05:15.083' AS DateTime), CAST(10.00 AS Decimal(10, 2)), 1, 1)
INSERT [dbo].[Bookings] ([booking_id], [user_id], [showtime_id], [book_date], [total_price], [status], [payment_method]) VALUES (2, 2, 2, CAST(N'2024-06-27T14:05:15.083' AS DateTime), CAST(24.00 AS Decimal(10, 2)), 1, 2)
SET IDENTITY_INSERT [dbo].[Bookings] OFF
GO
SET IDENTITY_INSERT [dbo].[Genres] ON 

INSERT [dbo].[Genres] ([genre_id], [name]) VALUES (1, N'Action')
INSERT [dbo].[Genres] ([genre_id], [name]) VALUES (2, N'Comedy')
INSERT [dbo].[Genres] ([genre_id], [name]) VALUES (3, N'Drama')
INSERT [dbo].[Genres] ([genre_id], [name]) VALUES (4, N'Horror')
INSERT [dbo].[Genres] ([genre_id], [name]) VALUES (5, N'Romance')
INSERT [dbo].[Genres] ([genre_id], [name]) VALUES (6, N'Sci-Fi')
INSERT [dbo].[Genres] ([genre_id], [name]) VALUES (7, N'Thriller')
SET IDENTITY_INSERT [dbo].[Genres] OFF
GO
INSERT [dbo].[Movie_Actor] ([movie_id], [actor_id]) VALUES (1, 1)
INSERT [dbo].[Movie_Actor] ([movie_id], [actor_id]) VALUES (2, 2)
INSERT [dbo].[Movie_Actor] ([movie_id], [actor_id]) VALUES (2, 5)
INSERT [dbo].[Movie_Actor] ([movie_id], [actor_id]) VALUES (3, 3)
INSERT [dbo].[Movie_Actor] ([movie_id], [actor_id]) VALUES (4, 4)
INSERT [dbo].[Movie_Actor] ([movie_id], [actor_id]) VALUES (5, 5)
INSERT [dbo].[Movie_Actor] ([movie_id], [actor_id]) VALUES (6, 6)
INSERT [dbo].[Movie_Actor] ([movie_id], [actor_id]) VALUES (7, 7)
GO
INSERT [dbo].[Movie_Genre] ([movie_id], [genre_id]) VALUES (1, 1)
INSERT [dbo].[Movie_Genre] ([movie_id], [genre_id]) VALUES (2, 1)
INSERT [dbo].[Movie_Genre] ([movie_id], [genre_id]) VALUES (2, 6)
INSERT [dbo].[Movie_Genre] ([movie_id], [genre_id]) VALUES (3, 1)
INSERT [dbo].[Movie_Genre] ([movie_id], [genre_id]) VALUES (3, 6)
INSERT [dbo].[Movie_Genre] ([movie_id], [genre_id]) VALUES (4, 3)
INSERT [dbo].[Movie_Genre] ([movie_id], [genre_id]) VALUES (5, 1)
INSERT [dbo].[Movie_Genre] ([movie_id], [genre_id]) VALUES (5, 6)
INSERT [dbo].[Movie_Genre] ([movie_id], [genre_id]) VALUES (6, 2)
INSERT [dbo].[Movie_Genre] ([movie_id], [genre_id]) VALUES (6, 5)
INSERT [dbo].[Movie_Genre] ([movie_id], [genre_id]) VALUES (7, 1)
GO
SET IDENTITY_INSERT [dbo].[Movies] ON 

INSERT [dbo].[Movies] ([movie_id], [title], [description], [duration], [release_date], [director], [language], [status], [img_url]) VALUES (1, N'Top Gun', N'A movie about fighter pilots', 110, CAST(N'1986-05-16' AS Date), N'Tony Scott', N'English', 1, N'top_gun.jpg')
INSERT [dbo].[Movies] ([movie_id], [title], [description], [duration], [release_date], [director], [language], [status], [img_url]) VALUES (2, N'The Avengers', N'Superheroes saving the world', 143, CAST(N'2012-05-04' AS Date), N'Joss Whedon', N'English', 1, N'the_avengers.jpg')
INSERT [dbo].[Movies] ([movie_id], [title], [description], [duration], [release_date], [director], [language], [status], [img_url]) VALUES (3, N'Inception', N'A thief who steals corporate secrets through dream-sharing technology', 148, CAST(N'2010-07-16' AS Date), N'Christopher Nolan', N'English', 1, N'inception.jpg')
INSERT [dbo].[Movies] ([movie_id], [title], [description], [duration], [release_date], [director], [language], [status], [img_url]) VALUES (4, N'The Hunger Games', N'A girl fights for survival in a dystopian future', 142, CAST(N'2024-06-06' AS Date), N'Gary Ross', N'English', 1, N'the_hunger_games.jpg')
INSERT [dbo].[Movies] ([movie_id], [title], [description], [duration], [release_date], [director], [language], [status], [img_url]) VALUES (5, N'Iron Man', N'A billionaire creates a suit of armor to fight crime', 126, CAST(N'2024-06-06' AS Date), N'Jon Favreau', N'English', 1, N'iron_man.jpg')
INSERT [dbo].[Movies] ([movie_id], [title], [description], [duration], [release_date], [director], [language], [status], [img_url]) VALUES (6, N'La La Land', N'A musician and an actress fall in love', 128, CAST(N'2024-05-09' AS Date), N'Damien Chazelle', N'English', 1, N'la_la_land.jpg')
INSERT [dbo].[Movies] ([movie_id], [title], [description], [duration], [release_date], [director], [language], [status], [img_url]) VALUES (7, N'Thor', N'A prince from another realm comes to Earth', 115, CAST(N'2024-05-08' AS Date), N'Kenneth Branagh', N'English', 1, N'thor.jpg')
INSERT [dbo].[Movies] ([movie_id], [title], [description], [duration], [release_date], [director], [language], [status], [img_url]) VALUES (8, N'Thor', N'A prince from another realm comes to Earth', 115, CAST(N'2024-05-07' AS Date), N'Kenneth Branagh', N'English', 1, N'thor.jpg')
INSERT [dbo].[Movies] ([movie_id], [title], [description], [duration], [release_date], [director], [language], [status], [img_url]) VALUES (9, N'Thor', N'A prince from another realm comes to Earth', 115, CAST(N'2024-05-06' AS Date), N'Kenneth Branagh', N'English', 1, N'thor.jpg')
INSERT [dbo].[Movies] ([movie_id], [title], [description], [duration], [release_date], [director], [language], [status], [img_url]) VALUES (10, N'La La Land', N'A musician and an actress fall in love', 128, CAST(N'2024-07-09' AS Date), N'Damien Chazelle', N'English', 1, N'la_la_land.jpg')
INSERT [dbo].[Movies] ([movie_id], [title], [description], [duration], [release_date], [director], [language], [status], [img_url]) VALUES (11, N'La La Land', N'A musician and an actress fall in love', 128, CAST(N'2024-07-08' AS Date), N'Damien Chazelle', N'English', 1, N'la_la_land.jpg')
INSERT [dbo].[Movies] ([movie_id], [title], [description], [duration], [release_date], [director], [language], [status], [img_url]) VALUES (12, N'La La Land', N'A musician and an actress fall in love', 128, CAST(N'2024-07-07' AS Date), N'Damien Chazelle', N'English', 1, N'la_la_land.jpg')
SET IDENTITY_INSERT [dbo].[Movies] OFF
GO
SET IDENTITY_INSERT [dbo].[Rooms] ON 

INSERT [dbo].[Rooms] ([room_id], [name], [capacity]) VALUES (1, N'Room 1', 100)
INSERT [dbo].[Rooms] ([room_id], [name], [capacity]) VALUES (2, N'Room 2', 80)
INSERT [dbo].[Rooms] ([room_id], [name], [capacity]) VALUES (3, N'Room 3', 120)
SET IDENTITY_INSERT [dbo].[Rooms] OFF
GO
SET IDENTITY_INSERT [dbo].[Seats] ON 

INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (1, 1, N'A1', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (2, 1, N'A2', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (3, 1, N'A3', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (4, 1, N'A4', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (5, 1, N'A5', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (6, 1, N'A6', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (7, 1, N'A7', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (8, 1, N'A8', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (9, 1, N'A9', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (10, 1, N'A10', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (11, 1, N'B1', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (12, 1, N'B2', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (13, 1, N'B3', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (14, 1, N'B4', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (15, 1, N'B5', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (16, 1, N'B6', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (17, 1, N'B7', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (18, 1, N'B8', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (19, 1, N'B9', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (20, 1, N'B10', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (21, 1, N'C1', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (22, 1, N'C2', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (23, 1, N'C3', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (24, 1, N'C4', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (25, 1, N'C5', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (26, 1, N'C6', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (27, 1, N'C7', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (28, 1, N'C8', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (29, 1, N'C9', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (30, 1, N'C10', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (31, 1, N'D1', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (32, 1, N'D2', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (33, 1, N'D3', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (34, 1, N'D4', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (35, 1, N'D5', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (36, 1, N'D6', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (37, 1, N'D7', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (38, 1, N'D8', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (39, 1, N'D9', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (40, 1, N'D10', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (41, 1, N'E1', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (42, 1, N'E2', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (43, 1, N'E3', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (44, 1, N'E4', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (45, 1, N'E5', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (46, 1, N'E6', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (47, 1, N'E7', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (48, 1, N'E8', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (49, 1, N'E9', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (50, 1, N'E10', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (51, 1, N'F1', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (52, 1, N'F2', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (53, 1, N'F3', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (54, 1, N'F4', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (55, 1, N'F5', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (56, 1, N'F6', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (57, 1, N'F7', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (58, 1, N'F8', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (59, 1, N'F9', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (60, 1, N'F10', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (61, 1, N'G1', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (62, 1, N'G2', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (63, 1, N'G3', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (64, 1, N'G4', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (65, 1, N'G5', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (66, 1, N'G6', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (67, 1, N'G7', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (68, 1, N'G8', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (69, 1, N'G9', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (70, 1, N'G10', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (71, 1, N'H1', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (72, 1, N'H2', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (73, 1, N'H3', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (74, 1, N'H4', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (75, 1, N'H5', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (76, 1, N'H6', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (77, 1, N'H7', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (78, 1, N'H8', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (79, 1, N'H9', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (80, 1, N'H10', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (81, 1, N'I1', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (82, 1, N'I2', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (83, 1, N'I3', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (84, 1, N'I4', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (85, 1, N'I5', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (86, 1, N'I6', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (87, 1, N'I7', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (88, 1, N'I8', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (89, 1, N'I9', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (90, 1, N'I10', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (91, 1, N'J1', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (92, 1, N'J2', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (93, 1, N'J3', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (94, 1, N'J4', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (95, 1, N'J5', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (96, 1, N'J6', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (97, 1, N'J7', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (98, 1, N'J8', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (99, 1, N'J9', 1)
GO
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (100, 1, N'J10', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (101, 2, N'A1', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (102, 2, N'A2', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (103, 2, N'A3', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (104, 2, N'A4', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (105, 2, N'A5', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (106, 2, N'A6', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (107, 2, N'A7', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (108, 2, N'A8', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (109, 2, N'A9', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (110, 2, N'A10', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (111, 2, N'B1', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (112, 2, N'B2', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (113, 2, N'B3', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (114, 2, N'B4', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (115, 2, N'B5', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (116, 2, N'B6', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (117, 2, N'B7', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (118, 2, N'B8', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (119, 2, N'B9', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (120, 2, N'B10', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (121, 2, N'C1', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (122, 2, N'C2', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (123, 2, N'C3', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (124, 2, N'C4', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (125, 2, N'C5', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (126, 2, N'C6', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (127, 2, N'C7', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (128, 2, N'C8', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (129, 2, N'C9', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (130, 2, N'C10', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (131, 2, N'D1', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (132, 2, N'D2', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (133, 2, N'D3', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (134, 2, N'D4', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (135, 2, N'D5', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (136, 2, N'D6', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (137, 2, N'D7', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (138, 2, N'D8', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (139, 2, N'D9', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (140, 2, N'D10', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (141, 2, N'E1', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (142, 2, N'E2', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (143, 2, N'E3', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (144, 2, N'E4', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (145, 2, N'E5', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (146, 2, N'E6', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (147, 2, N'E7', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (148, 2, N'E8', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (149, 2, N'E9', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (150, 2, N'E10', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (151, 2, N'F1', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (152, 2, N'F2', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (153, 2, N'F3', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (154, 2, N'F4', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (155, 2, N'F5', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (156, 2, N'F6', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (157, 2, N'F7', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (158, 2, N'F8', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (159, 2, N'F9', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (160, 2, N'F10', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (161, 2, N'G1', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (162, 2, N'G2', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (163, 2, N'G3', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (164, 2, N'G4', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (165, 2, N'G5', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (166, 2, N'G6', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (167, 2, N'G7', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (168, 2, N'G8', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (169, 2, N'G9', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (170, 2, N'G10', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (171, 2, N'H1', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (172, 2, N'H2', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (173, 2, N'H3', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (174, 2, N'H4', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (175, 2, N'H5', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (176, 2, N'H6', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (177, 2, N'H7', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (178, 2, N'H8', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (179, 2, N'H9', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (180, 2, N'H10', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (181, 2, N'I1', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (182, 2, N'I2', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (183, 2, N'I3', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (184, 2, N'I4', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (185, 2, N'I5', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (186, 2, N'I6', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (187, 2, N'I7', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (188, 2, N'I8', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (189, 2, N'I9', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (190, 2, N'I10', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (191, 2, N'J1', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (192, 2, N'J2', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (193, 2, N'J3', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (194, 2, N'J4', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (195, 2, N'J5', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (196, 2, N'J6', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (197, 2, N'J7', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (198, 2, N'J8', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (199, 2, N'J9', 1)
GO
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (200, 2, N'J10', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (201, 3, N'A1', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (202, 3, N'A2', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (203, 3, N'A3', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (204, 3, N'A4', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (205, 3, N'A5', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (206, 3, N'A6', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (207, 3, N'A7', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (208, 3, N'A8', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (209, 3, N'A9', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (210, 3, N'A10', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (211, 3, N'B1', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (212, 3, N'B2', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (213, 3, N'B3', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (214, 3, N'B4', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (215, 3, N'B5', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (216, 3, N'B6', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (217, 3, N'B7', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (218, 3, N'B8', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (219, 3, N'B9', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (220, 3, N'B10', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (221, 3, N'C1', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (222, 3, N'C2', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (223, 3, N'C3', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (224, 3, N'C4', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (225, 3, N'C5', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (226, 3, N'C6', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (227, 3, N'C7', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (228, 3, N'C8', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (229, 3, N'C9', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (230, 3, N'C10', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (231, 3, N'D1', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (232, 3, N'D2', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (233, 3, N'D3', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (234, 3, N'D4', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (235, 3, N'D5', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (236, 3, N'D6', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (237, 3, N'D7', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (238, 3, N'D8', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (239, 3, N'D9', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (240, 3, N'D10', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (241, 3, N'E1', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (242, 3, N'E2', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (243, 3, N'E3', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (244, 3, N'E4', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (245, 3, N'E5', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (246, 3, N'E6', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (247, 3, N'E7', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (248, 3, N'E8', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (249, 3, N'E9', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (250, 3, N'E10', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (251, 3, N'F1', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (252, 3, N'F2', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (253, 3, N'F3', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (254, 3, N'F4', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (255, 3, N'F5', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (256, 3, N'F6', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (257, 3, N'F7', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (258, 3, N'F8', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (259, 3, N'F9', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (260, 3, N'F10', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (261, 3, N'G1', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (262, 3, N'G2', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (263, 3, N'G3', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (264, 3, N'G4', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (265, 3, N'G5', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (266, 3, N'G6', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (267, 3, N'G7', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (268, 3, N'G8', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (269, 3, N'G9', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (270, 3, N'G10', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (271, 3, N'H1', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (272, 3, N'H2', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (273, 3, N'H3', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (274, 3, N'H4', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (275, 3, N'H5', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (276, 3, N'H6', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (277, 3, N'H7', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (278, 3, N'H8', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (279, 3, N'H9', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (280, 3, N'H10', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (281, 3, N'I1', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (282, 3, N'I2', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (283, 3, N'I3', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (284, 3, N'I4', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (285, 3, N'I5', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (286, 3, N'I6', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (287, 3, N'I7', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (288, 3, N'I8', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (289, 3, N'I9', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (290, 3, N'I10', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (291, 3, N'J1', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (292, 3, N'J2', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (293, 3, N'J3', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (294, 3, N'J4', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (295, 3, N'J5', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (296, 3, N'J6', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (297, 3, N'J7', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (298, 3, N'J8', 1)
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (299, 3, N'J9', 1)
GO
INSERT [dbo].[Seats] ([seat_id], [room_id], [seat_number], [seat_status]) VALUES (300, 3, N'J10', 1)
SET IDENTITY_INSERT [dbo].[Seats] OFF
GO
SET IDENTITY_INSERT [dbo].[Showtimes] ON 

INSERT [dbo].[Showtimes] ([showtime_id], [movie_id], [room_id], [start_time], [price]) VALUES (1, 1, 1, CAST(N'2024-07-01T14:00:00.000' AS DateTime), CAST(10.00 AS Decimal(10, 2)))
INSERT [dbo].[Showtimes] ([showtime_id], [movie_id], [room_id], [start_time], [price]) VALUES (2, 2, 2, CAST(N'2024-07-01T16:00:00.000' AS DateTime), CAST(12.00 AS Decimal(10, 2)))
INSERT [dbo].[Showtimes] ([showtime_id], [movie_id], [room_id], [start_time], [price]) VALUES (3, 3, 3, CAST(N'2024-07-01T18:00:00.000' AS DateTime), CAST(15.00 AS Decimal(10, 2)))
INSERT [dbo].[Showtimes] ([showtime_id], [movie_id], [room_id], [start_time], [price]) VALUES (4, 4, 1, CAST(N'2024-07-02T14:00:00.000' AS DateTime), CAST(10.00 AS Decimal(10, 2)))
INSERT [dbo].[Showtimes] ([showtime_id], [movie_id], [room_id], [start_time], [price]) VALUES (5, 5, 2, CAST(N'2024-07-02T16:00:00.000' AS DateTime), CAST(12.00 AS Decimal(10, 2)))
INSERT [dbo].[Showtimes] ([showtime_id], [movie_id], [room_id], [start_time], [price]) VALUES (6, 6, 3, CAST(N'2024-07-02T18:00:00.000' AS DateTime), CAST(15.00 AS Decimal(10, 2)))
INSERT [dbo].[Showtimes] ([showtime_id], [movie_id], [room_id], [start_time], [price]) VALUES (7, 7, 1, CAST(N'2024-07-03T14:00:00.000' AS DateTime), CAST(10.00 AS Decimal(10, 2)))
SET IDENTITY_INSERT [dbo].[Showtimes] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([user_id], [email], [password], [name], [phone], [role], [status], [dob]) VALUES (1, N'staff@gmail.com', N'AQAAAAEAACcQAAAAEE62rCz4NvZntNK4TV7C33OijjP1RNMjyjY8BdeHcnUopmZHN4rHgBxWXDozFVC86Q==', N'Staff', N'111', 2, 1, CAST(N'1985-06-15' AS Date))
INSERT [dbo].[Users] ([user_id], [email], [password], [name], [phone], [role], [status], [dob]) VALUES (2, N'user@gmail.com', N'AQAAAAEAACcQAAAAEE62rCz4NvZntNK4TV7C33OijjP1RNMjyjY8BdeHcnUopmZHN4rHgBxWXDozFVC86Q==', N'User', N'0987654321', 1, 1, CAST(N'1990-09-21' AS Date))
INSERT [dbo].[Users] ([user_id], [email], [password], [name], [phone], [role], [status], [dob]) VALUES (3, N'admin@gmail.com', N'AQAAAAEAACcQAAAAENPjrWamNVQcOP6urJxiBEuo2AabP+MvXQmpkJq0bfu/m7pJb4gQfC6E/Ng26xSFMw==', N'Admin', N'1122334455', 3, 1, CAST(N'1980-01-01' AS Date))
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Genres__72E12F1B9DB8B64F]    Script Date: 7/6/2024 5:03:09 PM ******/
ALTER TABLE [dbo].[Genres] ADD UNIQUE NONCLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Users__AB6E6164B7F05DC1]    Script Date: 7/6/2024 5:03:09 PM ******/
ALTER TABLE [dbo].[Users] ADD UNIQUE NONCLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Bookings] ADD  DEFAULT (getdate()) FOR [book_date]
GO
ALTER TABLE [dbo].[Bookings] ADD  DEFAULT ((0)) FOR [status]
GO
ALTER TABLE [dbo].[Movies] ADD  DEFAULT ((1)) FOR [status]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((1)) FOR [status]
GO
ALTER TABLE [dbo].[BookingDetails]  WITH CHECK ADD FOREIGN KEY([booking_id])
REFERENCES [dbo].[Bookings] ([booking_id])
GO
ALTER TABLE [dbo].[BookingDetails]  WITH CHECK ADD FOREIGN KEY([seat_id])
REFERENCES [dbo].[Seats] ([seat_id])
GO
ALTER TABLE [dbo].[Bookings]  WITH CHECK ADD FOREIGN KEY([showtime_id])
REFERENCES [dbo].[Showtimes] ([showtime_id])
GO
ALTER TABLE [dbo].[Bookings]  WITH CHECK ADD FOREIGN KEY([user_id])
REFERENCES [dbo].[Users] ([user_id])
GO
ALTER TABLE [dbo].[Movie_Actor]  WITH CHECK ADD  CONSTRAINT [FK_Movie_Actor_Actors] FOREIGN KEY([actor_id])
REFERENCES [dbo].[Actors] ([actor_id])
GO
ALTER TABLE [dbo].[Movie_Actor] CHECK CONSTRAINT [FK_Movie_Actor_Actors]
GO
ALTER TABLE [dbo].[Movie_Actor]  WITH CHECK ADD  CONSTRAINT [FK_Movie_Actor_Movies] FOREIGN KEY([movie_id])
REFERENCES [dbo].[Movies] ([movie_id])
GO
ALTER TABLE [dbo].[Movie_Actor] CHECK CONSTRAINT [FK_Movie_Actor_Movies]
GO
ALTER TABLE [dbo].[Movie_Genre]  WITH CHECK ADD FOREIGN KEY([genre_id])
REFERENCES [dbo].[Genres] ([genre_id])
GO
ALTER TABLE [dbo].[Movie_Genre]  WITH CHECK ADD FOREIGN KEY([movie_id])
REFERENCES [dbo].[Movies] ([movie_id])
GO
ALTER TABLE [dbo].[Seats]  WITH CHECK ADD FOREIGN KEY([room_id])
REFERENCES [dbo].[Rooms] ([room_id])
GO
ALTER TABLE [dbo].[Showtimes]  WITH CHECK ADD FOREIGN KEY([movie_id])
REFERENCES [dbo].[Movies] ([movie_id])
GO
ALTER TABLE [dbo].[Showtimes]  WITH CHECK ADD FOREIGN KEY([room_id])
REFERENCES [dbo].[Rooms] ([room_id])
GO
USE [master]
GO
ALTER DATABASE [MovieBooking] SET  READ_WRITE 
GO
