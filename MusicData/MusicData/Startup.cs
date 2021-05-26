using MediaData.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MusicData.Services;
using MusicData.Services.DataReader;
using MusicData.Services.DataReader.DataHandlers;
using MusicData.Services.Factory;

namespace MusicData
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddSeq();
            });


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MediaData", Version = "v1" });
            });

            services.AddTransient<IMusicDataServiceFacade, MediaDataServiceFacade>();
            services.AddTransient<IMusicDataProvider, MusicDataProvider>();
            services.AddTransient<IMusicDataRetrieverFactory, MusicDataRetrieverFactory>();
            services.AddTransient<IBulkElasticDataInjector, BulkElasticDataInjector>();
            services.AddScoped<ArtistDataReader>()
            .AddScoped<IMediaDataProxy, ArtistDataReader>(s => s.GetService<ArtistDataReader>());
            services.AddSingleton<IDataHandler, ArtistDataHandler>();
            services.AddSingleton<IDataHandler, ArtistCollectionDataHandler>();
            services.AddSingleton<IDataHandler, CollectionDataHandler>();
            services.AddSingleton<IDataHandler, CollectionMatchDataHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MusicData v1"));
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
