DECLARE @SourceTable VARCHAR(200)
DECLARE @ReferenceObject VARCHAR(200)
DECLARE @DisableConstraints NVARCHAR(MAX) = N'';

------------ DISABLE CONSTRAINTS --------------
DECLARE disablingCursor CURSOR FOR  
SELECT DISTINCT OBJECT_NAME(referenced_object_id) as ReferenceTable  
FROM sys.foreign_keys WHERE OBJECT_NAME(object_id) LIKE '%_X'
UNION
SELECT DISTINCT OBJECT_NAME(parent_object_id) as SourceTable  
FROM sys.foreign_keys WHERE OBJECT_NAME(object_id) LIKE '%_X'

OPEN disablingCursor   
FETCH NEXT FROM disablingCursor INTO @ReferenceObject

WHILE @@FETCH_STATUS = 0   
BEGIN   
		--Disable constraints
		SET @DisableConstraints = N'ALTER TABLE ' + QUOTENAME(@ReferenceObject) + ' NOCHECK CONSTRAINT ALL ;'
		PRINT @DisableConstraints 
		EXEC(@DisableConstraints)

		FETCH NEXT FROM disablingCursor INTO @ReferenceObject   
END   
CLOSE disablingCursor   
DEALLOCATE disablingCursor

------------ RENAME TABLES --------------
DECLARE @RenameCommand VARCHAR(200)
DECLARE renamingCursor CURSOR FOR  
SELECT name FROM sysobjects WHERE 
	xtype = 'U' AND name LIKE '%_X' OR
	xtype = 'PK' AND name LIKE '%_X' OR
	xtype = 'D' AND name LIKE '%_X' OR
	xtype = 'F' AND name LIKE '%_X' OR
	xtype = 'UQ' AND name LIKE '%_X'

OPEN renamingCursor   
FETCH NEXT FROM renamingCursor INTO @ReferenceObject

WHILE @@FETCH_STATUS = 0   
BEGIN   
		--Disable constraints
		SET @RenameCommand = 'sp_rename ' + QUOTENAME(@ReferenceObject) + ', ' + REPLACE(QUOTENAME(@ReferenceObject), '_X', '') + ';'
		PRINT @RenameCommand 
		EXEC(@RenameCommand)

		FETCH NEXT FROM renamingCursor INTO @ReferenceObject   
END   
CLOSE renamingCursor   
DEALLOCATE renamingCursor

------------ RE-ENABLE CONSTRAINTS --------------

DECLARE @EnableTable VARCHAR(200)
DECLARE @EnableConstraints NVARCHAR(MAX) = N'';
DECLARE enablingCursor CURSOR FOR  

SELECT DISTINCT OBJECT_NAME(referenced_object_id) as ReferenceTable  
FROM sys.foreign_keys 
UNION
SELECT DISTINCT OBJECT_NAME(parent_object_id) as SourceTable  
FROM sys.foreign_keys 

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