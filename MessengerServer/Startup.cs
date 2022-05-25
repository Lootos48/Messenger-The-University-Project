using MessengerServer.BLL;
using MessengerServer.DAL;
using MessengerServer.DAL.Repositories;
using MessengerServer.Util;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MessengerServer
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("MSLocalDBConnection");

            services.AddDbContext<MessengerDBContext>(options =>
                options.UseSqlServer(connection)
            );
            services.AddScoped<DbContext, MessengerDBContext>();

            services.AddTransient<ChatRepository>();
            services.AddTransient<MessageRepository>();
            services.AddTransient<UserRepository>();
            services.AddTransient<UserChatsRepository>();
            services.AddTransient<UserPictureRepository>();
            services.AddTransient<ImageRepository>();

            services.AddTransient<ChatsService>();
            services.AddTransient<UserService>();
            services.AddTransient<UsersChatsService>();
            services.AddTransient<MessagesService>();


            services.AddAutoMapper(typeof(MessengerMappingProfile));

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder
                .WithOrigins("http://localhost:3000")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            });

            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            string baseDir = env.ContentRootPath;

            AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(baseDir, "App_Data"));

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
