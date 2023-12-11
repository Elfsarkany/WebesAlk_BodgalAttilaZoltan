using System.Text.Json;

namespace FélévesFeladat_BodgálAttilaZoltánGVFPFT;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        app.MapGet("/", () => "Hello World!");

        app.MapGet("/storeCharacter", (string jsonElement) =>
        {

            Console.WriteLine("Request to store new Character...");
            tempcharacter tempch = JsonSerializer.Deserialize<tempcharacter>(jsonElement);
            Controllers.storeCharacter(Controllers.createChFromTemp(tempch));
            return "Done";
        });

        app.MapGet("/refreshCharacters", () =>
        {
            Console.WriteLine("Request to synch Characters...");
            Console.WriteLine(JsonSerializer.Serialize(Controllers.readInForSelection()));
            return JsonSerializer.Serialize(Controllers.readInForSelection());
        });

        app.MapGet("/getCharacter", (string jsonCharacterRequest) =>
        {
            Console.WriteLine("Request for Character...");
            return JsonSerializer.Serialize(Controllers.readInCharacter(JsonSerializer.Deserialize<CharacterForSelection>(jsonCharacterRequest)));
        });

        app.Run();
    }
}