using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using What_Should_I_eat.Data.Entities;

namespace What_Should_I_eat.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public virtual DbSet<Continent> Continents { get; set; }
        public virtual DbSet<Cuisine> Cuisines { get; set; }
        public virtual DbSet<Dish> Dishes { get; set; }
        public virtual DbSet<Ingredient> Ingredients { get; set; }
        public virtual DbSet<CuisineDish> CuisineDishes  { get; set; }
        public virtual  DbSet<DishIngredient> DishIngredients { get; set; }



        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Cuisine>()
                .HasOne<Continent>(c=>c.Continent)
                .WithMany(c => c.Cuisines)
                .HasForeignKey(c => c.ContinentId);

            builder.Entity<CuisineDish>().
                HasKey(cd => new {cd.CuisineId, cd.DishId});
            builder.Entity<DishIngredient>()
                .HasKey(di => new {di.DishId,di.IngredientId});

            base.OnModelCreating(builder);
        }
    }
}
