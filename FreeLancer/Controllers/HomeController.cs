using Azure;
using FreeLancer.Data;
using FreeLancer.Models.Models.Domain;
using FreeLancer.Models.Models.DTO;
using FreeLancer.Models.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace FreeLancer.Controllers
{
    public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		public readonly IHttpClientFactory _clientfactory;
		public readonly AppDbContext _db;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory clientfactory, AppDbContext db)
        {
            _logger = logger;
            _clientfactory = clientfactory;
            _db = db;
        }

        [HttpGet]
		public async Task<IActionResult> Index()
		{
			List<JobDTO> jobs = new();
			HttpClient client = _clientfactory.CreateClient("FreeLancer.Api");
			string uri = client.BaseAddress + "/Jobs/GetAll";
			HttpRequestMessage request = new(HttpMethod.Get, uri);
			HttpResponseMessage response = await client.SendAsync(request);
			response.EnsureSuccessStatusCode();
			if (response.IsSuccessStatusCode)
			{
				string data = response.Content.ReadAsStringAsync().Result;
				jobs = JsonConvert.DeserializeObject<List<JobDTO>>(data);
				return View(jobs);
			}
			return View();

		}

		[HttpGet]
		public IActionResult Create()
		{
                IEnumerable<SelectListItem> Skills = _db.Skills.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.SkillName
                });
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([FromForm] JobDTO model)
		{
			HttpClient client = _clientfactory.CreateClient("FreeLancer.Api");
			string uri = client.BaseAddress + "/Jobs/Post";
			var json = JsonConvert.SerializeObject(model);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			HttpResponseMessage response = await client.PostAsync(uri, content);
			response.EnsureSuccessStatusCode();
			if (response.IsSuccessStatusCode)
			{
				return RedirectToAction(nameof(Index));
			}
			return View(model);
		}
		[HttpGet]
		public async Task<IActionResult> Edit([FromRoute] int id)
		{
			HttpClient client = _clientfactory.CreateClient("FreeLancer.Api");
			string uri = $"{client.BaseAddress}/Jobs/GetById/get_byId/{id}";
			HttpRequestMessage request = new(HttpMethod.Get, uri);
			HttpResponseMessage response = await client.SendAsync(request);
			response.EnsureSuccessStatusCode();
			if (response.IsSuccessStatusCode)
			{
				string data = response.Content.ReadAsStringAsync().Result;
				var job = JsonConvert.DeserializeObject<Job>(data);
				return View(job);
			}
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit([FromForm] Job model, [FromForm] int Id)
		{
			HttpClient client = _clientfactory.CreateClient("FreeLancer.Api");
			string uri = client.BaseAddress + $"/Jobs/Put/updateJob/{Id}";
			var json = JsonConvert.SerializeObject(model);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			HttpResponseMessage response = await client.PutAsync(uri, content);
			response.EnsureSuccessStatusCode();
			if (response.IsSuccessStatusCode)
			{
				return RedirectToAction(nameof(Index));
			}
			return View(model);
		}
		public async Task<IActionResult> Delete([FromRoute] int id)
		{
			HttpClient client = _clientfactory.CreateClient("FreeLancer.Api");
			string uri = client.BaseAddress + $"/Jobs/Delete/DeleteJob/{id}";
			HttpResponseMessage response = await client.DeleteAsync(uri);
			response.EnsureSuccessStatusCode();
			if (response.IsSuccessStatusCode)
			{
				return RedirectToAction(nameof(Index));
			}
			return View();
		}
	}
}
