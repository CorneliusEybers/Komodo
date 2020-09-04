// - Required Assemblies
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// - Application Assemblies
using Komodo.Ui.Repositories.Commodity;

namespace Komodo.Ui
{
  public class Startup
  {
    #region Properties

    public IConfiguration Configuration
    {
      get; internal set;
    }

    #endregion

    #region Construct

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    #endregion

    #region Methods

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc();

      //services.AddSingleton<ICommodityRepository, CommodityRepositoryMock>();
      services.AddSingleton<ICommodityRepository, CommodityRepositoryToService>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder applicationBuilder, IWebHostEnvironment webHostEnvironment)
    {
      if (webHostEnvironment.IsDevelopment())
      {
        applicationBuilder.UseDeveloperExceptionPage();
      }

      applicationBuilder.UseStaticFiles();

      // - MVC
      applicationBuilder.UseRouting();
      applicationBuilder.UseEndpoints(endpoints => { endpoints.MapControllerRoute("default","{controller=Commodity}/{action=Index}"); });

    }

    #endregion
  }
}
