using Customer.API.Entities;
using Customer.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Customer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// Endpoint responsible for customer registration. 
        /// </summary>
        [HttpPost("create")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateCustomer([FromBody] CustomerData customer)
        {
            try
            {
                var response = await _customerService.CreateCustomer(customer);
                return Ok("Request successfully. Wait for proposal analysis.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}