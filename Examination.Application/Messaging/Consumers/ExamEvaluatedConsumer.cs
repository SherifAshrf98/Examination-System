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

namespace Examination.Application.Messaging.Consumers
{
	public class ExamEvaluatedConsumer : IConsumer<ExamEvaluatedEvent>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly INotificationManger _notificationManger;

		public ExamEvaluatedConsumer(IUnitOfWork unitOfWork, INotificationManger notificationManger)
		{
			_unitOfWork = unitOfWork;
			_notificationManger = notificationManger;
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

			await _notificationManger.NotifyStudentAsync(data.StudentId, $"Your {submission.Exam.Subject.Name} exam has been graded! You scored {submission.Score}/100!");

			await _notificationManger.NotifyAdminsAsync($"Student with Id : {data.StudentId} scored {data.Score} at the {submission.Exam.Subject.Name} exam ");
		}
	}
}
