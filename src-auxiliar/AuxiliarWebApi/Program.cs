var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => @"Hola amigo.

Esta es una minimal api auxiliar para servir de fuente de datos para otros proyectos.

End-points:
/AuxiliarApi/rates.json
/AuxiliarApi/transactions.json

Creada por Fran Diaz.");

app.MapGet("/AuxiliarApi/rates.json", () => @"[
 { ""From"": ""EUR"", ""To"": ""USD"", ""Rate"": 1.359 },
 { ""From"": ""USD"", ""To"": ""EUR"", ""Rate"": 0.736 },
 { ""From"": ""CAD"", ""To"": ""EUR"", ""Rate"": 0.732 },
 { ""From"": ""EUR"", ""To"": ""CAD"", ""Rate"": 1.366 },
 { ""From"": ""CAD"", ""To"": ""USD"", ""Rate"": 0.995 },
 { ""From"": ""USD"", ""To"": ""CAD"", ""Rate"": 1.005 }
]");

app.MapGet("/AuxiliarApi/transactions.json", () => @"[
 { ""Sku"": ""T2006"", ""Amount"": 10.05, ""Currency"": ""USD"" },
 { ""Sku"": ""M2007"", ""Amount"": 34.57, ""Currency"": ""CAD"" },
 { ""Sku"": ""R2008"", ""Amount"": 17.95, ""Currency"": ""USD"" },
 { ""Sku"": ""T2006"", ""Amount"": 7.63,  ""Currency"": ""EUR"" },
 { ""Sku"": ""B2009"", ""Amount"": 21.23, ""Currency"": ""USD"" }
]");

app.Run();
