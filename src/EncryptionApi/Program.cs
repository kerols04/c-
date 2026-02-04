using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

var app = builder.Build();

app.MapGet("/", () => Results.Ok(new { message = "Encryption API is running" }));

app.MapPost("/encrypt", (CipherRequest request) =>
{
    if (string.IsNullOrWhiteSpace(request.Text))
    {
        return Results.BadRequest(new { error = "Text is required." });
    }

    var shift = request.Shift ?? 3;
    var result = CaesarCipher.Transform(request.Text, shift);

    return Results.Ok(new CipherResponse(result));
});

app.MapPost("/decrypt", (CipherRequest request) =>
{
    if (string.IsNullOrWhiteSpace(request.Text))
    {
        return Results.BadRequest(new { error = "Text is required." });
    }

    var shift = request.Shift ?? 3;
    var result = CaesarCipher.Transform(request.Text, -shift);

    return Results.Ok(new CipherResponse(result));
});

app.Run();

public record CipherRequest(string Text, int? Shift);
public record CipherResponse(string Result);

public static class CaesarCipher
{
    public static string Transform(string input, int shift)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }

        shift = shift % 26;

        var chars = input.Select(character => TransformChar(character, shift)).ToArray();
        return new string(chars);
    }

    private static char TransformChar(char character, int shift)
    {
        if (!char.IsLetter(character))
        {
            return character;
        }

        var offset = char.IsUpper(character) ? 'A' : 'a';
        var normalized = character - offset;
        var shifted = (normalized + shift + 26) % 26;

        return (char)(shifted + offset);
    }
}
