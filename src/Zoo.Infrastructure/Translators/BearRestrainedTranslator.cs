namespace Zoo.Infrastructure.Translators
{
    using AutoMapper;

    using Entities;

    using Park.BearsAggregate.Models;

    internal class BearRestrainedTranslator : Profile
    {
        public BearRestrainedTranslator()
        {
            this.CreateMap<Animal, BearRestrained>()
                .ForMember(bear => bear.Family, opt => opt.Ignore());
        }
    }
}