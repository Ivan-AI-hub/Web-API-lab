using MediatR;
using Whosales.Domain;

namespace Whosales.Application.Queries.ReleaseReports
{
	public record GetReleaseReportsCount(Func<ReleaseReport, bool>? whereRule = null) : IRequest<int>;
}
