using Examination.Domain.Entities;
using Examination.Application.Interfaces.Repositories;
using Examination.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Infrastructure.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly AppDbContext _dbContext;
		private readonly IExamRepository _exams;
		private readonly ISubjectRepository _subjects;
		private readonly IStudentSubjectRepository _studentSubjects;
		private readonly IUserRepository _user;
		private readonly IQuestionRepository _questions;
		private readonly IExamSubmisisonRepository _examSubmissions;
		private readonly IGenericRepository<ExamConfigurations> _examConfigurations;
		private readonly IGenericRepository<ExamQuestion> _examQuestions;
		private readonly IGenericRepository<QuestionOption> _questionOptions;
		private readonly IGenericRepository<SubmissionAnswer> _submissionAnswers;
		private readonly IDashboardRepository _dashboard;

		public UnitOfWork(AppDbContext dbContext,
		IExamRepository exams,
		ISubjectRepository subjects,
		IQuestionRepository questions,
		IUserRepository user,
		IStudentSubjectRepository StudentSubjects,
		IGenericRepository<ExamConfigurations> examConfigurations,
		IExamSubmisisonRepository examSubmissions,
		IGenericRepository<ExamQuestion> examQuestions,
		IGenericRepository<QuestionOption> questionOptions,
		IGenericRepository<SubmissionAnswer> submissionAnswers,
		IDashboardRepository dashboard)
		{
			_dbContext = dbContext;
			_exams = exams;
			_subjects = subjects;
			_examSubmissions = examSubmissions;
			_questions = questions;
			_examQuestions = examQuestions;
			_questionOptions = questionOptions;
			_submissionAnswers = submissionAnswers;
			_dashboard = dashboard;
			_studentSubjects = StudentSubjects;
			_examConfigurations = examConfigurations;
			_user = user;
		}
		public IExamRepository ExamsRepository => _exams;
		public ISubjectRepository SubjectsRepository => _subjects;
		public IStudentSubjectRepository StudentSubjectsRepository => _studentSubjects;
		public IQuestionRepository QuestionsRepository => _questions;
		public IUserRepository UserRepository => _user;
		public IDashboardRepository DashboardRepository => _dashboard;
		public IGenericRepository<ExamQuestion> ExamQuestionsRepository => _examQuestions;
		public IGenericRepository<QuestionOption> QuestionOptionsRepository => _questionOptions;
		public IGenericRepository<SubmissionAnswer> SubmissionAnswersRepository => _submissionAnswers;
		public IGenericRepository<ExamConfigurations> ExamConfigurationsRepository => _examConfigurations;
		public IExamSubmisisonRepository ExamSubmissionsRepository => _examSubmissions;

		public async Task<int> CompleteAsync()
		{
			return await _dbContext.SaveChangesAsync();
		}
		public void Dispose()
		{
			_dbContext.Dispose();
		}
	}
}
