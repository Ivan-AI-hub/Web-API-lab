using MediatR;

namespace Whosales.Application.Comands.Customers
{
	public record DeleteCustomer(int id) : IRequest;
}
