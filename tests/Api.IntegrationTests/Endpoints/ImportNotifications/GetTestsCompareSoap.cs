using System.Reflection;
using System.Xml.Linq;
using Defra.PhaImportNotifications.BtmsStub;
using Defra.PhaImportNotifications.Testing;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using WireMock.Server;
using Xunit.Abstractions;
using ChedReferenceNumbers = Defra.PhaImportNotifications.Testing.ChedReferenceNumbers;

namespace Defra.PhaImportNotifications.Api.IntegrationTests.Endpoints.ImportNotifications;

public class GetTestsCompareSoap : EndpointTestBase, IClassFixture<WireMockContext>
{
    private readonly ITestOutputHelper _outputHelper;

    public GetTestsCompareSoap(
        ApiWebApplicationFactory factory,
        ITestOutputHelper outputHelper,
        WireMockContext context
    )
        : base(factory, outputHelper)
    {
        _outputHelper = outputHelper;
        WireMock = context.Server;
        WireMock.Reset();
        HttpClient = context.HttpClient;
    }

    private WireMockServer WireMock { get; }
    private HttpClient HttpClient { get; }

    [Fact]
    public async Task Get_ChedAFromSoap()
    {
        var response = await StubAndCall(ChedReferenceNumbers.ChedAFromSoap);

        await VerifyJson(response).UseStrictJson().DontScrubGuids().DontScrubDateTimes();
    }

    [Fact]
    public async Task Get_ChedDFromSoap()
    {
        var response = await StubAndCall(ChedReferenceNumbers.ChedDFromSoap);

        await VerifyJson(response).UseStrictJson().DontScrubGuids().DontScrubDateTimes();
    }

    [Fact]
    public async Task Get_ChedPFromSoap()
    {
        var response = await StubAndCall(ChedReferenceNumbers.ChedPFromSoap);

        await VerifyJson(response).UseStrictJson().DontScrubGuids().DontScrubDateTimes();
    }

    [Fact]
    public async Task Get_ChedPPFromSoap()
    {
        var response = await StubAndCall(ChedReferenceNumbers.ChedPPFromSoap);

        await VerifyJson(response).UseStrictJson().DontScrubGuids().DontScrubDateTimes();
    }

    [Fact]
    public void Compare_ChedAXmlWithPhaApiResponse()
    {
        var type = typeof(GetTestsCompareSoap);

        Dictionary<string, string> xmlKeyValues;

        using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"{type.Namespace}.cheda.xml"))
        {
            if (stream is null)
                throw new InvalidOperationException("Unable to find embedded resource cheda.xml");

            using var reader = new StreamReader(stream);
            var document = XDocument.Load(reader);

            document.Should().NotBeNull();

            xmlKeyValues = FlattenElements(document.Root, "", new Dictionary<string, string>());
        }

        using (
            var stream = Assembly
                .GetExecutingAssembly()
                .GetManifestResourceStream($"{type.Namespace}.GetTestsCompareSoap.Get_ChedAFromSoap.verified.json")
        )
        {
            if (stream is null)
                throw new InvalidOperationException(
                    "Unable to find embedded resource GetTestsCompareSoap.Get_ChedAFromSoap.verified.json"
                );

            using var reader = new StreamReader(stream);
            var json = reader.ReadToEnd();

            var jsonKeyValues = RecurseJson(JObject.Parse(json), "", new Dictionary<string, string>());

            foreach (var kvp in xmlKeyValues)
            {
                _outputHelper.WriteLine($"{kvp.Key} = {kvp.Value}");
            }

            foreach (var kvp in jsonKeyValues)
            {
                _outputHelper.WriteLine($"{kvp.Key} = {kvp.Value}");
            }

            /*
            foreach (var kvp in xmlKeyValues)
            {
                var positions = FindAllOccurrences(json, kvp.Value);

                switch (positions.Count)
                {
                    case 0:
                        _outputHelper.WriteLine($"Not found - {kvp.Key} = {kvp.Value}");
                        break;
                    case 1:
                        continue;
                    case > 1:
                        _outputHelper.WriteLine($"Multiple found - {kvp.Key} = {kvp.Value}");
                        break;
                }
            }
            */
        }
    }

    private static Dictionary<string, string> FlattenElements(
        XElement? element,
        string parentPath,
        Dictionary<string, string> result
    )
    {
        if (element is null)
            return result;

        var key = string.IsNullOrEmpty(parentPath) ? element.Name.LocalName : $"{parentPath}.{element.Name.LocalName}";

        if (!string.IsNullOrWhiteSpace(element.Value) && !element.HasElements)
        {
            var count = 1;
            var key1 = key;
            while (result.ContainsKey(key))
            {
                key = $"{key1}.{count++}";
            }

            result[key] = element.Value.Trim();
        }

        return element.Elements().Aggregate(result, (current, child) => FlattenElements(child, key, current));
    }

    private static Dictionary<string, string> RecurseJson(
        JToken node,
        string parentPath,
        Dictionary<string, string> result
    )
    {
        if (node is JObject @object)
        {
            foreach (var property in @object.Properties())
            {
                var currentPath = string.IsNullOrEmpty(parentPath) ? property.Name : $"{parentPath}.{property.Name}";

                result = RecurseJson(property.Value, currentPath, result);
            }
        }
        else if (node is JArray array)
        {
            for (var i = 0; i < array.Count; i++)
            {
                var currentPath = $"{parentPath}[{i}]";

                result = RecurseJson(array[i], currentPath, result);
            }
        }
        else
        {
            if (node.Type != JTokenType.Null)
            {
                var count = 1;
                var key = parentPath;
                while (result.ContainsKey(parentPath))
                {
                    parentPath = $"{key}.{count++}";
                }

                result[parentPath] = node.ToString();
            }
        }

        return result;
    }

    private static List<int> FindAllOccurrences(string input, string valueToFind)
    {
        var positions = new List<int>();

        if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(valueToFind))
            return positions;

        var index = input.IndexOf(valueToFind, StringComparison.OrdinalIgnoreCase);
        while (index != -1)
        {
            positions.Add(index);
            index = input.IndexOf(valueToFind, index + valueToFind.Length, StringComparison.OrdinalIgnoreCase);
        }

        return positions;
    }

    private async Task<string> StubAndCall(string chedReferenceNumber)
    {
        var client = CreateClient();

        WireMock.StubSingleImportNotification(chedReferenceNumber: chedReferenceNumber);

        return await client.GetStringAsync(Testing.Endpoints.ImportNotifications.Get(chedReferenceNumber));
    }

    protected override void ConfigureHostConfiguration(IConfigurationBuilder config)
    {
        config.AddInMemoryCollection(
            new Dictionary<string, string?> { { "Btms:BaseUrl", HttpClient.BaseAddress?.ToString() } }
        );

        base.ConfigureHostConfiguration(config);
    }
}
