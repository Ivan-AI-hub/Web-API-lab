using MediatR;
using Whosales.Domain;

namespace Whosales.Application.Comands.Employers
{
	public record UpdateEmployer(int id, Employer Employer) : IRequest;
}
