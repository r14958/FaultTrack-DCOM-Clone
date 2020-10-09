CREATE TABLE [dbo].[UserToken]
(
	[UserTokenId] INT NOT NULL IDENTITY, 
    [UserId] INT NOT NULL, 
    [ExpirationDate] DATETIME NOT NULL, 
    [Token] NVARCHAR(128) NOT NULL
	CONSTRAINT [PK_UserToken] PRIMARY KEY ([UserTokenId]),
	CONSTRAINT [FK_UserToken_User] FOREIGN KEY ([UserId]) REFERENCES [User]([UserId])
)