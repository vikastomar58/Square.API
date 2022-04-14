# Square.API

This is a readme file for our Sqare API repo. Square api allow authrozie users to add points. It also provide the number of square based on points added by user.

Below are few points:-
* It is build in Dot net core 3.1.
* It uses Entity Frame work core to communicate with SQL Server.
* It also provide swagger document while running.

Building and running the application:-
* Through Command Line Infertace - 
	1. After opening the command line interface at the root level run command **dotnet build**
	2. After successful completion of above command run command **dotnet run --project Squares.API/Squares.API.csproj**
	3. It will start the api and will tell on which port it is listening the request.
	4. You can stop it by pressing **ctrl+c** on command line interface.
	
* Through Docker - 
	1. You must have Docker hub installed on your sytem.
	2. Open the command line interface at root level directory of project and run **docker build -t squareapi -f Dockerfile .**
	3. After completion of above command run **docker run -p 8080:80 --name app squareapi**

Controllers:-
* RegisterController - This controller contains 2 methods
	1. SignUp - To register the user by providing uer details in body
	2. Login - This method returns token for authenticated user.

* SquareController - This controller has 4 methods
	1. Post - This method is used to add an array of points into the database for log in user.
	2. Get("Upload") - This method is used to upload points for a Json file for log in user.
	3. Get - This method returns the number of squares for log in user.
	4. Delete - This method delete a Point from database for log in user.
	

In order to work for this API it requires SQL Server. So before running you need to change the connectionstring in appsettings.json file.