CREATE PROCEDURE dbo.sp_inserttestTables
	@ID int,
	@LastName varchar(250),
	@FirstName varchar(250),
	@Age int
	AS
	begin
		INSERT INTO testTable(ID, LastName, FirstName, Age) VALUES (@ID, @LastName, @FirstName, @Age);
		INSERT INTO testTableBildirim(ID, LastName, FirstName, Age, ChangeStatus) VALUES (@ID, @LastName, @FirstName, @Age, 'Inserted');
	end