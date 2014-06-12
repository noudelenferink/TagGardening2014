CREATE TABLE [dbo].[Tag] (
    [TagId]       INT NOT NULL IDENTITY,
    [MediaItemId] INT NOT NULL,
    [TagValue] NVARCHAR(50) NULL, 
    [Position] NVARCHAR(20) NULL, 
    PRIMARY KEY CLUSTERED ([TagId] ASC),
    CONSTRAINT [FK_Tag_MediaItem] FOREIGN KEY ([MediaItemId]) REFERENCES [dbo].[MediaItem] ([MediaItemId])
);

