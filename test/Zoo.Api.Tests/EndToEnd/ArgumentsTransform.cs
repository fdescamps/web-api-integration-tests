﻿namespace Zoo.Api.Tests.EndToEnd
{
    using System;
    using System.Collections;
    using System.Collections.Immutable;
    using System.Net;
    using System.Net.Http;

    using Infrastructure.Entities;

    using Park.BearsAggregate.Models;
    using Park.Common.Models;

    using TechTalk.SpecFlow;
    using TechTalk.SpecFlow.Assist;

    [Binding]
    public class ArgumentsTransform : Steps
    {
        [StepArgumentTransformation(@"(GET|POST|PUT|DELETE)")]
        public HttpMethod HttpMethodTransform(string httpMethod)
        {
            return new HttpMethod(httpMethod);
        }

        [StepArgumentTransformation("the http status code of response is (.*)")]
        public HttpStatusCode HttpStatusCodeTransform(int status)
        {
            return (HttpStatusCode)status;
        }

        [StepArgumentTransformation(@"the referential have any animals")]
        [StepArgumentTransformation(@"the referential have any families")]
        [StepArgumentTransformation(@"the referential have any classification")]
        [StepArgumentTransformation(@"restrained bears")]
        [StepArgumentTransformation(@"the referential have any foods")]
        [StepArgumentTransformation(@"the animal can eats")]
        public IEnumerable RestrainedAnimalsTransform(Table table)
        {
            var stepTitle = this.StepContext.StepInfo.Text;
            if (stepTitle.Contains("restrained bears"))
            {
                return table.CreateSet<BearRestrained>().ToImmutableArray();
            }

            if (stepTitle.Contains("restrained animals"))
            {
                return table.CreateSet<AnimalRestrained>().ToImmutableArray();
            }

            if (stepTitle.Contains("animals"))
            {
                return table.CreateSet<Animal>().ToImmutableArray();
            }

            if (stepTitle.Contains("families"))
            {
                return table.CreateSet<Family>().ToImmutableArray();
            }

            if (stepTitle.Contains("classification"))
            {
                return table.CreateSet<Classification>().ToImmutableArray();
            }

            if (stepTitle.Contains("food"))
            {
                return table.CreateSet<Food>().ToImmutableArray();
            }


            if (stepTitle.Contains("animal can eat"))
            {
                return table.CreateSet<AnimalCanEat>().ToImmutableArray();
            }

            throw new ArgumentOutOfRangeException(stepTitle);
        }
    }
}