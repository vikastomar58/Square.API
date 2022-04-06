DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[tbl_UserDetails]') AND [c].[name] = N'Password');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [tbl_UserDetails] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [tbl_UserDetails] ALTER COLUMN [Password] nvarchar(500) NOT NULL;

GO

ALTER TABLE [tbl_UserDetails] ADD [Salt] nvarchar(100) NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220406173433_ThirdMigration', N'3.1.23');

GO

