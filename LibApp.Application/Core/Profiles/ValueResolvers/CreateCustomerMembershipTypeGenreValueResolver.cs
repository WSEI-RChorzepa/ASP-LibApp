using AutoMapper;
using LibApp.Application.Core.Contracts.Persistence;
using LibApp.Application.Core.Dtos;
using LibApp.Application.UseCases.Customers.Commands;

namespace LibApp.Application.Core.Profiles.ValueResolvers
{
    public class CreateCustomerMembershipTypeGenreValueResolver : IValueResolver<CreateCustomer.Command, CustomerDto, MembershipTypeDto>
    {
        private readonly IMembershipTypeRepository _membershipTypeRepository;

        public CreateCustomerMembershipTypeGenreValueResolver(IMembershipTypeRepository membershipTypeRepository)
        {
            _membershipTypeRepository = membershipTypeRepository;
        }

        public MembershipTypeDto Resolve(CreateCustomer.Command source, CustomerDto destination, MembershipTypeDto destMember, ResolutionContext context)
        {
            var membershipType = _membershipTypeRepository.GetAsync(source.MembershipTypeId).Result;

            return context.Mapper.Map<MembershipTypeDto>(membershipType);
        }
    }
}
