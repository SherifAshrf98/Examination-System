using Examination.Application.Interfaces;
using Examination.Application.Interfaces.Repositories;
using Examination.Contracts.Events;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Infrastructure.Messaging.Consumers
{
	public class ExamEvaluatedConsumer : IConsumer<ExamEvaluatedEvent>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly INotificationService _notificationService;

		public ExamEvaluatedConsumer(IUnitOfWork unitOfWork, INotificationService notificationService)
		{
			_unitOfWork = unitOfWork;
			_notificationService = notificationService;
		}
		public async Task Consume(ConsumeContext<ExamEvaluatedEvent> context)
		{
			var data = context.Message;

			var submission = await _unitOfWork.ExamSubmissionsRepository.GetByExamIdAndStudentId(data.ExamId, data.StudentId);

			if (submission is null)
			{
				return;
			}
			submission.Score = data.Score;

			await _unitOfWork.CompleteAsync();

			await _notificationService.NotifyStudentAsync(data.StudentId, $"Your exam has been graded! You scored {submission.Score}/10!");

			await _notificationService.NotifyAdminAsync($"Student {data.StudentId} scored {data.Score}");
		}
	}
}
