using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
using System.Text.Json.Serialization;
using TaleLearnCode.rQuote;

JsonSerializerOptions jsonSerializerOptions = new()
{
	PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
	DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
	DictionaryKeyPolicy = JsonNamingPolicy.CamelCase
};

var host = new HostBuilder()
	.ConfigureFunctionsWorkerDefaults()
	.ConfigureServices(s =>
	{
		s.AddDbContext<SqlContext>();
		s.AddSingleton((s) => { return jsonSerializerOptions; });
	})
	.Build();

host.Run();
