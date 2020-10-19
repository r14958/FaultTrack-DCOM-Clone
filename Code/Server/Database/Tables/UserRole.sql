CREATE TABLE [dbo].[UserRole]
(
	[UserRoleId] INT NOT NULL PRIMARY KEY, 
    [UserId] INT NOT NULL, 
    [RoleId] INT NOT NULL, 
    CONSTRAINT [FK_UserRole_User] FOREIGN KEY ([UserId]) REFERENCES [User]([UserId]),
    CONSTRAINT [FK_UserRole_Role] FOREIGN KEY ([RoleId]) REFERENCES [Role]([RoleId])
)