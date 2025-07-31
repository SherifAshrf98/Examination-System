using EvaluationService.Consumer;
using MassTransit;
using System.Reflection;

namespace EvaluationService
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = Host.CreateApplicationBuilder(args);


			builder.Services.AddMassTransit(x =>
			{
				x.AddConsumer<ExamSubmittedConsumer>();

				x.UsingRabbitMq((ctx, cfg) =>
				{
					cfg.Host("localhost", "/", h =>
					{
						h.Username("guest");
						h.Password("guest");
					});

					cfg.ReceiveEndpoint("exam-submitted-queue", e =>
					{
						e.ConfigureConsumer<ExamSubmittedConsumer>(ctx);
					});
				});
			});

			var host = builder.Build();
			host.Run();
		}
	}
}