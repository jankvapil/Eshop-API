using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Interceptors;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Eshop.GraphQL;
using Eshop.GraphQL.Types;
using Eshop.GraphQL.Data;
using Eshop.GraphQL.DataLoader;
using Eshop.GraphQL.Users;
using Eshop.GraphQL.Orders;
using Eshop.GraphQL.Products;
using Eshop.GraphQL.OrderItems;
using HotChocolate.AspNetCore.Voyager;

namespace GraphQL
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
                => this.Configuration = configuration;


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddCors(options =>
            {
                options.AddPolicy("DefaultPolicy", builder =>
                {
                    builder.AllowAnyHeader()
                           .WithMethods("GET", "POST")
                           .WithOrigins("*");
                });
            });

            services.AddScoped<IIdentityService, IdentityService>();
            services.AddHttpContextAccessor();

            services.AddAuthorization(x =>
            {
                x.AddPolicy("hr-department", builder =>
                    builder
                        .RequireAuthenticatedUser()
                        .RequireRole("hr")
                );

                x.AddPolicy("DevDepartment", builder =>
                    builder.RequireRole("dev")
                );
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = "audience",
                    ValidIssuer = "issuer",
                    RequireSignedTokens = false,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secretsecretsecret"))
                };

                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
            });


            // "Database": "Filename=./eshop.db"
            
            services.AddPooledDbContextFactory<ApplicationDbContext>(
                options => options.UseSqlite(Configuration.GetConnectionString("Database")));

            //services.AddPooledDbContextFactory<ApplicationDbContext>(
            //    options => options.UseSqlite("Data Source=eshop.db"));

            services
                .AddGraphQLServer()
                .AddQueryType(d => d.Name("Query"))
                    .AddTypeExtension<UserQueries>()
                    .AddTypeExtension<OrderQueries>()
                    .AddTypeExtension<ProductQueries>()
                    .AddTypeExtension<OrderItemQueries>()
                .AddMutationType(d => d.Name("Mutation"))
                    .AddTypeExtension<UserMutations>()
                    .AddTypeExtension<OrderMutations>()
                    .AddTypeExtension<ProductMutations>()
                    .AddTypeExtension<OrderItemMutations>()
                .AddType<UserType>()
                .AddType<OrderType>()
                .AddType<OrderItemType>()
                // .EnableRelaySupport()
                .AddDataLoader<OrderByIdDataLoader>()
                .AddDataLoader<UserByIdDataLoader>()
                .AddAuthorization()
                .AddHttpRequestInterceptor(
                    (context, executor, builder, ct) =>
                    {
                         builder.SetProperty("currentUser",
                                new CurrentUser(
                                    context.User.FindFirstValue(ClaimTypes.NameIdentifier),
                                    context.User.Claims.Select(x => $"{x.Type} : {x.Value}").ToList()));

                        try {
                           

                            var role = context.User.FindFirstValue(ClaimTypes.Role);
                            if (role.Length >= 1) {
                                Console.WriteLine(role);
                            }
                        } catch (Exception e) {
                            
                            Console.WriteLine(e);
                        }


                        return new ValueTask();
                    });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseVoyager(new VoyagerOptions {
                Path = "/voyager",
                QueryPath = "/graphql"
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseCors("DefaultPolicy");
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });
        }
    }
}
