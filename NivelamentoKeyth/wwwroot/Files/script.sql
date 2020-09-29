USE [master]
GO

/****** Object:  Database [NivelamentoKeyth]    Script Date: 28/09/2020 23:12:09 ******/
CREATE DATABASE [NivelamentoKeyth]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'NivelamentoKeyth', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\NivelamentoKeyth.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'NivelamentoKeyth_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\NivelamentoKeyth_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [NivelamentoKeyth].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [NivelamentoKeyth] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [NivelamentoKeyth] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [NivelamentoKeyth] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [NivelamentoKeyth] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [NivelamentoKeyth] SET ARITHABORT OFF 
GO

ALTER DATABASE [NivelamentoKeyth] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [NivelamentoKeyth] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [NivelamentoKeyth] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [NivelamentoKeyth] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [NivelamentoKeyth] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [NivelamentoKeyth] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [NivelamentoKeyth] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [NivelamentoKeyth] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [NivelamentoKeyth] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [NivelamentoKeyth] SET  DISABLE_BROKER 
GO

ALTER DATABASE [NivelamentoKeyth] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [NivelamentoKeyth] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [NivelamentoKeyth] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [NivelamentoKeyth] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [NivelamentoKeyth] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [NivelamentoKeyth] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [NivelamentoKeyth] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [NivelamentoKeyth] SET RECOVERY FULL 
GO

ALTER DATABASE [NivelamentoKeyth] SET  MULTI_USER 
GO

ALTER DATABASE [NivelamentoKeyth] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [NivelamentoKeyth] SET DB_CHAINING OFF 
GO

ALTER DATABASE [NivelamentoKeyth] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [NivelamentoKeyth] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [NivelamentoKeyth] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [NivelamentoKeyth] SET QUERY_STORE = OFF
GO

ALTER DATABASE [NivelamentoKeyth] SET  READ_WRITE 
GO

/***********************************************************************************************************************************************************/

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ProductDelivery](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DeliveryDate] [date] NOT NULL,
	[ProductName] [varchar](50) NOT NULL,
	[Amount] [int] NOT NULL,
	[UnitaryValue] [money] NOT NULL
) ON [PRIMARY]
GO

/*********************************************************************************************************************************************************/


INSERT INTO [dbo].[ProductDelivery]([DeliveryDate] ,[ProductName],[Amount],[UnitaryValue])
     VALUES ('2020-10-05', 'PROD 1', 15, 12.90)
GO

INSERT INTO [dbo].[ProductDelivery]([DeliveryDate] ,[ProductName],[Amount],[UnitaryValue])
     VALUES ('2020-10-06', 'PROD 2', 10, 24.90)
GO

INSERT INTO [dbo].[ProductDelivery]([DeliveryDate] ,[ProductName],[Amount],[UnitaryValue])
     VALUES ('2020-10-07', 'PROD 3', 3, 13.00)
GO


/*********************************************************************************************************************************************************/

SELECT * FROM ProductDelivery