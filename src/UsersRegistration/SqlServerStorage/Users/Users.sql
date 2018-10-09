CREATE TABLE [dbo].[Users]
(
	[UserId] INT NOT NULL PRIMARY KEY, 
	[TenantId] INT NOT NULL,
	[Email] NVARCHAR(256) NOT NULL, 
    [PasswordHash] VARCHAR(50) NOT NULL, 
    [PasswordSalt] VARCHAR(30) NOT NULL,
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL, 
    [Address] NVARCHAR(100) NOT NULL, 
    [PersonalNumber] NVARCHAR(20) NULL, 
    [FavoriteFootballTeam] NVARCHAR(50) NULL, 
    CONSTRAINT [FK_Users_ToTenants] FOREIGN KEY ([TenantId]) REFERENCES [Tenants]([TenantId])
    
    
)

GO

CREATE UNIQUE INDEX [IX_Users_TenantId_Email] ON [dbo].[Users] (TenantId,Email)
