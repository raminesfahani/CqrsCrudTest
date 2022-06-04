using Mc2.CrudTest.Application.DTOs.Customer;
using Mc2.CrudTest.Application.Responses;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Mc2.CrudTest.UI.Services
{
    public class CustomerService
    {
        private readonly string _baseUrl = "https://localhost:44388/api";
        private HttpClient _httpClient { get; set; }
        public CustomerService(HttpClient Http)
        {
            _httpClient = Http;
        }

        public async Task<BaseResponseObj<List<CustomerDto>>> GetCustomers()
        {
            var result = new BaseResponseObj<List<CustomerDto>>();
            var customers = (await _httpClient.GetFromJsonAsync<BaseResponseObj<List<CustomerDto>>>($"{_baseUrl}/customers"));
            return customers;
        }

        public async Task<BaseResponseObj<object>> AddCustomer(CustomerDto customer)
        {
            var response = await _httpClient.PostAsJsonAsync<CustomerDto>($"{_baseUrl}/customers", customer);
            return await response.Content.ReadFromJsonAsync<BaseResponseObj<object>>();
        }
        public async Task<BaseResponseObj<object>> EditCustomer(CustomerDto customer)
        {
            var response = await _httpClient.PutAsJsonAsync<CustomerDto>($"{_baseUrl}/customers", customer);
            return await response.Content.ReadFromJsonAsync<BaseResponseObj<object>>();
        }

        public async Task DeleteCustomer(int id)
        {
            await _httpClient.DeleteAsync($"{_baseUrl}/customers/{id}");
        }
    }
}
