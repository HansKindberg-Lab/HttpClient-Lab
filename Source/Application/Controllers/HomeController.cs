using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading.Tasks;
using Application.Models.Views.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Application.Controllers
{
	public class HomeController : Controller
	{
		#region Constructors

		public HomeController(ILoggerFactory loggerFactory)
		{
			this.Logger = (loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory))).CreateLogger(this.GetType());
		}

		#endregion

		#region Properties

		protected internal virtual ILogger Logger { get; }

		#endregion

		#region Methods

		public virtual async Task<IActionResult> Index()
		{
			return await Task.FromResult(this.View(new HomeViewModel()));
		}

		[HttpPost]
		[SuppressMessage("Design", "CA1031:Do not catch general exception types")]
		public virtual async Task<IActionResult> Index(Form form)
		{
			if(form == null)
				throw new ArgumentNullException(nameof(form));

			var model = new HomeViewModel
			{
				Form = form
			};

			if(form.Url != null)
			{
				if(Uri.TryCreate(form.Url, UriKind.Absolute, out var url))
				{
					try
					{
						using(var httpClient = new HttpClient())
						{
							var httpResponseMessage = await httpClient.GetAsync(url);

							httpResponseMessage.EnsureSuccessStatusCode();

							model.Content = await httpResponseMessage.Content.ReadAsStringAsync();
						}
					}
					catch(Exception exception)
					{
						model.Exception = new InvalidOperationException($"Could not get a response from \"{url}\".", exception);
					}
				}
				else
				{
					model.Exception = new InvalidOperationException($"The url \"{form.Url}\" is not a valid absolute url.");
				}
			}
			else
			{
				model.Exception = new InvalidOperationException("The url can not be null.");
			}

			return await Task.FromResult(this.View(model));
		}

		#endregion
	}
}