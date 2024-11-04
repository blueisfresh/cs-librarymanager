CREATE TABLE [TblStudent](
    [LibraryCardNum] int NOT NULL,
    [FirstName] nvarchar(50) NOT NULL,
    [LastName] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_TblStudent] PRIMARY KEY CLUSTERED ([LibraryCardNum] ASC)
) ON [PRIMARY]
GO

CREATE TABLE [TblBook](
    [BookNum] char(10) NOT NULL,
    [Title] nvarchar(100) NOT NULL,
    [Author] nvarchar(100) NOT NULL,
    [Publisher] nvarchar(100) NOT NULL,
    [ISBN] nchar(13) NOT NULL,
    [PublicationPlace] nvarchar(50) NOT NULL,
    [PublicationDate] datetime NOT NULL,
    CONSTRAINT [PK_TblBook] PRIMARY KEY CLUSTERED ([BookNum] ASC)
) ON [PRIMARY]
GO

CREATE TABLE [TblBorrow](
    [BorrowID] int IDENTITY(1, 1) NOT NULL,
    [StudentLibraryCardNum] int NOT NULL,
    [BookBookNum] char(10) NOT NULL,
    [BorrowDate] datetime NOT NULL,
    [ReturnDate] datetime NOT NULL,
    [DueDate] datetime NOT NULL,
    CONSTRAINT [PK_TblBorrow] PRIMARY KEY CLUSTERED ([BorrowID] ASC)
) ON [PRIMARY]
GO

-- Adding foreign key constraints after table creation
ALTER TABLE [TblBorrow] WITH CHECK ADD CONSTRAINT [FK_TblBorrow_TblStudent] FOREIGN KEY ([StudentLibraryCardNum])
REFERENCES [TblStudent] ([LibraryCardNum])
GO
ALTER TABLE [TblBorrow] CHECK CONSTRAINT [FK_TblBorrow_TblStudent]
GO

ALTER TABLE [TblBorrow] WITH CHECK ADD CONSTRAINT [FK_TblBorrow_TblBook] FOREIGN KEY ([BookBookNum])
REFERENCES [TblBook] ([BookNum])
GO
ALTER TABLE [TblBorrow] CHECK CONSTRAINT [FK_TblBorrow_TblBook]
GO
