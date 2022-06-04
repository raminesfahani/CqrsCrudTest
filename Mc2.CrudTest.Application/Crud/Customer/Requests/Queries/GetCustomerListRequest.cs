using Mc2.CrudTest.Application.DTOs;
using Mc2.CrudTest.Application.DTOs.Customer;
using Mc2.CrudTest.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mc2.CrudTest.Application.Crud.Customer.Requests.Queries
{
    public class GetCustomerListRequest : IRequest<BaseResponseObj<List<CustomerDto>>>
    {

    }
}
