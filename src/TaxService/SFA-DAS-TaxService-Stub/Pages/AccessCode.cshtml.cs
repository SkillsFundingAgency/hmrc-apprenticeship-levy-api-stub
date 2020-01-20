using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SFA_DAS_TaxService_Stub.Pages
{
    public class AccessCodeModel : PageModel
    {
        [BindProperty(Name = "continue", SupportsGet = true)]
        public string ContinueUrl { get; set; }
        [BindProperty(Name = "origin", SupportsGet = true)]
        public string Origin { get; set; }

        public void OnGet()
        {
            ViewData["ContUrl"] = ContinueUrl;
            ViewData["Origin"] = Origin;
        }

        public async Task<IActionResult> OnPost()
        {
            return Redirect(ContinueUrl);
        }
    }
}