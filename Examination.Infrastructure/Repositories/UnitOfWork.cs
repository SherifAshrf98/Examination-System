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
		private readonly IGenericRepository<ExamSubmission> _examSubmissions;
		private readonly IGenericRepository<Question> _questions;
		private readonly IGenericRepository<ExamQuestion> _examQuestions;
		private readonly IGenericRepository<QuestionOption> _questionOptions;
		private readonly IGenericRepository<SubmissionAnswer> _submissionAnswers;
		private readonly IGenericRepository<StudentSubject> _studentSubjects;

		public UnitOfWork(AppDbContext dbContext,
		IExamRepository exams,
		ISubjectRepository subjects,
		IGenericRepository<ExamSubmission> examSubmissions,
		IGenericRepository<Question> questions,
		IGenericRepository<ExamQuestion> examQuestions,
		IGenericRepository<QuestionOption> questionOptions,
		IGenericRepository<SubmissionAnswer> submissionAnswers,
		IGenericRepository<StudentSubject> StudentSubjects)
		{
			_dbContext = dbContext;
			_exams = exams;
			_subjects = subjects;
			_examSubmissions = examSubmissions;
			_questions = questions;
			_examQuestions = examQuestions;
			_questionOptions = questionOptions;
			_submissionAnswers = submissionAnswers;
			_studentSubjects = StudentSubjects;
		}
		public IExamRepository ExamsRepository => _exams;
		public ISubjectRepository SubjectsRepository => _subjects;
		public IGenericRepository<ExamSubmission> ExamSubmissionsRepository => _examSubmissions;
		public IGenericRepository<Question> QuestionsRepository => _questions;
		public IGenericRepository<ExamQuestion> ExamQuestionsRepository => _examQuestions;
		public IGenericRepository<QuestionOption> QuestionOptionsRepository => _questionOptions;
		public IGenericRepository<SubmissionAnswer> SubmissionAnswersRepository => _submissionAnswers;
		public IGenericRepository<StudentSubject> StudentSubjectsRepository => _studentSubjects;


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
