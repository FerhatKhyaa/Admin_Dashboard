using Kargo_Bitirme_Projesi.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connection = builder.Configuration["Ayarlar:BaglantiSatirim"];
builder.Services.AddDbContext<FerhatKahyaDbSetContext>(options => options.UseSqlServer(connection));

builder.Services.AddIdentity<User, Role>().AddEntityFrameworkStores<FerhatKahyaDbSetContext>().AddDefaultTokenProviders();

//Þifre kurallarýný kullanýcýlar için tanýmlýyoruz. Kurallarý geliþtirmek
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false; //þifresinde sayýlar zorunlu olsun
    options.Password.RequireNonAlphanumeric = false; //þifresinde yazý karakterleri zorunlu olsun
    options.Password.RequireLowercase = false;  //þifresinde küçük harf zorunlu olsun
    options.Password.RequiredLength = 6; //minimum 6 karakter olsun
    options.Password.RequireUppercase = false; //þifresinde büyük harfte zorunlu olsun

    options.Lockout.MaxFailedAccessAttempts = 5;// 5 kez yanlýþ girilirse uzaklaþtýr
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // yanlýþ girilirse 6 dk uzaklaþtýr
    options.Lockout.AllowedForNewUsers = true;//Yeni kullanýcýlar içinde bu uzaklaþtýrma geçerli.

    options.User.RequireUniqueEmail = true;//Mail ile kayýt olma iþlemi bir mail 1 kez kullanýlabilir
    options.SignIn.RequireConfirmedEmail = false;//Mail adresine onaylama gidecek ve onaylama iþlemi yapýlýrsa üyeliði aktif edilecek.
    options.SignIn.RequireConfirmedPhoneNumber = false; //Telefon numarasýda doðrulanmýþ ise giriþ yapýlabilecek. //False dedik gerek yok telefon onaylamaya
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/UserLogin/Login"; // Kullanýcýnýn login olacaðý  sayfa 
    options.LogoutPath = "/UserLogin/Exit"; // Kiþinin çýkýþ yapmasý için 
    options.AccessDeniedPath = "/UserLogin/NotAuthorized"; //kiþinin eriþim yetkisi yoksa yönlendirilecek sayfa.
    options.SlidingExpiration = true; //Cookie default 20 dk ise 15. dkda sisteme tekkrar girerse20 dk yenilenir
    options.Cookie = new CookieBuilder
    {
        HttpOnly = true, //client scripti vasýtasýylada eriþim saðlanabilir.
        Name = ".AdonetCore.UserLogin.Cookie",
        Path = "/",
        //SameSite=SameSiteMode.Strict// Uygulama dýþýndan eriþimi cookie isteði bulunmasýný engelliyoruz.
        SameSite = SameSiteMode.Lax// yapýlan istekle ayný domainde ise çalýþabilecek olacak þekilde gelirse yine kullanýlabilir.
    };
});
#region Session Kullanýmý

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
});
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCookiePolicy();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
