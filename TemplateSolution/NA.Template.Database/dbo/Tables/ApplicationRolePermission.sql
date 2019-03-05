CREATE TABLE [dbo].[ApplicationRolePermission]
(
	[ApplicationRoleId] INT NOT NULL, 
    [PermissionId] INT NOT NULL,
	PRIMARY KEY ([ApplicationRoleId], [PermissionId]),
    CONSTRAINT [FK_ApplicationUserRole_User1] FOREIGN KEY ([ApplicationRoleId]) REFERENCES [ApplicationRole]([Id]),
    CONSTRAINT [FK_ApplicationUserRole_Role1] FOREIGN KEY ([PermissionId]) REFERENCES [Permission]([Id])
)
