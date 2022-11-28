using MediatR;
using Whosales.Domain;

namespace Whosales.Application.Queries.ReleaseReports
{
	public record GetReleaseReportById(int id) : IRequest<ReleaseReport?>;
}
