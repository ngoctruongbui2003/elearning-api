namespace ElearningAPI.Responses
{
	public class ServiceResponses
	{
		public record class GeneralResponse<T>(bool Flag, string Message, T metadata);
		public record class LoginResponse(bool Flag, string Token, string Message);
	}
}
