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

* For compensation, that is data that is separate from the employee itself, to a certain extent. There are no dependencies on employee data, and it is very possible in the future we'd want to look at compensation data divorced from the employees themselves. Compensation data would go in its own table, with a fk to the employee. 

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