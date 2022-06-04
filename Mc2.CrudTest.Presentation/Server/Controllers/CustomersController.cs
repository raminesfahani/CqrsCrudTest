using Mc2.CrudTest.Application.Crud.Customer.Requests.Commands;
using Mc2.CrudTest.Application.Crud.Customer.Requests.Queries;
using Mc2.CrudTest.Application.DTOs.Customer;
using Mc2.CrudTest.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mc2.CrudTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<CustomersController>
        [HttpGet]
        public async Task<ActionResult<BaseResponseObj<List<CustomerDto>>>> Get()
        {
            var response = await _mediator.Send(new GetCustomerListRequest());
            return Ok(response);
        }

        // GET api/<CustomersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponseObj<CustomerDto>>> Get(int id)
        {
            var response = await _mediator.Send(new GetCustomerDetailRequest { Id = id });
            return Ok(response);
        }

        // POST api/<CustomersController>
        [HttpPost]
        public async Task<ActionResult<BaseResponseObj<CustomerDto>>> Post([FromBody] CreateCustomerDto Customer)
        {
            var command = new CreateCustomerCommand { CustomerDto = Customer };
            var repsonse = await _mediator.Send(command);
            return Ok(repsonse);
        }

        // PUT api/<CustomersController>/5
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UpdateCustomerDto Customer)
        {
            var command = new UpdateCustomerCommand { CustomerDto = Customer };
            await _mediator.Send(command);
            return Ok();
        }

        // DELETE api/<CustomersController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteCustomerCommand { Id = id };
            await _mediator.Send(command);
            return Ok();
        }
    }
}
