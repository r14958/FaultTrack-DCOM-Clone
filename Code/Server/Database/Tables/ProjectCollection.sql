CREATE TABLE [dbo].[ProjectCollection]
(
	[ProjectCollectionId] INT NOT NULL IDENTITY, 
    [Name] NVARCHAR(128) NOT NULL,
	CONSTRAINT [PK_ProjectCollection] PRIMARY KEY ([ProjectCollectionId]), 
	CONSTRAINT [AK_ProjectCollection_Name] UNIQUE NONCLUSTERED ([Name]),
    CONSTRAINT [CK_ProjectCollection_Name] CHECK ([Name] != N'')
)
