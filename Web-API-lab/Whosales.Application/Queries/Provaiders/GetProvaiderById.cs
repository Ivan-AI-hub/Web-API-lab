using MediatR;
using Whosales.Domain;

namespace Whosales.Application.Queries.Provaiders
{
	public record GetProvaiderById(int id) : IRequest<Provaider?>;
}
