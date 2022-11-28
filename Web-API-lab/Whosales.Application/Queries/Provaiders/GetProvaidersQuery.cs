using MediatR;
using Whosales.Domain;

namespace Whosales.Application.Queries.Provaiders
{
	public record GetProvaidersQuery : IRequest<IQueryable<Provaider>>;
}
