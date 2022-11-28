using MediatR;
using Whosales.Domain;

namespace Whosales.Application.Queries.Customers
{
	public record GetCustomersQueryPage(int pageSize, int pageNumber,
		Func<Customer, dynamic>? orderByRule = null, Func<Customer, bool>? whereRule = null)
		: IRequest<IQueryable<Customer>>;
}
