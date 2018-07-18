using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Empower.Network.Services;
using Empower.Settings.Services;
using Ninject;
using System.Threading;
using Ninject.Activation;
using Ninject.Infrastructure.Disposal;
using Microsoft.AspNetCore.Http;
using Empower.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Microsoft.AspNetCore.Authentication.Cookies;
using Empower.Services;
using nh = NHibernate;
using Empower.NHibernate.Interfaces;
using Empower.NHibernate;
using Empower.NHibernate.Services;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Newtonsoft.Json;

namespace Empower.Mvc
{
    public class Startup
    {
        private readonly AsyncLocal<Scope> scopeProvider = new AsyncLocal<Scope>();
        private IKernel Kernel { get; set; }

        private object Resolve(Type type) => Kernel.Get(type);
        private object RequestScope(IContext context) => scopeProvider.Value;

        private sealed class Scope : DisposableObject { }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
          
            services.AddRequestScopingMiddleware(() => scopeProvider.Value = new Scope());
            services.AddCustomControllerActivation(Resolve);
            services.AddCustomViewComponentActivation(Resolve);
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Empower API", Version = "v1" });
            });
            services.AddMvc();
           
            var corsBuilder = new CorsPolicyBuilder();
            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyMethod();
            //corsBuilder.AllowAnyOrigin(); // For anyone access.
            corsBuilder.WithOrigins("http://localhost:4200"); // for a specific url. Don't add a forward slash on the end!
            //corsBuilder.AllowCredentials();

            services.AddCors(options =>
            {
                options.AddPolicy("SiteCorsPolicy", corsBuilder.Build());
            });

            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("SiteCorsPolicy"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            this.Kernel = this.RegisterApplicationComponents(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Empower API V1");
            });

            app.UseAuthentication();

            // ********************
            // Setup CORS
            // ********************

            app.UseCors("SiteCorsPolicy");
        }

        private IKernel RegisterApplicationComponents(IApplicationBuilder app)
        {
            // IKernelConfiguration config = new KernelConfiguration();
            var kernel = new StandardKernel();

            // Register application services
            foreach (var ctrlType in app.GetControllerTypes())
            {
                kernel.Bind(ctrlType).ToSelf().InScope(RequestScope);
            }

           
            kernel.Bind<IConfiguration>().ToMethod(m => this.Configuration)
                .InSingletonScope();
            kernel.Bind<ISettingsService>().To<SettingsService>()
                .InSingletonScope();
            kernel.Bind<IEmailSettingsService>().To<EmailSettingsService>();
            kernel.Bind<IEmailService>().To<EmailService>().InSingletonScope();


            kernel.Bind<nh.ISession>().ToMethod(m => new Empower.NHibernate.Setup.NhHelper(
                 new SettingsService(Configuration)
                ).Session)
                .InScope(RequestScope);
            kernel.Bind(typeof(IRepository<>))
                .To(typeof(NHibernateRepository<>))
                .InScope(RequestScope);
            kernel.Bind<IFilmService>()
                .To<FilmService>()
                .InScope(RequestScope);
            kernel.Bind<ICategoryService>()
                .To<CategoryService>()
                .InScope(RequestScope);
            kernel.Bind<IActorService>()
               .To<ActorService>()
               .InScope(RequestScope);
            // Cross-wire required framework services
            kernel.BindToMethod(app.GetRequestService<IViewBufferScope>);

            return kernel;
        }
    }
}