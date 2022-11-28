using MediatR;
using Whosales.Domain;

namespace Whosales.Application.Queries.Employers
{
	public record GetEmployersQuery : IRequest<IQueryable<Employer>>;
}
