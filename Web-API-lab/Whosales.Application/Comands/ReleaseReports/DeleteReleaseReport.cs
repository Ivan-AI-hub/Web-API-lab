using MediatR;

namespace Whosales.Application.Comands.ReleaseReports
{
	public record DeleteReleaseReport(int id) : IRequest;
}
