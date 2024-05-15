using Microsoft.AspNetCore.Identity;

namespace ElearningAPI.Datas
{
	public class User : IdentityUser
	{
		public string? FullName { get; set; }
		
	}
}
