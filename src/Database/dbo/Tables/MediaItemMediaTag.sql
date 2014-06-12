CREATE TABLE [dbo].[MediaItemMediaTag] (
    [MediaItemMediaTagId] INT       NOT NULL IDENTITY,
    [MediaTagId]          INT       NOT NULL,
    [MediaItemId]         INT       NOT NULL,
    [Position]            NVARCHAR(20) NOT NULL,
    PRIMARY KEY CLUSTERED ([MediaItemMediaTagId] ASC),
    CONSTRAINT [FK_MediaItemMediaTag_MediaItem] FOREIGN KEY ([MediaItemId]) REFERENCES [dbo].[MediaItem] ([MediaItemId]),
    CONSTRAINT [FK_MediaItemMediaTag_MediaTag] FOREIGN KEY ([MediaTagId]) REFERENCES [dbo].[MediaTag] ([MediaTagId])
);

