-- =============================================
-- Author:		Javier Espinoza
-- Create date: 2021-09-30
-- Description:	CRUD de Contact
-- =============================================
CREATE PROCEDURE USP_CRUDContact(
@Id BIGINT=NULL,
@FullName VARCHAR(100)=NULL,
@Email VARCHAR(50)=NULL,
@PhoneNumber VARCHAR(20)=NULL,
@ContactDate DATETIME=NULL,
@Country VARCHAR(100)=NULL
,@Option INT = 0
,@PageNumber INT = 0
,@RecordsPerPage INT = 0
,@ReturnAll BIT = 0)
AS
BEGIN
	IF @Option=1 BEGIN -->Accion para crear nuevo registro
		BEGIN TRY
			INSERT INTO [Contact]([FullName],[Email],[PhoneNumber],[ContactDate],[Country]) 
			VALUES (@FullName,@Email,@PhoneNumber,@ContactDate,@Country)
			SELECT CAST(SCOPE_IDENTITY() AS BIGINT) as IdAfected
				,'Registro creado exitosamente...' as [Description]
				,1 as Success
			FOR JSON PATH, INCLUDE_NULL_VALUES, WITHOUT_ARRAY_WRAPPER
		END TRY
		BEGIN CATCH
			SELECT
				 0 as IdAfected
				,ERROR_MESSAGE() as [Description]
				,0 as Success
			FOR JSON PATH, INCLUDE_NULL_VALUES, WITHOUT_ARRAY_WRAPPER
		END CATCH
	END
	IF @Option=2 BEGIN -->Accion para actualizar registro
		BEGIN TRY
			UPDATE TOP(1) [Contact] SET
				[FullName]=ISNULL(@FullName,[FullName]),
				[Email]=ISNULL(@Email,[Email]),
				[PhoneNumber]=ISNULL(@PhoneNumber,[PhoneNumber]),
				[ContactDate]=ISNULL(@ContactDate,[ContactDate]),
				[Country]=ISNULL(@Country,[Country])
			WHERE [Id]=@Id
			SELECT @Id as IdAfected
				,'Registro actualizado exitosamente...' as [Description]
				,1 as Success
			FOR JSON PATH, INCLUDE_NULL_VALUES, WITHOUT_ARRAY_WRAPPER
		END TRY
		BEGIN CATCH
			SELECT
				 0 as IdAfected
				,ERROR_MESSAGE() as [Description]
				,0 as Success
			FOR JSON PATH, INCLUDE_NULL_VALUES, WITHOUT_ARRAY_WRAPPER
		END CATCH
	END
	IF @Option=4 BEGIN -->Accion para extraer datos paginados o sin paginar
		IF @ReturnAll=1 BEGIN
			With ContactActived As
			(
				SELECT C.[Id],
				C.[FullName],
				C.[Email],
				C.[PhoneNumber],
				C.[ContactDate],
				C.[Country]
			FROM [Contact] C WITH(NOLOCK)
 
			),
			RecordCount AS (SELECT TOP(1) COUNT(1) As TotalRecords FROM ContactActived)
			Select *
			From ContactActived, RecordCount
			Order By [Id] ASC
			OFFSET (@PageNumber - 1) * @RecordsPerPage ROWS
			FETCH NEXT @RecordsPerPage ROWS ONLY
			FOR JSON PATH, INCLUDE_NULL_VALUES
		END
		ELSE BEGIN
			With ContactActived As
			(
				SELECT C.[Id],
				C.[FullName],
				C.[Email],
				C.[PhoneNumber],
				C.[ContactDate],
				C.[Country]
			FROM [Contact] C WITH(NOLOCK)
 
			),
			RecordCount AS (SELECT TOP(1) COUNT(1) As TotalRecords FROM ContactActived)
			Select *
			From ContactActived, RecordCount
			Order By [Id] ASC
			OFFSET (@PageNumber - 1) * @RecordsPerPage ROWS
			FETCH NEXT @RecordsPerPage ROWS ONLY
			FOR JSON PATH, INCLUDE_NULL_VALUES
		END
	END
	IF @Option=5 BEGIN -->Accion para extaer datos por ID
		SELECT TOP(1) [Id],[FullName],[Email],[PhoneNumber],[ContactDate],[Country]
		FROM [Contact] WITH(NOLOCK)
		WHERE [Id]=@Id
		FOR JSON PATH, INCLUDE_NULL_VALUES, WITHOUT_ARRAY_WRAPPER
	END
	IF @Option=6 BEGIN -->Accion para borrar una registro (borrado físico)
		BEGIN TRY
			DELETE TOP(1) FROM [Contact]
			WHERE [Id]=@Id
			SELECT @Id as IdAfected
				,'Registro eliminado exitosamente...' as [Description]
				,1 as Success
			FOR JSON PATH, INCLUDE_NULL_VALUES, WITHOUT_ARRAY_WRAPPER
		END TRY
		BEGIN CATCH
			SELECT
				 0 as IdAfected
				,ERROR_MESSAGE() as [Description]
				,0 as Success
			FOR JSON PATH, INCLUDE_NULL_VALUES, WITHOUT_ARRAY_WRAPPER
		END CATCH
	END
END

