using System;
using System.Collections.Generic;
using System.Text;

namespace Mc2.CrudTest.Application.Responses
{
    public class BaseResponseObj<T> where T : class
    {
        public T Result { get; set; } = null;
        public bool Success { get; set; } = true;
        public string Message { get; set; }
        public List<string> Errors { get; set; }
    }
}
