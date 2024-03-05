using AssesmentEpsilon;
using IdentityModel.Client;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddHttpClient("WebAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
builder.Services.AddSingleton(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("WebAPI"));
builder.Services.AddSingleton<Cookie>();

var app = builder.Build();
var client = app.Services.GetRequiredService<HttpClient>();
var disco = await client.GetDiscoveryDocumentAsync("https://localhost:6001");
if (disco.IsError)
{
    Console.WriteLine(disco.Error);
    return;
}
// request token
var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
{
    Address = disco.TokenEndpoint,

    ClientId = "client",
    ClientSecret = "secret",
    Scope = "api1"
});

if (tokenResponse.IsError)
{
    Console.WriteLine(tokenResponse.Error);
    return;
}

Console.WriteLine(tokenResponse.Json);

// call api
client.SetBearerToken(tokenResponse.AccessToken);

await app.RunAsync();
