using Common.Options;
using Common.Profiles;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repository;
using Repository.Repositories.Implementation;
using Repository.Repositories.Interfaces;
using Service.DataServices;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Postgers");
var jwtOptions = builder.Configuration.GetSection(nameof(JwtOption)).Get<JwtOption>();

builder.Services.Configure<JwtOption>(builder.Configuration.GetSection(nameof(JwtOption)));
builder.Services.Configure<LocalStorageOption>(builder.Configuration.GetSection(nameof(LocalStorageOption)));

builder.Services.AddAutoMapper(typeof(UserProfile));
builder.Services.AddAutoMapper(typeof(TripProfile));
builder.Services.AddAutoMapper(typeof(PointProfile));
builder.Services.AddAutoMapper(typeof(CommentProfile));

builder.Services.AddDbContext<TripContext>(e => e.UseNpgsql(connectionString));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITripRepository, TripRepository>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();
builder.Services.AddScoped<IFriendRepository, FriendRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();

builder.Services.AddScoped<UserDataService>();
builder.Services.AddScoped<TripDataService>();
builder.Services.AddScoped<ImageDataService>();
builder.Services.AddScoped<CommentDataService>();

builder.Services.AddHangfire(config => 
    config.UsePostgreSqlStorage(connectionString));

builder.Services.AddHangfireServer();

builder.Services.AddControllers();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtOptions?.Issuer,
        ValidAudience = jwtOptions?.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(jwtOptions?.ByteKey)
    };
});
builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy => policy.WithOrigins(jwtOptions!.Audience)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowSpecificOrigin");
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseHttpsRedirection();
app.UseHangfireDashboard("/hangfire");
app.Run();