﻿using EF_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace EF_Core
{
    public static class DataSeed
    {
        public static void DataSeeding(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Electronics" },
                new Category { Id = 2, Name = "Food" },
                new Category { Id = 3, Name = "Cloths" },
                new Category { Id = 4, Name = "Bauty" }
                );

        }
    }
}
