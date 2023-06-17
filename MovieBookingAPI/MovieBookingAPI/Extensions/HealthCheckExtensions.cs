﻿using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using MovieBooking.Models.HealthChecks;
using Newtonsoft.Json;

namespace MovieBookingAPI.Extensions
{
	public static class HealthCheckExtensions
	{
		public static IApplicationBuilder RegisterHealthCheck(this IApplicationBuilder app)
		{
			return app.UseHealthChecks("/healthChecks", new HealthCheckOptions()
			{
				ResponseWriter = async (context, report) =>
				{
					context.Response.ContentType = "application/json";
					var response = new HealthCheckResponse()
					{
						Status = report.Status.ToString(),
						HealthChecks = report.Entries.Select(x => new IndividualHealthCheckResponse()
						{
							Component = x.Key,
							Status = x.Value.Status.ToString(),
							Description = x.Value.Description
						}),
						HealthCheckDuration = report.TotalDuration
					};
					await context.Response.WriteAsync(JsonConvert.SerializeObject(response, Formatting.Indented));
				}
			});
		}
	}
}
