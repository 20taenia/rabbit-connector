USE [red-data-prod-local]
GO
/****** Object:  Table [dbo].[Addresses_X]    Script Date: 17/05/2016 15:39:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Addresses_X](
	[AddressID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](4000) NULL,
	[LastName] [nvarchar](4000) NULL,
	[Company] [nvarchar](4000) NULL,
	[Address1] [nvarchar](4000) NOT NULL,
	[Address2] [nvarchar](4000) NULL,
	[City] [nvarchar](4000) NOT NULL,
	[StateProvinceID] [int] NULL,
	[CountryID] [char](2) NOT NULL,
	[ZipPostalCode] [nvarchar](4000) NULL,
	[Email] [nvarchar](4000) NULL,
	[PhoneNumber] [nvarchar](100) NULL,
	[CreatedBy] [nvarchar](500) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](500) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Addresses_X] PRIMARY KEY CLUSTERED 
(
	[AddressID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Countries_X]    Script Date: 17/05/2016 15:39:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Countries_X](
	[CountryID] [char](2) NOT NULL,
	[Name] [varchar](80) NOT NULL,
	[ISO3Code] [char](3) NULL,
	[NumericCode] [smallint] NULL,
	[CreatedBy] [nvarchar](500) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](500) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK__Countries_X] PRIMARY KEY CLUSTERED 
(
	[CountryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Currencies_X]    Script Date: 17/05/2016 15:39:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Currencies_X](
	[CurrencyID] [char](3) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[CreatedBy] [nvarchar](500) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](500) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK__Currencies_X] PRIMARY KEY CLUSTERED 
(
	[CurrencyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ__CurrencyName_X] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Factories_X]    Script Date: 17/05/2016 15:39:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Factories_X](
	[FactoryID] [int] IDENTITY(1,1) NOT NULL,
	[Reference] [nvarchar](1000) NULL,
	[Name] [nvarchar](4000) NULL,
	[FactoryAddressID] [int] NULL,
	[PrimaryContactAddressID] [int] NULL,
	[SecondaryContactAddressID] [int] NULL,
	[Deposit] [decimal](18, 4) NULL,
	[CreatedBy] [nvarchar](500) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](500) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Factories_X] PRIMARY KEY CLUSTERED 
(
	[FactoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FactoryProductionDuration_X]    Script Date: 17/05/2016 15:39:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FactoryProductionDuration_X](
	[ProductionDurationID] [int] IDENTITY(1,1) NOT NULL,
	[FactoryID] [int] NULL,
	[NoOfUnits] [int] NULL,
	[DaysToProduction] [int] NULL,
	[CreatedBy] [nvarchar](500) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](500) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_FactoryProductionDuration_X] PRIMARY KEY CLUSTERED 
(
	[ProductionDurationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FactoryProducts_X]    Script Date: 17/05/2016 15:39:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FactoryProducts_X](
	[FactoryProductID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NOT NULL,
	[FactoryID] [int] NOT NULL,
	[Reference] [nvarchar](500) NULL,
	[ArtworkPDFMediaID] [int] NULL,
	[ArtworkAIMediaID] [int] NULL,
	[ProductionCost] [decimal](18, 4) NULL,
	[MaterialCost] [decimal](18, 4) NULL,
	[LabourCost] [decimal](18, 4) NULL,
	[SeaFreightCost] [decimal](18, 4) NULL,
	[AirFreightCost] [decimal](18, 4) NULL,
	[DutyCost] [decimal](18, 4) NULL,
	[HTCCode] [nvarchar](50) NULL,
	[QuantityInBox] [int] NULL,
	[QuantityInPackage] [int] NULL,
	[ManufacturingDuration] [int] NULL,
	[CommodityDescription] [nvarchar](500) NULL,
	[IsDeleted] [bit] NOT NULL,
	[IsDiscontinued] [bit] NOT NULL,
	[CreatedBy] [nvarchar](500) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](500) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_FactoryProducts_X] PRIMARY KEY CLUSTERED 
(
	[FactoryProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Languages_X]    Script Date: 17/05/2016 15:39:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Languages_X](
	[LanguageID] [char](2) NOT NULL,
	[NameEN] [nvarchar](96) NOT NULL,
	[NameFR] [nvarchar](96) NOT NULL,
	[NameDE] [nvarchar](96) NOT NULL,
	[CreatedBy] [nvarchar](500) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](500) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK__Language_X] PRIMARY KEY CLUSTERED 
(
	[LanguageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MarketplaceProducts_X]    Script Date: 17/05/2016 15:39:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MarketplaceProducts_X](
	[MarketplaceProductID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NOT NULL,
	[MarketplaceID] [int] NOT NULL,
	[FulfilmentWarehouseID] [int] NOT NULL,
	[Reference] [nvarchar](100) NULL,
	[SKU] [nvarchar](100) NULL,
	[Title] [nvarchar](1000) NULL,
	[Description] [nvarchar](4000) NULL,
	[RecommendedRetailPrice] [decimal](18, 4) NULL,
	[StrikeOutPrice] [decimal](18, 4) NULL,
	[ProductConditionID] [int] NULL,
	[KeyFeature1] [nvarchar](1000) NULL,
	[KeyFeature2] [nvarchar](1000) NULL,
	[KeyFeature3] [nvarchar](1000) NULL,
	[KeyFeature4] [nvarchar](1000) NULL,
	[KeyFeature5] [nvarchar](1000) NULL,
	[KeyFeature6] [nvarchar](1000) NULL,
	[KeyFeature7] [nvarchar](1000) NULL,
	[KeyFeature8] [nvarchar](1000) NULL,
	[KeyFeature9] [nvarchar](1000) NULL,
	[KeyFeature10] [nvarchar](1000) NULL,
	[Keyword1] [nvarchar](1000) NULL,
	[Keyword2] [nvarchar](1000) NULL,
	[Keyword3] [nvarchar](1000) NULL,
	[Keyword4] [nvarchar](1000) NULL,
	[Keyword5] [nvarchar](1000) NULL,
	[Keyword6] [nvarchar](1000) NULL,
	[Keyword7] [nvarchar](1000) NULL,
	[Keyword8] [nvarchar](1000) NULL,
	[Keyword9] [nvarchar](1000) NULL,
	[Keyword10] [nvarchar](1000) NULL,
	[BrowseNode1] [nvarchar](50) NULL,
	[BrowseNode2] [nvarchar](50) NULL,
	[BrowseNode3] [nvarchar](50) NULL,
	[BrowseNode4] [nvarchar](50) NULL,
	[BrowseNode5] [nvarchar](50) NULL,
	[VariationThemeID] [int] NULL,
	[VariationThemeName1] [nvarchar](50) NULL,
	[VariationThemeName2] [nvarchar](50) NULL,
	[VariationThemeName3] [nvarchar](50) NULL,
	[VariationThemeName4] [nvarchar](50) NULL,
	[VariationThemeName5] [nvarchar](50) NULL,
	[ImageMediaID1] [int] NULL,
	[ImageMediaID2] [int] NULL,
	[ImageMediaID3] [int] NULL,
	[ImageMediaID4] [int] NULL,
	[ImageMediaID5] [int] NULL,
	[ImageMediaID6] [int] NULL,
	[ImageMediaID7] [int] NULL,
	[ImageMediaID8] [int] NULL,
	[ImageMediaID9] [int] NULL,
	[ImageMediaID10] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
	[IsDiscontinued] [bit] NOT NULL,
	[CreatedBy] [nvarchar](500) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](500) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_MarketplaceProducts_X] PRIMARY KEY CLUSTERED 
(
	[MarketplaceProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Marketplaces_X]    Script Date: 17/05/2016 15:39:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Marketplaces_X](
	[MarketplaceID] [int] IDENTITY(1,1) NOT NULL,
	[Reference] [nvarchar](100) NOT NULL,
	[Name] [nvarchar](500) NOT NULL,
	[LanguageID] [char](2) NOT NULL,
	[CurrencyID] [char](3) NOT NULL,
	[CountryID] [char](2) NOT NULL,
	[CreatedBy] [nvarchar](500) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](500) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Marketplaces_X] PRIMARY KEY CLUSTERED 
(
	[MarketplaceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Media_X]    Script Date: 17/05/2016 15:39:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Media_X](
	[MediaID] [int] IDENTITY(1,1) NOT NULL,
	[FileName] [nvarchar](500) NULL,
	[FilePath] [nvarchar](4000) NULL,
	[CreatedBy] [nvarchar](500) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](500) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Media_X] PRIMARY KEY CLUSTERED 
(
	[MediaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Owners_X]    Script Date: 17/05/2016 15:39:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Owners_X](
	[OwnerID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](300) NULL,
	[Description] [nvarchar](4000) NULL,
	[IsCharonCompany] [bit] NOT NULL,
	[CreatedBy] [nvarchar](500) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](500) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Owners_X] PRIMARY KEY CLUSTERED 
(
	[OwnerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductConditions_X]    Script Date: 17/05/2016 15:39:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductConditions_X](
	[ProductConditionID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](4000) NULL,
	[IsNew] [bit] NOT NULL,
	[IsRefurbished] [bit] NOT NULL,
	[IsUsed] [bit] NOT NULL,
	[CreatedBy] [nvarchar](500) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](500) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_ProductConditions_X] PRIMARY KEY CLUSTERED 
(
	[ProductConditionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductPhysicalAttributes_X]    Script Date: 17/05/2016 15:39:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductPhysicalAttributes_X](
	[PhysicalAttributeID] [int] IDENTITY(1,1) NOT NULL,
	[Length] [decimal](8, 4) NULL,
	[Width] [decimal](8, 4) NULL,
	[Height] [decimal](8, 4) NULL,
	[PackagingLength] [decimal](8, 4) NULL,
	[PackagingWidth] [decimal](8, 4) NULL,
	[PackagingHeight] [decimal](8, 4) NULL,
	[Weight] [decimal](8, 4) NULL,
	[PackagedWeight] [decimal](8, 4) NULL,
	[CreatedBy] [nvarchar](500) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](500) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_PhysicalAttributes_X] PRIMARY KEY CLUSTERED 
(
	[PhysicalAttributeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Products_X]    Script Date: 17/05/2016 15:39:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Products_X](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[Barcode] [varchar](20) NOT NULL,
	[Name] [nvarchar](1000) NULL,
	[Description] [nvarchar](4000) NULL,
	[OwnerID] [int] NOT NULL,
	[PhysicalAttributeID] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
	[IsDiscontinued] [bit] NOT NULL,
	[CreatedBy] [nvarchar](500) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](500) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Products_X] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[StateProvinces_X]    Script Date: 17/05/2016 15:39:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[StateProvinces_X](
	[StateProvinceID] [int] IDENTITY(1,1) NOT NULL,
	[CountryID] [char](2) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Abbreviation] [nvarchar](100) NULL,
	[CreatedBy] [nvarchar](500) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](500) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_StateProvinces_X] PRIMARY KEY CLUSTERED 
(
	[StateProvinceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[VariationThemes_X]    Script Date: 17/05/2016 15:39:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VariationThemes_X](
	[VariationThemeID] [int] IDENTITY(1,1) NOT NULL,
	[Reference] [nvarchar](100) NULL,
	[Title] [nvarchar](500) NULL,
	[ImageID] [int] NULL,
	[ProductType] [nvarchar](500) NULL,
	[Manufacturer] [nvarchar](500) NULL,
	[BrandName] [nvarchar](500) NULL,
	[VariationThemeTypeName] [nvarchar](500) NULL,
	[VariationThemeName1] [nvarchar](500) NULL,
	[VariationThemeName2] [nvarchar](500) NULL,
	[VariationThemeName3] [nvarchar](500) NULL,
	[VariationThemeName4] [nvarchar](500) NULL,
	[VariationThemeName5] [nvarchar](500) NULL,
	[CreatedBy] [nvarchar](500) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](500) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_VariationThemes_X] PRIMARY KEY CLUSTERED 
(
	[VariationThemeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[WarehouseProducts_X]    Script Date: 17/05/2016 15:39:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WarehouseProducts_X](
	[WarehouseProductID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NOT NULL,
	[WarehouseID] [int] NOT NULL,
	[DaysofBuffer] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
	[IsDiscontinued] [bit] NOT NULL,
	[CreatedBy] [nvarchar](500) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](500) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_WarehouseProducts_X] PRIMARY KEY CLUSTERED 
(
	[WarehouseProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Warehouses_X]    Script Date: 17/05/2016 15:39:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Warehouses_X](
	[WarehouseID] [int] IDENTITY(1,1) NOT NULL,
	[CountryID] [char](2) NOT NULL,
	[Reference] [nvarchar](100) NOT NULL,
	[IsCharonWarehouse] [bit] NOT NULL,
	[CreatedBy] [nvarchar](500) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](500) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Warehouses_X] PRIMARY KEY CLUSTERED 
(
	[WarehouseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[FactoryProducts_X] ADD  CONSTRAINT [FP_IsDeleted_X]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[FactoryProducts_X] ADD  CONSTRAINT [FP_IsDiscontinued_X]  DEFAULT ((0)) FOR [IsDiscontinued]
GO
ALTER TABLE [dbo].[MarketplaceProducts_X] ADD  CONSTRAINT [MP_IsDeleted_X]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[MarketplaceProducts_X] ADD  CONSTRAINT [MP_IsDiscontinued_X]  DEFAULT ((0)) FOR [IsDiscontinued]
GO
ALTER TABLE [dbo].[Products_X] ADD  CONSTRAINT [P_IsDeleted_X]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Products_X] ADD  CONSTRAINT [P_IsDiscontinued_X]  DEFAULT ((0)) FOR [IsDiscontinued]
GO
ALTER TABLE [dbo].[WarehouseProducts_X] ADD  CONSTRAINT [WP_IsDeleted_X]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[WarehouseProducts_X] ADD  CONSTRAINT [WP_IsDiscontinued_X]  DEFAULT ((0)) FOR [IsDiscontinued]
GO
ALTER TABLE [dbo].[Addresses_X]  WITH CHECK ADD  CONSTRAINT [FK_Addresses_Countries1_X] FOREIGN KEY([CountryID])
REFERENCES [dbo].[Countries_X] ([CountryID])
GO
ALTER TABLE [dbo].[Addresses_X] CHECK CONSTRAINT [FK_Addresses_Countries1_X]
GO
ALTER TABLE [dbo].[Addresses_X]  WITH CHECK ADD  CONSTRAINT [FK_Addresses_StateProvinces_X] FOREIGN KEY([StateProvinceID])
REFERENCES [dbo].[StateProvinces_X] ([StateProvinceID])
GO
ALTER TABLE [dbo].[Addresses_X] CHECK CONSTRAINT [FK_Addresses_StateProvinces_X]
GO
ALTER TABLE [dbo].[Factories_X]  WITH CHECK ADD  CONSTRAINT [FK_Factories_FactoryAddresses_X] FOREIGN KEY([FactoryAddressID])
REFERENCES [dbo].[Addresses_X] ([AddressID])
GO
ALTER TABLE [dbo].[Factories_X] CHECK CONSTRAINT [FK_Factories_FactoryAddresses_X]
GO
ALTER TABLE [dbo].[Factories_X]  WITH CHECK ADD  CONSTRAINT [FK_Factories_PrimaryContactAddresses_X] FOREIGN KEY([PrimaryContactAddressID])
REFERENCES [dbo].[Addresses_X] ([AddressID])
GO
ALTER TABLE [dbo].[Factories_X] CHECK CONSTRAINT [FK_Factories_PrimaryContactAddresses_X]
GO
ALTER TABLE [dbo].[Factories_X]  WITH CHECK ADD  CONSTRAINT [FK_Factories_SecondaryContactAddresses_X] FOREIGN KEY([SecondaryContactAddressID])
REFERENCES [dbo].[Addresses_X] ([AddressID])
GO
ALTER TABLE [dbo].[Factories_X] CHECK CONSTRAINT [FK_Factories_SecondaryContactAddresses_X]
GO
ALTER TABLE [dbo].[FactoryProductionDuration_X]  WITH CHECK ADD  CONSTRAINT [FK_FactoryProductionDuration_Factories_X] FOREIGN KEY([FactoryID])
REFERENCES [dbo].[Factories_X] ([FactoryID])
GO
ALTER TABLE [dbo].[FactoryProductionDuration_X] CHECK CONSTRAINT [FK_FactoryProductionDuration_Factories_X]
GO
ALTER TABLE [dbo].[FactoryProducts_X]  WITH CHECK ADD  CONSTRAINT [FK_FactoryProducts_ArtworkAIMedia_X] FOREIGN KEY([ArtworkAIMediaID])
REFERENCES [dbo].[Media_X] ([MediaID])
GO
ALTER TABLE [dbo].[FactoryProducts_X] CHECK CONSTRAINT [FK_FactoryProducts_ArtworkAIMedia_X]
GO
ALTER TABLE [dbo].[FactoryProducts_X]  WITH CHECK ADD  CONSTRAINT [FK_FactoryProducts_ArtworkPDFMedia_X] FOREIGN KEY([ArtworkPDFMediaID])
REFERENCES [dbo].[Media_X] ([MediaID])
GO
ALTER TABLE [dbo].[FactoryProducts_X] CHECK CONSTRAINT [FK_FactoryProducts_ArtworkPDFMedia_X]
GO
ALTER TABLE [dbo].[FactoryProducts_X]  WITH CHECK ADD  CONSTRAINT [FK_FactoryProducts_Factories_X] FOREIGN KEY([FactoryID])
REFERENCES [dbo].[Factories_X] ([FactoryID])
GO
ALTER TABLE [dbo].[FactoryProducts_X] CHECK CONSTRAINT [FK_FactoryProducts_Factories_X]
GO
ALTER TABLE [dbo].[FactoryProducts_X]  WITH CHECK ADD  CONSTRAINT [FK_FactoryProducts_Products_X] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products_X] ([ProductID])
GO
ALTER TABLE [dbo].[FactoryProducts_X] CHECK CONSTRAINT [FK_FactoryProducts_Products_X]
GO
ALTER TABLE [dbo].[MarketplaceProducts_X]  WITH CHECK ADD  CONSTRAINT [FK_MarketplaceProducts_FulfilmentWarehouses_X] FOREIGN KEY([FulfilmentWarehouseID])
REFERENCES [dbo].[Warehouses_X] ([WarehouseID])
GO
ALTER TABLE [dbo].[MarketplaceProducts_X] CHECK CONSTRAINT [FK_MarketplaceProducts_FulfilmentWarehouses_X]
GO
ALTER TABLE [dbo].[MarketplaceProducts_X]  WITH CHECK ADD  CONSTRAINT [FK_MarketplaceProducts_Marketplaces_X] FOREIGN KEY([MarketplaceID])
REFERENCES [dbo].[Marketplaces_X] ([MarketplaceID])
GO
ALTER TABLE [dbo].[MarketplaceProducts_X] CHECK CONSTRAINT [FK_MarketplaceProducts_Marketplaces_X]
GO
ALTER TABLE [dbo].[MarketplaceProducts_X]  WITH CHECK ADD  CONSTRAINT [FK_MarketplaceProducts_Media1_X] FOREIGN KEY([ImageMediaID1])
REFERENCES [dbo].[Media_X] ([MediaID])
GO
ALTER TABLE [dbo].[MarketplaceProducts_X] CHECK CONSTRAINT [FK_MarketplaceProducts_Media1_X]
GO
ALTER TABLE [dbo].[MarketplaceProducts_X]  WITH CHECK ADD  CONSTRAINT [FK_MarketplaceProducts_Media10_X] FOREIGN KEY([ImageMediaID10])
REFERENCES [dbo].[Media_X] ([MediaID])
GO
ALTER TABLE [dbo].[MarketplaceProducts_X] CHECK CONSTRAINT [FK_MarketplaceProducts_Media10_X]
GO
ALTER TABLE [dbo].[MarketplaceProducts_X]  WITH CHECK ADD  CONSTRAINT [FK_MarketplaceProducts_Media2_X] FOREIGN KEY([ImageMediaID2])
REFERENCES [dbo].[Media_X] ([MediaID])
GO
ALTER TABLE [dbo].[MarketplaceProducts_X] CHECK CONSTRAINT [FK_MarketplaceProducts_Media2_X]
GO
ALTER TABLE [dbo].[MarketplaceProducts_X]  WITH CHECK ADD  CONSTRAINT [FK_MarketplaceProducts_Media3_X] FOREIGN KEY([ImageMediaID3])
REFERENCES [dbo].[Media_X] ([MediaID])
GO
ALTER TABLE [dbo].[MarketplaceProducts_X] CHECK CONSTRAINT [FK_MarketplaceProducts_Media3_X]
GO
ALTER TABLE [dbo].[MarketplaceProducts_X]  WITH CHECK ADD  CONSTRAINT [FK_MarketplaceProducts_Media4_X] FOREIGN KEY([ImageMediaID4])
REFERENCES [dbo].[Media_X] ([MediaID])
GO
ALTER TABLE [dbo].[MarketplaceProducts_X] CHECK CONSTRAINT [FK_MarketplaceProducts_Media4_X]
GO
ALTER TABLE [dbo].[MarketplaceProducts_X]  WITH CHECK ADD  CONSTRAINT [FK_MarketplaceProducts_Media5_X] FOREIGN KEY([ImageMediaID5])
REFERENCES [dbo].[Media_X] ([MediaID])
GO
ALTER TABLE [dbo].[MarketplaceProducts_X] CHECK CONSTRAINT [FK_MarketplaceProducts_Media5_X]
GO
ALTER TABLE [dbo].[MarketplaceProducts_X]  WITH CHECK ADD  CONSTRAINT [FK_MarketplaceProducts_Media6_X] FOREIGN KEY([ImageMediaID6])
REFERENCES [dbo].[Media_X] ([MediaID])
GO
ALTER TABLE [dbo].[MarketplaceProducts_X] CHECK CONSTRAINT [FK_MarketplaceProducts_Media6_X]
GO
ALTER TABLE [dbo].[MarketplaceProducts_X]  WITH CHECK ADD  CONSTRAINT [FK_MarketplaceProducts_Media7_X] FOREIGN KEY([ImageMediaID7])
REFERENCES [dbo].[Media_X] ([MediaID])
GO
ALTER TABLE [dbo].[MarketplaceProducts_X] CHECK CONSTRAINT [FK_MarketplaceProducts_Media7_X]
GO
ALTER TABLE [dbo].[MarketplaceProducts_X]  WITH CHECK ADD  CONSTRAINT [FK_MarketplaceProducts_Media8_X] FOREIGN KEY([ImageMediaID8])
REFERENCES [dbo].[Media_X] ([MediaID])
GO
ALTER TABLE [dbo].[MarketplaceProducts_X] CHECK CONSTRAINT [FK_MarketplaceProducts_Media8_X]
GO
ALTER TABLE [dbo].[MarketplaceProducts_X]  WITH CHECK ADD  CONSTRAINT [FK_MarketplaceProducts_Media9_X] FOREIGN KEY([ImageMediaID9])
REFERENCES [dbo].[Media_X] ([MediaID])
GO
ALTER TABLE [dbo].[MarketplaceProducts_X] CHECK CONSTRAINT [FK_MarketplaceProducts_Media9_X]
GO
ALTER TABLE [dbo].[MarketplaceProducts_X]  WITH CHECK ADD  CONSTRAINT [FK_MarketplaceProducts_Products_X] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products_X] ([ProductID])
GO
ALTER TABLE [dbo].[MarketplaceProducts_X] CHECK CONSTRAINT [FK_MarketplaceProducts_Products_X]
GO
ALTER TABLE [dbo].[MarketplaceProducts_X]  WITH CHECK ADD  CONSTRAINT [FK_MarketplaceProducts_VariationThemes_X] FOREIGN KEY([VariationThemeID])
REFERENCES [dbo].[VariationThemes_X] ([VariationThemeID])
GO
ALTER TABLE [dbo].[MarketplaceProducts_X] CHECK CONSTRAINT [FK_MarketplaceProducts_VariationThemes_X]
GO
ALTER TABLE [dbo].[Marketplaces_X]  WITH CHECK ADD  CONSTRAINT [FK_Marketplaces_Countries_X] FOREIGN KEY([CountryID])
REFERENCES [dbo].[Countries_X] ([CountryID])
GO
ALTER TABLE [dbo].[Marketplaces_X] CHECK CONSTRAINT [FK_Marketplaces_Countries_X]
GO
ALTER TABLE [dbo].[Marketplaces_X]  WITH CHECK ADD  CONSTRAINT [FK_Marketplaces_Currencies_X] FOREIGN KEY([CurrencyID])
REFERENCES [dbo].[Currencies_X] ([CurrencyID])
GO
ALTER TABLE [dbo].[Marketplaces_X] CHECK CONSTRAINT [FK_Marketplaces_Currencies_X]
GO
ALTER TABLE [dbo].[Marketplaces_X]  WITH CHECK ADD  CONSTRAINT [FK_Marketplaces_Languages_X] FOREIGN KEY([LanguageID])
REFERENCES [dbo].[Languages_X] ([LanguageID])
GO
ALTER TABLE [dbo].[Marketplaces_X] CHECK CONSTRAINT [FK_Marketplaces_Languages_X]
GO
ALTER TABLE [dbo].[Products_X]  WITH CHECK ADD  CONSTRAINT [FK_Products_Owners_X] FOREIGN KEY([OwnerID])
REFERENCES [dbo].[Owners_X] ([OwnerID])
GO
ALTER TABLE [dbo].[Products_X] CHECK CONSTRAINT [FK_Products_Owners_X]
GO
ALTER TABLE [dbo].[Products_X]  WITH CHECK ADD  CONSTRAINT [FK_Products_ProductPhysicalAttributes_X] FOREIGN KEY([PhysicalAttributeID])
REFERENCES [dbo].[ProductPhysicalAttributes_X] ([PhysicalAttributeID])
GO
ALTER TABLE [dbo].[Products_X] CHECK CONSTRAINT [FK_Products_ProductPhysicalAttributes_X]
GO
ALTER TABLE [dbo].[StateProvinces_X]  WITH CHECK ADD  CONSTRAINT [FK_StateProvinces_Countries_X] FOREIGN KEY([CountryID])
REFERENCES [dbo].[Countries_X] ([CountryID])
GO
ALTER TABLE [dbo].[StateProvinces_X] CHECK CONSTRAINT [FK_StateProvinces_Countries_X]
GO
ALTER TABLE [dbo].[VariationThemes_X]  WITH CHECK ADD  CONSTRAINT [FK_VariationThemes_Media_X] FOREIGN KEY([ImageID])
REFERENCES [dbo].[Media_X] ([MediaID])
GO
ALTER TABLE [dbo].[VariationThemes_X] CHECK CONSTRAINT [FK_VariationThemes_Media_X]
GO
ALTER TABLE [dbo].[WarehouseProducts_X]  WITH CHECK ADD  CONSTRAINT [FK_WarehouseProducts_Products_X] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products_X] ([ProductID])
GO
ALTER TABLE [dbo].[WarehouseProducts_X] CHECK CONSTRAINT [FK_WarehouseProducts_Products_X]
GO
ALTER TABLE [dbo].[WarehouseProducts_X]  WITH CHECK ADD  CONSTRAINT [FK_WarehouseProducts_Warehouses_X] FOREIGN KEY([WarehouseID])
REFERENCES [dbo].[Warehouses_X] ([WarehouseID])
GO
ALTER TABLE [dbo].[WarehouseProducts_X] CHECK CONSTRAINT [FK_WarehouseProducts_Warehouses_X]
GO
ALTER TABLE [dbo].[Warehouses_X]  WITH CHECK ADD  CONSTRAINT [FK_Warehouses_Countries_X] FOREIGN KEY([CountryID])
REFERENCES [dbo].[Countries_X] ([CountryID])
GO
ALTER TABLE [dbo].[Warehouses_X] CHECK CONSTRAINT [FK_Warehouses_Countries_X]
GO
