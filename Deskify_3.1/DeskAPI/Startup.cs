using DeskBusiness.Services;
using DeskData.Data;
using DeskData.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeskAPI
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
            services.AddControllers();
            string connectionStr = Configuration.GetConnectionString("sqlConnection");
            services.AddDbContext<DeskDbContext>(options => options.UseSqlServer(connectionStr));


            services.AddTransient<BookingRoomService, BookingRoomService>();
            services.AddTransient<IBookingRoomRepository, BookingRoomRepository>();

            services.AddTransient<FloorService, FloorService>();
            services.AddTransient<IFloorRepository, FloorRepository>();

            services.AddTransient<EmployeeService, EmployeeService>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();

            services.AddTransient<SeatService, SeatService>();
            services.AddTransient<ISeatRepository, SeatRepository>();

            services.AddTransient<RoomService, RoomService>();
            services.AddTransient<IRoomRepository, RoomRepository>();

            services.AddTransient<ChoicesService, ChoicesService>();
            services.AddTransient<IChoicesRepository, ChoicesRepository>();

            services.AddTransient<ReservedRoomService, ReservedRoomService>();
            services.AddTransient<IReservedRoomRepository, ReservedRoomRepository>();

            services.AddTransient<BookingSeatService, BookingSeatService>();
            services.AddTransient<IBookingSeatRepository, BookingSeatRepository>();

            services.AddTransient<LoginTableService, LoginTableService>();
            services.AddTransient<ILoginTableRepository, LoginTableRepository>();

            services.AddTransient<QRScannerService, QRScannerService>();
            services.AddTransient<IQRScannerRepository, QRScannerRepository>();





            services.AddControllers();
            services.AddSwaggerGen();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Desk API",
                    Description = "Office Desk Booking System API",
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI(options =>

            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Desk API"));

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
