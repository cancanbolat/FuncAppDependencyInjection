using System;
using System.Collections.Generic;
using System.Text;

namespace FuncAppDepInject.Services
{
    public class MyService : IMyService
    {
        public string MyName(string name)
        {
            return $"Hello {name}";
        }
    }
}
