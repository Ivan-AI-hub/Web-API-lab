using MediatR;
using Whosales.Domain;

namespace Whosales.Application.Comands.Provaiders
{
	public record UpdateProvaider(int id, Provaider Provaider) : IRequest;
}
