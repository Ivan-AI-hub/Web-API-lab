using MediatR;
using Whosales.Domain;


namespace Whosales.Application.Queries.Employers
{
	public record GetEmployersQueryPage(int pageSize, int pageNumber,
		Func<Employer, dynamic>? orderByRule = null, Func<Employer, bool>? whereRule = null) : IRequest<IQueryable<Employer>>;
}
