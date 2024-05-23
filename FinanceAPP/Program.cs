using FinanceService.Contracts.FinancialItam;
using FinanceService.Contracts.Partner;
using FinanceService.Contracts.PartnerFinancialItem;
using FinanceService.Contracts.TransferExcel2List;
using FinanceService.Contracts.UploadFile;
using FinanceService.Mappings;
using FinanceService.Services.FinanceItem;
using FinanceService.Services.Partner;
using FinanceService.Services.PartnerFinancialItems;
using FinanceService.Services.TransferExcel2List;
using FinanceService.Services.UploadFile;
using Microsoft.Extensions.DependencyInjection;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
//  AutoMapper
builder.Services.AddAutoMapper(typeof(Maps));

//Service registrations
var srv = builder.Services.AddTransient<IFinancialItem, FinancialItemSrv>();
builder.Services.AddTransient<IPartner, PartnerSrv>();
builder.Services.AddTransient<IUploadFile, UploadFileSrv>();
builder.Services.AddTransient(typeof(ITransferExcel2List<>), typeof(TransferExcel2ListSrv<>));


builder.Services.AddTransient<IPartnerFinancialItems>(sp =>
{   
    return new PartnerFinancialItemsSrv(sp.GetService<PartnerSrv>()?? new PartnerSrv(), sp.GetService<FinancialItemSrv>() ?? new FinancialItemSrv());
});


DateTime dateTimeFile = DateTime.Now;
//+ dateTimeFile.Hour.ToString() + dateTimeFile.Minute.ToString() + dateTimeFile.Second.ToString()
string fileName = dateTimeFile.Year.ToString() + dateTimeFile.Month.ToString() + dateTimeFile.Day.ToString() ;

Log.Logger = new LoggerConfiguration()
   .MinimumLevel.Debug()
   .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
   .Enrich.FromLogContext()
   //.WriteTo.Console()
   .WriteTo.File("Logs/"+fileName + ".txt")
   .CreateLogger();



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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Partners}/{action=Index}/{id?}");

app.Run();
