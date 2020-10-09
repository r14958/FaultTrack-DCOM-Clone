CREATE TABLE [dbo].[Project]
(
	[ProjectId] INT NOT NULL IDENTITY,
    [ParentProjectId] INT NULL, 
    [ProjectCollectionId] INT NOT NULL, 
    [Name] NVARCHAR(128) NOT NULL, 
	CONSTRAINT [PK_Project] PRIMARY KEY ([ProjectId]),
    CONSTRAINT [FK_Project_ProjectCollection] FOREIGN KEY ([ProjectCollectionId]) REFERENCES [ProjectCollection]([ProjectCollectionId]), 
    CONSTRAINT [FK_Project_Project] FOREIGN KEY ([ParentProjectId]) REFERENCES [Project]([ProjectId]), 
    CONSTRAINT [CK_Project_ParentProjectId] CHECK ([ProjectId] != [ParentProjectId]),
	CONSTRAINT [AK_Project_Name] UNIQUE NONCLUSTERED ([ProjectCollectionId], [ParentProjectId], [Name]),
	CONSTRAINT [CK_Project_Name] CHECK ([Name] != N'')
)