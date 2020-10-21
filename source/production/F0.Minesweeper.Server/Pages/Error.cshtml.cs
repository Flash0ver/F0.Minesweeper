using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace F0.Minesweeper.Server.Pages
{
	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	[IgnoreAntiforgeryToken]
	public class ErrorModel : PageModel
	{
		public string? RequestId { get; set; }

		public bool ShowRequestId => !String.IsNullOrEmpty(RequestId);

		private readonly ILogger<ErrorModel> logger;

		public ErrorModel(ILogger<ErrorModel> logger)
			=> this.logger = logger;

		public void OnGet()
			=> RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
	}
}
