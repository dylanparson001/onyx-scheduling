using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnyxScheduling.Dtos;
using OnyxScheduling.Interfaces;
using OnyxScheduling.Models;

namespace OnyxScheduling.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public UserController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        [HttpGet]
        [Route("GetAllOfficeStaff")]
        public async Task<ActionResult<List<OfficeStaffDto>>> GetAllOfficeStaff()
        {
            var officeStaffResult = await _accountRepository.GetAllOfficeStaff();
            List<OfficeStaffDto> officeStaffDtos = new List<OfficeStaffDto>();

            foreach (var user in officeStaffResult)
            {
                officeStaffDtos.Add(new OfficeStaffDto()
                {
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    City = user.City,
                    State = user.State
                });
            }
            return officeStaffDtos;
        }

        [HttpGet]
        [Route("GetAllCustomers")]
        public async Task<ActionResult<List<CustomerDto>>> GetAllCustomers()
        {
            var customerResult = await _accountRepository.GetAllCustomers();
            List<CustomerDto> customerDtoResut = new List<CustomerDto>();

            foreach (var customer in customerResult)
            {
                customerDtoResut.Add(new CustomerDto()
                {
                    UserName = customer.UserName,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    City = customer.City,
                    State = customer.State
                });
            }

            return customerDtoResut;
        }

        [HttpGet]
        [Route("GetAllTechnicians")]
        public async Task<ActionResult<List<TechnicianDto>>> GetAllFieldStaff()
        {
            var technicianResult = await _accountRepository.GetAllTechnicians();
            List<TechnicianDto> technicianDtos = new List<TechnicianDto>();

            foreach (var technician in technicianResult)
            {
                technicianDtos.Add(new TechnicianDto()
                {
                    UserName = technician.UserName,
                    FirstName = technician.FirstName,
                    LastName = technician.LastName,
                    City = technician.City,
                    State = technician.State
                });
            }
            return technicianDtos;
        }

        [HttpGet]
        [Route("GetCustomerFromInvoice")]
        public async Task<ActionResult<CustomerDto>> GetCustomerFromInvoice(string customerId)
        {
            var customer = await _accountRepository.GetCustomersFromCustomerId(customerId);

            var customerDto = new CustomerDto()
            {
                UserName = customer.UserName,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                City = customer.City,
                State = customer.State
            };


            return Ok(customerDto);
        }
    }
}
