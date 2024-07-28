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
        private readonly IInvoiceRepository _invoiceRepository;

        public UserController(IAccountRepository accountRepository, IInvoiceRepository invoiceRepository)
        {
            _accountRepository = accountRepository;
            _invoiceRepository = invoiceRepository;
        }
        
        private string GetCompanyIdFromHeader()
        {            
            Request.Headers.TryGetValue("CompanyId", out var companyIdHeader);

            return companyIdHeader.FirstOrDefault();
        }
        [HttpGet]
        [Route("GetAllOfficeStaff")]
        public async Task<ActionResult<List<OfficeStaffDto>>> GetAllOfficeStaff()
        {
            var companyId = GetCompanyIdFromHeader();

            var officeStaffResult = await _accountRepository.GetAllOfficeStaff(companyId);
            List<OfficeStaffDto> officeStaffDtos = new List<OfficeStaffDto>();

            foreach (var user in officeStaffResult)
            {
                officeStaffDtos.Add(new OfficeStaffDto()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    City = user.City,
                    State = user.State,
                    Email = user.Email
                });
            }
            return officeStaffDtos;
        }

        [HttpGet]
        [Route("GetAllCustomers")]
        public async Task<ActionResult<List<CustomerDto>>> GetAllCustomers()
        {
            var companyId = GetCompanyIdFromHeader();

            var customerResult = await _accountRepository.GetAllCustomers(companyId);
            List<CustomerDto> customerDtoResut = new List<CustomerDto>();

            foreach (var customer in customerResult)
            {
                customerDtoResut.Add(new CustomerDto()
                {
                    Id = customer.Id,
                    UserName = customer.UserName,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    City = customer.City,
                    State = customer.State,
                    Address = customer.Address
                });
            }

            customerDtoResut = customerDtoResut.OrderBy(x => x.LastName).ToList(); 

            return customerDtoResut;
        }

        [HttpGet]
        [Route("GetUsers")]
        public async Task<ActionResult<List<UserDto>>> GetUsers(int position, int take)
        {
            var companyId = GetCompanyIdFromHeader();

            var result = await _accountRepository.GetUsers(position, take, companyId);
            List<UserDto> listOfUserDtos = new List<UserDto>();
            foreach (var user in result)
            {
                var userDto = new UserDto()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Role = user.Role,
                    Address = user.Address,
                    City = user.City,
                    Phone = user.Phone,
                    State = user.State,
                    Email = user.Email
                };
                
                listOfUserDtos.Add(userDto);
            }
            return Ok(listOfUserDtos);
        }

        [HttpGet]
        [Route("GetCountOfUsers")]
        public async Task<ActionResult<int>> GetCountOfUsers()
        {
            var companyId = GetCompanyIdFromHeader();

            return await _accountRepository.GetCountUsers(companyId);
        }

        [HttpGet]
        [Route("GetAllTechnicians")]
        public async Task<ActionResult<List<TechnicianDto>>> GetAllFieldStaff()
        {
            var companyId = GetCompanyIdFromHeader();

            var technicianResult = await _accountRepository.GetAllTechnicians(companyId);
            List<TechnicianDto> technicianDtos = new List<TechnicianDto>();

            foreach (var technician in technicianResult)
            {
                var todaysDate = DateTime.Today;
                var techsInvoicesForToday = await _invoiceRepository.GetInvoicesByTechnicianByDate(technician.Id, todaysDate);

                double total = 0.0;
                
                // sending techs daily total
                foreach (var techInvoice in techsInvoicesForToday)
                {
                    total += techInvoice.Total_Price;
                }
                
                technicianDtos.Add(new TechnicianDto()
                {
                    Id = technician.Id,
                    UserName = technician.UserName,
                    FirstName = technician.FirstName,
                    LastName = technician.LastName,
                    City = technician.City,
                    State = technician.State,
                    DailyTotal = total
                });
            }
            return technicianDtos;
        }

        [HttpGet]
        [Route("GetRoleOfUser")]
        public async Task<ActionResult<string>> GetRoleOfUser(string userId)
        {
            return Ok("test");
        }

        [HttpGet]
        [Route("GetCustomerFromInvoice")]
        public async Task<ActionResult<CustomerDto>> GetCustomerFromInvoice(string customerId)
        {
            var companyId = GetCompanyIdFromHeader();

            var customer = await GetCustomerFromCustomerId(customerId);

            if (customer == null) return BadRequest();
            
            var customerDto = new CustomerDto()
            {
                Id = customer.Id,
                UserName = customer.UserName,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                City = customer.City,
                State = customer.State,
            };


            return Ok(customerDto);
        }
        [HttpGet]
        public async Task<User> GetCustomerFromCustomerId(string customerId)
        {
            var companyId = GetCompanyIdFromHeader();

             return await _accountRepository.GetCustomersFromCustomerId(customerId);
        }

        [HttpGet]
        [Route("GetTechnicianFromInvoice")]
        public async Task<ActionResult<TechnicianDto>> GetTechniciansFromInvoice(string technicianId)
        {
            var companyId = GetCompanyIdFromHeader();

            var technician = await _accountRepository.GetTechnciainsFromTechId(technicianId);
            
            if (technician == null)
            {
                return BadRequest();
            }
            

            var techDto = new TechnicianDto()
            {
                Id = technician.Id,
                UserName = technician.UserName,
                FirstName = technician.FirstName,
                LastName = technician.LastName,
                City = technician.City,
                State = technician.State
            };

            return Ok(techDto);
        }
        [HttpGet]
        [Route("GetTechFromId")]
        public async Task<User> GetTechFromId(string techId)
        {
            var companyId = GetCompanyIdFromHeader();

            return await _accountRepository.GetTechnciainsFromTechId(techId);
        }

        [HttpGet]
        [Route("GetCustomerFromJob")]
        public async Task<ActionResult<User>> GetCustomersFromJob(string customerId)
        {
            var companyId = GetCompanyIdFromHeader();

            var result = await _accountRepository.GetCustomersFromCustomerId(customerId);

            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateUserInfo")]
        public async Task<ActionResult> UpdateUserInfo(string userId, [FromBody] UserDto userDto)
        {
            var companyId = GetCompanyIdFromHeader();

            await _accountRepository.UpdateUserInfo(userId, userDto);
            return Ok();
        }

        [HttpGet]
        [Route("GetUserFromUsername")]
        public async Task<ActionResult<UserDto>> GetUserFromUsername(string username)
        {
            var companyId = GetCompanyIdFromHeader();

            var result =  await _accountRepository.GetUserFromUsername(username, companyId);

            var userDto = new UserDto()
            {
                Id = result.Id,
                UserName = result.UserName,
                FirstName = result.FirstName,
                LastName = result.LastName,
                Role = result.Role,
                Email = result.Email
            };

            return userDto;
        }

        [HttpGet]
        [Route("SearchUsers")]
        public async Task<ActionResult<List<UserDto>>> SearchUsername(string username)
        {
            var companyId = GetCompanyIdFromHeader();

            var result = await _accountRepository.SearchUsernames(username, companyId);
            List<UserDto> userDtoResult = new List<UserDto>();
            if (result.Count > 0)
            {
                foreach (var user in result)
                {
                    var userDto = new UserDto()
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Address = user.Address,
                        City = user.City,
                        State = user.State,
                        Email = user.Email,
                        Phone = user.Phone,
                        Role = user.Role
                    };
                    userDtoResult.Add(userDto);
                }

                return Ok(userDtoResult);
            }

            return Ok(userDtoResult);

        }
    }
    
}
