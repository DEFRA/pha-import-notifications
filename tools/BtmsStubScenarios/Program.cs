// BTMS scenarios are taken from redacted production data currently
// This console app requests the scenarios we are using in order to track schema changes
// Expected args are BTMS scheme/host and BTMS environment basic auth password

using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

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
    // The following CHED shows the relationship JSON structure
    // we need to follow so if it changes, the logic for fake
    // movements also needs to change below
    "CHEDA.GB.2024.5149729",
};

foreach (var ched in cheds)
{
    var json = await GetDocument($"api/import-notifications/{ched}", client);

    // Preserve river tees BCP so responses remain valid
    json = json.Replace("GBAPHA1A", "GBTEEP1");
    json = json.Replace("GBLGW4", "GBTEEP1");
    json = json.Replace("GBLGP1", "GBTEEP1");
    json = json.Replace("GBHCH4PP", "GBTEEP1");

    // Fake movement relationships for CHED
    if (ched == "CHEDA.GB.2024.5129502")
    {
        var document = JsonNode.Parse(json);
        if (document is null)
            throw new InvalidOperationException("Failed to parse JSON");
        var dataDocument = document["data"]?.AsObject();
        if (dataDocument is null)
            throw new InvalidOperationException("Failed to get data as object");
        if (!dataDocument.ContainsKey("relationships"))
            throw new InvalidOperationException("Failed to find relationships in data");

        dataDocument["relationships"] = new JsonObject
        {
            ["movements"] = new JsonObject
            {
                ["links"] = new JsonObject
                {
                    ["self"] = "/api/import-notifications/CHEDA.GB.2024.5129502",
                    ["related"] = "/api/import-notifications/CHEDA.GB.2024.5129502/movements",
                },
                ["data"] = new JsonArray
                {
                    new JsonObject
                    {
                        ["type"] = "movements",
                        ["id"] = "24GBC1IQDD278IZAR8",
                        ["meta"] = new JsonObject
                        {
                            ["matched"] = true,
                            ["sourceItem"] = 1,
                            ["destinationItem"] = 1,
                            ["matchingLevel"] = 1,
                            ["self"] = "/api/movements/24GBC1IQDD278IZAR8/import-notifications",
                        },
                    },
                    new JsonObject
                    {
                        ["type"] = "movements",
                        ["id"] = "24GBCND8RONCFGAAR3",
                        ["meta"] = new JsonObject
                        {
                            ["matched"] = true,
                            ["sourceItem"] = 1,
                            ["destinationItem"] = 1,
                            ["matchingLevel"] = 1,
                            ["self"] = "/api/movements/24GBCND8RONCFGAAR3/import-notifications",
                        },
                    },
                },
            },
        };

#pragma warning disable CA1869
        json = JsonSerializer.Serialize(document, new JsonSerializerOptions { WriteIndented = true });
#pragma warning restore CA1869
    }

    await File.WriteAllTextAsync($"{outputPath}btms-import-notification-single-{ched}.json", json);
}

var movements = new[] { "24GBE0XBAS7Z0J5AR0", "24GBC1IQDD278IZAR8", "24GBCND8RONCFGAAR3" };

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
