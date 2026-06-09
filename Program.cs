using CalculadoraAlgebra;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseStaticFiles();

app.MapPost("/api/calculo", (Dictionary<string, object> dados) =>
{
    try
    {
        string tipo = dados["tipo"].ToString();
        
        return tipo switch
        {
            "soma" => Results.Ok(new { resultado = CalculadoraEngine.Soma(double.Parse(dados["n1"].ToString()), double.Parse(dados["n2"].ToString())) }),
            "subtracao" => Results.Ok(new { resultado = CalculadoraEngine.Subtracao(double.Parse(dados["n1"].ToString()), double.Parse(dados["n2"].ToString())) }),
            "multiplicacao" => Results.Ok(new { resultado = CalculadoraEngine.Multiplicacao(double.Parse(dados["n1"].ToString()), double.Parse(dados["n2"].ToString())) }),
            "divisao" => Results.Ok(new { resultado = CalculadoraEngine.Divisao(double.Parse(dados["n1"].ToString()), double.Parse(dados["n2"].ToString())) }),
            "potencia" => Results.Ok(new { resultado = CalculadoraEngine.Potencia(double.Parse(dados["n1"].ToString()), double.Parse(dados["n2"].ToString())) }),
            "raiz" => Results.Ok(new { resultado = CalculadoraEngine.RaizQuadrada(double.Parse(dados["n"].ToString())) }),
            "modulo" => Results.Ok(new { resultado = CalculadoraEngine.Modulo(double.Parse(dados["n1"].ToString()), double.Parse(dados["n2"].ToString())) }),
            "pi" => Results.Ok(new { resultado = CalculadoraEngine.PI() }),
            "seno" => Results.Ok(new { resultado = CalculadoraEngine.Seno(double.Parse(dados["valor"].ToString())) }),
            "cosseno" => Results.Ok(new { resultado = CalculadoraEngine.Cosseno(double.Parse(dados["valor"].ToString())) }),
            "tangente" => Results.Ok(new { resultado = CalculadoraEngine.Tangente(double.Parse(dados["valor"].ToString())) }),
            "arcoseno" => Results.Ok(new { resultado = CalculadoraEngine.ArcoSeno(double.Parse(dados["valor"].ToString())) }),
            "arcocosseno" => Results.Ok(new { resultado = CalculadoraEngine.ArcoCosseno(double.Parse(dados["valor"].ToString())) }),
            "arcotangente" => Results.Ok(new { resultado = CalculadoraEngine.ArcoTangente(double.Parse(dados["valor"].ToString())) }),
            "sinh" => Results.Ok(new { resultado = CalculadoraEngine.Sinh(double.Parse(dados["valor"].ToString())) }),
            "cosh" => Results.Ok(new { resultado = CalculadoraEngine.Cosh(double.Parse(dados["valor"].ToString())) }),
            "tanh" => Results.Ok(new { resultado = CalculadoraEngine.Tanh(double.Parse(dados["valor"].ToString())) }),
            "log10" => Results.Ok(new { resultado = CalculadoraEngine.LogBase10(double.Parse(dados["valor"].ToString())) }),
            "ln" => Results.Ok(new { resultado = CalculadoraEngine.LogNatural(double.Parse(dados["valor"].ToString())) }),
            "exp" => Results.Ok(new { resultado = CalculadoraEngine.ExpX(double.Parse(dados["valor"].ToString())) }),
            "fatorial" => Results.Ok(new { resultado = CalculadoraEngine.Fatorial(int.Parse(dados["n"].ToString())) }),
            "permutacao" => Results.Ok(new { resultado = CalculadoraEngine.Permutacao(int.Parse(dados["n"].ToString()), int.Parse(dados["r"].ToString())) }),
            "combinacao" => Results.Ok(new { resultado = CalculadoraEngine.Combinacao(int.Parse(dados["n"].ToString()), int.Parse(dados["r"].ToString())) }),
            "retpolar" => ResultadoRetPolar(dados),
            "polret" => ResultadoPolRet(dados),
            "retapor2pontos" => Results.Ok(CalculadoraEngine.RetaPorDoisPontos(double.Parse(dados["x1"].ToString()), double.Parse(dados["y1"].ToString()), double.Parse(dados["x2"].ToString()), double.Parse(dados["y2"].ToString()))),
            "retaporponto" => Results.Ok(CalculadoraEngine.RetaPorPontoECoeficiente(double.Parse(dados["x1"].ToString()), double.Parse(dados["y1"].ToString()), double.Parse(dados["m"].ToString()))),
            "circpocentro" => Results.Ok(CalculadoraEngine.CircunferenciaPorCentroRaio(double.Parse(dados["xc"].ToString()), double.Parse(dados["yc"].ToString()), double.Parse(dados["r"].ToString()))),
            "circequacao" => Results.Ok(CalculadoraEngine.AnalisarEquacaoGeral(double.Parse(dados["A"].ToString()), double.Parse(dados["B"].ToString()), double.Parse(dados["C"].ToString()))),
            _ => Results.BadRequest(new { erro = "Tipo inválido" })
        };
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { erro = ex.Message });
    }
});

IResult ResultadoRetPolar(Dictionary<string, object> dados)
{
    var result = CalculadoraEngine.RetangularParaPolar(double.Parse(dados["x"].ToString()), double.Parse(dados["y"].ToString()));
    return Results.Ok(new { r = result.Item1, theta = result.Item2 });
}

IResult ResultadoPolRet(Dictionary<string, object> dados)
{
    var result = CalculadoraEngine.PolarParaRetangular(double.Parse(dados["r"].ToString()), double.Parse(dados["theta"].ToString()));
    return Results.Ok(new { x = result.Item1, y = result.Item2 });
}

app.MapGet("/", async (HttpContext ctx) =>
{
    var indexPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "index.html");
    if (System.IO.File.Exists(indexPath))
    {
        ctx.Response.ContentType = "text/html";
        await ctx.Response.SendFileAsync(indexPath);
    }
    else
    {
        ctx.Response.StatusCode = 404;
        await ctx.Response.WriteAsJsonAsync(new { erro = $"Arquivo não encontrado: {indexPath}" });
    }
});

app.Run();
