DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[tbl_UserDetails]') AND [c].[name] = N'Password');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [tbl_UserDetails] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [tbl_UserDetails] ALTER COLUMN [Password] nvarchar(50) NOT NULL;

GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[tbl_UserDetails]') AND [c].[name] = N'First_Name');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [tbl_UserDetails] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [tbl_UserDetails] ALTER COLUMN [First_Name] nvarchar(100) NOT NULL;

GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[tbl_UserDetails]') AND [c].[name] = N'Email');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [tbl_UserDetails] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [tbl_UserDetails] ALTER COLUMN [Email] nvarchar(50) NOT NULL;

GO

CREATE UNIQUE INDEX [IX_tbl_UserDetails_Email] ON [tbl_UserDetails] ([Email]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220404184350_AddedPoints', N'3.1.23');

GO

