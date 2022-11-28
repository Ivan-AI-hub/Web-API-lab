using MediatR;

namespace Whosales.Application.Comands.Employers
{
	public record DeleteEmployer(int id) : IRequest;
}
