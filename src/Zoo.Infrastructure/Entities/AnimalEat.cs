﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Zoo.Infrastructure.Entities
{
    public partial class AnimalEat
    {
        public int AnimalId { get; set; }
        public int FoodId { get; set; }

        public virtual Animal Animal { get; set; }
        public virtual Food Food { get; set; }
    }
}