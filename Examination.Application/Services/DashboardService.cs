using Examination.Application.Common;
using Examination.Application.Dtos.Dashboards;
using Examination.Application.Interfaces;
using Examination.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Application.Services
{
	public class DashboardService : IDashboardService
	{
		private readonly IUnitOfWork _unitOfWork;

		public DashboardService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

	

		//public async Task<Result<int>> GetTotalFailedExamsAsync()
		//{
		//	var TotalFailedExams = await _unitOfWork.DashboardRepository.GetTotalFailedExams();

		//	return Result<int>.Success(TotalFailedExams);
		//}

		//public async Task<Result<int>> GetTotalPassedExamsAsync()
		//{
		//	var TotalPassedExams = await _unitOfWork.DashboardRepository.GetTotalPassedExams();

		//	return Result<int>.Success(TotalPassedExams);
		//}

		//public async Task<Result<int>> GetTotalStudentsAsync()
		//{
		//	var totalStudents = await _unitOfWork.DashboardRepository.GetTotalStudentsCount();

		//	return Result<int>.Success(totalStudents);
		//}

		//public async Task<Result<int>> GetTotalSubmittedExamsAsync()
		//{
		//	var totalSubmittedExams = await _unitOfWork.DashboardRepository.GetTotalSubmittedExams();

		//	return Result<int>.Success(totalSubmittedExams);
		//}

		public async Task<Result<AdminDashboardStatsDto>> GetAdminDashboardStatsAsync()
		{
			try
			{
				var stats = new AdminDashboardStatsDto
				{
					TotalStudents = await _unitOfWork.DashboardRepository.GetTotalStudentsCount(),
					TotalPassedExams = await _unitOfWork.DashboardRepository.GetTotalPassedExams(),
					TotalFailedExams = await _unitOfWork.DashboardRepository.GetTotalFailedExams(),
					TotalSubmittedExams = await _unitOfWork.DashboardRepository.GetTotalSubmittedExams()
				};

				return Result<AdminDashboardStatsDto>.Success(stats);
			}
			catch (Exception ex)
			{
				return Result<AdminDashboardStatsDto>.Failure("Failed to retrieve dashboard statistics: " + ex.Message);
			}
		}

		public async Task<Result<StudentDashboardStatsDto>> GetStudentDashboardStatsAsync(string studentId)
		{
			try
			{
				var stats = new StudentDashboardStatsDto
				{
					TotalExams = await _unitOfWork.DashboardRepository.GetStudentTotalExams(studentId),
					CompletedExams = await _unitOfWork.DashboardRepository.GetStudentSubmittedExams(studentId),
					AverageScore = await _unitOfWork.DashboardRepository.GetStudentAverageScore(studentId),
					TotalSubjects = await _unitOfWork.DashboardRepository.GetStudentTotalSubjects(studentId)
				};

				return Result<StudentDashboardStatsDto>.Success(stats);
			}
			catch (Exception ex)
			{
				return Result<StudentDashboardStatsDto>.Failure("Failed to retrieve dashboard statistics: " + ex.Message);
			}
		}
	}
}

