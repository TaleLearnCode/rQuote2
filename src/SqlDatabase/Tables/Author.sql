CREATE TABLE dbo.Author
(
  AuthorId   NVARCHAR(100) NOT NULL,
  AuthorName NVARCHAR(100) NOT NULL,
  Bio        NVARCHAR(500) NOT NULL,
  DateAdded  DATETIME2     NOT NULL CONSTRAINT dfAuthor_DateAdded DEFAULT (GETUTCDATE()),
  CONSTRAINT pkcAuthor PRIMARY KEY CLUSTERED (AuthorId)
)
GO

EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Author',                                    @value=N'Represents an author of one or more quotes within the database.',                  @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Author', @level2name=N'AuthorId',           @value=N'Identifier of the author record using a slug format.',                             @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Author', @level2name=N'AuthorName',         @value=N'The name of the author.',                                                          @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Author', @level2name=N'DateAdded',          @value=N'The UTC date/time the author was added.',                                          @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Author', @level2name=N'pkcAuthor',          @value=N'Defines the primary key for the Author table using the AuthorId column.',          @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Author', @level2name=N'dfAuthor_DateAdded', @value=N'Defines the default value for the DateAdded column to the current UTC date/time.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO