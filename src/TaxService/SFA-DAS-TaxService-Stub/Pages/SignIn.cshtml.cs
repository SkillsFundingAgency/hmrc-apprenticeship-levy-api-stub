using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SFA.DAS.HMRC.API.Stub.Services;
using SFA_DAS_TaxService_Stub.Configuration;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace SFA_DAS_TaxService_Stub.Pages
{
    public class Login
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class SignInModel : PageModel
    {
        private IAuthenticate _authService;

        [BindProperty(Name = "continue", SupportsGet = true)]
        public string ContinueUrl { get; set; }
        [BindProperty(Name = "origin", SupportsGet = true)]
        public string Origin { get; set; }

        public SignInModel(IAuthenticate authService)
        {
            _authService = authService;
        }

        public void OnGet()
        {
            ViewData["ContUrl"] = ContinueUrl;
            ViewData["Origin"] = Origin;
        }

        public async Task<IActionResult> OnPost(Login login)
        {
            var result = await _authService.Validate(login.UserId, login.Password);

            if(result == null)
            {
                ModelState.AddModelError("credentials_invalid", "Bad user name or password");

                return Redirect($"SignIn?continue={ContinueUrl}&origin={Origin}");
            }

            if(result.Require2SV)
            {
                AddToSession(Constants.GATEWAYIDSESSIONKEY, login.UserId);
                return Redirect($"AccessCode?continue={ContinueUrl}&origin={Origin}");
            }
            else
            {
                AddToSession(Constants.GATEWAYIDSESSIONKEY, login.UserId);
                return Redirect(ContinueUrl);
            }
        }

        public void AddToSession(string key, string value)
        {
            HttpContext.Session.SetString(key, value);
        }
    }
}