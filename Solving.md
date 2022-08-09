# Solving the coding challenge

## Setup
* create gitlab repo with given files as baseline
* run `dotnet build` to create bin and obj dirs
* add those dirs to `.gitignore`
* run `dotnet test` to make sure tests work.
* check out tests, and overall structure of code
   * MVC structure
   * Given tests are pretty sparse, but this is just a coding challenge, not production ready

## Task 1
* I decided to simply add a new method to the Employee Service, and add a new method to the Employee controller.
* The concept of a "Reporting Structure" is all nice, but the requirements are only to provide an interface that returns the number of recursive employees. To me, this does not warrant the creation of more classes, but rather simply adding on to the functionality of the Employee controller

* The number of reports is something that must be computed using data that is already part of the employee

* READ
   * HTTP Method: GET
   * URL: localhost:8080/api/employee/{id}/num_reports
   * RESPONSE: ReportingStructure

```json
{
    "type": "ReportingStructure"
    "properties": {
        "employee": {
            "type": "Employee"
        },
        "numberOfReports": {
            "type": "integer"
        }
    }
}
```

* I ran into issues with the testing - namely other tests were altering the state of the context, so now each test creates its own TestServer(), which forces a reload of the data. Now, each test can do anything it wants - even delete all the employees, without affecting other tests. Tests can be run in any order, or in isolation.

    * I added a couple of lines to the EmployeeDataSeeder class ctor to ensure that the underlying database is fresh and clean. That
    * In the EmployeeRepository ctor, I added a call to load the `Employees` dbset into the current context. This causes the DirectReports to get populated for each Employee.

## Task 2
* For compensation, that is data that is separate from the employee itself. There are no dependencies on other employee data, and it is very possible in the future we'd want to look at compensation data divorced from the employees themselves. Compensation data would go in its own table, with a fk to the Employee. 

* I used the same context and data seeder

## To talk about
* Looking back, I should have left the tests alone, and perhaps renamed them as integration tests, because that's what they are. Then, I should have tailored the new tests taking into account that the state is held on a per TestClass basis.
* I'm not paricularly happy with leaving the link between `Compensation`

## To talk about
* Looking back, I should have left the tests alone, and perhaps renamed them as integration tests, because that's what they are. Then, I should have tailored the new tests taking into account that the state is held on a per TestClass basis.
* I'm not paricularly happy with leaving the link between `Compensation.Id` and `Employee.EmployeeId` be as weak as it is. With more time, I would have strengthened that relationship to result in a FK. 
* As it is, you can create a compensation with a bogus employee Id. This is probably error-prone, but it does allow for behavior, like even if an employee is deleted, their compensation data remains behind.
* The key for the Compensation should be its own Id, or perhaps a composite key made of the employee id, salary, and effective date.
* Also consider a link in each Employee to its collection of Compensation data. 