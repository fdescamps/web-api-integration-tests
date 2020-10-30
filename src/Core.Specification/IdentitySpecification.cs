namespace Core.Specification
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq.Expressions;

    [ExcludeFromCodeCoverage]
    internal sealed class IdentitySpecification<TEntity> : Specification<TEntity>
    {
        public override Expression<Func<TEntity, bool>> ToExpression()
        {
            return x => true;
        }
    }
}