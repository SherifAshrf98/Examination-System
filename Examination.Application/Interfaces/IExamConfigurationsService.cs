using Examination.Application.Common;
using Examination.Application.Dtos.ExamConfigurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Application.Interfaces
{
	public interface IExamConfigurationsService
	{
		Task<Result<int>> AddExamConfigurations(int subjectId, CreateExamConfigurationsDto createExamConfigurationsDto);
		Task<Result<bool>> UpdateExamConfigurations(int subjectId, UpdateExamConfigurationsDto updateExamConfigurationsDto);
		Task<Result<ExamConfigurationsDto>> GetConfigurationsBySubjectIdAsync(int SubjectId);
	}
}
