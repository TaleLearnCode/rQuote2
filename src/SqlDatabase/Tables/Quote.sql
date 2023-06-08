CREATE TABLE dbo.Quote
(
  QuoteId   INT           NOT NULL IDENTITY(1,1),
  AuthorId  NVARCHAR(100) NOT NULL,
  Content   NVARCHAR(500) NOT NULL,
  DateAdded DATETIME2     NOT NULL CONSTRAINT dfQuote_DateAdded DEFAULT(GETUTCDATE()),
  CONSTRAINT pkcQuote PRIMARY KEY CLUSTERED (QuoteId),
  CONSTRAINT fkQuote_Author FOREIGN KEY (AuthorId) REFERENCES dbo.Author (AuthorId)
)
GO

EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Quote',                                   @value=N'Represents an quote.',                                                                    @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Quote', @level2name=N'QuoteId',           @value=N'Identifier of the quote record using an auto-incremented value.',                         @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Quote', @level2name=N'AuthorId',          @value=N'Identifier of the author the quote is attributed to.',                                    @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Quote', @level2name=N'Content',           @value=N'The content of the quote.',                                                               @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Quote', @level2name=N'DateAdded',         @value=N'The UTC date/time the quote was added.',                                                  @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Quote', @level2name=N'pkcQuote',          @value=N'Defines the primary key for the Quote table using the QuoteId column.',                   @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Quote', @level2name=N'dfQuote_DateAdded', @value=N'Defines the default value for the DateAdded column to the current UTC date/time.',        @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Quote', @level2name=N'fkQuote_Author',    @value=N'Defines the relationship between the Quote and Author tables using the AuthorId column.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO