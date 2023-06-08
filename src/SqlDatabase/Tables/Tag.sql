CREATE TABLE dbo.Tag
(
  TagId        NVARCHAR(100) NOT NULL,
  TagName      NVARCHAR(100) NOT NULL,
  DateAdded    DATETIME2     NOT NULL CONSTRAINT dfTag_DateAdded DEFAULT(GETUTCDATE())
  CONSTRAINT pkcTag PRIMARY KEY CLUSTERED (TagId)
)
GO

EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Tag',                                 @value=N'Represents a tag applied to a quote in order to categorize quotes.',               @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Tag', @level2name=N'TagId',           @value=N'Identifier of the tag record using a slug format.',                                @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Tag', @level2name=N'TagName',         @value=N'The name of the tag.',                                                             @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Tag', @level2name=N'DateAdded',       @value=N'The UTC date/time the tag was added.',                                             @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Tag', @level2name=N'pkcTag',          @value=N'Defines the primary key for the Tag table using the TagId column.',                @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Tag', @level2name=N'dfTag_DateAdded', @value=N'Defines the default value for the DateAdded column to the current UTC date/time.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO