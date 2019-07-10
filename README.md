# Internship-Projects-1
The projects i did on my first internship

Microsoft Visual Studio 2013 -  C# / .NET / SQL Database

Multithreaded server-client model.

*Client

  Client creates a threadpool with minimum 4 maximum 16 threads. Reads a SQL Table, for each row, using a thread, sends a request to the server and waits for a response and writes them both to another sql table.

*Server
  
  Server gets the response from client, looks up the SQL Table to find the response, updates the table and sends response back to client.

  There are multiple server implementations each of them has different threading. One of them uses built-in ThreadPool class, one of them uses threads and handles availability for them and the last one is Implements ThreadPool class handles synchronization and availability. All of them have their advantages and disadvantages in terms of efficiency, simplicity, readability/writeability from case to case.

PS. Server implementations are under ConsoleApplication4 Folder. Among with SQL Connection Class and config file

