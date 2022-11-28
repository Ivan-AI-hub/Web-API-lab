using MediatR;
using Whosales.Domain;

namespace Whosales.Application.Comands.Employers
{
	public record AddEmployer(Employer Employer) : IRequest;
}
