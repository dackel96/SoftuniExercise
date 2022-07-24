using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastFood.Data
{
    class FastFoodDesignTimeDbContextFactory : IDesignTimeDbContextFactory<FastFoodContext>
    {
        public FastFoodContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<FastFoodContext>();
            //TODO Hide connection string
            builder.UseSqlServer(@"Server=DESKTOP-73BH94J\SQLEXPRESS;Database=FastFood;Trusted_Connection=True;MultipleActiveResultSets=true");
            return new FastFoodContext(builder.Options);
        }
    }
}
