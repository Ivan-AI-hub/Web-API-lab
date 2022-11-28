using MediatR;
using Whosales.Domain;

namespace Whosales.Application.Comands.ReleaseReports
{
	public record AddReleaseReport(ReleaseReport ReleaseReport) : IRequest;
}
