CREATE TABLE [dbo].[Address]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Stree] NVARCHAR(50) NOT NULL, 
    [PersonId] INT NOT NULL, 
    CONSTRAINT [FK_Address_Person] FOREIGN KEY (PersonId) REFERENCES Person(Id)
)
