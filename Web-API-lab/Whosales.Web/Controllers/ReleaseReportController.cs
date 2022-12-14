using Microsoft.AspNetCore.Mvc;
using Whosales.Domain;
using Whosales.Web.Services;

namespace Whosales.Web.Controllers
{
	[Route("/api/ReleaseReports")]
	public class ReleaseReportController : BaseController<ReleaseReportService, ReleaseReport>
	{
		public ReleaseReportController(ReleaseReportService service) : base(service)
		{
		}

		#region Create
		[HttpPost]
		public IActionResult Post([FromBody] ReleaseReport ReleaseReport)
		{
			if (ReleaseReport == null || ReleaseReport.Cost == 0)
				return BadRequest();
			Service.Add(ReleaseReport);
			return Ok(ReleaseReport);
		}
		#endregion

		#region Update
		[HttpPut]
		public ActionResult Put([FromBody] ReleaseReport ReleaseReport)
		{
			if (ReleaseReport == null)
				return BadRequest();
			Service.Update(ReleaseReport.ReleaseReportId, ReleaseReport);
			return Ok(ReleaseReport);
		}
		#endregion

		#region Delete
		[HttpDelete("{id}")]
		public ActionResult Delete(int id)
		{
			Service.Delete(id);
			return Ok();
		}
		#endregion

		#region Read
		[HttpGet]
		public async Task<IEnumerable<ReleaseReport>> GetAll()
		{
			var releaseReports = await Service.GetAll();

			return releaseReports;
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var ReleaseReport = await Service.GetById(id);
			if (ReleaseReport == null)
				return NotFound();
			return new ObjectResult(ReleaseReport);
		}
		#endregion
	}
}
