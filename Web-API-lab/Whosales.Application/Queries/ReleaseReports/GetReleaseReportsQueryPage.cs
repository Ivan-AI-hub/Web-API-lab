using MediatR;
using Whosales.Domain;

namespace Whosales.Application.Queries.ReleaseReports
{
	public record GetReleaseReportsQueryPage(int pageSize, int pageNumber,
		Func<ReleaseReport, dynamic>? orderByRule = null, Func<ReleaseReport, bool>? whereRule = null)
		: IRequest<IQueryable<ReleaseReport>>;
}
