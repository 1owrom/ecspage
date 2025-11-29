using System;
using Microsoft.Extensions.DependencyInjection;
using WinFormsApp = System.Windows.Forms.Application;

using ecspage.Bootstrap;

using ecspage.Application.Contracts;

using ecspage.Application.Services;
using ecspage.Application.Totales;

using ecspage.Infrastructure.Abstractions;
using ecspage.Infrastructure.Persistence;
using ecspage.Infrastructure.Repositories;
using ecspage.Infrastructure.Export;

namespace ecspage
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            WinFormsApp.EnableVisualStyles();
            WinFormsApp.SetCompatibleTextRenderingDefault(false);

            var services = new ServiceCollection();

            services.AddSingleton<IConnectionFactory, SqlConnectionFactoryAdapter>();
            services.AddScoped<IUnitOfWork, SqlUnitOfWork>();

            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IProductoRepository, ProductoRepository>();
            services.AddScoped<IFacturaRepository, FacturaRepository>();
            services.AddScoped<IDetalleFacturaRepository, DetalleFacturaRepository>();

            services.AddScoped<IProductoService, ProductoService>();
            services.AddScoped<IFacturaService, FacturaService>();

            services.AddScoped<IImpuestoPolicy, IGV18Policy>();
            services.AddScoped<IDescuentoPolicy, SinDescuentoPolicy>();
            services.AddScoped<ITotalesCalculator, TotalesCalculator>();

            services.AddScoped<IInvoiceExporter, PdfInvoiceExporter>();

            AppHost.Init(services.BuildServiceProvider());

            WinFormsApp.Run(new Form5());
        }
    }
}
