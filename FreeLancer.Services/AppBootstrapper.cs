using FreeLancer.Models;
using FreeLancer.Services.Application.Handler;
using FreeLancer.Services.Application.Interface;
using FreeLancer.Services.Jobs;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FreeLancer.Services
{
	public static class AppBootstrapper
	{
		public static void InitServices(this IServiceCollection service)
		{
			service.AddTransient<IJobService, JobsService>();
			service.AddTransient<IApplication, JobApplicationService>();
		}
	}
}
