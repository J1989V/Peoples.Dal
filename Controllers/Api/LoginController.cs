using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity.Owin;

namespace Peoples.Dal.Api
{
	public class LoginController : ApiController
	{
		private ApplicationSignInManager _signInManager;
		private ApplicationUserManager _userManager;

		public LoginController( ) { }

		public LoginController( ApplicationUserManager userManager, ApplicationSignInManager signInManager )
		{
			UserManager = userManager;
			SignInManager = signInManager;
		}

		public ApplicationSignInManager SignInManager
		{
			get { return _signInManager ?? Request.GetOwinContext( ).Get<ApplicationSignInManager>( ); }
			private set { _signInManager = value; }
		}

		public ApplicationUserManager UserManager
		{
			get { return _userManager ?? Request.GetOwinContext( ).GetUserManager<ApplicationUserManager>( ); }
			private set { _userManager = value; }
		}

		[HttpPost]
		[AllowAnonymous]
		public ApiLoginResult Login( ApiLogin value )
		{
			// This doesn't count login failures towards account lockout
			// To enable password failures to trigger account lockout, change to shouldLockout: true
			var result = SignInManager.PasswordSignInAsync( value.Email, value.Password, false, shouldLockout: false );

			if ( result.Result == SignInStatus.Success )
			{
				return new ApiLoginResult
				{
					Result = result.Result.ToString( ),
					Token = "ToDoGenerateToken"
				};
			}
			
			return new ApiLoginResult
			{
				Result = result.Result.ToString( )
			};
		}

		public class ApiLogin
		{
			public string Email { get; set; }

			public string Password { get; set; }
		}

		public class ApiLoginResult
		{
			public string Result { get; set; }

			public string Token { get; set; }
		}
	}
}