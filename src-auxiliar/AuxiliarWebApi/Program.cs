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
 { ""from"": ""CAD"", ""to"": ""EUR"", ""rate"": ""0.732"" },
 { ""from"": ""USD"", ""to"": ""EUR"", ""rate"": ""0.736"" },
 { ""from"": ""EUR"", ""to"": ""CAD"", ""rate"": ""1.366"" }
]");

app.MapGet("/AuxiliarApi/transactions.json", () => @"[
 { ""sku"": ""T2006"", ""amount"": ""10.00"", ""currency"": ""USD"" },
 { ""sku"": ""M2007"", ""amount"": ""34.57"", ""currency"": ""CAD"" },
 { ""sku"": ""R2008"", ""amount"": ""17.95"", ""currency"": ""USD"" },
 { ""sku"": ""T2006"", ""amount"": ""7.63"", ""currency"": ""EUR"" },
 { ""sku"": ""B2009"", ""amount"": ""21.23"", ""currency"": ""USD"" }
]");

app.Run();
