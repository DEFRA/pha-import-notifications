// BTMS scenarios are taken from redacted production data currently
// This console app requests the scenarios we are using in order to track schema changes
// Expected args are BTMS scheme/host and BTMS environment basic auth password

using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

// ReSharper disable InconsistentNaming

const string solutionPath = "../../../../../";
const string outputPath = $"{solutionPath}tests/BtmsStub/Scenarios/";
const string username = "PhaService";
var client = new HttpClient
{
    BaseAddress = new Uri(args[0]),
    DefaultRequestHeaders =
    {
        Authorization = new AuthenticationHeaderValue(
            "Basic",
            Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{args[1]}"))
        ),
    },
};

var cheds = new[]
{
    "CHEDA.GB.2024.4792831",
    "CHEDA.GB.2024.5129502",
    "CHEDD.GB.2024.5019877",
    "CHEDP.GB.2024.4144842",
    "CHEDPP.GB.2024.3726460",
};

foreach (var ched in cheds)
{
    var json = await GetDocument($"api/import-notifications/{ched}", client);

    // Preserve river tees BCP so responses remain valid
    json = json.Replace("GBAPHA1A", "GBTEEP1");
    json = json.Replace("GBLGW4", "GBTEEP1");
    json = json.Replace("GBLGP1", "GBTEEP1");
    json = json.Replace("GBHCH4PP", "GBTEEP1");

    await File.WriteAllTextAsync($"{outputPath}btms-import-notification-single-{ched}.json", json);
}

var movements = new[] { "24GBC1IQDD278IZAR8", "24GBCND8RONCFGAAR3" };

foreach (var movement in movements)
{
    var json = await GetDocument($"api/movements/{movement}", client);
    await File.WriteAllTextAsync($"{outputPath}btms-movement-single-{movement}.json", json);
}

return;

async Task<string> GetDocument(string path, HttpClient httpClient)
{
    Console.WriteLine($"Requesting {path}");
    var response = await httpClient.GetAsync(path);
    response.EnsureSuccessStatusCode();

    var json1 = await response.Content.ReadAsStringAsync();
    var document = JsonDocument.Parse(json1);
#pragma warning disable CA1869
    json1 = JsonSerializer.Serialize(document, new JsonSerializerOptions { WriteIndented = true });
#pragma warning restore CA1869
    return json1;
}
