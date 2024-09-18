using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VoteForCauseFinal.Data;
using VoteForCauseFinal.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


//injecting the db context in the program
builder.Services.AddDbContext<VoteDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("VoteDbConnectionString")));

builder.Services.AddDbContext<AuthDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("VoteAuthDbConnectionString")));

//telling it to use the roles created
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AuthDbContext>();

//configuring the identity options
builder.Services.Configure<IdentityOptions>(options =>
{
    //default settings
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});





//injecting repos to be used
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<ICausePostRepository, CausePostRepository>();
builder.Services.AddScoped<IImageRepository, CloudinaryImageRepository>();
//for signing the post
builder.Services.AddScoped<ICausePostSignRepository, CausePostSignRepository>();
//for comments
builder.Services.AddScoped<ICausePostCommentRepository, CausePostCommentRepository>();  
//for managing users
builder.Services.AddScoped<IUserRepository, UserRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//authenticate first
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
