using System.Net.Http;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity.Owin;

namespace Peoples.Dal.Controllers.Api
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
		public async Task<ApiLoginResult> Login( ApiLogin value )
		{
			// This doesn't count login failures towards account lockout
			// To enable password failures to trigger account lockout, change to shouldLockout: true
			var result = await SignInManager.PasswordSignInAsync( value.Email, value.Password, false, shouldLockout: false );

			return new ApiLoginResult
			{
				User = User,
				SignInStatus = result
			};
		}

		public class ApiLogin
		{
			public string Email { get; set; }

			public string Password { get; set; }
		}

		public class ApiLoginResult
		{
			public IPrincipal User { get; set; }

			public SignInStatus SignInStatus { get; set; }
		}
	}
}