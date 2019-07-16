USE [master]
GO
/****** Object:  Database [UpCenter]    Script Date: 07/16/2019 7:55:04 AM ******/
CREATE DATABASE [UpCenter]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'UpCenter', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQL2012\MSSQL\DATA\UpCenter.mdf' , SIZE = 4288KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'UpCenter_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQL2012\MSSQL\DATA\UpCenter_log.ldf' , SIZE = 832KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [UpCenter] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [UpCenter].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [UpCenter] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [UpCenter] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [UpCenter] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [UpCenter] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [UpCenter] SET ARITHABORT OFF 
GO
ALTER DATABASE [UpCenter] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [UpCenter] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [UpCenter] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [UpCenter] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [UpCenter] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [UpCenter] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [UpCenter] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [UpCenter] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [UpCenter] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [UpCenter] SET  ENABLE_BROKER 
GO
ALTER DATABASE [UpCenter] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [UpCenter] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [UpCenter] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [UpCenter] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [UpCenter] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [UpCenter] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [UpCenter] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [UpCenter] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [UpCenter] SET  MULTI_USER 
GO
ALTER DATABASE [UpCenter] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [UpCenter] SET DB_CHAINING OFF 
GO
ALTER DATABASE [UpCenter] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [UpCenter] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [UpCenter]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 07/16/2019 7:55:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 07/16/2019 7:55:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 07/16/2019 7:55:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 07/16/2019 7:55:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 07/16/2019 7:55:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 07/16/2019 7:55:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 07/16/2019 7:55:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 07/16/2019 7:55:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChiPhiCoDinhs]    Script Date: 07/16/2019 7:55:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiPhiCoDinhs](
	[ChiPhiCoDinhId] [uniqueidentifier] NOT NULL,
	[Gia] [float] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[IsDisabled] [bit] NOT NULL,
 CONSTRAINT [PK_ChiPhiCoDinhs] PRIMARY KEY CLUSTERED 
(
	[ChiPhiCoDinhId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GiaoViens]    Script Date: 07/16/2019 7:55:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GiaoViens](
	[GiaoVienId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Phone] [nvarchar](max) NULL,
	[TeachingRate] [float] NOT NULL,
	[TutoringRate] [float] NOT NULL,
	[BasicSalary] [float] NOT NULL,
	[FacebookAccount] [nvarchar](max) NULL,
	[DiaChi] [nvarchar](max) NULL,
	[InitialName] [nvarchar](max) NULL,
	[CMND] [nvarchar](max) NULL,
	[IsDisabled] [bit] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[LoaiGiaoVienId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_GiaoViens] PRIMARY KEY CLUSTERED 
(
	[GiaoVienId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GioHocs]    Script Date: 07/16/2019 7:55:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GioHocs](
	[GioHocId] [uniqueidentifier] NOT NULL,
	[To] [nvarchar](max) NULL,
	[IsDisabled] [bit] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[From] [nvarchar](max) NULL,
 CONSTRAINT [PK_GioHocs] PRIMARY KEY CLUSTERED 
(
	[GioHocId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HocPhis]    Script Date: 07/16/2019 7:55:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HocPhis](
	[HocPhiId] [uniqueidentifier] NOT NULL,
	[Gia] [float] NOT NULL,
	[IsDisabled] [bit] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[GhiChu] [nvarchar](max) NULL,
	[NgayApDung] [datetime2](7) NULL,
 CONSTRAINT [PK_HocPhis] PRIMARY KEY CLUSTERED 
(
	[HocPhiId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HocVien_LopHocs]    Script Date: 07/16/2019 7:55:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HocVien_LopHocs](
	[HocVien_LopHocId] [uniqueidentifier] NOT NULL,
	[HocVienId] [uniqueidentifier] NOT NULL,
	[LopHocId] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_HocVien_LopHocs] PRIMARY KEY CLUSTERED 
(
	[HocVien_LopHocId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HocVien_NgayHocs]    Script Date: 07/16/2019 7:55:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HocVien_NgayHocs](
	[HocVien_NgayHocId] [uniqueidentifier] NOT NULL,
	[LopHocId] [uniqueidentifier] NOT NULL,
	[HocVienId] [uniqueidentifier] NOT NULL,
	[NgayBatDau] [datetime2](7) NOT NULL,
	[NgayKetThuc] [datetime2](7) NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_HocVien_NgayHocs] PRIMARY KEY CLUSTERED 
(
	[HocVien_NgayHocId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HocVien_Nos]    Script Date: 07/16/2019 7:55:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HocVien_Nos](
	[HocVien_NoId] [uniqueidentifier] NOT NULL,
	[HocVienId] [uniqueidentifier] NOT NULL,
	[LopHocId] [uniqueidentifier] NOT NULL,
	[TienNo] [float] NOT NULL,
	[NgayNo] [datetime2](7) NOT NULL,
	[IsDisabled] [bit] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_HocVien_Nos] PRIMARY KEY CLUSTERED 
(
	[HocVien_NoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HocViens]    Script Date: 07/16/2019 7:55:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HocViens](
	[HocVienId] [uniqueidentifier] NOT NULL,
	[FullName] [nvarchar](max) NULL,
	[Phone] [nvarchar](max) NULL,
	[FacebookAccount] [nvarchar](max) NULL,
	[ParentFullName] [nvarchar](max) NULL,
	[ParentPhone] [nvarchar](max) NULL,
	[QuanHeId] [uniqueidentifier] NULL,
	[ParentFacebookAccount] [nvarchar](max) NULL,
	[NgaySinh] [datetime2](7) NULL,
	[EnglishName] [nvarchar](max) NULL,
	[IsAppend] [bit] NOT NULL,
	[IsDisabled] [bit] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_HocViens] PRIMARY KEY CLUSTERED 
(
	[HocVienId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KhoaHocs]    Script Date: 07/16/2019 7:55:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KhoaHocs](
	[KhoaHocId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[IsDisabled] [bit] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_KhoaHocs] PRIMARY KEY CLUSTERED 
(
	[KhoaHocId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoaiGiaoViens]    Script Date: 07/16/2019 7:55:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiGiaoViens](
	[LoaiGiaoVienId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[IsDisabled] [bit] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_LoaiGiaoViens] PRIMARY KEY CLUSTERED 
(
	[LoaiGiaoVienId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LopHoc_DiemDanhs]    Script Date: 07/16/2019 7:55:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LopHoc_DiemDanhs](
	[LopHoc_DiemDanhId] [uniqueidentifier] NOT NULL,
	[LopHocId] [uniqueidentifier] NOT NULL,
	[HocVienId] [uniqueidentifier] NOT NULL,
	[IsOff] [bit] NOT NULL,
	[NgayDiemDanh] [datetime2](7) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[IsDuocNghi] [bit] NULL,
 CONSTRAINT [PK_LopHoc_DiemDanhs] PRIMARY KEY CLUSTERED 
(
	[LopHoc_DiemDanhId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LopHocs]    Script Date: 07/16/2019 7:55:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LopHocs](
	[LopHocId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[IsDisabled] [bit] NOT NULL,
	[IsGraduated] [bit] NOT NULL,
	[IsCanceled] [bit] NOT NULL,
	[KhoaHocId] [uniqueidentifier] NOT NULL,
	[NgayHocId] [uniqueidentifier] NOT NULL,
	[GioHocId] [uniqueidentifier] NOT NULL,
	[NgayKhaiGiang] [datetime2](7) NOT NULL,
	[NgayKetThuc] [datetime2](7) NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[HocPhiId] [uniqueidentifier] NOT NULL,
	[GiaoVienId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_LopHocs] PRIMARY KEY CLUSTERED 
(
	[LopHocId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NgayHocs]    Script Date: 07/16/2019 7:55:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NgayHocs](
	[NgayHocId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[IsDisabled] [bit] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_NgayHocs] PRIMARY KEY CLUSTERED 
(
	[NgayHocId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NhanVienKhacs]    Script Date: 07/16/2019 7:55:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhanVienKhacs](
	[NhanVienKhacId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Phone] [nvarchar](max) NULL,
	[BasicSalary] [float] NOT NULL,
	[FacebookAccount] [nvarchar](max) NULL,
	[DiaChi] [nvarchar](max) NULL,
	[CMND] [nvarchar](max) NULL,
	[IsDisabled] [bit] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_NhanVienKhacs] PRIMARY KEY CLUSTERED 
(
	[NhanVienKhacId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuanHes]    Script Date: 07/16/2019 7:55:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuanHes](
	[QuanHeId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[IsDisabled] [bit] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_QuanHes] PRIMARY KEY CLUSTERED 
(
	[QuanHeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sachs]    Script Date: 07/16/2019 7:55:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sachs](
	[SachId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Gia] [float] NOT NULL,
	[IsDisabled] [bit] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_Sachs] PRIMARY KEY CLUSTERED 
(
	[SachId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ThongKe_ChiPhis]    Script Date: 07/16/2019 7:55:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ThongKe_ChiPhis](
	[ThongKe_ChiPhiId] [uniqueidentifier] NOT NULL,
	[ChiPhi] [float] NOT NULL,
	[NgayChiPhi] [datetime2](7) NOT NULL,
	[IsDisabled] [bit] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_ThongKe_ChiPhis] PRIMARY KEY CLUSTERED 
(
	[ThongKe_ChiPhiId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ThongKe_DoanhThuHocPhis]    Script Date: 07/16/2019 7:55:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ThongKe_DoanhThuHocPhis](
	[ThongKe_DoanhThuHocPhiId] [uniqueidentifier] NOT NULL,
	[HocVienId] [uniqueidentifier] NOT NULL,
	[LopHocId] [uniqueidentifier] NOT NULL,
	[HocPhi] [float] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[NgayDong] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_ThongKe_DoanhThuHocPhis] PRIMARY KEY CLUSTERED 
(
	[ThongKe_DoanhThuHocPhiId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'00000000000000_CreateIdentitySchema', N'2.2.1-servicing-10028')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190219142940_AddRoleAdmin', N'2.2.1-servicing-10028')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190219152624_AddKhoaHocs', N'2.2.1-servicing-10028')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190223071102_AddQuanHes', N'2.2.1-servicing-10028')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190223100056_AddKhoaHocNgayHoc', N'2.2.1-servicing-10028')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190224040329_AddLopHocs', N'2.2.1-servicing-10028')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190224040919_UpdateNgayKetThuc', N'2.2.1-servicing-10028')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190302100435_AddHocPhis', N'2.2.1-servicing-10028')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190302155358_UpdateLopHoc', N'2.2.1-servicing-10028')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190303030228_AddSachAndLopHocSach', N'2.2.1-servicing-10028')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190308150955_UpdateForeignKey', N'2.2.1-servicing-10028')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190313150852_AddHocVien', N'2.2.1-servicing-10028')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190414095359_AddGiaoVien', N'2.2.1-servicing-10028')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190419160738_LopHoc_DiemDanh', N'2.2.1-servicing-10028')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190427043004_UpdateHocPhi', N'2.2.1-servicing-10028')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190508144719_UpdateHocVienTable', N'2.2.1-servicing-10028')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190508152957_UpdateGioHocTable', N'2.2.1-servicing-10028')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190511083854_AddDuocNghi', N'2.2.1-servicing-10028')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190511090149_AddRelationship', N'2.2.1-servicing-10028')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190512160803_AddLoaiGiaoVien', N'2.2.1-servicing-10028')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190512161306_updategiaovien', N'2.2.1-servicing-10028')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190520160411_thongke', N'2.2.1-servicing-10028')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190523155630_Addhocvien_ngayhoc', N'2.2.1-servicing-10028')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190528160231_addroleagain', N'2.2.1-servicing-10028')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190604161311_AddChiPhiCODInh', N'2.2.1-servicing-10028')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190605155658_AddDisable', N'2.2.1-servicing-10028')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190608092909_HocVienNo', N'2.2.1-servicing-10028')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190611145037_AddNo2', N'2.2.1-servicing-10028')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190612143607_AddDoanhThu', N'2.2.1-servicing-10028')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190613142837_AddNgayDongHP', N'2.2.1-servicing-10028')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190620162253_AddNhanVienKhac', N'2.2.1-servicing-10028')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190710153141_ChangeNgaySinh', N'2.2.1-servicing-10028')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190714073854_RemoveLopHoc_Sach', N'2.2.1-servicing-10028')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'ea19b33d-a2ef-4919-b91f-efda405ac726', N'Admin', N'ADMIN', N'662855fc-aeef-4bb7-bf86-fdbdb3c4ea58')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'6335c4f1-7fc4-4517-a307-5a21575be823', N'ea19b33d-a2ef-4919-b91f-efda405ac726')
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'6335c4f1-7fc4-4517-a307-5a21575be823', N'huynhquan.nguyen@gmail.com', N'HUYNHQUAN.NGUYEN@GMAIL.COM', N'huynhquan.nguyen@gmail.com', N'HUYNHQUAN.NGUYEN@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAED/tRwLi98tRVILTARn/FF31jkjoXi8DscWFSzyAqFE0oaqiC6kN//9TlkXSUcCFKQ==', N'4S3DCCHQBOGIBU3AZQLPRSGW2O6BH5AC', N'6d0ad5db-6228-40f8-92a0-065416806419', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'6a21c900-2765-4f51-ad5d-850a2d1eaae8', N'b@gmail.com', N'B@GMAIL.COM', N'b@gmail.com', N'B@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEBKHSiaFN+OwFarJ7cONvQQZUUkalPDKBhnHqZD7K77jTRJycCnvo+4Uv1GnsE+xAg==', N'R2M2KZJ4NBTGVJMFSCXK6FVXSTF3SIS5', N'8947ad62-c7c9-4f27-820f-84a171ac7367', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'8a83c405-8b12-4b06-9061-a9e5dfe435a3', N'a@gmail.com', N'A@GMAIL.COM', N'a@gmail.com', N'A@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEFQ4tVgeX7o/OIh2GTmNxIighmSBB7YceXZMCr6A8PNOuyL7HREhgXsnfN3wPoSViQ==', N'NSUCAFONNVA57F32AXMWGYG2XWKRIGTH', N'69e9a047-bac1-4f80-8aa4-f6bafcf89d85', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'ebc73d2a-f206-4590-a0cc-804c14f5bdce', N'llala@gmail.com', N'LLALA@GMAIL.COM', N'llala@gmail.com', N'LLALA@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAENhpnWXtH6RspVYIW+2FOxZgR5WxX9xs6pwNpVOBS4XIjq5WbXUucAruUwPW3muXRw==', N'HVDA6LNW2X7YLNTQXYWVSX23WPNASFRK', N'bc940875-981e-4c1a-a582-8361f866ab47', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[ChiPhiCoDinhs] ([ChiPhiCoDinhId], [Gia], [Name], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [IsDisabled]) VALUES (N'8cbe91a6-eab6-43a7-be8d-08d6e9d54ca2', 1500000, N'Điện', CAST(N'2019-06-05T23:46:04.2411914' AS DateTime2), N'huynhquan.nguyen@gmail.com', CAST(N'2019-06-05T23:46:27.2894017' AS DateTime2), N'huynhquan.nguyen@gmail.com', 0)
INSERT [dbo].[ChiPhiCoDinhs] ([ChiPhiCoDinhId], [Gia], [Name], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [IsDisabled]) VALUES (N'b83588cb-8072-4ef7-502d-08d6ea8e429a', 10000000, N'Nhà', CAST(N'2019-06-06T21:50:04.2991379' AS DateTime2), N'huynhquan.nguyen@gmail.com', NULL, NULL, 0)
INSERT [dbo].[ChiPhiCoDinhs] ([ChiPhiCoDinhId], [Gia], [Name], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [IsDisabled]) VALUES (N'c0d3a85c-d88e-4b93-502e-08d6ea8e429a', 1000000, N'Bãi Xe', CAST(N'2019-06-06T21:50:16.0298271' AS DateTime2), N'huynhquan.nguyen@gmail.com', NULL, NULL, 0)
INSERT [dbo].[GiaoViens] ([GiaoVienId], [Name], [Phone], [TeachingRate], [TutoringRate], [BasicSalary], [FacebookAccount], [DiaChi], [InitialName], [CMND], [IsDisabled], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [LoaiGiaoVienId]) VALUES (N'1679f635-8e92-4f8e-c3ea-08d6fc64d5bc', N'phu', N'123', 123, 123, 123, N'231', N'215B37 Nguyễn Văn Hưởng, P, Quận 2, Hồ Chí Minh', N'123', N'123', 0, CAST(N'2019-06-29T14:38:53.2176968' AS DateTime2), N'huynhquan.nguyen@gmail.com', CAST(N'2019-07-09T21:17:02.5613956' AS DateTime2), N'huynhquan.nguyen@gmail.com', N'19f5c9fa-9601-4d23-1730-08d6d6f43666')
INSERT [dbo].[GioHocs] ([GioHocId], [To], [IsDisabled], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [From]) VALUES (N'6adcca66-0861-4209-acac-08d6fc64f42e', N'09:00', 0, CAST(N'2019-06-29T14:39:44.3116185' AS DateTime2), N'huynhquan.nguyen@gmail.com', NULL, NULL, N'07:00')
INSERT [dbo].[HocPhis] ([HocPhiId], [Gia], [IsDisabled], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [GhiChu], [NgayApDung]) VALUES (N'3fcf3c83-8726-4ae0-e5f2-08d6fc64e6fb', 1000000, 0, CAST(N'2019-06-29T14:39:22.1684528' AS DateTime2), N'huynhquan.nguyen@gmail.com', NULL, NULL, N'c', CAST(N'2019-06-29T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[HocVien_LopHocs] ([HocVien_LopHocId], [HocVienId], [LopHocId], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'ffae44b0-c73f-4034-9b10-08d6fefc144a', N'830a3916-99b3-45d3-922e-08d6fc651bab', N'3deb15cb-aa10-4ede-3ff2-08d6fc6506f8', CAST(N'2019-07-02T21:46:34.5111166' AS DateTime2), N'huynhquan.nguyen@gmail.com', NULL, NULL)
INSERT [dbo].[HocVien_LopHocs] ([HocVien_LopHocId], [HocVienId], [LopHocId], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'2fe668ed-948e-41c7-3ab3-08d70152bb3a', N'5d1dc20f-ef57-44d3-f0e6-08d6fc694ed7', N'3deb15cb-aa10-4ede-3ff2-08d6fc6506f8', CAST(N'2019-07-05T21:11:53.6256063' AS DateTime2), N'huynhquan.nguyen@gmail.com', NULL, NULL)
INSERT [dbo].[HocVien_LopHocs] ([HocVien_LopHocId], [HocVienId], [LopHocId], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'd5a521e8-2324-4469-63bc-08d704799f44', N'c3a1eead-a08e-4360-a64f-08d704799f3b', N'3deb15cb-aa10-4ede-3ff2-08d6fc6506f8', CAST(N'2019-07-09T21:27:50.5808265' AS DateTime2), N'huynhquan.nguyen@gmail.com', NULL, NULL)
INSERT [dbo].[HocVien_LopHocs] ([HocVien_LopHocId], [HocVienId], [LopHocId], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'ce1eeb94-cbc8-4722-63bd-08d704799f44', N'd47893a6-9afb-47a3-a650-08d704799f3b', N'3deb15cb-aa10-4ede-3ff2-08d6fc6506f8', CAST(N'2019-07-09T21:27:50.7016265' AS DateTime2), N'huynhquan.nguyen@gmail.com', NULL, NULL)
INSERT [dbo].[HocVien_LopHocs] ([HocVien_LopHocId], [HocVienId], [LopHocId], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'b687210c-2e8d-494a-b04f-08d704804d51', N'eac9d48c-4ae7-42cc-3419-08d704804d4a', N'3deb15cb-aa10-4ede-3ff2-08d6fc6506f8', CAST(N'2019-07-09T22:15:39.5721016' AS DateTime2), N'huynhquan.nguyen@gmail.com', NULL, NULL)
INSERT [dbo].[HocVien_NgayHocs] ([HocVien_NgayHocId], [LopHocId], [HocVienId], [NgayBatDau], [NgayKetThuc], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'b560ad4f-8233-4b85-41f8-08d6fc6522ec', N'3deb15cb-aa10-4ede-3ff2-08d6fc6506f8', N'830a3916-99b3-45d3-922e-08d6fc651bab', CAST(N'2019-07-12T00:00:00.0000000' AS DateTime2), NULL, CAST(N'2019-06-29T14:41:02.7288477' AS DateTime2), N'huynhquan.nguyen@gmail.com', CAST(N'2019-07-05T07:49:21.8106429' AS DateTime2), N'huynhquan.nguyen@gmail.com')
INSERT [dbo].[HocVien_NgayHocs] ([HocVien_NgayHocId], [LopHocId], [HocVienId], [NgayBatDau], [NgayKetThuc], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'ce83e541-4b14-4a4e-bf03-08d6fc69d761', N'3deb15cb-aa10-4ede-3ff2-08d6fc6506f8', N'5d1dc20f-ef57-44d3-f0e6-08d6fc694ed7', CAST(N'2019-06-29T00:00:00.0000000' AS DateTime2), NULL, CAST(N'2019-06-29T15:14:43.4700693' AS DateTime2), N'huynhquan.nguyen@gmail.com', CAST(N'2019-07-05T07:51:18.4155938' AS DateTime2), N'huynhquan.nguyen@gmail.com')
INSERT [dbo].[HocViens] ([HocVienId], [FullName], [Phone], [FacebookAccount], [ParentFullName], [ParentPhone], [QuanHeId], [ParentFacebookAccount], [NgaySinh], [EnglishName], [IsAppend], [IsDisabled], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'830a3916-99b3-45d3-922e-08d6fc651bab', N'p', N'1', N'1', N'1', N'', N'5aa5af51-1859-40ea-0233-08d6995fa408', N'', CAST(N'2019-06-29T00:00:00.0000000' AS DateTime2), N'p', 0, 0, CAST(N'2019-06-29T14:40:50.5492324' AS DateTime2), N'huynhquan.nguyen@gmail.com', CAST(N'2019-07-02T21:46:34.4724689' AS DateTime2), N'huynhquan.nguyen@gmail.com')
INSERT [dbo].[HocViens] ([HocVienId], [FullName], [Phone], [FacebookAccount], [ParentFullName], [ParentPhone], [QuanHeId], [ParentFacebookAccount], [NgaySinh], [EnglishName], [IsAppend], [IsDisabled], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'5d1dc20f-ef57-44d3-f0e6-08d6fc694ed7', N'mm', N'123', N'123', N'', N'', NULL, N'', CAST(N'2019-06-29T00:00:00.0000000' AS DateTime2), N'm', 0, 1, CAST(N'2019-06-29T15:10:54.3899095' AS DateTime2), N'huynhquan.nguyen@gmail.com', CAST(N'2019-07-09T22:04:21.0248330' AS DateTime2), N'huynhquan.nguyen@gmail.com')
INSERT [dbo].[HocViens] ([HocVienId], [FullName], [Phone], [FacebookAccount], [ParentFullName], [ParentPhone], [QuanHeId], [ParentFacebookAccount], [NgaySinh], [EnglishName], [IsAppend], [IsDisabled], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'33fd4442-f0a3-4a10-0a70-08d7015d9a5c', N'zxc', N'z', N'z', N'', N'', NULL, N'', CAST(N'2019-07-05T00:00:00.0000000' AS DateTime2), N'z', 0, 1, CAST(N'2019-07-05T22:29:42.9158266' AS DateTime2), N'huynhquan.nguyen@gmail.com', CAST(N'2019-07-05T23:23:41.7260280' AS DateTime2), N'huynhquan.nguyen@gmail.com')
INSERT [dbo].[HocViens] ([HocVienId], [FullName], [Phone], [FacebookAccount], [ParentFullName], [ParentPhone], [QuanHeId], [ParentFacebookAccount], [NgaySinh], [EnglishName], [IsAppend], [IsDisabled], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'4b528e30-379b-4238-fee4-08d7015f1444', N'cxzczx', N'21', N'D4', N'F4', N'I4', N'4e057a75-a459-4f82-0234-08d6995fa408', N'H4', CAST(N'1999-09-19T00:00:00.0000000' AS DateTime2), N'zx', 0, 1, CAST(N'2019-07-05T22:40:16.9581638' AS DateTime2), N'huynhquan.nguyen@gmail.com', CAST(N'2019-07-05T22:41:26.5616757' AS DateTime2), N'huynhquan.nguyen@gmail.com')
INSERT [dbo].[HocViens] ([HocVienId], [FullName], [Phone], [FacebookAccount], [ParentFullName], [ParentPhone], [QuanHeId], [ParentFacebookAccount], [NgaySinh], [EnglishName], [IsAppend], [IsDisabled], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'df428244-ce85-4edc-199c-08d701608889', N'phan phu', N'123', N'D3', N'', N'', NULL, N'', CAST(N'1991-09-29T00:00:00.0000000' AS DateTime2), N'â', 0, 1, CAST(N'2019-07-05T22:50:41.5201145' AS DateTime2), N'huynhquan.nguyen@gmail.com', CAST(N'2019-07-05T22:53:46.2599861' AS DateTime2), N'huynhquan.nguyen@gmail.com')
INSERT [dbo].[HocViens] ([HocVienId], [FullName], [Phone], [FacebookAccount], [ParentFullName], [ParentPhone], [QuanHeId], [ParentFacebookAccount], [NgaySinh], [EnglishName], [IsAppend], [IsDisabled], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'36f4815c-9456-407b-199d-08d701608889', N'cxzczx', N'21', N'D4', N'F4', N'I4', N'4e057a75-a459-4f82-0234-08d6995fa408', N'H4', CAST(N'1999-09-19T00:00:00.0000000' AS DateTime2), N'zx', 0, 1, CAST(N'2019-07-05T22:50:41.6688980' AS DateTime2), N'huynhquan.nguyen@gmail.com', CAST(N'2019-07-05T22:53:49.5672037' AS DateTime2), N'huynhquan.nguyen@gmail.com')
INSERT [dbo].[HocViens] ([HocVienId], [FullName], [Phone], [FacebookAccount], [ParentFullName], [ParentPhone], [QuanHeId], [ParentFacebookAccount], [NgaySinh], [EnglishName], [IsAppend], [IsDisabled], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'7cc008a5-a6ed-49a4-4516-08d70161067b', N'phan phu', N'123', N'123', N'', N'', NULL, N'', CAST(N'1991-09-29T00:00:00.0000000' AS DateTime2), N'â', 0, 1, CAST(N'2019-07-05T22:54:12.8371774' AS DateTime2), N'huynhquan.nguyen@gmail.com', CAST(N'2019-07-05T23:07:06.5029001' AS DateTime2), N'huynhquan.nguyen@gmail.com')
INSERT [dbo].[HocViens] ([HocVienId], [FullName], [Phone], [FacebookAccount], [ParentFullName], [ParentPhone], [QuanHeId], [ParentFacebookAccount], [NgaySinh], [EnglishName], [IsAppend], [IsDisabled], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'22a41c04-e10a-4c38-4517-08d70161067b', N'cxzczx', N'21', N'123', N'âbc', N'123', N'4e057a75-a459-4f82-0234-08d6995fa408', N'ád', CAST(N'1999-09-19T00:00:00.0000000' AS DateTime2), N'zx', 0, 1, CAST(N'2019-07-05T22:54:12.8582960' AS DateTime2), N'huynhquan.nguyen@gmail.com', CAST(N'2019-07-05T23:07:08.4282103' AS DateTime2), N'huynhquan.nguyen@gmail.com')
INSERT [dbo].[HocViens] ([HocVienId], [FullName], [Phone], [FacebookAccount], [ParentFullName], [ParentPhone], [QuanHeId], [ParentFacebookAccount], [NgaySinh], [EnglishName], [IsAppend], [IsDisabled], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'0d76aa80-9e36-4516-180d-08d701630ad5', N'phan phu', N'123', N'123', N'', N'', NULL, N'', CAST(N'1991-09-29T00:00:00.0000000' AS DateTime2), N'â', 0, 1, CAST(N'2019-07-05T23:08:39.1299230' AS DateTime2), N'huynhquan.nguyen@gmail.com', CAST(N'2019-07-05T23:10:24.4549802' AS DateTime2), N'huynhquan.nguyen@gmail.com')
INSERT [dbo].[HocViens] ([HocVienId], [FullName], [Phone], [FacebookAccount], [ParentFullName], [ParentPhone], [QuanHeId], [ParentFacebookAccount], [NgaySinh], [EnglishName], [IsAppend], [IsDisabled], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'4813a0fd-91e3-4e91-180e-08d701630ad5', N'cxzczx', N'21', N'123', N'âbc', N'123', N'4e057a75-a459-4f82-0234-08d6995fa408', N'ád', CAST(N'1999-09-19T00:00:00.0000000' AS DateTime2), N'zx', 0, 1, CAST(N'2019-07-05T23:08:39.1584902' AS DateTime2), N'huynhquan.nguyen@gmail.com', CAST(N'2019-07-05T23:10:21.6600454' AS DateTime2), N'huynhquan.nguyen@gmail.com')
INSERT [dbo].[HocViens] ([HocVienId], [FullName], [Phone], [FacebookAccount], [ParentFullName], [ParentPhone], [QuanHeId], [ParentFacebookAccount], [NgaySinh], [EnglishName], [IsAppend], [IsDisabled], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'9c6cdf4f-cd7e-4383-93cb-08d701635bee', N'phan phu', N'123', N'123', N'', N'', NULL, N'', CAST(N'1991-09-29T00:00:00.0000000' AS DateTime2), N'â', 0, 1, CAST(N'2019-07-05T23:10:55.1925586' AS DateTime2), N'huynhquan.nguyen@gmail.com', CAST(N'2019-07-05T23:11:29.6762919' AS DateTime2), N'huynhquan.nguyen@gmail.com')
INSERT [dbo].[HocViens] ([HocVienId], [FullName], [Phone], [FacebookAccount], [ParentFullName], [ParentPhone], [QuanHeId], [ParentFacebookAccount], [NgaySinh], [EnglishName], [IsAppend], [IsDisabled], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'7661b512-60be-417d-93cc-08d701635bee', N'cxzczx', N'21', N'123', N'âbc', N'123', N'4e057a75-a459-4f82-0234-08d6995fa408', N'ád', CAST(N'1999-09-19T00:00:00.0000000' AS DateTime2), N'zx', 0, 1, CAST(N'2019-07-05T23:10:55.2232537' AS DateTime2), N'huynhquan.nguyen@gmail.com', CAST(N'2019-07-05T23:11:27.6195736' AS DateTime2), N'huynhquan.nguyen@gmail.com')
INSERT [dbo].[HocViens] ([HocVienId], [FullName], [Phone], [FacebookAccount], [ParentFullName], [ParentPhone], [QuanHeId], [ParentFacebookAccount], [NgaySinh], [EnglishName], [IsAppend], [IsDisabled], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'3840d665-2edd-4950-3574-08d7016386a6', N'phan phu', N'123', N'123', N'', N'', NULL, N'', CAST(N'1991-09-29T00:00:00.0000000' AS DateTime2), N'â', 0, 1, CAST(N'2019-07-05T23:12:06.8448041' AS DateTime2), N'huynhquan.nguyen@gmail.com', CAST(N'2019-07-05T23:17:40.4588928' AS DateTime2), N'huynhquan.nguyen@gmail.com')
INSERT [dbo].[HocViens] ([HocVienId], [FullName], [Phone], [FacebookAccount], [ParentFullName], [ParentPhone], [QuanHeId], [ParentFacebookAccount], [NgaySinh], [EnglishName], [IsAppend], [IsDisabled], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'b74c2536-7659-45d3-3575-08d7016386a6', N'cxzczx', N'21', N'123', N'âbc', N'123', N'4e057a75-a459-4f82-0234-08d6995fa408', N'ád', CAST(N'1999-09-19T00:00:00.0000000' AS DateTime2), N'zx', 0, 1, CAST(N'2019-07-05T23:12:20.0358945' AS DateTime2), N'huynhquan.nguyen@gmail.com', CAST(N'2019-07-05T23:17:42.5296757' AS DateTime2), N'huynhquan.nguyen@gmail.com')
INSERT [dbo].[HocViens] ([HocVienId], [FullName], [Phone], [FacebookAccount], [ParentFullName], [ParentPhone], [QuanHeId], [ParentFacebookAccount], [NgaySinh], [EnglishName], [IsAppend], [IsDisabled], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'e661f6fc-de2a-474c-4713-08d70164553d', N'phan phu', N'123', N'123', N'', N'', NULL, N'', CAST(N'1991-09-29T00:00:00.0000000' AS DateTime2), N'â', 0, 1, CAST(N'2019-07-05T23:17:53.4600547' AS DateTime2), N'huynhquan.nguyen@gmail.com', CAST(N'2019-07-05T23:18:28.2988183' AS DateTime2), N'huynhquan.nguyen@gmail.com')
INSERT [dbo].[HocViens] ([HocVienId], [FullName], [Phone], [FacebookAccount], [ParentFullName], [ParentPhone], [QuanHeId], [ParentFacebookAccount], [NgaySinh], [EnglishName], [IsAppend], [IsDisabled], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'dedd25dd-c64d-423d-4714-08d70164553d', N'cxzczx', N'21', N'123', N'âbc', N'123', N'4e057a75-a459-4f82-0234-08d6995fa408', N'ád', CAST(N'1999-09-19T00:00:00.0000000' AS DateTime2), N'zx', 0, 1, CAST(N'2019-07-05T23:18:03.7490229' AS DateTime2), N'huynhquan.nguyen@gmail.com', CAST(N'2019-07-05T23:18:25.8733760' AS DateTime2), N'huynhquan.nguyen@gmail.com')
INSERT [dbo].[HocViens] ([HocVienId], [FullName], [Phone], [FacebookAccount], [ParentFullName], [ParentPhone], [QuanHeId], [ParentFacebookAccount], [NgaySinh], [EnglishName], [IsAppend], [IsDisabled], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'0f9f4be3-16e1-4c1e-4715-08d70164553d', N'phan phu', N'123', N'123', N'', N'', NULL, N'', CAST(N'1991-09-29T00:00:00.0000000' AS DateTime2), N'â', 0, 1, CAST(N'2019-07-05T23:18:43.7003834' AS DateTime2), N'huynhquan.nguyen@gmail.com', CAST(N'2019-07-05T23:19:47.1119750' AS DateTime2), N'huynhquan.nguyen@gmail.com')
INSERT [dbo].[HocViens] ([HocVienId], [FullName], [Phone], [FacebookAccount], [ParentFullName], [ParentPhone], [QuanHeId], [ParentFacebookAccount], [NgaySinh], [EnglishName], [IsAppend], [IsDisabled], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'259174bc-a9ab-4e19-4716-08d70164553d', N'cxzczx', N'21', N'123', N'âbc', N'123', N'4e057a75-a459-4f82-0234-08d6995fa408', N'ád', CAST(N'1999-09-19T00:00:00.0000000' AS DateTime2), N'zx', 0, 1, CAST(N'2019-07-05T23:18:43.7148148' AS DateTime2), N'huynhquan.nguyen@gmail.com', CAST(N'2019-07-05T23:19:43.1383411' AS DateTime2), N'huynhquan.nguyen@gmail.com')
INSERT [dbo].[HocViens] ([HocVienId], [FullName], [Phone], [FacebookAccount], [ParentFullName], [ParentPhone], [QuanHeId], [ParentFacebookAccount], [NgaySinh], [EnglishName], [IsAppend], [IsDisabled], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'8f537874-3373-465a-4717-08d70164553d', N'phan phu', N'123', N'123', N'', N'', NULL, N'', CAST(N'1991-09-29T00:00:00.0000000' AS DateTime2), N'â', 0, 1, CAST(N'2019-07-05T23:19:57.2395572' AS DateTime2), N'huynhquan.nguyen@gmail.com', CAST(N'2019-07-05T23:23:24.0205430' AS DateTime2), N'huynhquan.nguyen@gmail.com')
INSERT [dbo].[HocViens] ([HocVienId], [FullName], [Phone], [FacebookAccount], [ParentFullName], [ParentPhone], [QuanHeId], [ParentFacebookAccount], [NgaySinh], [EnglishName], [IsAppend], [IsDisabled], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'311d1168-375f-43fa-4718-08d70164553d', N'cxzczx', N'21', N'123', N'âbc', N'123', N'4e057a75-a459-4f82-0234-08d6995fa408', N'ád', CAST(N'1999-09-19T00:00:00.0000000' AS DateTime2), N'zx', 0, 1, CAST(N'2019-07-05T23:19:57.2628265' AS DateTime2), N'huynhquan.nguyen@gmail.com', CAST(N'2019-07-05T23:23:21.7749035' AS DateTime2), N'huynhquan.nguyen@gmail.com')
INSERT [dbo].[HocViens] ([HocVienId], [FullName], [Phone], [FacebookAccount], [ParentFullName], [ParentPhone], [QuanHeId], [ParentFacebookAccount], [NgaySinh], [EnglishName], [IsAppend], [IsDisabled], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'3e3cd2f3-c35b-4fbd-db5c-08d701651f7d', N'phan phu', N'123', N'123', N'', N'', NULL, N'', CAST(N'1991-09-29T00:00:00.0000000' AS DateTime2), N'â', 0, 1, CAST(N'2019-07-05T23:23:32.7830409' AS DateTime2), N'huynhquan.nguyen@gmail.com', CAST(N'2019-07-05T23:23:39.9433465' AS DateTime2), N'huynhquan.nguyen@gmail.com')
INSERT [dbo].[HocViens] ([HocVienId], [FullName], [Phone], [FacebookAccount], [ParentFullName], [ParentPhone], [QuanHeId], [ParentFacebookAccount], [NgaySinh], [EnglishName], [IsAppend], [IsDisabled], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'2d98600a-ca69-4c94-db5d-08d701651f7d', N'cxzczx', N'21', N'123', N'âbc', N'123', N'4e057a75-a459-4f82-0234-08d6995fa408', N'ád', CAST(N'1999-09-19T00:00:00.0000000' AS DateTime2), N'zx', 0, 1, CAST(N'2019-07-05T23:23:32.8111904' AS DateTime2), N'huynhquan.nguyen@gmail.com', CAST(N'2019-07-09T21:21:32.4007771' AS DateTime2), N'huynhquan.nguyen@gmail.com')
INSERT [dbo].[HocViens] ([HocVienId], [FullName], [Phone], [FacebookAccount], [ParentFullName], [ParentPhone], [QuanHeId], [ParentFacebookAccount], [NgaySinh], [EnglishName], [IsAppend], [IsDisabled], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'02446b9e-1929-4e3c-db5e-08d701651f7d', N'phan phu', N'123', N'123', N'', N'', NULL, N'', CAST(N'1991-09-29T00:00:00.0000000' AS DateTime2), N'â', 0, 1, CAST(N'2019-07-05T23:23:56.9859966' AS DateTime2), N'huynhquan.nguyen@gmail.com', CAST(N'2019-07-09T21:21:39.4089746' AS DateTime2), N'huynhquan.nguyen@gmail.com')
INSERT [dbo].[HocViens] ([HocVienId], [FullName], [Phone], [FacebookAccount], [ParentFullName], [ParentPhone], [QuanHeId], [ParentFacebookAccount], [NgaySinh], [EnglishName], [IsAppend], [IsDisabled], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'f6f75e69-9274-40c5-db5f-08d701651f7d', N'cxzczx', N'21', N'123', N'âbc', N'123', N'4e057a75-a459-4f82-0234-08d6995fa408', N'ád', CAST(N'1999-09-19T00:00:00.0000000' AS DateTime2), N'zx', 0, 1, CAST(N'2019-07-05T23:23:56.9903913' AS DateTime2), N'huynhquan.nguyen@gmail.com', CAST(N'2019-07-09T21:21:35.4715602' AS DateTime2), N'huynhquan.nguyen@gmail.com')
INSERT [dbo].[HocViens] ([HocVienId], [FullName], [Phone], [FacebookAccount], [ParentFullName], [ParentPhone], [QuanHeId], [ParentFacebookAccount], [NgaySinh], [EnglishName], [IsAppend], [IsDisabled], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'2b4c569f-0a89-44c9-161d-08d70478ccdb', N'phan phu', N'123', N'123', N'', N'', NULL, N'', CAST(N'1991-09-29T00:00:00.0000000' AS DateTime2), N'â', 0, 1, CAST(N'2019-07-09T21:21:57.5650141' AS DateTime2), N'huynhquan.nguyen@gmail.com', CAST(N'2019-07-09T21:22:02.5644167' AS DateTime2), N'huynhquan.nguyen@gmail.com')
INSERT [dbo].[HocViens] ([HocVienId], [FullName], [Phone], [FacebookAccount], [ParentFullName], [ParentPhone], [QuanHeId], [ParentFacebookAccount], [NgaySinh], [EnglishName], [IsAppend], [IsDisabled], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'8ccae302-3240-47ea-161e-08d70478ccdb', N'cxzczx', N'21', N'123', N'âbc', N'123', N'4e057a75-a459-4f82-0234-08d6995fa408', N'ád', CAST(N'1999-09-19T00:00:00.0000000' AS DateTime2), N'zx', 0, 1, CAST(N'2019-07-09T21:21:57.6004775' AS DateTime2), N'huynhquan.nguyen@gmail.com', CAST(N'2019-07-09T21:22:04.6734426' AS DateTime2), N'huynhquan.nguyen@gmail.com')
INSERT [dbo].[HocViens] ([HocVienId], [FullName], [Phone], [FacebookAccount], [ParentFullName], [ParentPhone], [QuanHeId], [ParentFacebookAccount], [NgaySinh], [EnglishName], [IsAppend], [IsDisabled], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'c3a1eead-a08e-4360-a64f-08d704799f3b', N'phan phu', N'123', N'123', N'', N'', NULL, N'', CAST(N'1991-09-29T00:00:00.0000000' AS DateTime2), N'â', 0, 1, CAST(N'2019-07-09T21:27:50.5074025' AS DateTime2), N'huynhquan.nguyen@gmail.com', CAST(N'2019-07-09T22:04:18.4982948' AS DateTime2), N'huynhquan.nguyen@gmail.com')
INSERT [dbo].[HocViens] ([HocVienId], [FullName], [Phone], [FacebookAccount], [ParentFullName], [ParentPhone], [QuanHeId], [ParentFacebookAccount], [NgaySinh], [EnglishName], [IsAppend], [IsDisabled], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'd47893a6-9afb-47a3-a650-08d704799f3b', N'cxzczx', N'21', N'123', N'âbc', N'123', N'4e057a75-a459-4f82-0234-08d6995fa408', N'ád', CAST(N'1999-09-19T00:00:00.0000000' AS DateTime2), N'zx', 0, 1, CAST(N'2019-07-09T21:27:50.6981726' AS DateTime2), N'huynhquan.nguyen@gmail.com', CAST(N'2019-07-09T22:05:20.0386185' AS DateTime2), N'huynhquan.nguyen@gmail.com')
INSERT [dbo].[HocViens] ([HocVienId], [FullName], [Phone], [FacebookAccount], [ParentFullName], [ParentPhone], [QuanHeId], [ParentFacebookAccount], [NgaySinh], [EnglishName], [IsAppend], [IsDisabled], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'b01cbe5c-679c-4308-3099-08d7047df570', N'vvvv123', N'123', N'', N'', N'', NULL, N'', CAST(N'2019-07-09T00:00:00.0000000' AS DateTime2), N'', 0, 1, CAST(N'2019-07-09T21:58:53.1184784' AS DateTime2), N'huynhquan.nguyen@gmail.com', CAST(N'2019-07-09T22:04:08.0480746' AS DateTime2), N'huynhquan.nguyen@gmail.com')
INSERT [dbo].[HocViens] ([HocVienId], [FullName], [Phone], [FacebookAccount], [ParentFullName], [ParentPhone], [QuanHeId], [ParentFacebookAccount], [NgaySinh], [EnglishName], [IsAppend], [IsDisabled], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'eac9d48c-4ae7-42cc-3419-08d704804d4a', N'nmnn', N'99999', N'', N'', N'', NULL, N'', CAST(N'1991-09-12T00:00:00.0000000' AS DateTime2), N'', 0, 0, CAST(N'2019-07-09T22:15:39.5047582' AS DateTime2), N'huynhquan.nguyen@gmail.com', NULL, NULL)
INSERT [dbo].[HocViens] ([HocVienId], [FullName], [Phone], [FacebookAccount], [ParentFullName], [ParentPhone], [QuanHeId], [ParentFacebookAccount], [NgaySinh], [EnglishName], [IsAppend], [IsDisabled], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'3dd88aff-0793-4ec1-341a-08d704804d4a', N'c', N'z', N'', N'', N'', NULL, N'', CAST(N'2019-07-09T00:00:00.0000000' AS DateTime2), N'', 0, 0, CAST(N'2019-07-09T22:20:06.6727192' AS DateTime2), N'huynhquan.nguyen@gmail.com', NULL, NULL)
INSERT [dbo].[HocViens] ([HocVienId], [FullName], [Phone], [FacebookAccount], [ParentFullName], [ParentPhone], [QuanHeId], [ParentFacebookAccount], [NgaySinh], [EnglishName], [IsAppend], [IsDisabled], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'1184cbdf-5d00-4a4b-22ec-08d7054c8e69', N'pp', N'123', N'', N'', N'', NULL, N'', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'', 0, 1, CAST(N'2019-07-10T22:37:46.0925996' AS DateTime2), N'huynhquan.nguyen@gmail.com', CAST(N'2019-07-10T22:38:49.6840354' AS DateTime2), N'huynhquan.nguyen@gmail.com')
INSERT [dbo].[HocViens] ([HocVienId], [FullName], [Phone], [FacebookAccount], [ParentFullName], [ParentPhone], [QuanHeId], [ParentFacebookAccount], [NgaySinh], [EnglishName], [IsAppend], [IsDisabled], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'e9a72bff-c5c6-4ae5-5c68-08d7054da1c4', N'p', N'5123', N'', N'', N'', NULL, N'', CAST(N'2019-07-04T00:00:00.0000000' AS DateTime2), N'', 0, 0, CAST(N'2019-07-10T22:45:28.0675292' AS DateTime2), N'huynhquan.nguyen@gmail.com', CAST(N'2019-07-11T21:39:10.5179396' AS DateTime2), N'huynhquan.nguyen@gmail.com')
INSERT [dbo].[KhoaHocs] ([KhoaHocId], [Name], [IsDisabled], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'1a86418f-c9fa-47d7-4c0f-08d699557c37', N'Anh Văn Giao Tiếp', 0, CAST(N'2019-02-23T13:18:13.8018850' AS DateTime2), N'huynhquan.nguyen@gmail.com', CAST(N'2019-02-23T14:23:29.6023329' AS DateTime2), N'huynhquan.nguyen@gmail.com')
INSERT [dbo].[KhoaHocs] ([KhoaHocId], [Name], [IsDisabled], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'32b1d9a4-eb39-440b-4c10-08d699557c37', N'Anh Văn Thiếu Nhi', 0, CAST(N'2019-02-23T13:19:17.3074759' AS DateTime2), N'huynhquan.nguyen@gmail.com', CAST(N'2019-02-23T13:21:09.1332788' AS DateTime2), N'huynhquan.nguyen@gmail.com')
INSERT [dbo].[KhoaHocs] ([KhoaHocId], [Name], [IsDisabled], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'9245eb2d-faa8-4ed6-e800-08d6c608db85', N'TOEIC', 0, CAST(N'2019-04-21T10:24:26.4512082' AS DateTime2), N'huynhquan.nguyen@gmail.com', NULL, NULL)
INSERT [dbo].[LoaiGiaoViens] ([LoaiGiaoVienId], [Name], [IsDisabled], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'19f5c9fa-9601-4d23-1730-08d6d6f43666', N'Full time', 0, CAST(N'2019-05-12T23:09:29.2323897' AS DateTime2), N'huynhquan.nguyen@gmail.com', NULL, NULL)
INSERT [dbo].[LoaiGiaoViens] ([LoaiGiaoVienId], [Name], [IsDisabled], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'adc1dcd8-ed5c-4781-1731-08d6d6f43666', N'Part time', 0, CAST(N'2019-05-12T23:09:40.1262052' AS DateTime2), N'huynhquan.nguyen@gmail.com', CAST(N'2019-05-12T23:09:53.6245447' AS DateTime2), N'huynhquan.nguyen@gmail.com')
INSERT [dbo].[LoaiGiaoViens] ([LoaiGiaoVienId], [Name], [IsDisabled], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'1d46067d-07d6-4a9a-1732-08d6d6f43666', N'GV Nước Ngoài', 0, CAST(N'2019-05-12T23:09:58.4615535' AS DateTime2), N'huynhquan.nguyen@gmail.com', CAST(N'2019-05-12T23:10:03.8673019' AS DateTime2), N'huynhquan.nguyen@gmail.com')
INSERT [dbo].[LopHoc_DiemDanhs] ([LopHoc_DiemDanhId], [LopHocId], [HocVienId], [IsOff], [NgayDiemDanh], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [IsDuocNghi]) VALUES (N'ff551573-fe20-4ade-f20b-08d6fc652cbd', N'3deb15cb-aa10-4ede-3ff2-08d6fc6506f8', N'830a3916-99b3-45d3-922e-08d6fc651bab', 0, CAST(N'2019-06-03T00:00:00.0000000' AS DateTime2), CAST(N'2019-06-29T14:41:19.2010985' AS DateTime2), N'huynhquan.nguyen@gmail.com', NULL, NULL, NULL)
INSERT [dbo].[LopHoc_DiemDanhs] ([LopHoc_DiemDanhId], [LopHocId], [HocVienId], [IsOff], [NgayDiemDanh], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [IsDuocNghi]) VALUES (N'094a3253-c755-4872-f20c-08d6fc652cbd', N'3deb15cb-aa10-4ede-3ff2-08d6fc6506f8', N'830a3916-99b3-45d3-922e-08d6fc651bab', 0, CAST(N'2019-06-05T00:00:00.0000000' AS DateTime2), CAST(N'2019-06-29T14:42:39.5722787' AS DateTime2), N'huynhquan.nguyen@gmail.com', NULL, NULL, NULL)
INSERT [dbo].[LopHoc_DiemDanhs] ([LopHoc_DiemDanhId], [LopHocId], [HocVienId], [IsOff], [NgayDiemDanh], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [IsDuocNghi]) VALUES (N'8f1790ad-abb9-44ac-8e4e-08d6fc65f444', N'3deb15cb-aa10-4ede-3ff2-08d6fc6506f8', N'830a3916-99b3-45d3-922e-08d6fc651bab', 1, CAST(N'2019-06-07T00:00:00.0000000' AS DateTime2), CAST(N'2019-06-29T14:46:53.9350431' AS DateTime2), N'huynhquan.nguyen@gmail.com', NULL, NULL, NULL)
INSERT [dbo].[LopHoc_DiemDanhs] ([LopHoc_DiemDanhId], [LopHocId], [HocVienId], [IsOff], [NgayDiemDanh], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [IsDuocNghi]) VALUES (N'61fb26ce-ee37-4dff-bba2-08d6fc695703', N'3deb15cb-aa10-4ede-3ff2-08d6fc6506f8', N'5d1dc20f-ef57-44d3-f0e6-08d6fc694ed7', 0, CAST(N'2019-06-03T00:00:00.0000000' AS DateTime2), CAST(N'2019-06-29T15:11:08.1109420' AS DateTime2), N'huynhquan.nguyen@gmail.com', NULL, NULL, NULL)
INSERT [dbo].[LopHoc_DiemDanhs] ([LopHoc_DiemDanhId], [LopHocId], [HocVienId], [IsOff], [NgayDiemDanh], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [IsDuocNghi]) VALUES (N'adcc7a20-d9e2-4ab2-bba3-08d6fc695703', N'3deb15cb-aa10-4ede-3ff2-08d6fc6506f8', N'5d1dc20f-ef57-44d3-f0e6-08d6fc694ed7', 1, CAST(N'2019-06-05T00:00:00.0000000' AS DateTime2), CAST(N'2019-06-29T15:11:13.3953626' AS DateTime2), N'huynhquan.nguyen@gmail.com', NULL, NULL, NULL)
INSERT [dbo].[LopHoc_DiemDanhs] ([LopHoc_DiemDanhId], [LopHocId], [HocVienId], [IsOff], [NgayDiemDanh], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [IsDuocNghi]) VALUES (N'054d4f53-4fab-4937-bba4-08d6fc695703', N'3deb15cb-aa10-4ede-3ff2-08d6fc6506f8', N'5d1dc20f-ef57-44d3-f0e6-08d6fc694ed7', 0, CAST(N'2019-06-07T00:00:00.0000000' AS DateTime2), CAST(N'2019-06-29T15:11:26.4605057' AS DateTime2), N'huynhquan.nguyen@gmail.com', NULL, NULL, NULL)
INSERT [dbo].[LopHoc_DiemDanhs] ([LopHoc_DiemDanhId], [LopHocId], [HocVienId], [IsOff], [NgayDiemDanh], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [IsDuocNghi]) VALUES (N'0b32d159-4df9-4a1b-12cd-08d6fc6a4895', N'3deb15cb-aa10-4ede-3ff2-08d6fc6506f8', N'830a3916-99b3-45d3-922e-08d6fc651bab', 0, CAST(N'2019-07-05T00:00:00.0000000' AS DateTime2), CAST(N'2019-06-29T15:17:53.3781545' AS DateTime2), N'huynhquan.nguyen@gmail.com', NULL, NULL, NULL)
INSERT [dbo].[LopHoc_DiemDanhs] ([LopHoc_DiemDanhId], [LopHocId], [HocVienId], [IsOff], [NgayDiemDanh], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [IsDuocNghi]) VALUES (N'5c3e161d-e5fb-4442-12ce-08d6fc6a4895', N'3deb15cb-aa10-4ede-3ff2-08d6fc6506f8', N'5d1dc20f-ef57-44d3-f0e6-08d6fc694ed7', 1, CAST(N'2019-07-05T00:00:00.0000000' AS DateTime2), CAST(N'2019-06-29T15:17:55.5336019' AS DateTime2), N'huynhquan.nguyen@gmail.com', NULL, NULL, NULL)
INSERT [dbo].[LopHoc_DiemDanhs] ([LopHoc_DiemDanhId], [LopHocId], [HocVienId], [IsOff], [NgayDiemDanh], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [IsDuocNghi]) VALUES (N'05f22c31-3980-4b87-f7c8-08d6fcb0fe64', N'3deb15cb-aa10-4ede-3ff2-08d6fc6506f8', N'830a3916-99b3-45d3-922e-08d6fc651bab', 1, CAST(N'2019-06-21T00:00:00.0000000' AS DateTime2), CAST(N'2019-06-29T23:44:03.1903066' AS DateTime2), N'huynhquan.nguyen@gmail.com', NULL, NULL, 1)
INSERT [dbo].[LopHoc_DiemDanhs] ([LopHoc_DiemDanhId], [LopHocId], [HocVienId], [IsOff], [NgayDiemDanh], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [IsDuocNghi]) VALUES (N'5caf0ebc-c1a2-45f9-f7c9-08d6fcb0fe64', N'3deb15cb-aa10-4ede-3ff2-08d6fc6506f8', N'5d1dc20f-ef57-44d3-f0e6-08d6fc694ed7', 1, CAST(N'2019-06-21T00:00:00.0000000' AS DateTime2), CAST(N'2019-06-29T23:44:03.2144920' AS DateTime2), N'huynhquan.nguyen@gmail.com', NULL, NULL, 1)
INSERT [dbo].[LopHoc_DiemDanhs] ([LopHoc_DiemDanhId], [LopHocId], [HocVienId], [IsOff], [NgayDiemDanh], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [IsDuocNghi]) VALUES (N'd5f1d1dc-5f2c-426b-9127-08d7061cc7c7', N'3deb15cb-aa10-4ede-3ff2-08d6fc6506f8', N'830a3916-99b3-45d3-922e-08d6fc651bab', 1, CAST(N'2019-07-12T00:00:00.0000000' AS DateTime2), CAST(N'2019-07-11T23:28:17.6744365' AS DateTime2), N'huynhquan.nguyen@gmail.com', NULL, NULL, 1)
INSERT [dbo].[LopHoc_DiemDanhs] ([LopHoc_DiemDanhId], [LopHocId], [HocVienId], [IsOff], [NgayDiemDanh], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [IsDuocNghi]) VALUES (N'9ec726e2-0855-4337-9128-08d7061cc7c7', N'3deb15cb-aa10-4ede-3ff2-08d6fc6506f8', N'5d1dc20f-ef57-44d3-f0e6-08d6fc694ed7', 1, CAST(N'2019-07-12T00:00:00.0000000' AS DateTime2), CAST(N'2019-07-11T23:28:17.6981083' AS DateTime2), N'huynhquan.nguyen@gmail.com', NULL, NULL, 1)
INSERT [dbo].[LopHocs] ([LopHocId], [Name], [IsDisabled], [IsGraduated], [IsCanceled], [KhoaHocId], [NgayHocId], [GioHocId], [NgayKhaiGiang], [NgayKetThuc], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [HocPhiId], [GiaoVienId]) VALUES (N'3deb15cb-aa10-4ede-3ff2-08d6fc6506f8', N'test', 0, 0, 0, N'32b1d9a4-eb39-440b-4c10-08d699557c37', N'e61f3cde-2c1c-4f8d-fb87-08d699a71d96', N'6adcca66-0861-4209-acac-08d6fc64f42e', CAST(N'2019-06-29T00:00:00.0000000' AS DateTime2), NULL, CAST(N'2019-06-29T14:40:15.8295132' AS DateTime2), N'huynhquan.nguyen@gmail.com', CAST(N'2019-07-14T14:41:16.8157830' AS DateTime2), N'huynhquan.nguyen@gmail.com', N'3fcf3c83-8726-4ae0-e5f2-08d6fc64e6fb', N'1679f635-8e92-4f8e-c3ea-08d6fc64d5bc')
INSERT [dbo].[LopHocs] ([LopHocId], [Name], [IsDisabled], [IsGraduated], [IsCanceled], [KhoaHocId], [NgayHocId], [GioHocId], [NgayKhaiGiang], [NgayKetThuc], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [HocPhiId], [GiaoVienId]) VALUES (N'23139f38-753c-4027-213f-08d708373a49', N'cx321', 0, 0, 0, N'9245eb2d-faa8-4ed6-e800-08d6c608db85', N'52c5e972-99a0-480a-fb88-08d699a71d96', N'6adcca66-0861-4209-acac-08d6fc64f42e', CAST(N'2019-07-14T00:00:00.0000000' AS DateTime2), NULL, CAST(N'2019-07-14T15:42:39.0245907' AS DateTime2), N'huynhquan.nguyen@gmail.com', CAST(N'2019-07-14T15:42:51.3342187' AS DateTime2), N'huynhquan.nguyen@gmail.com', N'3fcf3c83-8726-4ae0-e5f2-08d6fc64e6fb', N'1679f635-8e92-4f8e-c3ea-08d6fc64d5bc')
INSERT [dbo].[NgayHocs] ([NgayHocId], [Name], [IsDisabled], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'e61f3cde-2c1c-4f8d-fb87-08d699a71d96', N'2 - 4 - 6', 0, CAST(N'2019-02-23T22:53:55.4281598' AS DateTime2), N'huynhquan.nguyen@gmail.com', NULL, NULL)
INSERT [dbo].[NgayHocs] ([NgayHocId], [Name], [IsDisabled], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'52c5e972-99a0-480a-fb88-08d699a71d96', N'3 - 5 - 7', 0, CAST(N'2019-02-23T22:54:04.5227038' AS DateTime2), N'huynhquan.nguyen@gmail.com', NULL, NULL)
INSERT [dbo].[NhanVienKhacs] ([NhanVienKhacId], [Name], [Phone], [BasicSalary], [FacebookAccount], [DiaChi], [CMND], [IsDisabled], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'd0e37143-cad0-4add-8172-08d6f59cf20d', N'Bảo Vệ', N'123123', 2000000, N'123ád', N'123', N'123', 0, CAST(N'2019-06-20T23:32:54.4002020' AS DateTime2), N'huynhquan.nguyen@gmail.com', CAST(N'2019-06-20T23:33:44.6407954' AS DateTime2), N'huynhquan.nguyen@gmail.com')
INSERT [dbo].[NhanVienKhacs] ([NhanVienKhacId], [Name], [Phone], [BasicSalary], [FacebookAccount], [DiaChi], [CMND], [IsDisabled], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'fd9f817a-24c6-4c1e-8173-08d6f59cf20d', N'Kế Toán', N'213123', 5000000, N'123', N'12321', N'1232', 0, CAST(N'2019-06-20T23:33:33.3894403' AS DateTime2), N'huynhquan.nguyen@gmail.com', NULL, NULL)
INSERT [dbo].[QuanHes] ([QuanHeId], [Name], [IsDisabled], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'5c112683-b273-4fca-0232-08d6995fa408', N'Cha', 0, CAST(N'2019-02-23T14:22:17.2279719' AS DateTime2), N'huynhquan.nguyen@gmail.com', NULL, NULL)
INSERT [dbo].[QuanHes] ([QuanHeId], [Name], [IsDisabled], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'5aa5af51-1859-40ea-0233-08d6995fa408', N'Mẹ', 0, CAST(N'2019-02-23T14:22:24.7754363' AS DateTime2), N'huynhquan.nguyen@gmail.com', NULL, NULL)
INSERT [dbo].[QuanHes] ([QuanHeId], [Name], [IsDisabled], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'4e057a75-a459-4f82-0234-08d6995fa408', N'Anh', 0, CAST(N'2019-02-23T14:22:27.9763552' AS DateTime2), N'huynhquan.nguyen@gmail.com', NULL, NULL)
INSERT [dbo].[QuanHes] ([QuanHeId], [Name], [IsDisabled], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'ae443c7b-084e-4a61-0235-08d6995fa408', N'Chị', 0, CAST(N'2019-02-23T14:22:31.2330267' AS DateTime2), N'huynhquan.nguyen@gmail.com', NULL, NULL)
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 07/16/2019 7:55:05 AM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 07/16/2019 7:55:05 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 07/16/2019 7:55:05 AM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 07/16/2019 7:55:05 AM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 07/16/2019 7:55:05 AM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 07/16/2019 7:55:05 AM ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 07/16/2019 7:55:05 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_GiaoViens_LoaiGiaoVienId]    Script Date: 07/16/2019 7:55:05 AM ******/
CREATE NONCLUSTERED INDEX [IX_GiaoViens_LoaiGiaoVienId] ON [dbo].[GiaoViens]
(
	[LoaiGiaoVienId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_HocVien_LopHocs_HocVienId]    Script Date: 07/16/2019 7:55:05 AM ******/
CREATE NONCLUSTERED INDEX [IX_HocVien_LopHocs_HocVienId] ON [dbo].[HocVien_LopHocs]
(
	[HocVienId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_HocVien_LopHocs_LopHocId]    Script Date: 07/16/2019 7:55:05 AM ******/
CREATE NONCLUSTERED INDEX [IX_HocVien_LopHocs_LopHocId] ON [dbo].[HocVien_LopHocs]
(
	[LopHocId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_HocVien_NgayHocs_HocVienId]    Script Date: 07/16/2019 7:55:05 AM ******/
CREATE NONCLUSTERED INDEX [IX_HocVien_NgayHocs_HocVienId] ON [dbo].[HocVien_NgayHocs]
(
	[HocVienId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_HocVien_NgayHocs_LopHocId]    Script Date: 07/16/2019 7:55:05 AM ******/
CREATE NONCLUSTERED INDEX [IX_HocVien_NgayHocs_LopHocId] ON [dbo].[HocVien_NgayHocs]
(
	[LopHocId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_HocVien_Nos_HocVienId]    Script Date: 07/16/2019 7:55:05 AM ******/
CREATE NONCLUSTERED INDEX [IX_HocVien_Nos_HocVienId] ON [dbo].[HocVien_Nos]
(
	[HocVienId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_HocVien_Nos_LopHocId]    Script Date: 07/16/2019 7:55:05 AM ******/
CREATE NONCLUSTERED INDEX [IX_HocVien_Nos_LopHocId] ON [dbo].[HocVien_Nos]
(
	[LopHocId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_HocViens_QuanHeId]    Script Date: 07/16/2019 7:55:05 AM ******/
CREATE NONCLUSTERED INDEX [IX_HocViens_QuanHeId] ON [dbo].[HocViens]
(
	[QuanHeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_LopHoc_DiemDanhs_HocVienId]    Script Date: 07/16/2019 7:55:05 AM ******/
CREATE NONCLUSTERED INDEX [IX_LopHoc_DiemDanhs_HocVienId] ON [dbo].[LopHoc_DiemDanhs]
(
	[HocVienId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_LopHoc_DiemDanhs_LopHocId]    Script Date: 07/16/2019 7:55:05 AM ******/
CREATE NONCLUSTERED INDEX [IX_LopHoc_DiemDanhs_LopHocId] ON [dbo].[LopHoc_DiemDanhs]
(
	[LopHocId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_LopHocs_GiaoVienId]    Script Date: 07/16/2019 7:55:05 AM ******/
CREATE NONCLUSTERED INDEX [IX_LopHocs_GiaoVienId] ON [dbo].[LopHocs]
(
	[GiaoVienId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_LopHocs_GioHocId]    Script Date: 07/16/2019 7:55:05 AM ******/
CREATE NONCLUSTERED INDEX [IX_LopHocs_GioHocId] ON [dbo].[LopHocs]
(
	[GioHocId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_LopHocs_HocPhiId]    Script Date: 07/16/2019 7:55:05 AM ******/
CREATE NONCLUSTERED INDEX [IX_LopHocs_HocPhiId] ON [dbo].[LopHocs]
(
	[HocPhiId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_LopHocs_KhoaHocId]    Script Date: 07/16/2019 7:55:05 AM ******/
CREATE NONCLUSTERED INDEX [IX_LopHocs_KhoaHocId] ON [dbo].[LopHocs]
(
	[KhoaHocId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_LopHocs_NgayHocId]    Script Date: 07/16/2019 7:55:05 AM ******/
CREATE NONCLUSTERED INDEX [IX_LopHocs_NgayHocId] ON [dbo].[LopHocs]
(
	[NgayHocId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ThongKe_DoanhThuHocPhis_HocVienId]    Script Date: 07/16/2019 7:55:05 AM ******/
CREATE NONCLUSTERED INDEX [IX_ThongKe_DoanhThuHocPhis_HocVienId] ON [dbo].[ThongKe_DoanhThuHocPhis]
(
	[HocVienId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ThongKe_DoanhThuHocPhis_LopHocId]    Script Date: 07/16/2019 7:55:05 AM ******/
CREATE NONCLUSTERED INDEX [IX_ThongKe_DoanhThuHocPhis_LopHocId] ON [dbo].[ThongKe_DoanhThuHocPhis]
(
	[LopHocId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ChiPhiCoDinhs] ADD  DEFAULT ((0)) FOR [IsDisabled]
GO
ALTER TABLE [dbo].[LopHocs] ADD  DEFAULT ('00000000-0000-0000-0000-000000000000') FOR [GiaoVienId]
GO
ALTER TABLE [dbo].[ThongKe_DoanhThuHocPhis] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [NgayDong]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[GiaoViens]  WITH CHECK ADD  CONSTRAINT [FK_GiaoViens_LoaiGiaoViens_LoaiGiaoVienId] FOREIGN KEY([LoaiGiaoVienId])
REFERENCES [dbo].[LoaiGiaoViens] ([LoaiGiaoVienId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GiaoViens] CHECK CONSTRAINT [FK_GiaoViens_LoaiGiaoViens_LoaiGiaoVienId]
GO
ALTER TABLE [dbo].[HocVien_LopHocs]  WITH CHECK ADD  CONSTRAINT [FK_HocVien_LopHocs_HocViens_HocVienId] FOREIGN KEY([HocVienId])
REFERENCES [dbo].[HocViens] ([HocVienId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[HocVien_LopHocs] CHECK CONSTRAINT [FK_HocVien_LopHocs_HocViens_HocVienId]
GO
ALTER TABLE [dbo].[HocVien_LopHocs]  WITH CHECK ADD  CONSTRAINT [FK_HocVien_LopHocs_LopHocs_LopHocId] FOREIGN KEY([LopHocId])
REFERENCES [dbo].[LopHocs] ([LopHocId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[HocVien_LopHocs] CHECK CONSTRAINT [FK_HocVien_LopHocs_LopHocs_LopHocId]
GO
ALTER TABLE [dbo].[HocVien_NgayHocs]  WITH CHECK ADD  CONSTRAINT [FK_HocVien_NgayHocs_HocViens_HocVienId] FOREIGN KEY([HocVienId])
REFERENCES [dbo].[HocViens] ([HocVienId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[HocVien_NgayHocs] CHECK CONSTRAINT [FK_HocVien_NgayHocs_HocViens_HocVienId]
GO
ALTER TABLE [dbo].[HocVien_NgayHocs]  WITH CHECK ADD  CONSTRAINT [FK_HocVien_NgayHocs_LopHocs_LopHocId] FOREIGN KEY([LopHocId])
REFERENCES [dbo].[LopHocs] ([LopHocId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[HocVien_NgayHocs] CHECK CONSTRAINT [FK_HocVien_NgayHocs_LopHocs_LopHocId]
GO
ALTER TABLE [dbo].[HocVien_Nos]  WITH CHECK ADD  CONSTRAINT [FK_HocVien_Nos_HocViens_HocVienId] FOREIGN KEY([HocVienId])
REFERENCES [dbo].[HocViens] ([HocVienId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[HocVien_Nos] CHECK CONSTRAINT [FK_HocVien_Nos_HocViens_HocVienId]
GO
ALTER TABLE [dbo].[HocVien_Nos]  WITH CHECK ADD  CONSTRAINT [FK_HocVien_Nos_LopHocs_LopHocId] FOREIGN KEY([LopHocId])
REFERENCES [dbo].[LopHocs] ([LopHocId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[HocVien_Nos] CHECK CONSTRAINT [FK_HocVien_Nos_LopHocs_LopHocId]
GO
ALTER TABLE [dbo].[HocViens]  WITH CHECK ADD  CONSTRAINT [FK_HocViens_QuanHes_QuanHeId] FOREIGN KEY([QuanHeId])
REFERENCES [dbo].[QuanHes] ([QuanHeId])
GO
ALTER TABLE [dbo].[HocViens] CHECK CONSTRAINT [FK_HocViens_QuanHes_QuanHeId]
GO
ALTER TABLE [dbo].[LopHoc_DiemDanhs]  WITH CHECK ADD  CONSTRAINT [FK_LopHoc_DiemDanhs_HocViens_HocVienId] FOREIGN KEY([HocVienId])
REFERENCES [dbo].[HocViens] ([HocVienId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LopHoc_DiemDanhs] CHECK CONSTRAINT [FK_LopHoc_DiemDanhs_HocViens_HocVienId]
GO
ALTER TABLE [dbo].[LopHoc_DiemDanhs]  WITH CHECK ADD  CONSTRAINT [FK_LopHoc_DiemDanhs_LopHocs_LopHocId] FOREIGN KEY([LopHocId])
REFERENCES [dbo].[LopHocs] ([LopHocId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LopHoc_DiemDanhs] CHECK CONSTRAINT [FK_LopHoc_DiemDanhs_LopHocs_LopHocId]
GO
ALTER TABLE [dbo].[LopHocs]  WITH CHECK ADD  CONSTRAINT [FK_LopHocs_GiaoViens_GiaoVienId] FOREIGN KEY([GiaoVienId])
REFERENCES [dbo].[GiaoViens] ([GiaoVienId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LopHocs] CHECK CONSTRAINT [FK_LopHocs_GiaoViens_GiaoVienId]
GO
ALTER TABLE [dbo].[LopHocs]  WITH CHECK ADD  CONSTRAINT [FK_LopHocs_GioHocs_GioHocId] FOREIGN KEY([GioHocId])
REFERENCES [dbo].[GioHocs] ([GioHocId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LopHocs] CHECK CONSTRAINT [FK_LopHocs_GioHocs_GioHocId]
GO
ALTER TABLE [dbo].[LopHocs]  WITH CHECK ADD  CONSTRAINT [FK_LopHocs_HocPhis_HocPhiId] FOREIGN KEY([HocPhiId])
REFERENCES [dbo].[HocPhis] ([HocPhiId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LopHocs] CHECK CONSTRAINT [FK_LopHocs_HocPhis_HocPhiId]
GO
ALTER TABLE [dbo].[LopHocs]  WITH CHECK ADD  CONSTRAINT [FK_LopHocs_KhoaHocs_KhoaHocId] FOREIGN KEY([KhoaHocId])
REFERENCES [dbo].[KhoaHocs] ([KhoaHocId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LopHocs] CHECK CONSTRAINT [FK_LopHocs_KhoaHocs_KhoaHocId]
GO
ALTER TABLE [dbo].[LopHocs]  WITH CHECK ADD  CONSTRAINT [FK_LopHocs_NgayHocs_NgayHocId] FOREIGN KEY([NgayHocId])
REFERENCES [dbo].[NgayHocs] ([NgayHocId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LopHocs] CHECK CONSTRAINT [FK_LopHocs_NgayHocs_NgayHocId]
GO
ALTER TABLE [dbo].[ThongKe_DoanhThuHocPhis]  WITH CHECK ADD  CONSTRAINT [FK_ThongKe_DoanhThuHocPhis_HocViens_HocVienId] FOREIGN KEY([HocVienId])
REFERENCES [dbo].[HocViens] ([HocVienId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ThongKe_DoanhThuHocPhis] CHECK CONSTRAINT [FK_ThongKe_DoanhThuHocPhis_HocViens_HocVienId]
GO
ALTER TABLE [dbo].[ThongKe_DoanhThuHocPhis]  WITH CHECK ADD  CONSTRAINT [FK_ThongKe_DoanhThuHocPhis_LopHocs_LopHocId] FOREIGN KEY([LopHocId])
REFERENCES [dbo].[LopHocs] ([LopHocId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ThongKe_DoanhThuHocPhis] CHECK CONSTRAINT [FK_ThongKe_DoanhThuHocPhis_LopHocs_LopHocId]
GO
USE [master]
GO
ALTER DATABASE [UpCenter] SET  READ_WRITE 
GO
