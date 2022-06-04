using Mc2.CrudTest.Application.DTOs.Customer;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mc2.CrudTest.Application.Crud.Customer.Requests.Commands
{
    public class UpdateCustomerCommand : IRequest<Unit>
    {
        public UpdateCustomerDto CustomerDto { get; set; }
    }
}
