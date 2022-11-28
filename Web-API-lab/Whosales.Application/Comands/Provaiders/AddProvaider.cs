using MediatR;
using Whosales.Domain;

namespace Whosales.Application.Comands.Provaiders
{
	public record AddProvaider(Provaider Provaider) : IRequest;
}
