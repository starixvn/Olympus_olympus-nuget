using Newtonsoft.Json;
using TestAspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

var list = new List<int>();
for (var i = 0; i < 1000000; i++)
{
    list.Add(i);
}

var dictionary = new Dictionary<string, object>();
dictionary.Add("Name", "Jacob");
dictionary.Add("Ids", list);

var simple = new SimpleCriteriaFactoryImp();

var json = JsonConvert.SerializeObject(dictionary);
var myData = simple.CreateCriteria(typeof(GetLastUpdatedPreMatchAssignmentCriteria), dictionary);
app.Run();
