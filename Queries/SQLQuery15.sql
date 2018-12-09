create proc sp_UpdateTestTableGiden
as
begin
	update	testTableGiden
	set		FirstName = testTable.FirstName,
			LastName = testTable.LastName,
			Age = testTable.Age
	from	testTableGiden
	join	testTable	on testTable.ID = testTableGiden.ID
	where	testTableGiden.FirstName <> testTable.FirstName
	or		testTableGiden.LastName <> testTable.LastName
	or		testTableGiden.Age<> testTable.Age

	insert	testTableGiden
	select	*
	from	testTable
	where	not exists(
				select	1
				from	testTableGiden
				where	testTableGiden.ID = testTable.ID
			)
	
	delete	testTableGiden
	where	not exists(
				select	1
				from	testTable
				where	testTableGiden.ID = testTable.ID
			)
end
go
