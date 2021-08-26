using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagazineConnector.Extensions
{
    public static class WebHostExtensions
    {
        public static IHost SeedData(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetService<Contexts.BlogContext>();

                context.Database.Migrate();

                new Contexts.Seeds.AuthorSeed(context).SeedData();
                new Contexts.Seeds.ArticleSeed(context).SeedData();
            }

            return host;
        }
    }
}
