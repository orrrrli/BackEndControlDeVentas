using Datos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace Presentacion
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BDContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Conexion")));

        }

    }
}
