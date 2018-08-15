# EntityFrameworkDemo
Sample project to demonstrate benefits and pitfalls of Entity Framework

The tests in this project need access to a database
* The Test project will attempt to create a new test database if it does not already exist when the tests run
* The Test project App.config controls the location of the test database in the connectionString setting
* The Test project contains a schema.sql file which is used to create the test database
* Each test runs within a transaction that gets rolled back at the end of the test
  * The RunTestsWithoutTransactions setting in the App.config allows you to run tests that commit changes instead of rolling back. This is useful for troubleshooting
  * Running tests that commit changes will pollute the test database and cause test failures. You can drop the database and the next test run will recreate it
