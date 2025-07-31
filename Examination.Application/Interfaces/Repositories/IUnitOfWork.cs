using Examination.Domain.Entities;
using Examination.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Application.Interfaces.Repositories
{
	public interface IUnitOfWork : IDisposable
	{
		public IExamRepository ExamsRepository { get; }
		public ISubjectRepository SubjectsRepository { get; }
		public IStudentSubjectRepository StudentSubjectsRepository { get; }
		public IQuestionRepository QuestionsRepository { get; }
		public IUserRepository UserRepository { get; }
		public IDashboardRepository DashboardRepository { get; }
		public IExamSubmisisonRepository ExamSubmissionsRepository { get; }
		public IGenericRepository<ExamConfigurations> ExamConfigurationsRepository { get; }
		public IGenericRepository<QuestionOption> QuestionOptionsRepository { get; }
		public IGenericRepository<SubmissionAnswer> SubmissionAnswersRepository { get; }
		public IGenericRepository<ExamQuestion> ExamQuestionsRepository { get; }
		public Task<int> CompleteAsync();
	}
}
