using Examination.Contracts.Events;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluationService.Consumer
{
	public class ExamSubmittedConsumer : IConsumer<ExamSubmittedEvent>
	{
		private readonly IPublishEndpoint _publishEndpoint;

		public ExamSubmittedConsumer(IPublishEndpoint publishEndpoint)
		{
			_publishEndpoint = publishEndpoint;
		}
		public async Task Consume(ConsumeContext<ExamSubmittedEvent> context)
		{

			var data = context.Message;

			var score = 0;

			foreach (var answer in data.Answers)
			{
				var correctAnswer = data.CorrectAnswers.FirstOrDefault(c => c.QuestionId == answer.QuestionId);

				if (correctAnswer != null && correctAnswer.CorrectOptionId == answer.SelectedOptionId)
				{
					score++;
				}
			}

			var examEvaluatedEvent = new ExamEvaluatedEvent
			{

				ExamId = data.ExamId,
				StudentId = data.StudentId,
				Score = score,
			};

			await _publishEndpoint.Publish(examEvaluatedEvent);
		}
	}
}