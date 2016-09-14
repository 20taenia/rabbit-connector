USE [red-data-prod-local]

DECLARE @SourceTable VARCHAR(200)
DECLARE @ReferenceTable VARCHAR(200)
DECLARE @DisableConstraints NVARCHAR(MAX) = N'';

------------ DISABLE CONSTRAINTS --------------
DECLARE disablingCursor CURSOR FOR  
SELECT DISTINCT OBJECT_NAME(referenced_object_id) as ReferenceTable  
FROM sys.foreign_keys WHERE OBJECT_NAME(object_id) LIKE '%_X'
UNION
SELECT DISTINCT OBJECT_NAME(parent_object_id) as SourceTable  
FROM sys.foreign_keys WHERE OBJECT_NAME(object_id) LIKE '%_X'

OPEN disablingCursor   
FETCH NEXT FROM disablingCursor INTO @ReferenceTable

WHILE @@FETCH_STATUS = 0   
BEGIN   
		--Disable constraints
		SET @DisableConstraints = N'ALTER TABLE ' + QUOTENAME(@ReferenceTable) + ' NOCHECK CONSTRAINT ALL ;'
		PRINT @DisableConstraints 
		EXEC(@DisableConstraints)

		FETCH NEXT FROM disablingCursor INTO @ReferenceTable   
END   
CLOSE disablingCursor   
DEALLOCATE disablingCursor

------------ COPY DATA --------------
--******* Warehouses
SET IDENTITY_INSERT [Warehouses_X] ON

INSERT INTO [dbo].[Warehouses_X] (WarehouseID, Reference, IsCharonWarehouse, CountryID, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
	SELECT Id, Name, CASE Discriminator WHEN 'CharonWarehouse' THEN 1 ELSE 0 END AS IsCharonWarehouse, Location_Country_Abbreviation, 'Migration' as CreatedBy, GETDATE() as CreatedDate, 'Migration' as ModifiedBy, GETDATE() as ModifiedDate 
	FROM Warehouses

SET IDENTITY_INSERT [Warehouses_X] OFF

UPDATE Warehouses_X SET CountryID = 'GB' WHERE CountryID = 'UK'

--******* Marketplaces
SET IDENTITY_INSERT [Marketplaces_X] ON

INSERT INTO [dbo].[Marketplaces_X] (MarketplaceID, Reference, Name, [LanguageID], CurrencyID, CountryID, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
	SELECT Id as MarketplaceID, Discriminator+'_' + CONVERT(VARCHAR, Id) as Reference, Name, 
	CASE [Language]	WHEN 0 THEN 'fr'
					WHEN 1 THEN 'en'
					WHEN 2 THEN 'es'
					WHEN 3 THEN 'ru'
					WHEN 4 THEN 'de'
					WHEN 5 THEN 'it'
					ELSE 'en' 
	END as [LanguageID], 
	CASE Currency	WHEN 0 THEN 'EUR'
					WHEN 1 THEN 'USD'
					WHEN 2 THEN 'AUD'
					WHEN 3 THEN 'CAD'
					WHEN 4 THEN 'GBP'
					WHEN 5 THEN 'JPY'
					WHEN 6 THEN 'INR'
					WHEN 7 THEN 'CNY'
					ELSE 'GBP' 
	END as [CurrencyID],
	ISNULL(Country_Abbreviation, 'UK') as CountryID,
	'Migration' as CreatedBy, GETDATE() as CreatedDate, 'Migration' as ModifiedBy, GETDATE() as ModifiedDate
	FROM Marketplaces 

SET IDENTITY_INSERT [Marketplaces_X] OFF

UPDATE Marketplaces_X SET CountryID = 'GB' WHERE CountryID = 'UK'

--******* Factories

	DECLARE @ID int
	DECLARE @Code nvarchar(4000)
	DECLARE @Name nvarchar(4000)
	DECLARE @Location_Street nvarchar(4000)
	DECLARE @Location_City nvarchar(4000)
	DECLARE @Location_CountyOrState nvarchar(4000)
	DECLARE @Location_Country_Name nvarchar(4000)
	DECLARE @Location_Country_Abbreviation nvarchar(4000)
	DECLARE @Location_ZipCode nvarchar(4000)
	DECLARE @PrimaryContact_Name nvarchar(4000)
	DECLARE @PrimaryContact_PhoneNumber nvarchar(4000)
	DECLARE @PrimaryContact_CompanyName nvarchar(4000)
	DECLARE @PrimaryContact_EmailAddress nvarchar(4000)
	DECLARE @PrimaryContact_Address_Street nvarchar(4000)
	DECLARE @PrimaryContact_Address_City nvarchar(4000)
	DECLARE @PrimaryContact_Address_CountyOrState nvarchar(4000)
	DECLARE @PrimaryContact_Address_Country_Name nvarchar(4000)
	DECLARE @PrimaryContact_Address_Country_Abbreviation nvarchar(4000)
	DECLARE @PrimaryContact_Address_ZipCode nvarchar(4000)
	DECLARE @SecondContact_Name nvarchar(4000)
	DECLARE @SecondContact_PhoneNumber nvarchar(4000)
	DECLARE @SecondContact_CompanyName nvarchar(4000)
	DECLARE @SecondContact_EmailAddress nvarchar(4000)
	DECLARE @SecondContact_Address_Street nvarchar(4000)
	DECLARE @SecondContact_Address_City nvarchar(4000)
	DECLARE @SecondContact_Address_CountyOrState nvarchar(4000)
	DECLARE @SecondContact_Address_Country_Name nvarchar(4000)
	DECLARE @SecondContact_Address_Country_Abbreviation nvarchar(4000)
	DECLARE @SecondContact_Address_ZipCode nvarchar(4000)
	DECLARE @_AverageTimeToManufacture10000Ticks bigint
	DECLARE @_AverageTimeToManufacture5000Ticks bigint
	DECLARE @_AverageTimeToManufacture1000Ticks bigint
	DECLARE @_AverageTimeToManufacture500Ticks bigint
	DECLARE @Deposit float

	DECLARE @FactoryAddressID int
	DECLARE @PrimaryAddressID int
	DECLARE @SecondaryAddressID int

	DECLARE factoryCursor CURSOR FOR  
	SELECT * FROM Factories

	OPEN factoryCursor   
	FETCH NEXT FROM factoryCursor INTO 
	@ID, @Code, @Name, @Location_Street, @Location_City, @Location_CountyOrState, @Location_Country_Name, @Location_Country_Abbreviation, @Location_ZipCode, 
	@PrimaryContact_Name, @PrimaryContact_PhoneNumber, @PrimaryContact_CompanyName, @PrimaryContact_EmailAddress, @PrimaryContact_Address_Street, 
	@PrimaryContact_Address_City, @PrimaryContact_Address_CountyOrState, @PrimaryContact_Address_Country_Name, @PrimaryContact_Address_Country_Abbreviation, @PrimaryContact_Address_ZipCode, 
	@SecondContact_Name, @SecondContact_PhoneNumber, @SecondContact_CompanyName, @SecondContact_EmailAddress, @SecondContact_Address_Street, @SecondContact_Address_City, 
	@SecondContact_Address_CountyOrState, @SecondContact_Address_Country_Name, @SecondContact_Address_Country_Abbreviation, @SecondContact_Address_ZipCode, 
	@_AverageTimeToManufacture10000Ticks, @_AverageTimeToManufacture5000Ticks, @_AverageTimeToManufacture1000Ticks, @_AverageTimeToManufacture500Ticks, 
	@Deposit


	WHILE @@FETCH_STATUS = 0   
	BEGIN   
			SET IDENTITY_INSERT Addresses_X OFF

			IF (@Location_Street <> @PrimaryContact_Address_Street)
			BEGIN
				INSERT INTO Addresses_X (FirstName, [LastName], [Company], [Address1], [Address2], [City], [StateProvinceID], [CountryID], [ZipPostalCode], [Email], [PhoneNumber], CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
				VALUES ('', '', '', ISNULL(@Location_Street, ''), NULL, ISNULL(@Location_City, ''), NULL, @Location_Country_Abbreviation, @Location_ZipCode, NULL, NULL, 'Migration', GETDATE(), 'Migration', GETDATE())
				SET @FactoryAddressID = @@IDENTITY
				
				INSERT INTO Addresses_X (FirstName, [LastName], [Company], [Address1], [Address2], [City], [StateProvinceID], [CountryID], [ZipPostalCode], [Email], [PhoneNumber], CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
				VALUES (@PrimaryContact_Name, '', @PrimaryContact_CompanyName, ISNULL(@PrimaryContact_Address_Street, ''), NULL, ISNULL(@PrimaryContact_Address_City, ''), NULL, @PrimaryContact_Address_Country_Abbreviation, @PrimaryContact_Address_ZipCode, @PrimaryContact_EmailAddress, @PrimaryContact_PhoneNumber, 'Migration', GETDATE(), 'Migration', GETDATE())
				SET @PrimaryAddressID = @@IDENTITY
			END
			ELSE
			BEGIN
				INSERT INTO Addresses_X (FirstName, [LastName], [Company], [Address1], [Address2], [City], [StateProvinceID], [CountryID], [ZipPostalCode], [Email], [PhoneNumber], CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
				VALUES (@PrimaryContact_Name, '', @PrimaryContact_CompanyName, ISNULL(@Location_Street, ''), NULL, ISNULL(@Location_City, ''), NULL, @Location_Country_Abbreviation, @Location_ZipCode, NULL, NULL, 'Migration', GETDATE(), 'Migration', GETDATE())
				SET @FactoryAddressID = @@IDENTITY

				SET @PrimaryAddressID = NULL
			END
			
			IF (@SecondContact_Name IS NOT NULL)
			BEGIN
				INSERT INTO Addresses_X (FirstName, [LastName], [Company], [Address1], [Address2], [City], [StateProvinceID], [CountryID], [ZipPostalCode], [Email], [PhoneNumber], CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
				VALUES (@SecondContact_Name, '', @SecondContact_CompanyName,  ISNULL(@SecondContact_Address_Street, '') , NULL, ISNULL(@SecondContact_Address_City, ''), NULL, @SecondContact_Address_Country_Abbreviation, @SecondContact_Address_ZipCode, @SecondContact_EmailAddress, @SecondContact_PhoneNumber, 'Migration', GETDATE(), 'Migration', GETDATE())
				SET @SecondaryAddressID = @@IDENTITY
			END
			ELSE
			BEGIN
				SET @SecondaryAddressID = NULL
			END 
			
			SET IDENTITY_INSERT [Factories_X] ON

			INSERT INTO [dbo].[Factories_X] ([FactoryID], [Reference], [Name], [FactoryAddressID], [PrimaryContactAddressID], [SecondaryContactAddressID], [Deposit], CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
			VALUES (@ID, @Code, @Name, @FactoryAddressID, @PrimaryAddressID, @SecondaryAddressID, @Deposit, 'Migration', GETDATE(), 'Migration', GETDATE())

			SET IDENTITY_INSERT [Factories_X] OFF

			SET IDENTITY_INSERT [FactoryProductionDuration_X] OFF

			INSERT INTO [dbo].[FactoryProductionDuration_X] ([FactoryID], [NoOfUnits], [DaysToProduction], CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
			VALUES (@ID, 500, CONVERT(int, FLOOR(@_AverageTimeToManufacture500Ticks/24/60/60/60/10000000)), 'Migration', GETDATE(), 'Migration', GETDATE())
			INSERT INTO [dbo].[FactoryProductionDuration_X] ([FactoryID], [NoOfUnits], [DaysToProduction], CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
			VALUES (@ID, 1000, CONVERT(int, FLOOR(@_AverageTimeToManufacture1000Ticks/24/60/60/60/10000000)), 'Migration', GETDATE(), 'Migration', GETDATE())
			INSERT INTO [dbo].[FactoryProductionDuration_X] ([FactoryID], [NoOfUnits], [DaysToProduction], CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
			VALUES (@ID, 5000, CONVERT(int, FLOOR(@_AverageTimeToManufacture5000Ticks/24/60/60/60/10000000)), 'Migration', GETDATE(), 'Migration', GETDATE())
			INSERT INTO [dbo].[FactoryProductionDuration_X] ([FactoryID], [NoOfUnits], [DaysToProduction], CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
			VALUES (@ID, 10000, CONVERT(int, FLOOR(@_AverageTimeToManufacture10000Ticks/24/60/60/60/10000000)), 'Migration', GETDATE(), 'Migration', GETDATE())

			FETCH NEXT FROM factoryCursor INTO @ID, @Code, @Name, @Location_Street, @Location_City, @Location_CountyOrState, @Location_Country_Name, @Location_Country_Abbreviation, @Location_ZipCode, 
				@PrimaryContact_Name, @PrimaryContact_PhoneNumber, @PrimaryContact_CompanyName, @PrimaryContact_EmailAddress, @PrimaryContact_Address_Street, 
				@PrimaryContact_Address_City, @PrimaryContact_Address_CountyOrState, @PrimaryContact_Address_Country_Name, @PrimaryContact_Address_Country_Abbreviation, @PrimaryContact_Address_ZipCode, 
				@SecondContact_Name, @SecondContact_PhoneNumber, @SecondContact_CompanyName, @SecondContact_EmailAddress, @SecondContact_Address_Street, @SecondContact_Address_City, 
				@SecondContact_Address_CountyOrState, @SecondContact_Address_Country_Name, @SecondContact_Address_Country_Abbreviation, @SecondContact_Address_ZipCode, 
				@_AverageTimeToManufacture10000Ticks, @_AverageTimeToManufacture5000Ticks, @_AverageTimeToManufacture1000Ticks, @_AverageTimeToManufacture500Ticks, 
				@Deposit   
	END   
	CLOSE factoryCursor   
	DEALLOCATE factoryCursor


--******* Products
SET IDENTITY_INSERT [Products_X] ON

INSERT INTO [dbo].[Products_X] (ProductID, Barcode, Name, [Description], OwnerID, PhysicalAttributeID, isDeleted, isDisContinued, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
	SELECT	ID as ProductID, BarcodeNumber as Barcode, Name, null as [Description], 1 as OwnerID, null as PhysicalAttributeID, 
			0 as isDeleted, 0 as isDiscontinued, 'Migration' as CreatedBy, GETDATE() as CreatedDate, 'Migration' as ModifiedBy, GETDATE() as ModifiedDate 
	FROM Products 

SET IDENTITY_INSERT [Products_X] OFF


--******* ProductPhysicalAttributes
DECLARE @PhysicalAttribID INT
DECLARE @ProductID float
DECLARE @Length float
DECLARE @Width float
DECLARE @Height float
DECLARE @Weight float
DECLARE @PackageLength float
DECLARE @PackageWidth float
DECLARE @PackageHeight float
DECLARE @PackageWeight float

DECLARE physicalAttribCursor CURSOR FOR  
SELECT ID, Dimensions_Length, Dimensions_Width, Dimensions_Height, [Weight], PackageDimensions_Length, PackageDimensions_Width, PackageDimensions_Height, PackagedWeight FROM Products

OPEN physicalAttribCursor   
FETCH NEXT FROM physicalAttribCursor INTO 
@ProductID, @Length, @Width, @Height, @Weight, @PackageLength, @PackageWidth, @PackageHeight, @PackageWeight

WHILE @@FETCH_STATUS = 0   
BEGIN 

	INSERT INTO ProductPhysicalAttributes_X([Length], Width, Height, [Weight], PackagingLength, PackagingWidth, PackagingHeight, PackagedWeight, IsDeleted, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
	SELECT @Length AS [Length], @Width AS Width, @Height AS Height, @Weight AS [Weight], @PackageLength AS PackagingLength, @PackageWidth AS PackagingWidth, @PackageHeight AS PackageHeight, @PackageWeight AS PackagedWeight, 0 as IsDeleted, 
		'Migration' as CreatedBy, GETDATE() as CreatedDate, 'Migration' as ModifiedBy, GETDATE() as ModifiedDate 

	SELECT @PhysicalAttribID = SCOPE_IDENTITY();

	UPDATE Products_X SET PhysicalAttributeID = @PhysicalAttribID WHERE ProductID = @ProductID
 
	FETCH NEXT FROM physicalAttribCursor INTO 
	@ProductID, @Length, @Width, @Height, @Weight, @PackageLength, @PackageWidth, @PackageHeight, @PackageWeight
END
CLOSE physicalAttribCursor
DEALLOCATE physicalAttribCursor


--******* MarketplaceProducts

SET IDENTITY_INSERT [MarketplaceProducts_X] ON

INSERT INTO [dbo].[MarketplaceProducts_X]	(MarketplaceProductID, ProductID, MarketplaceID, FulfilmentWarehouseID, Reference, SKU, Title, [Description],
											 RecommendedRetailPrice, StrikeOutPrice, ProductConditionID, 
											 KeyFeature1,KeyFeature2,KeyFeature3,KeyFeature4,KeyFeature5,KeyFeature6,KeyFeature7,KeyFeature8,KeyFeature9,KeyFeature10,
											 Keyword1,Keyword2,Keyword3,Keyword4,Keyword5,Keyword6,Keyword7,Keyword8,Keyword9,Keyword10,
											 BrowseNode1,BrowseNode2,BrowseNode3,BrowseNode4,BrowseNode5,
											 VariationThemeID,
											 VariationTheme1,VariationTheme2,VariationTheme3,VariationTheme4,VariationTheme5,
											 ImageMedia1ID,ImageMedia2ID,ImageMedia3ID,ImageMedia4ID,ImageMedia5ID,ImageMedia6ID,ImageMedia7ID,ImageMedia8ID,ImageMedia9ID,ImageMedia10ID,
											 isDeleted, DoNotResend,
											 CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
	SELECT	MP.ID as MarketplaceProductID, MP.Product_ID as ProductID, MP.Marketplace_ID as MarketplaceID, 1 as FulfilmentWarehouseID, NULL as Reference, NULL as SKU, P.Name as Title, NULL as [Description],
			P.RecommendedRetailPrice as RecommendedRetailPrice, P.StrikeOutPrice as StrikeOutPrice, 1 as ProductConditionID, 
			MP.SpecialFeature1 as KeyFeature1, MP.SpecialFeature2 as KeyFeature2, MP.SpecialFeature3 as KeyFeature3, MP.SpecialFeature4 as KeyFeature4, MP.SpecialFeature5 as KeyFeature5, 
			NULL as KeyFeature6, NULL as KeyFeature7, NULL as KeyFeature8, NULL as KeyFeature9, NULL as KeyFeature10, 
			NULL as Keyword1,NULL as Keyword2,NULL as Keyword3,NULL as Keyword4,NULL as Keyword5,NULL as Keyword6,NULL as Keyword7,NULL as Keyword8,NULL as Keyword9,NULL as Keyword10,
			NULL as BrowseNode1, NULL as BrowseNode2, NULL as BrowseNode3, NULL as BrowseNode4, NULL as BrowseNode5,
			1 as VariationThemeID,
			NULL as VariationThemeName1, NULL as VariationThemeName2, NULL as VariationThemeName3, NULL as VariationThemeName4, NULL as VariationThemeName5,
			NULL as ImageMediaID1, NULL as ImageMediaID2, NULL as ImageMediaID3, NULL as ImageMediaID4, NULL as ImageMediaID5, NULL as ImageMediaID6, NULL as ImageMediaID7, NULL as ImageMediaID8, NULL as ImageMediaID9, NULL as ImageMediaID10,
			0 as isDeleted, 0 as DoNotResend,
			'Migration' as CreatedBy, GETDATE() as CreatedDate, 'Migration' as ModifiedBy, GETDATE() as ModifiedDate 
	FROM MarketplaceProducts MP, Products P, Marketplaces M
	WHERE MP.Product_ID = P.ID AND MP.Marketplace_ID = M.ID

SET IDENTITY_INSERT [MarketplaceProducts_X] OFF

--******* FactoryProducts

SET IDENTITY_INSERT [FactoryProducts_X] ON

INSERT INTO [dbo].[FactoryProducts_X] ([FactoryProductID], [ProductID], [FactoryID], [Reference], [ProductionCost], QuantityInBox, HTCCode, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
	SELECT	FPL.ID as [FactoryProductID], FPL.Product_ID as [ProductID], FPL.Factory_ID as [FactoryID], F.Code + '_' + P.BarcodeNumber as [Reference], FPL.Cost as [ProductionCost], P.QuantityInBox, P.HarmonizedTarifCode,
			'Migration' as CreatedBy, GETDATE() as CreatedDate, 'Migration' as ModifiedBy, GETDATE() as ModifiedDate 
	FROM [dbo].[FactoryProductListings] FPL, Factories F, Products P
	WHERE FPL.Factory_ID = F.ID
		  AND FPL.Product_ID = P.ID

SET IDENTITY_INSERT [FactoryProducts_X] OFF

--******* Media

SET IDENTITY_INSERT [Media_X] ON

INSERT INTO [dbo].[Media_X] ([MediaID], [FileName], [FilePath], [MimeType], [IsDeleted], CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
SELECT [ID] ,[Name],[FilePath],[MIMEType],[FileRemoved],'Migration' as CreatedBy, GETDATE() as CreatedDate, 'Migration' as ModifiedBy, GETDATE() as ModifiedDate  
  FROM [dbo].[Media]

SET IDENTITY_INSERT [Media_X] OFF

------------ ENABLE CONSTRAINTS --------------

DECLARE @EnableTable VARCHAR(200)
DECLARE @EnableConstraints NVARCHAR(MAX) = N'';
DECLARE enablingCursor CURSOR FOR  

SELECT DISTINCT OBJECT_NAME(referenced_object_id) as ReferenceTable  
FROM sys.foreign_keys WHERE OBJECT_NAME(object_id) LIKE '%_X'
UNION
SELECT DISTINCT OBJECT_NAME(parent_object_id) as SourceTable  
FROM sys.foreign_keys WHERE OBJECT_NAME(object_id) LIKE '%_X'

OPEN enablingCursor   
FETCH NEXT FROM enablingCursor INTO @EnableTable

WHILE @@FETCH_STATUS = 0   
BEGIN   
		--Enable constraints
		SET @EnableConstraints = N'ALTER TABLE ' + QUOTENAME(@EnableTable) + ' WITH CHECK CHECK CONSTRAINT ALL ;'
		PRINT @EnableConstraints 
		EXEC(@EnableConstraints)

		FETCH NEXT FROM enablingCursor INTO @EnableTable   
END   
CLOSE enablingCursor   
DEALLOCATE enablingCursor



------------ DROP TABLES --------------
--DECLARE @DropTables NVARCHAR(MAX) = N'';
--DECLARE dropTablesCursor CURSOR FOR  
--SELECT DISTINCT OBJECT_NAME(referenced_object_id) as ReferenceTable  
--FROM sys.foreign_keys WHERE OBJECT_NAME(object_id) LIKE '%_X'
--UNION
--SELECT DISTINCT OBJECT_NAME(parent_object_id) as SourceTable  
--FROM sys.foreign_keys WHERE OBJECT_NAME(object_id) LIKE '%_X'

--OPEN dropTablesCursor   
--FETCH NEXT FROM dropTablesCursor INTO @ReferenceTable

--WHILE @@FETCH_STATUS = 0   
--BEGIN   
--		--Enable constraints
--		--ALTER TABLE MyTable WITH CHECK CHECK CONSTRAINT ALL
--		SET @DropTables = N'DROP TABLE ' + QUOTENAME(@ReferenceTable) + ';'

--		PRINT @DropTables --exec sp_executesql
		
--		FETCH NEXT FROM dropTablesCursor INTO @ReferenceTable   
--END   
--CLOSE dropTablesCursor   
--DEALLOCATE dropTablesCursor


--SELECT * FROM MarketplaceProducts

--SELECT * FROM Marketplaces

--SELECT * FROM Products

--SELECT * FROM Warehouses

--SELECT * FROM Factories

--SELECT * FROM [dbo].[FactoryProductListings] WHERE Cost <> 0