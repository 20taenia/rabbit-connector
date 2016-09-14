USE [Charon]
GO
/****** Object:  Table [dbo].[FactoryProducts]    Script Date: 11/05/2016 17:22:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FactoryProducts](
	[FactoryProductID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[Reference] [nvarchar](500) NULL,
	[PhysicalAttributeID] [int] NULL,
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
 CONSTRAINT [PK_FactoryProducts] PRIMARY KEY CLUSTERED 
(
	[FactoryProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MarketplaceProducts]    Script Date: 11/05/2016 17:22:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MarketplaceProducts](
	[MarketplaceProductID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[MarketplaceID] [int] NOT NULL,
	[Reference] [nvarchar](100) NULL,
	[Title] [nvarchar](1000) NULL,
	[Description] [nvarchar](4000) NULL,
	[RecommendedRetailPrice] [decimal](18, 4) NULL,
	[StrikeOutPrice] [decimal](18, 4) NULL,
	[ProductConditionID] [int] NULL,
	[PhysicalAttributeID] [int] NULL,
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
 CONSTRAINT [PK_MarketplaceProducts] PRIMARY KEY CLUSTERED 
(
	[MarketplaceProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Marketplaces]    Script Date: 11/05/2016 17:22:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Marketplaces](
	[MarketplaceID] [int] NOT NULL,
	[Reference] [nvarchar](100) NOT NULL,
	[Name] [nvarchar](500) NOT NULL,
	[Language] [nvarchar](500) NOT NULL,
	[CurrencyCode] [nvarchar](500) NOT NULL,
	[CountryName] [nvarchar](500) NOT NULL,
	[CountryCode] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Marketplaces] PRIMARY KEY CLUSTERED 
(
	[MarketplaceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Media]    Script Date: 11/05/2016 17:22:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Media](
	[MediaID] [int] NOT NULL,
	[FileName] [nvarchar](500) NULL,
	[FilePath] [nvarchar](4000) NULL,
 CONSTRAINT [PK_Media] PRIMARY KEY CLUSTERED 
(
	[MediaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Owners]    Script Date: 11/05/2016 17:22:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Owners](
	[OwnerID] [int] NOT NULL,
	[Name] [nvarchar](300) NULL,
	[Description] [nvarchar](4000) NULL,
	[IsCharonCompany] [bit] NOT NULL,
 CONSTRAINT [PK_Owners] PRIMARY KEY CLUSTERED 
(
	[OwnerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductConditions]    Script Date: 11/05/2016 17:22:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductConditions](
	[ProductConditionID] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](4000) NULL,
	[IsNew] [bit] NOT NULL,
	[IsRefurbished] [bit] NOT NULL,
	[IsUsed] [bit] NOT NULL,
 CONSTRAINT [PK_ProductConditions] PRIMARY KEY CLUSTERED 
(
	[ProductConditionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductPhysicalAttributes]    Script Date: 11/05/2016 17:22:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductPhysicalAttributes](
	[PhysicalAttributeID] [int] NOT NULL,
	[Length] [decimal](8, 4) NULL,
	[Width] [decimal](8, 4) NULL,
	[Height] [decimal](8, 4) NULL,
	[PackagingLength] [decimal](8, 4) NULL,
	[PackagingWidth] [decimal](8, 4) NULL,
	[PackagingHeight] [decimal](8, 4) NULL,
	[Weight] [decimal](8, 4) NULL,
	[PackagedWeight] [decimal](8, 4) NULL,
 CONSTRAINT [PK_PhysicalAttributes] PRIMARY KEY CLUSTERED 
(
	[PhysicalAttributeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Products]    Script Date: 11/05/2016 17:22:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Products](
	[ProductID] [int] NOT NULL,
	[Barcode] [varchar](20) NOT NULL,
	[Name] [nvarchar](1000) NULL,
	[Description] [nvarchar](4000) NULL,
	[OwnerID] [int] NOT NULL,
	[PhysicalAttributeID] [int] NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[VariationThemes]    Script Date: 11/05/2016 17:22:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VariationThemes](
	[VariationThemeID] [int] NOT NULL,
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
 CONSTRAINT [PK_VariationThemesX] PRIMARY KEY CLUSTERED 
(
	[VariationThemeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[WarehouseProducts]    Script Date: 11/05/2016 17:22:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WarehouseProducts](
	[WarehouseProductID] [int] NOT NULL,
	[ProductID] [int] NULL,
	[PhysicalAttributeID] [int] NULL,
	[DaysofBuffer] [int] NULL,
 CONSTRAINT [PK_WarehouseProducts] PRIMARY KEY CLUSTERED 
(
	[WarehouseProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

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
ALTER TABLE [dbo].[FactoryProducts]  WITH CHECK ADD  CONSTRAINT [FK_FactoryProducts_PhysicalAttribute] FOREIGN KEY([PhysicalAttributeID])
REFERENCES [dbo].[ProductPhysicalAttributes] ([PhysicalAttributeID])
GO
ALTER TABLE [dbo].[FactoryProducts] CHECK CONSTRAINT [FK_FactoryProducts_PhysicalAttribute]
GO
ALTER TABLE [dbo].[FactoryProducts]  WITH CHECK ADD  CONSTRAINT [FK_FactoryProducts_Products] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ProductID])
GO
ALTER TABLE [dbo].[FactoryProducts] CHECK CONSTRAINT [FK_FactoryProducts_Products]
GO
ALTER TABLE [dbo].[MarketplaceProducts]  WITH CHECK ADD  CONSTRAINT [FK_MarketplaceProducts_Marketplaces] FOREIGN KEY([MarketplaceID])
REFERENCES [dbo].[Marketplaces] ([MarketplaceID])
GO
ALTER TABLE [dbo].[MarketplaceProducts] CHECK CONSTRAINT [FK_MarketplaceProducts_Marketplaces]
GO
ALTER TABLE [dbo].[MarketplaceProducts]  WITH CHECK ADD  CONSTRAINT [FK_MarketplaceProducts_Media1] FOREIGN KEY([ImageMediaID1])
REFERENCES [dbo].[Media] ([MediaID])
GO
ALTER TABLE [dbo].[MarketplaceProducts] CHECK CONSTRAINT [FK_MarketplaceProducts_Media1]
GO
ALTER TABLE [dbo].[MarketplaceProducts]  WITH CHECK ADD  CONSTRAINT [FK_MarketplaceProducts_Media10] FOREIGN KEY([ImageMediaID10])
REFERENCES [dbo].[Media] ([MediaID])
GO
ALTER TABLE [dbo].[MarketplaceProducts] CHECK CONSTRAINT [FK_MarketplaceProducts_Media10]
GO
ALTER TABLE [dbo].[MarketplaceProducts]  WITH CHECK ADD  CONSTRAINT [FK_MarketplaceProducts_Media2] FOREIGN KEY([ImageMediaID2])
REFERENCES [dbo].[Media] ([MediaID])
GO
ALTER TABLE [dbo].[MarketplaceProducts] CHECK CONSTRAINT [FK_MarketplaceProducts_Media2]
GO
ALTER TABLE [dbo].[MarketplaceProducts]  WITH CHECK ADD  CONSTRAINT [FK_MarketplaceProducts_Media3] FOREIGN KEY([ImageMediaID3])
REFERENCES [dbo].[Media] ([MediaID])
GO
ALTER TABLE [dbo].[MarketplaceProducts] CHECK CONSTRAINT [FK_MarketplaceProducts_Media3]
GO
ALTER TABLE [dbo].[MarketplaceProducts]  WITH CHECK ADD  CONSTRAINT [FK_MarketplaceProducts_Media4] FOREIGN KEY([ImageMediaID4])
REFERENCES [dbo].[Media] ([MediaID])
GO
ALTER TABLE [dbo].[MarketplaceProducts] CHECK CONSTRAINT [FK_MarketplaceProducts_Media4]
GO
ALTER TABLE [dbo].[MarketplaceProducts]  WITH CHECK ADD  CONSTRAINT [FK_MarketplaceProducts_Media5] FOREIGN KEY([ImageMediaID5])
REFERENCES [dbo].[Media] ([MediaID])
GO
ALTER TABLE [dbo].[MarketplaceProducts] CHECK CONSTRAINT [FK_MarketplaceProducts_Media5]
GO
ALTER TABLE [dbo].[MarketplaceProducts]  WITH CHECK ADD  CONSTRAINT [FK_MarketplaceProducts_Media6] FOREIGN KEY([ImageMediaID6])
REFERENCES [dbo].[Media] ([MediaID])
GO
ALTER TABLE [dbo].[MarketplaceProducts] CHECK CONSTRAINT [FK_MarketplaceProducts_Media6]
GO
ALTER TABLE [dbo].[MarketplaceProducts]  WITH CHECK ADD  CONSTRAINT [FK_MarketplaceProducts_Media7] FOREIGN KEY([ImageMediaID7])
REFERENCES [dbo].[Media] ([MediaID])
GO
ALTER TABLE [dbo].[MarketplaceProducts] CHECK CONSTRAINT [FK_MarketplaceProducts_Media7]
GO
ALTER TABLE [dbo].[MarketplaceProducts]  WITH CHECK ADD  CONSTRAINT [FK_MarketplaceProducts_Media8] FOREIGN KEY([ImageMediaID8])
REFERENCES [dbo].[Media] ([MediaID])
GO
ALTER TABLE [dbo].[MarketplaceProducts] CHECK CONSTRAINT [FK_MarketplaceProducts_Media8]
GO
ALTER TABLE [dbo].[MarketplaceProducts]  WITH CHECK ADD  CONSTRAINT [FK_MarketplaceProducts_Media9] FOREIGN KEY([ImageMediaID9])
REFERENCES [dbo].[Media] ([MediaID])
GO
ALTER TABLE [dbo].[MarketplaceProducts] CHECK CONSTRAINT [FK_MarketplaceProducts_Media9]
GO
ALTER TABLE [dbo].[MarketplaceProducts]  WITH CHECK ADD  CONSTRAINT [FK_MarketplaceProducts_ProductConditions] FOREIGN KEY([ProductConditionID])
REFERENCES [dbo].[ProductConditions] ([ProductConditionID])
GO
ALTER TABLE [dbo].[MarketplaceProducts] CHECK CONSTRAINT [FK_MarketplaceProducts_ProductConditions]
GO
ALTER TABLE [dbo].[MarketplaceProducts]  WITH CHECK ADD  CONSTRAINT [FK_MarketplaceProducts_ProductPhysicalAttributes] FOREIGN KEY([PhysicalAttributeID])
REFERENCES [dbo].[ProductPhysicalAttributes] ([PhysicalAttributeID])
GO
ALTER TABLE [dbo].[MarketplaceProducts] CHECK CONSTRAINT [FK_MarketplaceProducts_ProductPhysicalAttributes]
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
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Owners] FOREIGN KEY([OwnerID])
REFERENCES [dbo].[Owners] ([OwnerID])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Owners]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_ProductPhysicalAttributes] FOREIGN KEY([PhysicalAttributeID])
REFERENCES [dbo].[ProductPhysicalAttributes] ([PhysicalAttributeID])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_ProductPhysicalAttributes]
GO
ALTER TABLE [dbo].[VariationThemes]  WITH CHECK ADD  CONSTRAINT [FK_VariationThemes_Media] FOREIGN KEY([ImageID])
REFERENCES [dbo].[Media] ([MediaID])
GO
ALTER TABLE [dbo].[VariationThemes] CHECK CONSTRAINT [FK_VariationThemes_Media]
GO
ALTER TABLE [dbo].[WarehouseProducts]  WITH CHECK ADD  CONSTRAINT [FK_WarehouseProducts_ProductPhysicalAttributes] FOREIGN KEY([PhysicalAttributeID])
REFERENCES [dbo].[ProductPhysicalAttributes] ([PhysicalAttributeID])
GO
ALTER TABLE [dbo].[WarehouseProducts] CHECK CONSTRAINT [FK_WarehouseProducts_ProductPhysicalAttributes]
GO
ALTER TABLE [dbo].[WarehouseProducts]  WITH CHECK ADD  CONSTRAINT [FK_WarehouseProducts_Products] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ProductID])
GO
ALTER TABLE [dbo].[WarehouseProducts] CHECK CONSTRAINT [FK_WarehouseProducts_Products]
GO
