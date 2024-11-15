// See https://aka.ms/new-console-template for more information

using System.Net;
using NJsonSchema.CodeGeneration.CSharp;
using NSwag;
using NSwag.CodeGeneration;
using NSwag.CodeGeneration.CSharp;

var webClient = new WebClient();         

var document = await OpenApiDocument.FromJsonAsync(webClient.DownloadString("http://localhost:5000/swagger/public-v0.1/swagger.json"));

webClient.Dispose();

var settings = new CSharpClientGeneratorSettings
{
    ClassName = "MyClass", 
    CSharpGeneratorSettings = 
    {
        Namespace = "MyNamespace"
    }
};

var generator = new NJsonSchema.CodeGeneration.CSharp.CSharpGenerator(document, new CSharpGeneratorSettings());

//var generator = new CSharpClientGenerator(document, settings);	
var code = generator.GenerateFile();
Console.WriteLine(code);