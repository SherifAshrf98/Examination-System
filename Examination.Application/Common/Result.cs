using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Application.Common
{
	public class Result<T>
	{
		public bool IsSuccess { get; set; }
		public T Value { get; set; }
		public List<string> Errors { get; set; }

		public static Result<T> Success(T value)
		{
			return new Result<T>
			{
				IsSuccess = true,
				Value = value,
				Errors = new List<string>()
			};
		}
		public static Result<T> Failure(List<string> errors)
		{
			return new Result<T>
			{
				IsSuccess = false,
				Value = default,
				Errors = errors ?? new List<string>()
			};
		}
		public static Result<T> Failure(string error)
		{
			return new Result<T>
			{
				IsSuccess = false,
				Value = default,
				Errors = new List<string> { error }
			};
		}
	}
}
