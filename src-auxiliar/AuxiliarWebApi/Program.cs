var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => @"Hola amigo.

Esta es una minimal api auxiliar para servir de fuente de datos para otros proyectos.

End-points:
/AuxiliarApi/rates.json
/AuxiliarApi/transactions.json

Creada por Fran Diaz.");

app.MapGet("/AuxiliarApi/rates.json", () => @"[
 { ""from"": ""EUR"", ""to"": ""USD"", ""rate"": ""1.359"" },
 { ""from"": ""USD"", ""to"": ""EUR"", ""rate"": ""0.736"" },
 { ""from"": ""CAD"", ""to"": ""EUR"", ""rate"": ""0.732"" },
 { ""from"": ""EUR"", ""to"": ""CAD"", ""rate"": ""1.366"" },
 { ""from"": ""CAD"", ""to"": ""USD"", ""rate"": ""0.995"" },
 { ""from"": ""USD"", ""to"": ""CAD"", ""rate"": ""1.005"" }
]");

app.MapGet("/AuxiliarApi/transactions.json", () => @"[
 { ""Sku"": ""T2006"", ""Amount"": 10.05, ""Currency"": ""USD"" },
 { ""Sku"": ""M2007"", ""Amount"": 34.57, ""Currency"": ""CAD"" },
 { ""Sku"": ""R2008"", ""Amount"": 17.95, ""Currency"": ""USD"" },
 { ""Sku"": ""T2006"", ""Amount"": 7.63,  ""Currency"": ""EUR"" },
 { ""Sku"": ""B2009"", ""Amount"": 21.23, ""Currency"": ""USD"" }
]");

app.Run();
