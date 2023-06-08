CREATE TABLE dbo.QuoteTag
(
  QuoteTagId INT           NOT NULL IDENTITY(1,1),
  QuoteId    INT           NOT NULL,
  TagId      NVARCHAR(100) NOT NULL,
  DateAdded  DATETIME2     NOT NULL CONSTRAINT dfQuoteTag_DateAdded DEFAULT (GETUTCDATE()),
  CONSTRAINT pkcQuoteTag PRIMARY KEY CLUSTERED (QuoteTagId),
  CONSTRAINT fkQuoteTag_Quote FOREIGN KEY (QuoteId) REFERENCES dbo.Quote (QuoteId),
  CONSTRAINT fkQuoteTag_Tag   FOREIGN KEY (TagId)   REFERENCES dbo.Tag (TagId)
)
GO

EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'QuoteTag',                                      @value=N'Represents the relationship between a quote and a tag.',                                   @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'QuoteTag', @level2name=N'QuoteTagId',           @value=N'Identifier of the tag/quote association using an auto-incremented value.',                 @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'QuoteTag', @level2name=N'QuoteId',              @value=N'Identifier of the associated quote.',                                                      @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'QuoteTag', @level2name=N'TagId',                @value=N'Identifier of the associated tag.',                                                        @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'QuoteTag', @level2name=N'DateAdded',            @value=N'The UTC date/time the tag/quote association was added.',                                   @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'QuoteTag', @level2name=N'pkcQuoteTag',          @value=N'Defines the primary key for the QuoteTag table using the QuoteTagId column.',              @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'QuoteTag', @level2name=N'dfQuoteTag_DateAdded', @value=N'Defines the default value for the DateAdded column to the current UTC date/time.',         @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'QuoteTag', @level2name=N'fkQuoteTag_Quote',     @value=N'Defines the relationship between the QuoteTag and Quote tables using the QuoteId column.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'QuoteTag', @level2name=N'fkQuoteTag_Tag',       @value=N'Defines the relationship between the QuoteTag and Tag tables using the TagId column.',     @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO