﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBLMS.DTO;
using WBLMS.IRepositories;
using WBLMS.IServices;
using WBLMS.Models;
using WBLMS.Utilities;

namespace WBLMS.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<GetEmployeeDTO> CreateEmployeeAsync(CreateEmployeeDTO employeeDTO)
        {
            var Password = PasswordHashing.getHashPassword(employeeDTO.Password);

            var employee = await _employeeRepository.CreateAsync
                (
                    new Employee()
                    {
                        FirstName = employeeDTO.FirstName,
                        LastName = employeeDTO.LastName,
                        Password = Password,
                        EmailAddress = employeeDTO.EmailAddress,
                        ContactNumber = employeeDTO.ContactNumber,
                        GenderId = employeeDTO.GenderId,
                        RoleId = employeeDTO.RoleId,
                        ManagerId = employeeDTO.ManagerId,
                        CreatedById = employeeDTO.CreatedById,
                        JoiningDate = DateOnly.FromDateTime(DateTime.Now),
                    }
                );

            return new GetEmployeeDTO
                (
                    employee.Id,
                    employee.FirstName,
                    employee.LastName,
                    employee.Password,
                    employee.EmailAddress,
                    employee.ContactNumber,
                    (long)employee.GenderId,
                    (long)employee.RoleId,
                    (long)employee.ManagerId,
                    (long)employee.CreatedById,
                    employee.UpdatedDate
                );

        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            try
            {
                var employee = await _employeeRepository.GetAsyncById(id);
                if(employee != null)
                {
                    await _employeeRepository.DeleteAsync(employee);
                    return true;
                }
            }catch(Exception ex)
            {
                throw;
            }
            return false;
        }

        public async Task<(IEnumerable<GetEmployeeDTO>, int)> GetAllEmployeeAsync(int page, int pageSize, string? sortColumn, string? sortOrder, GetEmployeeDTO employeeDTO)
        {
            try
            {
                var employee = new Employee()
                {
                    FirstName = employeeDTO.FirstName,
                    LastName = employeeDTO.LastName,
                    Password = employeeDTO.Password,
                    EmailAddress = employeeDTO.EmailAddress,
                    ContactNumber = employeeDTO.ContactNumber,
                    GenderId = employeeDTO.GenderId,
                    RoleId = employeeDTO.RoleId,
                    ManagerId = employeeDTO.ManagerId,
                    CreatedById = employeeDTO.CreatedById,
                    JoiningDate = DateOnly.FromDateTime(DateTime.Now),
                };
                var employeeList = await _employeeRepository.GetAllEmployee(page,pageSize,sortColumn,sortOrder,employee);

                var result = employeeList.Item1.Select
                    (
                    employee =>
                        new GetEmployeeDTO
                            (
                                employee.Id,
                                employee.FirstName,
                                employee.LastName,
                                employee.Password,
                                employee.EmailAddress,
                                employee.ContactNumber,
                                (long)employee.GenderId,
                                (long)employee.RoleId,
                                (long)employee.ManagerId,
                                (long)employee.CreatedById,
                                employee.UpdatedDate
                            )
                    );
                return (result, employeeList.Item2);
            }catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<GetEmployeeDTO> GetEmployeeByIdAsync(int id)
        {
            try
            {
                var employee = await _employeeRepository.GetAsyncById(id);
                if(employee != null)
                {
                    return new GetEmployeeDTO
                    (
                        employee.Id,
                        employee.FirstName,
                        employee.LastName,
                        employee.Password,
                        employee.EmailAddress,
                        employee.ContactNumber,
                        (long)employee.GenderId,
                        (long)employee.RoleId,
                        (long)employee.ManagerId,
                        (long)employee.CreatedById,
                        employee.UpdatedDate
                    );
                }

            }
            catch(Exception e)
            {
                throw;
            }
            return null;
        }

        public async Task<GetEmployeeDTO> UpdateEmployeeAsync(UpdateEmployeeDTO employeeDTO, int id)
        {
            try
            {
                var oldEmployee = await _employeeRepository.GetAsyncById(id);

                if(oldEmployee != null)
                {
                    var hashPassword = PasswordHashing.getHashPassword(employeeDTO.Password);

                    oldEmployee.FirstName = employeeDTO.FirstName;
                    oldEmployee.LastName = employeeDTO.LastName;
                    oldEmployee.Password = hashPassword;
                    oldEmployee.EmailAddress = employeeDTO.EmailAddress;
                    oldEmployee.ContactNumber = employeeDTO.ContactNumber;
                    oldEmployee.GenderId = employeeDTO.GenderId;
                    oldEmployee.RoleId = employeeDTO.RoleId;
                    oldEmployee.ManagerId = employeeDTO.ManagerId;
                    oldEmployee.UpdateById = employeeDTO.UpdatedById;
                    oldEmployee.UpdatedDate = DateOnly.FromDateTime(DateTime.Now);

                    var employee = await _employeeRepository.UpdateAsync(oldEmployee);

                    return new GetEmployeeDTO
                    (
                        employee.Id,
                        employee.FirstName,
                        employee.LastName,
                        employee.Password,
                        employee.EmailAddress,
                        employee.ContactNumber,
                        (long)employee.GenderId,
                        (long)employee.RoleId,
                        (long)employee.ManagerId,
                        (long)employee.CreatedById,
                        employee.UpdatedDate
                    );
                }
            }catch(Exception e) { throw; }
            return null;
        }
    }
}
