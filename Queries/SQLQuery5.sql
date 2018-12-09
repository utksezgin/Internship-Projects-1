CREATE PROCEDURE dbo.sp_deletetestTables
	@ID int
	AS
	begin
		DELETE FROM testTable WHERE ID = @ID;
		UPDATE testTableBildirim	SET ChangeStatus = 'Deleted' WHERE ID = @ID;
	end