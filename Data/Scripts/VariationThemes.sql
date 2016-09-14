USE [red-data-prod-local]
GO
SET IDENTITY_INSERT [dbo].[VariationThemes_X] ON 

INSERT [dbo].[VariationThemes_X] ([VariationThemeID], [Reference], [Title], [ImageID], [ProductType], [Manufacturer], [BrandName], [VariationThemeTypeName], [VariationThemeName1], [VariationThemeName2], [VariationThemeName3], [VariationThemeName4], [VariationThemeName5], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (1, N'Dummy', N'DummyTheme', NULL, NULL, NULL, NULL, N'SizeColor', N'Size', N'Color', NULL, NULL, NULL, N'Migration', CAST(N'2016-05-18 00:00:00.0000000' AS DateTime2), N'Migration', CAST(N'2016-05-18 00:00:00.0000000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[VariationThemes_X] OFF
