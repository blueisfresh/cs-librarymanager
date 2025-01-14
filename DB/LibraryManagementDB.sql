USE [master]
GO
/****** Object:  Database [LibraryManagementDB]    Script Date: 08.11.2024 08:09:05 ******/
CREATE DATABASE [LibraryManagementDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'LibraryManagementDB', FILENAME = N'C:\Users\elias\LibraryManagementDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'LibraryManagementDB_log', FILENAME = N'C:\Users\elias\LibraryManagementDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [LibraryManagementDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [LibraryManagementDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [LibraryManagementDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [LibraryManagementDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [LibraryManagementDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [LibraryManagementDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [LibraryManagementDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [LibraryManagementDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [LibraryManagementDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [LibraryManagementDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [LibraryManagementDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [LibraryManagementDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [LibraryManagementDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [LibraryManagementDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [LibraryManagementDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [LibraryManagementDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [LibraryManagementDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [LibraryManagementDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [LibraryManagementDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [LibraryManagementDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [LibraryManagementDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [LibraryManagementDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [LibraryManagementDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [LibraryManagementDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [LibraryManagementDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [LibraryManagementDB] SET  MULTI_USER 
GO
ALTER DATABASE [LibraryManagementDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [LibraryManagementDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [LibraryManagementDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [LibraryManagementDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [LibraryManagementDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [LibraryManagementDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [LibraryManagementDB] SET QUERY_STORE = OFF
GO
USE [LibraryManagementDB]
GO
/****** Object:  Table [dbo].[TblBook]    Script Date: 08.11.2024 08:09:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblBook](
	[BookNum] [char](10) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[Author] [nvarchar](100) NOT NULL,
	[Publisher] [nvarchar](100) NOT NULL,
	[ISBN] [nchar](13) NOT NULL,
	[PublicationPlace] [nvarchar](50) NOT NULL,
	[PublicationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_TblBook] PRIMARY KEY CLUSTERED 
(
	[BookNum] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblBorrow]    Script Date: 08.11.2024 08:09:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblBorrow](
	[BorrowID] [int] IDENTITY(1,1) NOT NULL,
	[StudentLibraryCardNum] [int] NOT NULL,
	[BookBookNum] [char](10) NOT NULL,
	[BorrowDate] [datetime] NOT NULL,
	[ReturnDate] [datetime] NULL,
	[DueDate] [datetime] NOT NULL,
 CONSTRAINT [PK_TblBorrow] PRIMARY KEY CLUSTERED 
(
	[BorrowID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblStudent]    Script Date: 08.11.2024 08:09:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblStudent](
	[LibraryCardNum] [int] NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_TblStudent] PRIMARY KEY CLUSTERED 
(
	[LibraryCardNum] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[TblBook] ([BookNum], [Title], [Author], [Publisher], [ISBN], [PublicationPlace], [PublicationDate]) VALUES (N'00001-2024', N'Der alte Mann und das Meer', N'Ernest Hemingway', N'Rowohlt Verlag', N'9783499551399', N'Hamburg', CAST(N'1952-09-01T00:00:00.000' AS DateTime))
INSERT [dbo].[TblBook] ([BookNum], [Title], [Author], [Publisher], [ISBN], [PublicationPlace], [PublicationDate]) VALUES (N'00002-2024', N'1984', N'George Orwell', N'Ullstein Verlag', N'9783548234106', N'Berlin', CAST(N'1949-06-08T00:00:00.000' AS DateTime))
INSERT [dbo].[TblBook] ([BookNum], [Title], [Author], [Publisher], [ISBN], [PublicationPlace], [PublicationDate]) VALUES (N'00003-2024', N'Stolz und Vorurteil', N'Jane Austen', N'Insel Verlag', N'9783458361528', N'Frankfurt', CAST(N'1813-01-28T00:00:00.000' AS DateTime))
INSERT [dbo].[TblBook] ([BookNum], [Title], [Author], [Publisher], [ISBN], [PublicationPlace], [PublicationDate]) VALUES (N'00004-2024', N'Krieg und Frieden', N'Leo Tolstoi', N'Hanser Verlag', N'9783446234208', N'München', CAST(N'1869-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[TblBook] ([BookNum], [Title], [Author], [Publisher], [ISBN], [PublicationPlace], [PublicationDate]) VALUES (N'00005-2024', N'Die Verwandlung', N'Franz Kafka', N'Fischer Verlag', N'9783100590115', N'Frankfurt', CAST(N'1915-10-01T00:00:00.000' AS DateTime))
INSERT [dbo].[TblBook] ([BookNum], [Title], [Author], [Publisher], [ISBN], [PublicationPlace], [PublicationDate]) VALUES (N'00006-2024', N'Faust', N'Johann Wolfgang von Goethe', N'Reclam Verlag', N'9783150000000', N'Stuttgart', CAST(N'1808-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[TblBook] ([BookNum], [Title], [Author], [Publisher], [ISBN], [PublicationPlace], [PublicationDate]) VALUES (N'00007-2024', N'Don Quijote', N'Miguel de Cervantes', N'Suhrkamp Verlag', N'9783518458508', N'Berlin', CAST(N'1753-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[TblBook] ([BookNum], [Title], [Author], [Publisher], [ISBN], [PublicationPlace], [PublicationDate]) VALUES (N'00008-2024', N'Die Brüder Karamasow', N'Fjodor Dostojewski', N'Diogenes Verlag', N'9783257069286', N'Zürich', CAST(N'1880-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[TblBook] ([BookNum], [Title], [Author], [Publisher], [ISBN], [PublicationPlace], [PublicationDate]) VALUES (N'00009-2024', N'Moby Dick', N'Herman Melville', N'Anaconda Verlag', N'9783730600006', N'Köln', CAST(N'1851-10-18T00:00:00.000' AS DateTime))
INSERT [dbo].[TblBook] ([BookNum], [Title], [Author], [Publisher], [ISBN], [PublicationPlace], [PublicationDate]) VALUES (N'00010-2024', N'Anna Karenina', N'Leo Tolstoi', N'Ullstein Verlag', N'9783548287782', N'Berlin', CAST(N'1878-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[TblBook] ([BookNum], [Title], [Author], [Publisher], [ISBN], [PublicationPlace], [PublicationDate]) VALUES (N'00011-2024', N'Der große Gatsby', N'F. Scott Fitzgerald', N'dtv', N'9783423130039', N'München', CAST(N'1925-04-10T00:00:00.000' AS DateTime))
INSERT [dbo].[TblBook] ([BookNum], [Title], [Author], [Publisher], [ISBN], [PublicationPlace], [PublicationDate]) VALUES (N'00012-2024', N'Schuld und Sühne', N'Fjodor Dostojewski', N'Aufbau Verlag', N'9783351025002', N'Berlin', CAST(N'1866-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[TblBook] ([BookNum], [Title], [Author], [Publisher], [ISBN], [PublicationPlace], [PublicationDate]) VALUES (N'00013-2024', N'Der Prozess', N'Franz Kafka', N'Fischer Verlag', N'9783100590016', N'Frankfurt', CAST(N'1925-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[TblBook] ([BookNum], [Title], [Author], [Publisher], [ISBN], [PublicationPlace], [PublicationDate]) VALUES (N'00014-2024', N'Ulysses', N'James Joyce', N'Suhrkamp Verlag', N'9783518228347', N'Frankfurt', CAST(N'1922-02-02T00:00:00.000' AS DateTime))
INSERT [dbo].[TblBook] ([BookNum], [Title], [Author], [Publisher], [ISBN], [PublicationPlace], [PublicationDate]) VALUES (N'00015-2024', N'Die Odyssee', N'Homer', N'Reclam Verlag', N'9783150000642', N'Stuttgart', CAST(N'2000-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[TblBook] ([BookNum], [Title], [Author], [Publisher], [ISBN], [PublicationPlace], [PublicationDate]) VALUES (N'00016-2024', N'Madame Bovary', N'Gustave Flaubert', N'Diogenes Verlag', N'9783257235803', N'Zürich', CAST(N'1857-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[TblBook] ([BookNum], [Title], [Author], [Publisher], [ISBN], [PublicationPlace], [PublicationDate]) VALUES (N'00017-2024', N'Jane Eyre', N'Charlotte Brontë', N'Reclam Verlag', N'9783150198523', N'Stuttgart', CAST(N'1847-10-16T00:00:00.000' AS DateTime))
INSERT [dbo].[TblBook] ([BookNum], [Title], [Author], [Publisher], [ISBN], [PublicationPlace], [PublicationDate]) VALUES (N'00018-2024', N'Das Bildnis des Dorian Gray', N'Oscar Wilde', N'Anaconda Verlag', N'9783730600013', N'Köln', CAST(N'1890-07-01T00:00:00.000' AS DateTime))
INSERT [dbo].[TblBook] ([BookNum], [Title], [Author], [Publisher], [ISBN], [PublicationPlace], [PublicationDate]) VALUES (N'00019-2024', N'Der Fänger im Roggen', N'J.D. Salinger', N'Knaur Verlag', N'9783426784550', N'München', CAST(N'1951-07-16T00:00:00.000' AS DateTime))
INSERT [dbo].[TblBook] ([BookNum], [Title], [Author], [Publisher], [ISBN], [PublicationPlace], [PublicationDate]) VALUES (N'00020-2024', N'Wuthering Heights', N'Emily Brontë', N'Fischer Verlag', N'9783596211393', N'Frankfurt', CAST(N'1847-12-01T00:00:00.000' AS DateTime))
INSERT [dbo].[TblBook] ([BookNum], [Title], [Author], [Publisher], [ISBN], [PublicationPlace], [PublicationDate]) VALUES (N'00026-2024', N'The Silent Patient', N'Alex Michaelides', N'Celadon Books', N'9781250301697', N'New York', CAST(N'2019-02-05T00:00:00.000' AS DateTime))
INSERT [dbo].[TblBook] ([BookNum], [Title], [Author], [Publisher], [ISBN], [PublicationPlace], [PublicationDate]) VALUES (N'00027-2024', N'Where the Crawdads Sing', N'Delia Owens', N'G.P. Putnam''s Sons', N'9780735219090', N'New York', CAST(N'2018-08-14T00:00:00.000' AS DateTime))
INSERT [dbo].[TblBook] ([BookNum], [Title], [Author], [Publisher], [ISBN], [PublicationPlace], [PublicationDate]) VALUES (N'00028-2024', N'Becoming', N'Michelle Obama', N'Crown Publishing Group', N'9781524763138', N'New York', CAST(N'2018-11-13T00:00:00.000' AS DateTime))
INSERT [dbo].[TblBook] ([BookNum], [Title], [Author], [Publisher], [ISBN], [PublicationPlace], [PublicationDate]) VALUES (N'00029-2024', N'Educated', N'Tara Westover', N'Random House', N'9780399590504', N'New York', CAST(N'2018-02-20T00:00:00.000' AS DateTime))
INSERT [dbo].[TblBook] ([BookNum], [Title], [Author], [Publisher], [ISBN], [PublicationPlace], [PublicationDate]) VALUES (N'00030-2024', N'The Testaments', N'Margaret Atwood', N'Nan A. Talese', N'9780385543781', N'Toronto', CAST(N'2019-09-10T00:00:00.000' AS DateTime))
INSERT [dbo].[TblBook] ([BookNum], [Title], [Author], [Publisher], [ISBN], [PublicationPlace], [PublicationDate]) VALUES (N'00031-2024', N'Circe', N'Madeline Miller', N'Little, Brown and Company', N'9780316556347', N'New York', CAST(N'2018-04-10T00:00:00.000' AS DateTime))
INSERT [dbo].[TblBook] ([BookNum], [Title], [Author], [Publisher], [ISBN], [PublicationPlace], [PublicationDate]) VALUES (N'00032-2024', N'The Goldfinch', N'Donna Tartt', N'Little, Brown and Company', N'9780316055444', N'New York', CAST(N'2013-10-22T00:00:00.000' AS DateTime))
INSERT [dbo].[TblBook] ([BookNum], [Title], [Author], [Publisher], [ISBN], [PublicationPlace], [PublicationDate]) VALUES (N'00033-2024', N'The Night Circus', N'Erin Morgenstern', N'Doubleday', N'9780385534635', N'New York', CAST(N'2011-09-13T00:00:00.000' AS DateTime))
INSERT [dbo].[TblBook] ([BookNum], [Title], [Author], [Publisher], [ISBN], [PublicationPlace], [PublicationDate]) VALUES (N'00034-2024', N'The Book Thief', N'Markus Zusak', N'Picador', N'9780316000000', N'Sydney', CAST(N'2005-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[TblBook] ([BookNum], [Title], [Author], [Publisher], [ISBN], [PublicationPlace], [PublicationDate]) VALUES (N'00035-2024', N'Sapiens: A Brief History of Humankind', N'Yuval Noah Harari', N'Harper', N'9780062316097', N'London', CAST(N'2011-06-04T00:00:00.000' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[TblBorrow] ON 

INSERT [dbo].[TblBorrow] ([BorrowID], [StudentLibraryCardNum], [BookBookNum], [BorrowDate], [ReturnDate], [DueDate]) VALUES (52, 123456, N'00007-2024', CAST(N'2024-11-06T13:44:50.397' AS DateTime), CAST(N'2024-11-06T13:44:50.423' AS DateTime), CAST(N'2024-11-13T13:44:50.393' AS DateTime))
INSERT [dbo].[TblBorrow] ([BorrowID], [StudentLibraryCardNum], [BookBookNum], [BorrowDate], [ReturnDate], [DueDate]) VALUES (53, 123456, N'00007-2024', CAST(N'2024-11-06T13:51:22.963' AS DateTime), CAST(N'2024-11-06T13:51:44.027' AS DateTime), CAST(N'2024-11-13T13:51:22.960' AS DateTime))
INSERT [dbo].[TblBorrow] ([BorrowID], [StudentLibraryCardNum], [BookBookNum], [BorrowDate], [ReturnDate], [DueDate]) VALUES (54, 123456, N'00007-2024', CAST(N'2024-11-06T14:00:09.683' AS DateTime), CAST(N'2024-11-06T14:00:09.710' AS DateTime), CAST(N'2024-11-13T14:00:09.680' AS DateTime))
INSERT [dbo].[TblBorrow] ([BorrowID], [StudentLibraryCardNum], [BookBookNum], [BorrowDate], [ReturnDate], [DueDate]) VALUES (55, 123456, N'00007-2024', CAST(N'2024-11-06T14:00:47.417' AS DateTime), CAST(N'2024-11-06T14:00:47.443' AS DateTime), CAST(N'2024-11-13T14:00:47.417' AS DateTime))
INSERT [dbo].[TblBorrow] ([BorrowID], [StudentLibraryCardNum], [BookBookNum], [BorrowDate], [ReturnDate], [DueDate]) VALUES (56, 123456, N'00007-2024', CAST(N'2024-11-06T14:02:35.393' AS DateTime), CAST(N'2024-11-06T14:02:35.430' AS DateTime), CAST(N'2024-11-13T14:02:35.393' AS DateTime))
INSERT [dbo].[TblBorrow] ([BorrowID], [StudentLibraryCardNum], [BookBookNum], [BorrowDate], [ReturnDate], [DueDate]) VALUES (57, 123456, N'00007-2024', CAST(N'2024-11-06T14:03:58.260' AS DateTime), CAST(N'2024-11-06T14:03:58.297' AS DateTime), CAST(N'2024-11-13T14:03:58.260' AS DateTime))
INSERT [dbo].[TblBorrow] ([BorrowID], [StudentLibraryCardNum], [BookBookNum], [BorrowDate], [ReturnDate], [DueDate]) VALUES (58, 123456, N'00007-2024', CAST(N'2024-11-06T14:07:20.567' AS DateTime), CAST(N'2024-11-06T14:07:34.417' AS DateTime), CAST(N'2024-11-13T14:07:20.540' AS DateTime))
INSERT [dbo].[TblBorrow] ([BorrowID], [StudentLibraryCardNum], [BookBookNum], [BorrowDate], [ReturnDate], [DueDate]) VALUES (59, 123456, N'00001-2024', CAST(N'2024-11-06T14:07:34.413' AS DateTime), NULL, CAST(N'2024-11-20T14:10:33.000' AS DateTime))
INSERT [dbo].[TblBorrow] ([BorrowID], [StudentLibraryCardNum], [BookBookNum], [BorrowDate], [ReturnDate], [DueDate]) VALUES (60, 123456, N'00002-2024', CAST(N'2024-11-06T14:07:34.413' AS DateTime), NULL, CAST(N'2024-11-13T14:07:34.387' AS DateTime))
INSERT [dbo].[TblBorrow] ([BorrowID], [StudentLibraryCardNum], [BookBookNum], [BorrowDate], [ReturnDate], [DueDate]) VALUES (61, 123456, N'00004-2024', CAST(N'2024-11-06T14:07:38.737' AS DateTime), NULL, CAST(N'2024-11-13T14:07:38.713' AS DateTime))
INSERT [dbo].[TblBorrow] ([BorrowID], [StudentLibraryCardNum], [BookBookNum], [BorrowDate], [ReturnDate], [DueDate]) VALUES (1007, 100001, N'00005-2024', CAST(N'2024-11-07T12:57:58.470' AS DateTime), CAST(N'2024-11-07T13:13:30.733' AS DateTime), CAST(N'2024-11-14T00:00:00.000' AS DateTime))
INSERT [dbo].[TblBorrow] ([BorrowID], [StudentLibraryCardNum], [BookBookNum], [BorrowDate], [ReturnDate], [DueDate]) VALUES (1008, 100001, N'00007-2024', CAST(N'2024-11-07T13:17:40.860' AS DateTime), CAST(N'2024-11-07T13:18:51.977' AS DateTime), CAST(N'2024-11-21T00:00:00.000' AS DateTime))
INSERT [dbo].[TblBorrow] ([BorrowID], [StudentLibraryCardNum], [BookBookNum], [BorrowDate], [ReturnDate], [DueDate]) VALUES (1009, 100001, N'00005-2024', CAST(N'2024-11-07T13:17:43.057' AS DateTime), CAST(N'2024-11-07T13:18:50.393' AS DateTime), CAST(N'2024-11-21T00:00:00.000' AS DateTime))
INSERT [dbo].[TblBorrow] ([BorrowID], [StudentLibraryCardNum], [BookBookNum], [BorrowDate], [ReturnDate], [DueDate]) VALUES (1010, 0, N'00005-2024', CAST(N'2024-11-07T13:31:27.383' AS DateTime), CAST(N'2024-11-07T13:32:04.313' AS DateTime), CAST(N'2024-11-14T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[TblBorrow] OFF
GO
INSERT [dbo].[TblStudent] ([LibraryCardNum], [FirstName], [LastName]) VALUES (0, N'Anonym', N'Anonym')
INSERT [dbo].[TblStudent] ([LibraryCardNum], [FirstName], [LastName]) VALUES (100001, N'David', N'Green')
INSERT [dbo].[TblStudent] ([LibraryCardNum], [FirstName], [LastName]) VALUES (100002, N'Samuel Updated', N'White')
INSERT [dbo].[TblStudent] ([LibraryCardNum], [FirstName], [LastName]) VALUES (100004, N'Ella', N'Scott')
INSERT [dbo].[TblStudent] ([LibraryCardNum], [FirstName], [LastName]) VALUES (100005, N'Mason', N'Gray')
INSERT [dbo].[TblStudent] ([LibraryCardNum], [FirstName], [LastName]) VALUES (100006, N'Henry', N'Taylor')
INSERT [dbo].[TblStudent] ([LibraryCardNum], [FirstName], [LastName]) VALUES (100007, N'Olivia', N'Taylor')
INSERT [dbo].[TblStudent] ([LibraryCardNum], [FirstName], [LastName]) VALUES (123456, N'John', N'Doe')
INSERT [dbo].[TblStudent] ([LibraryCardNum], [FirstName], [LastName]) VALUES (158937, N'Sophia', N'Fischer')
INSERT [dbo].[TblStudent] ([LibraryCardNum], [FirstName], [LastName]) VALUES (185432, N'Anna', N'Schmidt')
INSERT [dbo].[TblStudent] ([LibraryCardNum], [FirstName], [LastName]) VALUES (192837, N'Luis', N'Braun')
INSERT [dbo].[TblStudent] ([LibraryCardNum], [FirstName], [LastName]) VALUES (243567, N'Paul', N'Müller')
INSERT [dbo].[TblStudent] ([LibraryCardNum], [FirstName], [LastName]) VALUES (246389, N'Clara', N'Krüger')
INSERT [dbo].[TblStudent] ([LibraryCardNum], [FirstName], [LastName]) VALUES (268745, N'Max', N'Wagner')
INSERT [dbo].[TblStudent] ([LibraryCardNum], [FirstName], [LastName]) VALUES (376589, N'Lena', N'Krause')
INSERT [dbo].[TblStudent] ([LibraryCardNum], [FirstName], [LastName]) VALUES (395467, N'Noah', N'Hartmann')
INSERT [dbo].[TblStudent] ([LibraryCardNum], [FirstName], [LastName]) VALUES (397845, N'Isabella', N'Schulz')
INSERT [dbo].[TblStudent] ([LibraryCardNum], [FirstName], [LastName]) VALUES (429876, N'Finn', N'Richter')
INSERT [dbo].[TblStudent] ([LibraryCardNum], [FirstName], [LastName]) VALUES (459321, N'Jonas', N'Becker')
INSERT [dbo].[TblStudent] ([LibraryCardNum], [FirstName], [LastName]) VALUES (512478, N'Mia', N'Hoffmann')
INSERT [dbo].[TblStudent] ([LibraryCardNum], [FirstName], [LastName]) VALUES (534891, N'Emilia', N'Klein')
INSERT [dbo].[TblStudent] ([LibraryCardNum], [FirstName], [LastName]) VALUES (654321, N'Alice', N'Smith')
INSERT [dbo].[TblStudent] ([LibraryCardNum], [FirstName], [LastName]) VALUES (675849, N'Lukas', N'Schneider')
INSERT [dbo].[TblStudent] ([LibraryCardNum], [FirstName], [LastName]) VALUES (682345, N'Ben', N'Wolf')
INSERT [dbo].[TblStudent] ([LibraryCardNum], [FirstName], [LastName]) VALUES (752189, N'Lara', N'Schmidt')
INSERT [dbo].[TblStudent] ([LibraryCardNum], [FirstName], [LastName]) VALUES (782456, N'Emma', N'Weber')
INSERT [dbo].[TblStudent] ([LibraryCardNum], [FirstName], [LastName]) VALUES (789012, N'Robert', N'Johnson')
INSERT [dbo].[TblStudent] ([LibraryCardNum], [FirstName], [LastName]) VALUES (817934, N'Tim', N'Neumann')
GO
ALTER TABLE [dbo].[TblBorrow]  WITH CHECK ADD  CONSTRAINT [FK_TblBorrow_TblBook] FOREIGN KEY([BookBookNum])
REFERENCES [dbo].[TblBook] ([BookNum])
GO
ALTER TABLE [dbo].[TblBorrow] CHECK CONSTRAINT [FK_TblBorrow_TblBook]
GO
ALTER TABLE [dbo].[TblBorrow]  WITH CHECK ADD  CONSTRAINT [FK_TblBorrow_TblStudent] FOREIGN KEY([StudentLibraryCardNum])
REFERENCES [dbo].[TblStudent] ([LibraryCardNum])
GO
ALTER TABLE [dbo].[TblBorrow] CHECK CONSTRAINT [FK_TblBorrow_TblStudent]
GO
USE [master]
GO
ALTER DATABASE [LibraryManagementDB] SET  READ_WRITE 
GO
