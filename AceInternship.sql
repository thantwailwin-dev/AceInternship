USE [master]
GO
/****** Object:  Database [AceInternship]    Script Date: 6/16/2024 11:36:18 PM ******/
CREATE DATABASE [AceInternship] ON  PRIMARY 
( NAME = N'AceInternship', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\AceInternship.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'AceInternship_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\AceInternship_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AceInternship].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AceInternship] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AceInternship] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AceInternship] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AceInternship] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AceInternship] SET ARITHABORT OFF 
GO
ALTER DATABASE [AceInternship] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [AceInternship] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AceInternship] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AceInternship] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AceInternship] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [AceInternship] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AceInternship] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AceInternship] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AceInternship] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AceInternship] SET  DISABLE_BROKER 
GO
ALTER DATABASE [AceInternship] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AceInternship] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AceInternship] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AceInternship] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AceInternship] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AceInternship] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [AceInternship] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [AceInternship] SET RECOVERY FULL 
GO
ALTER DATABASE [AceInternship] SET  MULTI_USER 
GO
ALTER DATABASE [AceInternship] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [AceInternship] SET DB_CHAINING OFF 
GO
EXEC sys.sp_db_vardecimal_storage_format N'AceInternship', N'ON'
GO
USE [AceInternship]
GO
/****** Object:  Table [dbo].[Tbl_Blog]    Script Date: 6/16/2024 11:36:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Blog](
	[BlogId] [int] IDENTITY(1,1) NOT NULL,
	[BlogTitle] [nvarchar](50) NOT NULL,
	[BlogAuthor] [nvarchar](50) NOT NULL,
	[BlogContent] [varchar](max) NOT NULL,
 CONSTRAINT [PK_Tbl_Blog] PRIMARY KEY CLUSTERED 
(
	[BlogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Tbl_Blog] ON 

INSERT [dbo].[Tbl_Blog] ([BlogId], [BlogTitle], [BlogAuthor], [BlogContent]) VALUES (17, N'string q', N'string w', N'string d')
INSERT [dbo].[Tbl_Blog] ([BlogId], [BlogTitle], [BlogAuthor], [BlogContent]) VALUES (18, N'test title', N'test author', N'test content')
INSERT [dbo].[Tbl_Blog] ([BlogId], [BlogTitle], [BlogAuthor], [BlogContent]) VALUES (19, N'title', N'author', N'content')
SET IDENTITY_INSERT [dbo].[Tbl_Blog] OFF
GO
USE [master]
GO
ALTER DATABASE [AceInternship] SET  READ_WRITE 
GO
