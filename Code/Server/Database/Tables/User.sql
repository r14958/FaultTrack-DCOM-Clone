CREATE TABLE [dbo].[User]
(
	[UserId] INT NOT NULL IDENTITY, 
    [UserName] NVARCHAR(128) NOT NULL, 
    [PasswordHash] NVARCHAR(128) NOT NULL,
    [FirstName] NVARCHAR(MAX) NULL,
    [MiddleInitial] NVARCHAR(1) NULL,
    [LastName] NVARCHAR(MAX) NULL,
	[Email] NVARCHAR(254) NOT NULL,
    [NormalizedEmail] AS (UPPER([Email])) PERSISTED, 
    [NormalizedUserName] AS (UPPER([UserName])) PERSISTED,
    CONSTRAINT [PK_User] PRIMARY KEY ([UserId])
)