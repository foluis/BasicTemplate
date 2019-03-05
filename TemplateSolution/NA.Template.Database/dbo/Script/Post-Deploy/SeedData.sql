--[Permission]
IF NOT EXISTS(SELECT * FROM [dbo].[Permission])
BEGIN

	INSERT INTO [Permission] 
		([Name])
	VALUES 
		('UserCanCreateSecurity'),
		('UserCanReadSecurity'),
		('UserCanUpdateSecurity'),
		('UserCanDeleteSecurity'),

		('UserCanCreateUserGeneral'),
		('UserCanReadUserGeneral'),
		('UserCanUpdateUserGeneral'),
		('UserCanDeleteUserGeneral'),

		('UserCanCreateReport'),
		('UserCanReadReport'),
		('UserCanUpdateReport'),
		('UserCanDeleteReport');
END

--[ApplicationRole]
IF NOT EXISTS(SELECT * FROM [dbo].[ApplicationRole])
BEGIN
	INSERT INTO [ApplicationRole] 
		([Name],[NormalizedName])
	VALUES 
		('Administrator','ADMINISTRATOR'),
		('User','USER');
END

--[ApplicationRolePermission]
IF NOT EXISTS(SELECT * FROM [dbo].[ApplicationRolePermission] WHERE [ApplicationRoleId] = 1 AND [PermissionId] = 1 )
BEGIN
	INSERT INTO [ApplicationRolePermission] ([ApplicationRoleId],[PermissionId])
	VALUES (1,1),
		(1,2);
END

--[ApplicationUser]
IF NOT EXISTS(SELECT * FROM [dbo].[ApplicationUser])
BEGIN
	INSERT INTO [ApplicationUser] 
		([UserName], [NormalizedUserName], [Email],[NormalizedEmail], [EmailConfirmed], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled])
	VALUES 
		('foluis@hotmail.com', 'FOLUIS@HOTMAIL.COM', 'foluis@hotmail.com','FOLUIS@HOTMAIL.COM', 0, 'AQAAAAEAACcQAAAAEMFulH/xUbgMx5Vwvk9DH6nXlSIzPwvqfAYNx8nxIHT1m4/OH2/8mQy7nn+DVzXZNg==',NULL, 0, 0);	
END

--[Add role do user foluis@hotmail.com (Admin) ]
IF (	
	(SELECT COUNT(1) FROM [dbo].[ApplicationUser] WHERE NormalizedUserName = 'FOLUIS@HOTMAIL.COM') = 0
	)
BEGIN
	INSERT INTO [ApplicationUserRole] ([UserId],[RoleId])
	VALUES (1,1)
END
