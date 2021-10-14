using Ecommerce.Service;
using EcommerceRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Utilities;

namespace Ecommerce
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<EcommerceDBContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("EcommerseConnection")));
            services.AddScoped<IEcommerceService, EcommerceService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<EcommerceDBContext>();
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins("http://localhost:4200")
                                      .AllowAnyHeader();
                                  });
            });

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this-is-my-secret-key"));
            var tokenValidationParameter = new TokenValidationParameters()
            {
                IssuerSigningKey = signingKey,
                ValidateIssuer =false,
                ValidateAudience=false,
                //ValidateLifetime=false,
                //ClockSkew=TimeSpan.Zero
            };


            services.AddAuthentication(x => x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(jwt => 
                    {
                        //jwt.SaveToken = true;
                        //jwt.RequireHttpsMetadata = true;
                        jwt.TokenValidationParameters = tokenValidationParameter;
                    }
                    );


            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseExceptionHandler(options => 
            //{
            //    options.Run(async context => {
            //        context.Response.StatusCode = 500;//Internal Server Error
            //        context.Response.ContentType = "application/json";
            //        await context.Response("We are woirking on it");
            //    });
            //});
            app.UseRouting();
            app.UseCors(MyAllowSpecificOrigins);
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
