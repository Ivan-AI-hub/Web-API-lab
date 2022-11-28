using MediatR;
using Whosales.Domain;

namespace Whosales.Application.Queries.Customers
{
	public record GetCustomerById(int id) : IRequest<Customer?>;
}
