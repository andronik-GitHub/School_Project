-- Created by Vertabelo (http://vertabelo.com)
-- Last modification date: 2023-03-14 14:31:59.556

CREATE DATABASE SchoolLibrary
GO

USE SchoolLibrary
GO

-- tables
-- Table: Authors
CREATE TABLE Authors (
    AuthorId uniqueidentifier  NOT NULL,
    FirstName nvarchar(50)  NOT NULL,
    LastName nvarchar(50)  NOT NULL,
    Birthdate date  NOT NULL,
    Nationality nvarchar(30)  NOT NULL,
    CONSTRAINT Authors_pk PRIMARY KEY  (AuthorId)
);

-- Table: BookAuthors
CREATE TABLE BookAuthors (
    BookId uniqueidentifier  NOT NULL,
    AuthorId uniqueidentifier  NOT NULL,
    CONSTRAINT BookAuthors_pk PRIMARY KEY  (AuthorId,BookId)
);

-- Table: BookDetails
CREATE TABLE BookDetails (
    BookDetailId uniqueidentifier  NOT NULL,
    BookId uniqueidentifier  NOT NULL,
    Pages int  NOT NULL,
    Language nvarchar(20)  NOT NULL,
    Format nvarchar(30)  NOT NULL,
    CONSTRAINT BookId_Unique UNIQUE (BookId),
    CONSTRAINT BookDetails_pk PRIMARY KEY  (BookDetailId)
);

-- Table: BookGenres
CREATE TABLE BookGenres (
    BookId uniqueidentifier  NOT NULL,
    GenreId uniqueidentifier  NOT NULL,
    CONSTRAINT BookGenres_pk PRIMARY KEY  (BookId,GenreId)
);

-- Table: Books
CREATE TABLE Books (
    BookId uniqueidentifier  NOT NULL,
    Title nvarchar(50)  NOT NULL,
    PublishingYear int  NOT NULL,
    PublisherId uniqueidentifier  NOT NULL,
    ISBN nvarchar(20)  NOT NULL,
    CONSTRAINT Books_pk PRIMARY KEY  (BookId)
);

-- Table: Genres
CREATE TABLE Genres (
    GenreId uniqueidentifier  NOT NULL,
    Name nvarchar(50)  NOT NULL,
    CONSTRAINT Genres_pk PRIMARY KEY  (GenreId)
);

-- Table: Loans
CREATE TABLE Loans (
    LoanId uniqueidentifier  NOT NULL,
    UserId uniqueidentifier  NOT NULL,
    BookId uniqueidentifier  NOT NULL,
    LoanDate date  NOT NULL,
    ReturnDate date  NOT NULL,
    CONSTRAINT Loans_pk PRIMARY KEY  (LoanId)
);

-- Table: Publishers
CREATE TABLE Publishers (
    PublisherId uniqueidentifier  NOT NULL,
    Name nvarchar(50)  NOT NULL,
    Location nvarchar(100)  NOT NULL,
    CONSTRAINT Publishers_pk PRIMARY KEY  (PublisherId)
);

-- Table: Reviews
CREATE TABLE Reviews (
    ReviewId uniqueidentifier  NOT NULL,
    UserId uniqueidentifier  NOT NULL,
    Rating decimal(2,1)  NOT NULL,
    ReviewText nvarchar(1000)  NOT NULL,
    BookId uniqueidentifier  NOT NULL,
    CONSTRAINT Reviews_pk PRIMARY KEY  (ReviewId)
);

-- Table: Users
CREATE TABLE Users (
    UserId uniqueidentifier  NOT NULL,
    FirstName nvarchar(50)  NOT NULL,
    LastName nvarchar(50)  NOT NULL,
    Email nvarchar(80)  NOT NULL,
    Password nvarchar(30)  NOT NULL,
    Address nvarchar(100)  NOT NULL,
    Phone nvarchar(20)  NOT NULL,
    CONSTRAINT Users_pk PRIMARY KEY  (UserId)
);

-- foreign keys
-- Reference: BookAuthors_Authors (table: BookAuthors)
ALTER TABLE BookAuthors ADD CONSTRAINT BookAuthors_Authors
    FOREIGN KEY (AuthorId)
    REFERENCES Authors (AuthorId);

-- Reference: BookAuthors_Books (table: BookAuthors)
ALTER TABLE BookAuthors ADD CONSTRAINT BookAuthors_Books
    FOREIGN KEY (BookId)
    REFERENCES Books (BookId);

-- Reference: BookGenres_Books (table: BookGenres)
ALTER TABLE BookGenres ADD CONSTRAINT BookGenres_Books
    FOREIGN KEY (BookId)
    REFERENCES Books (BookId);

-- Reference: BookGenres_Genres (table: BookGenres)
ALTER TABLE BookGenres ADD CONSTRAINT BookGenres_Genres
    FOREIGN KEY (GenreId)
    REFERENCES Genres (GenreId);

-- Reference: Books_BookDetails (table: BookDetails)
ALTER TABLE BookDetails ADD CONSTRAINT Books_BookDetails
    FOREIGN KEY (BookId)
    REFERENCES Books (BookId);

-- Reference: Books_Publishers (table: Books)
ALTER TABLE Books ADD CONSTRAINT Books_Publishers
    FOREIGN KEY (PublisherId)
    REFERENCES Publishers (PublisherId);

-- Reference: Loans_Books (table: Loans)
ALTER TABLE Loans ADD CONSTRAINT Loans_Books
    FOREIGN KEY (BookId)
    REFERENCES Books (BookId);

-- Reference: Loans_Users (table: Loans)
ALTER TABLE Loans ADD CONSTRAINT Loans_Users
    FOREIGN KEY (UserId)
    REFERENCES Users (UserId);

-- Reference: Reviews_Books (table: Reviews)
ALTER TABLE Reviews ADD CONSTRAINT Reviews_Books
    FOREIGN KEY (BookId)
    REFERENCES Books (BookId);

-- Reference: Reviews_Users (table: Reviews)
ALTER TABLE Reviews ADD CONSTRAINT Reviews_Users
    FOREIGN KEY (UserId)
    REFERENCES Users (UserId);

-- End of file.

