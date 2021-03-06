USE [D:\DATABASES\WHATWASREAD\WHATWASREAD.MDF]
GO
/****** Object:  Table [dbo].[Authors]******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Authors](
	[AuthorId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](30) NOT NULL,
	[LastName] [nvarchar](30) NOT NULL,
 CONSTRAINT [PK_Authors] PRIMARY KEY CLUSTERED 
(
	[AuthorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Languages] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Languages](
	[LanguageId] [int] IDENTITY(1,1) NOT NULL,
	[NameForLabels] [nvarchar](50) NOT NULL,
	[NameForLinks] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Languages] PRIMARY KEY CLUSTERED 
(
	[LanguageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tags] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tags](
	[TagId] [int] IDENTITY(1,1) NOT NULL,
	[NameForLabels] [nvarchar](50) NOT NULL,
	[NameForLinks] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Tags] PRIMARY KEY CLUSTERED 
(
	[TagId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Books]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Books](
	[BookId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](300) NOT NULL,
	[LanguageId] [int] NOT NULL,
	[Pages] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CategoryId] [int] NOT NULL,
	[Year] [int] NOT NULL,
	[ImageData] [varbinary](max) NULL,
	[ImageMimeType] [nvarchar](30) NULL,
 CONSTRAINT [PK_Books] PRIMARY KEY CLUSTERED 
(
	[BookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AuthorsOfBooks] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AuthorsOfBooks](
	[AuthorId] [int] NOT NULL,
	[BookId] [int] NOT NULL,
 CONSTRAINT [PK_AuthorsOfBooks] PRIMARY KEY CLUSTERED 
(
	[AuthorId] ASC,
	[BookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BookTags] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookTags](
	[BookId] [int] NOT NULL,
	[TagId] [int] NOT NULL,
 CONSTRAINT [PK_BookTags] PRIMARY KEY CLUSTERED 
(
	[BookId] ASC,
	[TagId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[BooksWithAuthors] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[BooksWithAuthors]
	AS 
	Select b.*, a.*, l.NameForLinks, t.TagId, t.NameForLabels as TagNameForLabels, t.NameForLinks as TagNameForLinks from [dbo].[Books] as b
	left join [dbo].[AuthorsOfBooks] as ab
	on b.BookId = ab.BookId
	inner join [dbo].[Authors] as a
	on ab.AuthorId = a.AuthorId
	left join [dbo].[Languages] as l
	on b.LanguageId = l.LanguageId
	left join [dbo].BookTags as bt
	on b.BookId = bt.BookId
	left join [dbo].Tags as t
	on bt.TagId = t.TagId

;
GO
/****** Object:  Table [dbo].[Categories] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[NameForLinks] [nvarchar](50) NOT NULL,
	[NameForLabels] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Filters] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Filters](
	[FilterId] [int] IDENTITY(1,1) NOT NULL,
	[FilterTargetId] [int] NOT NULL,
	[FilterColumnName] [nvarchar](50) NOT NULL,
	[QueryWord] [nvarchar](10) NOT NULL,
	[Comparator] [nvarchar](10) NOT NULL,
	[FilterName] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_Filters] PRIMARY KEY CLUSTERED 
(
	[FilterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FilterTargets] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FilterTargets](
	[FilterTargetId] [int] IDENTITY(1,1) NOT NULL,
	[FilterTargetName] [nvarchar](50) NOT NULL,
	[FilterTargetSchema] [nvarchar](50) NOT NULL,
	[ControllerName] [nvarchar](50) NOT NULL,
	[ActionName] [nvarchar](50) NOT NULL,
	[FilterName] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_FilterTargets] PRIMARY KEY CLUSTERED 
(
	[FilterTargetId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AuthorsOfBooks]  WITH CHECK ADD  CONSTRAINT [FK_AuthorsOfBooks_Authors_AuthorId] FOREIGN KEY([AuthorId])
REFERENCES [dbo].[Authors] ([AuthorId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AuthorsOfBooks] CHECK CONSTRAINT [FK_AuthorsOfBooks_Authors_AuthorId]
GO
ALTER TABLE [dbo].[AuthorsOfBooks]  WITH CHECK ADD  CONSTRAINT [FK_AuthorsOfBooks_Books_BookId] FOREIGN KEY([BookId])
REFERENCES [dbo].[Books] ([BookId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AuthorsOfBooks] CHECK CONSTRAINT [FK_AuthorsOfBooks_Books_BookId]
GO
ALTER TABLE [dbo].[Books]  WITH CHECK ADD  CONSTRAINT [FK_Books_Categories_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([CategoryId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Books] CHECK CONSTRAINT [FK_Books_Categories_CategoryId]
GO
ALTER TABLE [dbo].[Books]  WITH CHECK ADD  CONSTRAINT [FK_Books_Languages_LanguageId] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Languages] ([LanguageId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Books] CHECK CONSTRAINT [FK_Books_Languages_LanguageId]
GO
ALTER TABLE [dbo].[BookTags]  WITH CHECK ADD  CONSTRAINT [FK_BookTags_Books_BookId] FOREIGN KEY([BookId])
REFERENCES [dbo].[Books] ([BookId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BookTags] CHECK CONSTRAINT [FK_BookTags_Books_BookId]
GO
ALTER TABLE [dbo].[BookTags]  WITH CHECK ADD  CONSTRAINT [FK_BookTags_Tags_TagId] FOREIGN KEY([TagId])
REFERENCES [dbo].[Tags] ([TagId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BookTags] CHECK CONSTRAINT [FK_BookTags_Tags_TagId]
GO
ALTER TABLE [dbo].[Filters]  WITH CHECK ADD  CONSTRAINT [FK_Filters_FilterTargets_FilterTargetId] FOREIGN KEY([FilterTargetId])
REFERENCES [dbo].[FilterTargets] ([FilterTargetId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Filters] CHECK CONSTRAINT [FK_Filters_FilterTargets_FilterTargetId]
GO
