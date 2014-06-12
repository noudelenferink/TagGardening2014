CREATE TABLE [dbo].[MediaItem] (
    [MediaItemId]       INT        NOT NULL IDENTITY,
    [MediaTypeId]       INT        NOT NULL,
    [MediaItemStatusId] INT        NOT NULL,
    [Title]             NVARCHAR(50) NULL,
    [FileName]          NVARCHAR(50) NULL,
    [ThumbFileName]     NVARCHAR(50) NULL,
    [CreatedDate]       DATETIME   NULL,
    [LastModifiedDate]  DATETIME   NULL,
    PRIMARY KEY CLUSTERED ([MediaItemId] ASC),
    CONSTRAINT [FK_MediaItem_MediaItemStatus] FOREIGN KEY ([MediaItemStatusId]) REFERENCES [dbo].[MediaItemStatus] ([MediaItemStatusId]),
    CONSTRAINT [FK_MediaItem_MediaType] FOREIGN KEY ([MediaTypeId]) REFERENCES [dbo].[MediaType] ([MediaTypeId])
);


GO
CREATE TRIGGER tr_MediaItem_LastModifiedDate ON [MediaItem]
FOR UPDATE 
AS
UPDATE [MediaItem] SET [MediaItem].LastModifiedDate = GETUTCDATE()
FROM [MediaItem] INNER JOIN Inserted ON [MediaItem].[MediaItemId]= Inserted.[MediaItemId]
GO

CREATE TRIGGER [dbo].[tr_MediaItem_CreatedDate] ON [dbo].[MediaItem]
FOR INSERT 
AS
UPDATE [MediaItem] SET MediaItem.[CreatedDate] = GETUTCDATE()
FROM [MediaItem] INNER JOIN Inserted ON MediaItem.[MediaItemId]= Inserted.[MediaItemId]
