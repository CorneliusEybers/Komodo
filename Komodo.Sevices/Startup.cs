// - Required Assemblies
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// - Application Assemblies
using Komodo.Sevices.DbContext;
using Komodo.Sevices.Repositories.Commodity;

namespace Komodo.Sevices
{
  public class Startup
  {

    #region Properties

    public IConfiguration Configuration
    {
      get;
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
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc();
      services.AddDbContextPool<KomodoDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("KomodoDbContext")));
      services.AddScoped<ICommodityRepository, CommodityRepositorySql>();
      services.AddControllers();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseRouting();
      app.UseEndpoints(endpoints => {endpoints.MapControllerRoute("default","/api/{controller=Commodity}/{action=GetCommodities}");});
    }

    #endregion
  }
}
