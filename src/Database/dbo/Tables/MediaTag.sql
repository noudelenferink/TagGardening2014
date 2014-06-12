CREATE TABLE [dbo].[MediaTag] (
    [MediaTagId]     INT        NOT NULL IDENTITY,
    [MediaTagTypeId] INT        NULL,
    [MediaTagValue]  NVARCHAR(30) NOT NULL,
    PRIMARY KEY CLUSTERED ([MediaTagId] ASC),
    CONSTRAINT [FK_MediaTag_MediaTagType] FOREIGN KEY ([MediaTagTypeId]) REFERENCES [dbo].[MediaTagType] ([MediaTagTypeId])
);

