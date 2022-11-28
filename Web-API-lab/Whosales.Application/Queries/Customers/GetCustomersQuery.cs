using MediatR;
using Whosales.Domain;

namespace Whosales.Application.Queries.Customers
{
	public record GetCustomersQuery : IRequest<IQueryable<Customer>>;
}
