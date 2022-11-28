using MediatR;
using Whosales.Domain;

namespace Whosales.Application.Comands.ReleaseReports
{
	public record UpdateReleaseReport(int id, ReleaseReport ReleaseReport) : IRequest;
}
