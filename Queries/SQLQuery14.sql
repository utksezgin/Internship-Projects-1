create proc sp_ProcessTestTableGiden
as
begin
	select	'I' AS Inserted, testTable.*
	from	testTable
	where	not exists(
				select	1
				from	testTableGiden
				where	testTableGiden.Id = testTable.Id
			)

	select	'U' AS Updated, testTable.*, testTableGiden.*
	from	testTable
	join	testTableGiden	on testTableGiden.ID = testTable.Id
	where	testTableGiden.FirstName <> testTable.FirstName
	or		testTableGiden.LastName <> testTable.LastName
	or		testTableGiden.Age<> testTable.Age

	select	'D' AS Deleted, testTableGiden.*
	from	testTableGiden
	where	not exists(
				select	1
				from	testTable
				where	testTable.Id = testTableGiden.Id
			)
end