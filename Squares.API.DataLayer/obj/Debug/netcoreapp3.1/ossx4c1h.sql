CREATE TABLE [tbl_Points] (
    [Point_Id] int NOT NULL IDENTITY,
    [X] int NOT NULL,
    [Y] int NOT NULL,
    [User_Id] int NOT NULL,
    CONSTRAINT [PK_tbl_Points] PRIMARY KEY ([Point_Id]),
    CONSTRAINT [FK_tbl_Points_tbl_UserDetails_User_Id] FOREIGN KEY ([User_Id]) REFERENCES [tbl_UserDetails] ([User_Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_tbl_Points_User_Id] ON [tbl_Points] ([User_Id]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220404185051_PointTable', N'3.1.23');

GO

