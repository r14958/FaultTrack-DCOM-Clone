CREATE TABLE [dbo].[ProjectVersion]
(
	[ProjectVersionId] INT NOT NULL IDENTITY,
    [ParentProjectVersionId] INT NULL, 
    [ProjectId] INT NOT NULL, 
    [Name] NVARCHAR(128) NOT NULL, 
	CONSTRAINT [PK_ProjectVersion] PRIMARY KEY ([ProjectVersionId]),
    CONSTRAINT [FK_ProjectVersion_Project] FOREIGN KEY ([ProjectId]) REFERENCES [Project]([ProjectId]), 
    CONSTRAINT [FK_ProjectVersion_ProjectVersion] FOREIGN KEY ([ParentProjectVersionId]) REFERENCES [ProjectVersion]([ProjectVersionId]), 
    CONSTRAINT [CK_ProjectVersion_ParentVersionId] CHECK ([ProjectVersionId] != [ParentProjectVersionId]),
	CONSTRAINT [AK_ProjectVersion_Name] UNIQUE NONCLUSTERED ([ProjectId], [ParentProjectVersionId], [Name]),
	CONSTRAINT [CK_ProjectVersion_Name] CHECK ([Name] != N'')
)