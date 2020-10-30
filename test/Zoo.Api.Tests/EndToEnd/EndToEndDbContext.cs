﻿namespace Zoo.Api.Tests.EndToEnd
{
    using Infrastructure.Entities;

    using Microsoft.EntityFrameworkCore;

    public sealed class EndToEndDbContext : PatternContext
    {
        public EndToEndDbContext(DbContextOptions<EndToEndDbContext> options)
            : base(options)
        {
        }
    }
}