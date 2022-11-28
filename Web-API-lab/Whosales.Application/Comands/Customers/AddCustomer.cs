using MediatR;
using Whosales.Domain;

namespace Whosales.Application.Comands.Customers
{
	public record AddCustomer(Customer customer) : IRequest;
}
