USE [red-data-prod-local]


DECLARE @SourceTable VARCHAR(200)
DECLARE @ReferenceTable VARCHAR(200)
DECLARE @DropConstraints NVARCHAR(MAX) = N'';
DECLARE @DropTables NVARCHAR(MAX) = N'';

------------ DROP X ForeignKeys and TABLES --------------
DECLARE droppingCursor CURSOR FOR  
SELECT DISTINCT name as ReferenceTable  
FROM sysobjects  WHERE name LIKE '%_X' AND xtype = 'U'

OPEN droppingCursor   
FETCH NEXT FROM droppingCursor INTO @ReferenceTable

WHILE @@FETCH_STATUS = 0   
BEGIN   
		--Remove foreign keys constraints
		SET @DropConstraints = N'sp_fkeys ' + QUOTENAME(@ReferenceTable) + ';'
		PRINT @DropConstraints 
		EXEC(@DropConstraints) 
		--Drop table
		SET @DropTables = N'DROP TABLE ' + QUOTENAME(@ReferenceTable) + ';'
		PRINT @DropTables 
		EXEC(@DropTables)

		FETCH NEXT FROM droppingCursor INTO @ReferenceTable   
END   
CLOSE droppingCursor   
DEALLOCATE droppingCursor
