USE [Pluto]
GO
/****** Object:  Table [dbo].[Addresses]    Script Date: 29/07/2016 07:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Addresses](
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
	[IsDeleted] [bit] NOT NULL CONSTRAINT [AD_IsDeleted]  DEFAULT ((0)),
	[CreatedBy] [nvarchar](500) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](500) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Addresses] PRIMARY KEY CLUSTERED 
(
	[AddressID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Countries]    Script Date: 29/07/2016 07:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Countries](
	[CountryID] [char](2) NOT NULL,
	[Name] [varchar](80) NOT NULL,
	[ISO3Code] [char](3) NULL,
	[NumericCode] [smallint] NULL,
	[IsDeleted] [bit] NOT NULL CONSTRAINT [C_IsDeleted]  DEFAULT ((0)),
	[CreatedBy] [nvarchar](500) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](500) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK__Countries] PRIMARY KEY CLUSTERED 
(
	[CountryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Currencies]    Script Date: 29/07/2016 07:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Currencies](
	[CurrencyID] [char](3) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[IsDeleted] [bit] NOT NULL CONSTRAINT [CU_IsDeleted]  DEFAULT ((0)),
	[CreatedBy] [nvarchar](500) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](500) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK__Currencies] PRIMARY KEY CLUSTERED 
(
	[CurrencyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ__CurrencyName] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Factories]    Script Date: 29/07/2016 07:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Factories](
	[FactoryID] [int] IDENTITY(1,1) NOT NULL,
	[Reference] [nvarchar](1000) NULL,
	[Name] [nvarchar](4000) NULL,
	[FactoryAddressID] [int] NULL,
	[PrimaryContactAddressID] [int] NULL,
	[SecondaryContactAddressID] [int] NULL,
	[Deposit] [decimal](18, 4) NULL,
	[IsDeleted] [bit] NOT NULL CONSTRAINT [F_IsDeleted]  DEFAULT ((0)),
	[CreatedBy] [nvarchar](500) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](500) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Factories] PRIMARY KEY CLUSTERED 
(
	[FactoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FactoryProductionDuration]    Script Date: 29/07/2016 07:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FactoryProductionDuration](
	[ProductionDurationID] [int] IDENTITY(1,1) NOT NULL,
	[FactoryID] [int] NULL,
	[NoOfUnits] [int] NULL,
	[DaysToProduction] [int] NULL,
	[IsDeleted] [bit] NOT NULL CONSTRAINT [FPD_IsDeleted]  DEFAULT ((0)),
	[CreatedBy] [nvarchar](500) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](500) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_FactoryProductionDuration] PRIMARY KEY CLUSTERED 
(
	[ProductionDurationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FactoryProducts]    Script Date: 29/07/2016 07:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FactoryProducts](
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
	[QuantityInBox] [int] NULL,
	[QuantityInPackage] [int] NULL,
	[ManufacturingDuration] [int] NULL,
	[CommodityDescription] [nvarchar](500) NULL,
	[IsDeleted] [bit] NOT NULL CONSTRAINT [FP_IsDeleted]  DEFAULT ((0)),
	[IsDiscontinued] [bit] NOT NULL CONSTRAINT [FP_IsDiscontinued]  DEFAULT ((0)),
	[CreatedBy] [nvarchar](500) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](500) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_FactoryProducts] PRIMARY KEY CLUSTERED 
(
	[FactoryProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Languages]    Script Date: 29/07/2016 07:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Languages](
	[LanguageID] [char](2) NOT NULL,
	[NameEN] [nvarchar](96) NOT NULL,
	[NameFR] [nvarchar](96) NOT NULL,
	[NameDE] [nvarchar](96) NOT NULL,
	[IsDeleted] [bit] NOT NULL CONSTRAINT [L_IsDeleted]  DEFAULT ((0)),
	[CreatedBy] [nvarchar](500) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](500) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK__Language] PRIMARY KEY CLUSTERED 
(
	[LanguageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MarketplaceProducts]    Script Date: 29/07/2016 07:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MarketplaceProducts](
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
	[VariationTheme1] [nvarchar](50) NULL,
	[VariationTheme2] [nvarchar](50) NULL,
	[VariationTheme3] [nvarchar](50) NULL,
	[VariationTheme4] [nvarchar](50) NULL,
	[VariationTheme5] [nvarchar](50) NULL,
	[ImageMedia1ID] [int] NULL,
	[ImageMedia2ID] [int] NULL,
	[ImageMedia3ID] [int] NULL,
	[ImageMedia4ID] [int] NULL,
	[ImageMedia5ID] [int] NULL,
	[ImageMedia6ID] [int] NULL,
	[ImageMedia7ID] [int] NULL,
	[ImageMedia8ID] [int] NULL,
	[ImageMedia9ID] [int] NULL,
	[ImageMedia10ID] [int] NULL,
	[DoNotResend] [bit] NOT NULL CONSTRAINT [MP_DoNotResend]  DEFAULT ((0)),
	[IsDeleted] [bit] NOT NULL CONSTRAINT [MP_IsDeleted]  DEFAULT ((0)),
	[CreatedBy] [nvarchar](500) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](500) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_MarketplaceProducts] PRIMARY KEY CLUSTERED 
(
	[MarketplaceProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Marketplaces]    Script Date: 29/07/2016 07:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Marketplaces](
	[MarketplaceID] [int] IDENTITY(1,1) NOT NULL,
	[Reference] [nvarchar](100) NOT NULL,
	[Name] [nvarchar](500) NOT NULL,
	[LanguageID] [char](2) NOT NULL,
	[CurrencyID] [char](3) NOT NULL,
	[CountryID] [char](2) NOT NULL,
	[IsDeleted] [bit] NOT NULL CONSTRAINT [M_IsDeleted]  DEFAULT ((0)),
	[CreatedBy] [nvarchar](500) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](500) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Marketplaces] PRIMARY KEY CLUSTERED 
(
	[MarketplaceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Media]    Script Date: 29/07/2016 07:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Media](
	[MediaID] [int] IDENTITY(1,1) NOT NULL,
	[FileName] [nvarchar](500) NULL,
	[FilePath] [nvarchar](4000) NULL,
	[MimeType] [nvarchar](100) NULL,
	[IsDeleted] [bit] NOT NULL CONSTRAINT [ME_IsDeleted]  DEFAULT ((0)),
	[CreatedBy] [nvarchar](500) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](500) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Media] PRIMARY KEY CLUSTERED 
(
	[MediaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NLog]    Script Date: 29/07/2016 07:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[NLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MachineName] [nvarchar](200) NULL,
	[SiteName] [nvarchar](200) NOT NULL,
	[Logged] [datetime] NOT NULL,
	[Level] [varchar](5) NOT NULL,
	[UserName] [nvarchar](200) NULL,
	[Message] [nvarchar](max) NOT NULL,
	[Logger] [nvarchar](300) NULL,
	[Properties] [nvarchar](max) NULL,
	[ServerName] [nvarchar](200) NULL,
	[Port] [nvarchar](100) NULL,
	[Url] [nvarchar](2000) NULL,
	[Https] [bit] NULL,
	[ServerAddress] [nvarchar](100) NULL,
	[RemoteAddress] [nvarchar](100) NULL,
	[Callsite] [nvarchar](300) NULL,
	[Exception] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Log] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Owners]    Script Date: 29/07/2016 07:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Owners](
	[OwnerID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](300) NULL,
	[Description] [nvarchar](4000) NULL,
	[IsCharonCompany] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL CONSTRAINT [O_IsDeleted]  DEFAULT ((0)),
	[CreatedBy] [nvarchar](500) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](500) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Owners] PRIMARY KEY CLUSTERED 
(
	[OwnerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductCategories]    Script Date: 29/07/2016 07:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductCategories](
	[ProductCategoryID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](500) NULL,
	[Description] [nvarchar](4000) NULL,
	[ParentCategoryID] [int] NULL,
	[ProductAttribute1Name] [nvarchar](500) NULL,
	[ProductAttribute2Name] [nvarchar](500) NULL,
	[ProductAttribute3Name] [nvarchar](500) NULL,
	[ProductAttribute4Name] [nvarchar](500) NULL,
	[ProductAttribute5Name] [nvarchar](500) NULL,
	[IsDeleted] [bit] NOT NULL CONSTRAINT [DF_ProductCategories_IsDeleted]  DEFAULT ((0)),
	[CreatedBy] [nvarchar](500) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](500) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_ProductCategories] PRIMARY KEY CLUSTERED 
(
	[ProductCategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductConditions]    Script Date: 29/07/2016 07:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductConditions](
	[ProductConditionID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](4000) NULL,
	[IsNew] [bit] NOT NULL,
	[IsRefurbished] [bit] NOT NULL,
	[IsUsed] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL CONSTRAINT [PC_IsDeleted]  DEFAULT ((0)),
	[CreatedBy] [nvarchar](500) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](500) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_ProductConditions] PRIMARY KEY CLUSTERED 
(
	[ProductConditionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductPhysicalAttributes]    Script Date: 29/07/2016 07:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductPhysicalAttributes](
	[PhysicalAttributeID] [int] IDENTITY(1,1) NOT NULL,
	[Length] [decimal](8, 4) NULL,
	[Width] [decimal](8, 4) NULL,
	[Height] [decimal](8, 4) NULL,
	[PackagingLength] [decimal](8, 4) NULL,
	[PackagingWidth] [decimal](8, 4) NULL,
	[PackagingHeight] [decimal](8, 4) NULL,
	[Weight] [decimal](8, 4) NULL,
	[PackagedWeight] [decimal](8, 4) NULL,
	[IsDeleted] [bit] NOT NULL CONSTRAINT [PPA_IsDeleted]  DEFAULT ((0)),
	[CreatedBy] [nvarchar](500) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](500) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_PhysicalAttributes] PRIMARY KEY CLUSTERED 
(
	[PhysicalAttributeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Products]    Script Date: 29/07/2016 07:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Products](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[Barcode] [varchar](20) NOT NULL,
	[Name] [nvarchar](1000) NULL,
	[Description] [nvarchar](4000) NULL,
	[OwnerID] [int] NOT NULL,
	[PhysicalAttributeID] [int] NULL,
	[ProductCategoryID] [int] NULL,
	[ProductAttribute1] [nvarchar](500) NULL,
	[ProductAttribute2] [nvarchar](500) NULL,
	[ProductAttribute3] [nvarchar](500) NULL,
	[ProductAttribute4] [nvarchar](500) NULL,
	[ProductAttribute5] [nvarchar](500) NULL,
	[IsDeleted] [bit] NOT NULL CONSTRAINT [P_IsDeleted]  DEFAULT ((0)),
	[IsDiscontinued] [bit] NOT NULL CONSTRAINT [P_IsDiscontinued]  DEFAULT ((0)),
	[CreatedBy] [nvarchar](500) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](500) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[StateProvinces]    Script Date: 29/07/2016 07:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[StateProvinces](
	[StateProvinceID] [int] IDENTITY(1,1) NOT NULL,
	[CountryID] [char](2) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Abbreviation] [nvarchar](100) NULL,
	[IsDeleted] [bit] NOT NULL CONSTRAINT [SP_IsDeleted]  DEFAULT ((0)),
	[CreatedBy] [nvarchar](500) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](500) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_StateProvinces] PRIMARY KEY CLUSTERED 
(
	[StateProvinceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[VariationThemes]    Script Date: 29/07/2016 07:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VariationThemes](
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
	[IsDeleted] [bit] NOT NULL CONSTRAINT [VT_IsDeleted]  DEFAULT ((0)),
	[CreatedBy] [nvarchar](500) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](500) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_VariationThemes] PRIMARY KEY CLUSTERED 
(
	[VariationThemeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[WarehouseProducts]    Script Date: 29/07/2016 07:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WarehouseProducts](
	[WarehouseProductID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NOT NULL,
	[WarehouseID] [int] NOT NULL,
	[DaysofBuffer] [int] NULL,
	[HarmonisedTariffCode] [nvarchar](50) NULL,
	[IsDeleted] [bit] NOT NULL,
	[IsDiscontinued] [bit] NOT NULL,
	[CreatedBy] [nvarchar](500) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](500) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_WarehouseProducts] PRIMARY KEY CLUSTERED 
(
	[WarehouseProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Warehouses]    Script Date: 29/07/2016 07:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Warehouses](
	[WarehouseID] [int] IDENTITY(1,1) NOT NULL,
	[CountryID] [char](2) NOT NULL,
	[Reference] [nvarchar](100) NOT NULL,
	[IsCharonWarehouse] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL CONSTRAINT [WA_IsDeleted]  DEFAULT ((0)),
	[CreatedBy] [nvarchar](500) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](500) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Warehouses] PRIMARY KEY CLUSTERED 
(
	[WarehouseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Addresses]  WITH CHECK ADD  CONSTRAINT [FK_Addresses_Countries1] FOREIGN KEY([CountryID])
REFERENCES [dbo].[Countries] ([CountryID])
GO
ALTER TABLE [dbo].[Addresses] CHECK CONSTRAINT [FK_Addresses_Countries1]
GO
ALTER TABLE [dbo].[Addresses]  WITH CHECK ADD  CONSTRAINT [FK_Addresses_StateProvinces] FOREIGN KEY([StateProvinceID])
REFERENCES [dbo].[StateProvinces] ([StateProvinceID])
GO
ALTER TABLE [dbo].[Addresses] CHECK CONSTRAINT [FK_Addresses_StateProvinces]
GO
ALTER TABLE [dbo].[Factories]  WITH CHECK ADD  CONSTRAINT [FK_Factories_FactoryAddresses] FOREIGN KEY([FactoryAddressID])
REFERENCES [dbo].[Addresses] ([AddressID])
GO
ALTER TABLE [dbo].[Factories] CHECK CONSTRAINT [FK_Factories_FactoryAddresses]
GO
ALTER TABLE [dbo].[Factories]  WITH CHECK ADD  CONSTRAINT [FK_Factories_PrimaryContactAddresses] FOREIGN KEY([PrimaryContactAddressID])
REFERENCES [dbo].[Addresses] ([AddressID])
GO
ALTER TABLE [dbo].[Factories] CHECK CONSTRAINT [FK_Factories_PrimaryContactAddresses]
GO
ALTER TABLE [dbo].[Factories]  WITH CHECK ADD  CONSTRAINT [FK_Factories_SecondaryContactAddresses] FOREIGN KEY([SecondaryContactAddressID])
REFERENCES [dbo].[Addresses] ([AddressID])
GO
ALTER TABLE [dbo].[Factories] CHECK CONSTRAINT [FK_Factories_SecondaryContactAddresses]
GO
ALTER TABLE [dbo].[FactoryProductionDuration]  WITH CHECK ADD  CONSTRAINT [FK_FactoryProductionDuration_Factories] FOREIGN KEY([FactoryID])
REFERENCES [dbo].[Factories] ([FactoryID])
GO
ALTER TABLE [dbo].[FactoryProductionDuration] CHECK CONSTRAINT [FK_FactoryProductionDuration_Factories]
GO
ALTER TABLE [dbo].[FactoryProducts]  WITH CHECK ADD  CONSTRAINT [FK_FactoryProducts_ArtworkAIMedia] FOREIGN KEY([ArtworkAIMediaID])
REFERENCES [dbo].[Media] ([MediaID])
GO
ALTER TABLE [dbo].[FactoryProducts] CHECK CONSTRAINT [FK_FactoryProducts_ArtworkAIMedia]
GO
ALTER TABLE [dbo].[FactoryProducts]  WITH CHECK ADD  CONSTRAINT [FK_FactoryProducts_ArtworkPDFMedia] FOREIGN KEY([ArtworkPDFMediaID])
REFERENCES [dbo].[Media] ([MediaID])
GO
ALTER TABLE [dbo].[FactoryProducts] CHECK CONSTRAINT [FK_FactoryProducts_ArtworkPDFMedia]
GO
ALTER TABLE [dbo].[FactoryProducts]  WITH CHECK ADD  CONSTRAINT [FK_FactoryProducts_Factories] FOREIGN KEY([FactoryID])
REFERENCES [dbo].[Factories] ([FactoryID])
GO
ALTER TABLE [dbo].[FactoryProducts] CHECK CONSTRAINT [FK_FactoryProducts_Factories]
GO
ALTER TABLE [dbo].[FactoryProducts]  WITH CHECK ADD  CONSTRAINT [FK_FactoryProducts_Products] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ProductID])
GO
ALTER TABLE [dbo].[FactoryProducts] CHECK CONSTRAINT [FK_FactoryProducts_Products]
GO
ALTER TABLE [dbo].[MarketplaceProducts]  WITH CHECK ADD  CONSTRAINT [FK_MarketplaceProducts_FulfilmentWarehouses] FOREIGN KEY([FulfilmentWarehouseID])
REFERENCES [dbo].[Warehouses] ([WarehouseID])
GO
ALTER TABLE [dbo].[MarketplaceProducts] CHECK CONSTRAINT [FK_MarketplaceProducts_FulfilmentWarehouses]
GO
ALTER TABLE [dbo].[MarketplaceProducts]  WITH CHECK ADD  CONSTRAINT [FK_MarketplaceProducts_Marketplaces] FOREIGN KEY([MarketplaceID])
REFERENCES [dbo].[Marketplaces] ([MarketplaceID])
GO
ALTER TABLE [dbo].[MarketplaceProducts] CHECK CONSTRAINT [FK_MarketplaceProducts_Marketplaces]
GO
ALTER TABLE [dbo].[MarketplaceProducts]  WITH CHECK ADD  CONSTRAINT [FK_MarketplaceProducts_Media1] FOREIGN KEY([ImageMedia1ID])
REFERENCES [dbo].[Media] ([MediaID])
GO
ALTER TABLE [dbo].[MarketplaceProducts] CHECK CONSTRAINT [FK_MarketplaceProducts_Media1]
GO
ALTER TABLE [dbo].[MarketplaceProducts]  WITH CHECK ADD  CONSTRAINT [FK_MarketplaceProducts_Media10] FOREIGN KEY([ImageMedia10ID])
REFERENCES [dbo].[Media] ([MediaID])
GO
ALTER TABLE [dbo].[MarketplaceProducts] CHECK CONSTRAINT [FK_MarketplaceProducts_Media10]
GO
ALTER TABLE [dbo].[MarketplaceProducts]  WITH CHECK ADD  CONSTRAINT [FK_MarketplaceProducts_Media2] FOREIGN KEY([ImageMedia2ID])
REFERENCES [dbo].[Media] ([MediaID])
GO
ALTER TABLE [dbo].[MarketplaceProducts] CHECK CONSTRAINT [FK_MarketplaceProducts_Media2]
GO
ALTER TABLE [dbo].[MarketplaceProducts]  WITH CHECK ADD  CONSTRAINT [FK_MarketplaceProducts_Media3] FOREIGN KEY([ImageMedia3ID])
REFERENCES [dbo].[Media] ([MediaID])
GO
ALTER TABLE [dbo].[MarketplaceProducts] CHECK CONSTRAINT [FK_MarketplaceProducts_Media3]
GO
ALTER TABLE [dbo].[MarketplaceProducts]  WITH CHECK ADD  CONSTRAINT [FK_MarketplaceProducts_Media4] FOREIGN KEY([ImageMedia4ID])
REFERENCES [dbo].[Media] ([MediaID])
GO
ALTER TABLE [dbo].[MarketplaceProducts] CHECK CONSTRAINT [FK_MarketplaceProducts_Media4]
GO
ALTER TABLE [dbo].[MarketplaceProducts]  WITH CHECK ADD  CONSTRAINT [FK_MarketplaceProducts_Media5] FOREIGN KEY([ImageMedia5ID])
REFERENCES [dbo].[Media] ([MediaID])
GO
ALTER TABLE [dbo].[MarketplaceProducts] CHECK CONSTRAINT [FK_MarketplaceProducts_Media5]
GO
ALTER TABLE [dbo].[MarketplaceProducts]  WITH CHECK ADD  CONSTRAINT [FK_MarketplaceProducts_Media6] FOREIGN KEY([ImageMedia6ID])
REFERENCES [dbo].[Media] ([MediaID])
GO
ALTER TABLE [dbo].[MarketplaceProducts] CHECK CONSTRAINT [FK_MarketplaceProducts_Media6]
GO
ALTER TABLE [dbo].[MarketplaceProducts]  WITH CHECK ADD  CONSTRAINT [FK_MarketplaceProducts_Media7] FOREIGN KEY([ImageMedia7ID])
REFERENCES [dbo].[Media] ([MediaID])
GO
ALTER TABLE [dbo].[MarketplaceProducts] CHECK CONSTRAINT [FK_MarketplaceProducts_Media7]
GO
ALTER TABLE [dbo].[MarketplaceProducts]  WITH CHECK ADD  CONSTRAINT [FK_MarketplaceProducts_Media8] FOREIGN KEY([ImageMedia8ID])
REFERENCES [dbo].[Media] ([MediaID])
GO
ALTER TABLE [dbo].[MarketplaceProducts] CHECK CONSTRAINT [FK_MarketplaceProducts_Media8]
GO
ALTER TABLE [dbo].[MarketplaceProducts]  WITH CHECK ADD  CONSTRAINT [FK_MarketplaceProducts_Media9] FOREIGN KEY([ImageMedia9ID])
REFERENCES [dbo].[Media] ([MediaID])
GO
ALTER TABLE [dbo].[MarketplaceProducts] CHECK CONSTRAINT [FK_MarketplaceProducts_Media9]
GO
ALTER TABLE [dbo].[MarketplaceProducts]  WITH CHECK ADD  CONSTRAINT [FK_MarketplaceProducts_ProductConditions] FOREIGN KEY([ProductConditionID])
REFERENCES [dbo].[ProductConditions] ([ProductConditionID])
GO
ALTER TABLE [dbo].[MarketplaceProducts] CHECK CONSTRAINT [FK_MarketplaceProducts_ProductConditions]
GO
ALTER TABLE [dbo].[MarketplaceProducts]  WITH CHECK ADD  CONSTRAINT [FK_MarketplaceProducts_Products] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ProductID])
GO
ALTER TABLE [dbo].[MarketplaceProducts] CHECK CONSTRAINT [FK_MarketplaceProducts_Products]
GO
ALTER TABLE [dbo].[MarketplaceProducts]  WITH CHECK ADD  CONSTRAINT [FK_MarketplaceProducts_VariationThemes] FOREIGN KEY([VariationThemeID])
REFERENCES [dbo].[VariationThemes] ([VariationThemeID])
GO
ALTER TABLE [dbo].[MarketplaceProducts] CHECK CONSTRAINT [FK_MarketplaceProducts_VariationThemes]
GO
ALTER TABLE [dbo].[Marketplaces]  WITH CHECK ADD  CONSTRAINT [FK_Marketplaces_Countries] FOREIGN KEY([CountryID])
REFERENCES [dbo].[Countries] ([CountryID])
GO
ALTER TABLE [dbo].[Marketplaces] CHECK CONSTRAINT [FK_Marketplaces_Countries]
GO
ALTER TABLE [dbo].[Marketplaces]  WITH CHECK ADD  CONSTRAINT [FK_Marketplaces_Currencies] FOREIGN KEY([CurrencyID])
REFERENCES [dbo].[Currencies] ([CurrencyID])
GO
ALTER TABLE [dbo].[Marketplaces] CHECK CONSTRAINT [FK_Marketplaces_Currencies]
GO
ALTER TABLE [dbo].[Marketplaces]  WITH CHECK ADD  CONSTRAINT [FK_Marketplaces_Languages] FOREIGN KEY([LanguageID])
REFERENCES [dbo].[Languages] ([LanguageID])
GO
ALTER TABLE [dbo].[Marketplaces] CHECK CONSTRAINT [FK_Marketplaces_Languages]
GO
ALTER TABLE [dbo].[ProductCategories]  WITH CHECK ADD  CONSTRAINT [FK_ProductCategories_ParentCategories] FOREIGN KEY([ParentCategoryID])
REFERENCES [dbo].[ProductCategories] ([ProductCategoryID])
GO
ALTER TABLE [dbo].[ProductCategories] CHECK CONSTRAINT [FK_ProductCategories_ParentCategories]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Owners] FOREIGN KEY([OwnerID])
REFERENCES [dbo].[Owners] ([OwnerID])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Owners]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_ProductCategories] FOREIGN KEY([ProductCategoryID])
REFERENCES [dbo].[ProductCategories] ([ProductCategoryID])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_ProductCategories]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_ProductPhysicalAttributes] FOREIGN KEY([PhysicalAttributeID])
REFERENCES [dbo].[ProductPhysicalAttributes] ([PhysicalAttributeID])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_ProductPhysicalAttributes]
GO
ALTER TABLE [dbo].[StateProvinces]  WITH CHECK ADD  CONSTRAINT [FK_StateProvinces_Countries] FOREIGN KEY([CountryID])
REFERENCES [dbo].[Countries] ([CountryID])
GO
ALTER TABLE [dbo].[StateProvinces] CHECK CONSTRAINT [FK_StateProvinces_Countries]
GO
ALTER TABLE [dbo].[VariationThemes]  WITH CHECK ADD  CONSTRAINT [FK_VariationThemes_Media] FOREIGN KEY([ImageID])
REFERENCES [dbo].[Media] ([MediaID])
GO
ALTER TABLE [dbo].[VariationThemes] CHECK CONSTRAINT [FK_VariationThemes_Media]
GO
ALTER TABLE [dbo].[WarehouseProducts]  WITH CHECK ADD  CONSTRAINT [FK_WarehouseProducts_Products] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ProductID])
GO
ALTER TABLE [dbo].[WarehouseProducts] CHECK CONSTRAINT [FK_WarehouseProducts_Products]
GO
ALTER TABLE [dbo].[WarehouseProducts]  WITH CHECK ADD  CONSTRAINT [FK_WarehouseProducts_Warehouses] FOREIGN KEY([WarehouseID])
REFERENCES [dbo].[Warehouses] ([WarehouseID])
GO
ALTER TABLE [dbo].[WarehouseProducts] CHECK CONSTRAINT [FK_WarehouseProducts_Warehouses]
GO
ALTER TABLE [dbo].[Warehouses]  WITH CHECK ADD  CONSTRAINT [FK_Warehouses_Countries] FOREIGN KEY([CountryID])
REFERENCES [dbo].[Countries] ([CountryID])
GO
ALTER TABLE [dbo].[Warehouses] CHECK CONSTRAINT [FK_Warehouses_Countries]
GO
/****** Object:  StoredProcedure [dbo].[NLog_AddEntry_p]    Script Date: 29/07/2016 07:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[NLog_AddEntry_p] (
  @machineName nvarchar(200),
  @siteName nvarchar(200),
  @logged datetime,
  @level varchar(5),
  @userName nvarchar(200),
  @message nvarchar(max),
  @logger nvarchar(300),
  @properties nvarchar(max),
  @serverName nvarchar(200),
  @port nvarchar(100),
  @url nvarchar(2000),
  @https bit,
  @serverAddress nvarchar(100),
  @remoteAddress nvarchar(100),
  @callSite nvarchar(300),
  @exception nvarchar(max)
) AS
BEGIN
  INSERT INTO [dbo].[NLog] (
    [MachineName],
    [SiteName],
    [Logged],
    [Level],
    [UserName],
    [Message],
    [Logger],
    [Properties],
    [ServerName],
    [Port],
    [Url],
    [Https],
    [ServerAddress],
    [RemoteAddress],
    [CallSite],
    [Exception]
  ) VALUES (
    @machineName,
    @siteName,
    @logged,
    @level,
    @userName,
    @message,
    @logger,
    @properties,
    @serverName,
    @port,
    @url,
    @https,
    @serverAddress,
    @remoteAddress,
    @callSite,
    @exception
  );
END








GO
