using MediatR;
using Whosales.Domain;

namespace Whosales.Application.Comands.Customers
{
	public record UpdateCustomer(int id, Customer customer) : IRequest;
}
