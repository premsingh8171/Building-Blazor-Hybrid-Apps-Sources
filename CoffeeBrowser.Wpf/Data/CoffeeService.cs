using CoffeeBrowser.Library.Data;
using System.Net.Http;
using System.Net.Http.Json;

namespace CoffeeBrowser.Wpf.Data;

public class CoffeeService : ICoffeeService
{
    private readonly HttpClient _httpClient = new();
    public async Task<IEnumerable<Coffee>?> LoadCoffeesAsync()
    {
        var coffees = await _httpClient.GetFromJsonAsync<IEnumerable<Coffee>>(
            "https://www.thomasclaudiushuber.com/pluralsight/coffees.json");
        //var coffees = new[]
        //{
        //        new Coffee("Cappuccino","Coffee with milk foam"),
        //        new Coffee("Doppio","Double expresso")
        //    };

        ////Simulate some server work
        //await Task.Delay(50);
        return coffees;
    }
}
