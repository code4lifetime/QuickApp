// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using QuickApp.SQLDAL.Models;

namespace QuickApp
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<QuickappContext>
    {
        public QuickappContext CreateDbContext(string[] args)
        {
            Mapper.Reset();

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Development.json", optional: true)
                .Build();

            var builder = new DbContextOptionsBuilder<QuickappContext>();

            builder.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"], b => b.MigrationsAssembly("QuickApp"));

            return new QuickappContext(builder.Options);
        }
    }
}
