using CRUD_WEB_API.IRepository;
using CRUD_WEB_API.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_WEB_API
{
    public class DependencyContainer
    {

        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IStudent, Student>();
        }
    }
}
