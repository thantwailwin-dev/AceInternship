// See https://aka.ms/new-console-template for more information
/*Console.WriteLine("Hello, World!");*/

/*using InternshipDotNetCore.ConsoleApp.AdoDotNetExamples;
using InternshipDotNetCore.ConsoleApp.EFCoreExamples;*/

/*AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
adoDotNetExample.Run();*/

/*DapperExample dapper = new DapperExample();
dapper.Run();*/

/*EFCoreExample eFCoreExample = new EFCoreExample();
eFCoreExample.Run();*/

/*AppDbContext dbContext = new AppDbContext();
int rowCount = dbContext.Blogs.Count();
int pageSize = 10;
int pageCount = rowCount / pageSize;
Console.WriteLine($"Page Count : {pageCount}");

if(rowCount % pageSize > 0)
{
	pageCount++;
}
Console.WriteLine($"New Page Count : {pageCount}");*/

using Serilog;

Log.Logger = new LoggerConfiguration()
           .MinimumLevel.Debug()
           .WriteTo.Console()
           .WriteTo.File("logs/InternshipDotNetCore.ConsoleApp.log", rollingInterval: RollingInterval.Day)
           .CreateLogger();

Log.Information("Hello, world!");

int a = 10, b = 0;
try
{
    Log.Debug("Dividing {A} by {B}", a, b);
    Console.WriteLine(a / b);
}
catch (Exception ex)
{
    Log.Error(ex, "Something went wrong");
}
finally
{
    await Log.CloseAndFlushAsync();
}

Console.ReadKey();