using MediatR;
using Whosales.Domain;

namespace Whosales.Application.Queries.Employers
{
	public record GetEmployersCount(Func<Employer, bool>? whereRule = null) : IRequest<int>;
}
