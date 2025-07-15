namespace Examination.Api.Helpers
{
	public class ApiResponse
	{
		public int StatusCode { set; get; }
		public string? Message { set; get; }

		public ApiResponse(int statusCode, string? message = null)
		{
			StatusCode = statusCode;

			Message = message ?? GetDefaultMessageForStatusCode(StatusCode);
		}

		private string? GetDefaultMessageForStatusCode(int? statusCode)
		{
			return statusCode switch
			{
				200 => "OK",
				201 => "Created",
				204 => "No Content",
				400 => "Bad Request",
				401 => "your are not Authorized",
				404 => "Source not Found",
				500 => "Internal Server Error",
				_ => null
			};
		}
	}

	public class ApiResponse<T> : ApiResponse
	{
		public T? Data { get; set; }

		public ApiResponse(int statusCode, T? data = default, string? message = null)
			: base(statusCode, message)
		{
			Data = data;
		}
	}
}
