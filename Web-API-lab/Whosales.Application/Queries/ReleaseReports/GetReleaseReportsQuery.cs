using MediatR;
using Whosales.Domain;

namespace Whosales.Application.Queries.ReleaseReports
{
	public record GetReleaseReportsQuery : IRequest<IQueryable<ReleaseReport>>;
}
