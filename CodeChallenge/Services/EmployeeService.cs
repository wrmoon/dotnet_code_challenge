using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using CodeChallenge.Repositories;

namespace CodeChallenge.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(ILogger<EmployeeService> logger, IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        public Employee Create(Employee employee)
        {
            if(employee != null)
            {
                _employeeRepository.Add(employee);
                _employeeRepository.SaveAsync().Wait();
            }

            return employee;
        }

        public Employee GetById(string id)
        {
            if(!String.IsNullOrEmpty(id))
            {
                Employee e = _employeeRepository.GetById(id);
                return e;
            }

            return null;
        }

        public ReportingStructure GetReportingStructureById(string id)
        {
            Employee employee = GetById(id);
            if (employee == null)
            {
                return null;
            }

            ReportingStructure reporting_structure = new ReportingStructure();
            reporting_structure.Employee = employee;
            reporting_structure.NumberOfReports = GetNumOfReports(employee);

            return reporting_structure;
        }

        // *** Danger this is recursive
        // IRL, this algorithm would have some safety mechanisms to ensure it won't get stuck
        private int GetNumOfReports(Employee employee)
        {
            int num_reports = 0;
            if (employee == null)
            {
                return 0;
            }
            if (employee.DirectReports == null)
            {
                return 0;
            }
            num_reports += employee.DirectReports.Count;
            foreach (Employee direct in employee.DirectReports)
            {
                if (direct != null)
                {
                    num_reports += GetNumOfReports(direct);
                }
            }
            return num_reports;
        }


        public Employee Replace(Employee originalEmployee, Employee newEmployee)
        {
            if(originalEmployee != null)
            {
                _employeeRepository.Remove(originalEmployee);
                if (newEmployee != null)
                {
                    // ensure the original has been removed, otherwise EF will complain another entity w/ same id already exists
                    _employeeRepository.SaveAsync().Wait();

                    _employeeRepository.Add(newEmployee);
                    // overwrite the new id with previous employee id
                    newEmployee.EmployeeId = originalEmployee.EmployeeId;
                }
                _employeeRepository.SaveAsync().Wait();
            }

            return newEmployee;
        }
    }
}
