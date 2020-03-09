CREATE VIEW [dbo].[FullPerson]
	AS 
	SELECT [p].[Id] as PersonId, [p].[FirstName], [p].[LastName],
		[a].[Id] as AddressId, [a].[StreetAddress], [a].[City], [a].[State], [a].[ZipCode]
	from dbo.Person p
	left join dbo.[Address] a on p.Id = a.Id

