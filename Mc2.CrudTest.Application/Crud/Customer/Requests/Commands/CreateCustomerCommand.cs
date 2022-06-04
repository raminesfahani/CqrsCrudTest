using Mc2.CrudTest.Application.DTOs.Customer;
using Mc2.CrudTest.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mc2.CrudTest.Application.Crud.Customer.Requests.Commands
{
    public class CreateCustomerCommand : IRequest<BaseResponseObj<CreateCustomerDto>>
    {
        public CreateCustomerDto CustomerDto { get; set; }
    }
}
