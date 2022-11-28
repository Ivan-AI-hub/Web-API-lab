using MediatR;
using Whosales.Domain;

namespace Whosales.Application.Queries.Customers
{
	public record GetCustomersCount(Func<Customer, bool>? whereRule = null) : IRequest<int>;
}
