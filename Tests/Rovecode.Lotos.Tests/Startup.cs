using System;
using Microsoft.Extensions.DependencyInjection;
using Rovecode.Lotos.DependencyInjection.Extensions;

namespace Rovecode.Lotos.Tests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLotos("mongodb://localhost:27017", "LotosTests");
        }
    }
}
