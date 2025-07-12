using Examination.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Domain.Interfaces
{
	public interface IUnitOfWork : IDisposable
	{
		public IExamRepository ExamsRepository { get; }
		public IGenericRepository<ExamSubmission> ExamSubmissionsRepository { get; }
		public IGenericRepository<Question> QuestionsRepository { get; }
		public IGenericRepository<Subject> SubjectsRepository { get; }
		public IGenericRepository<ExamQuestion> ExamQuestionsRepository { get; }
		public IGenericRepository<QuestionOption> QuestionOptionsRepository { get; }
		public IGenericRepository<SubmissionAnswer> SubmissionAnswersRepository { get; }
		public Task<int> CompleteAsync();
	}
}
