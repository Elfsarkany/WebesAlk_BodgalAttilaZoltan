using System.Text.Json;

namespace FélévesFeladat_BodgálAttilaZoltánGVFPFT;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        app.UseHttpsRedirection();

        app.MapGet("/", () => "Hello World!");

        app.MapGet("/storeCharacter", (string jsonElement) =>
        {
            Console.WriteLine("Request to store new Character...");
            Console.WriteLine(jsonElement);
            jsonCharacter jc = JsonSerializer.Deserialize<jsonCharacter>(jsonElement);
            Console.WriteLine(jc.ToString());

          /*  tempcharacter tempch = JsonSerializer.Deserialize<tempcharacter>(jsonElement);
            Controllers.storeCharacter(Controllers.createChFromTemp(tempch)); */
            
            Controllers.storeCharacter(jc.ToCharacter());
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
            string result = "";
            try
            {
                result = JsonSerializer.Serialize(Controllers.readInCharacter(JsonSerializer.Deserialize<CharacterForSelection>(jsonCharacterRequest)));
            }
            catch (JsonException e)
            {
                Console.WriteLine(e.Message);
            }
            return result;
        });

        app.Run();
    }
}