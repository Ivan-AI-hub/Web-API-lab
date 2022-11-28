using MediatR;
using Whosales.Domain;

namespace Whosales.Application.Queries.Employers
{
	public record GetEmployerById(int Id) : IRequest<Employer?>;
}
