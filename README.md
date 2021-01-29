# Fullstackathon: To Do List!

Our challenge today is to build the backend for this todo list.

The front end for our app can be found in `FrontEnd/` and is already functional! We just need to supply the backend which you will find in `BackEnd/`.

- To run the front end simply open `FrontEnd/index.html` in your browser
- To run the back end: `dotnet run BackEnd/ToDoApi.csproj` (or `dotnet watch run` if you are feeling fancy...)

## Task 1

The following tasks do not have to be completed in order. Break down what you would like to do first and the steps needed.

### Create a ToDoItems Postgres table and a ToDoItem POCO (plain old c# object)

A **ToDoItem** should have the following properties...

- Id (long)
- Title (string)
- Priority (string)
- IsComplete (bool)

### Create a ToDoItem Controller Class

The following endpoints are required to make the front end work...

- `GET /todoitems`
  - returns all the todoitems
- `GET /todoitems/{id}`
  - returns just the todoitem with corresponding id
- `DELETE /todoitems/{id}`
  - deletes just the todoitem with corresponding id
- `PUT /todoitems/{id}`
  - updates just the todoitem with corresponding id
- `POST /todoitems`
  - inserts a todoitem

### Complete the RepositoryBase Class

Use `Npgsql` and put in your postgres connection settings.

**Methods**

- _private_ SqlConnection
  - return a new Npgsql connection
- _public_ CreateConnection
  - return an open connection ready to use

### Create a ToDoItem Repository Class

Use `Dapper` to perform your database interactions.

## Task 2

Once you have completed the backend, your front end should be a fully functional to do list that saves your entries to a database!

_Next Steps_

- Style your page with CSS (use flex box and/or grid for a nice layout)
- Add a button to sort your list by priority
- Add a button to delete all the completed To Dos
- Add the functionality to edit the text of a To Do
- Get creative, what else would you like To Do??
