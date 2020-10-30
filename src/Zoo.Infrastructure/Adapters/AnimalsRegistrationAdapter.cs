namespace Zoo.Infrastructure.Adapters
{
    using System.Threading.Tasks;

    using Administration.AnimalsRegistrationAggregate;
    using Administration.Common;

    using AutoMapper;

    using Entities;

    using Park.Common.Models;

    using Store;

    internal class AnimalsRegistrationAdapter : IAnimalsRegistrationAdapter
    {
        private readonly IMapper mapper;
        
        private readonly IWriter writer;

        public AnimalsRegistrationAdapter(IMapper mapper, IWriter writer)
        {
            this.mapper = mapper;
            this.writer = writer;
        }
        
        public async Task<TDetails> RegisterAsync<TCreating, TDetails>(TCreating creatingAnimal) 
            where TCreating : AnimalCreating
        {  
            var entity = this.mapper.Map<Animal>(creatingAnimal);
            this.writer.Create(entity);
            await this.writer.SaveAsync();
            return this.mapper.Map<TDetails>(entity);
        }
    }
}