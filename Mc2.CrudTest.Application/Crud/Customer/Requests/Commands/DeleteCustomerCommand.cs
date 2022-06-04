using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mc2.CrudTest.Application.Crud.Customer.Requests.Commands
{
    public class DeleteCustomerCommand : IRequest
    {
        public int Id { get; set; }
    }
}
