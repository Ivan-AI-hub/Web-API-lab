using MediatR;
using Whosales.Domain;

namespace Whosales.Application.Queries.Provaiders
{
	public record GetProvaidersCount(Func<Provaider, bool>? whereRule = null) : IRequest<int>;
}
