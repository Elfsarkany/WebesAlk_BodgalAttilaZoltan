using System.Text.Json;

namespace FélévesFeladat_BodgálAttilaZoltánGVFPFT
{
    public class Controllers
    {
        static public int GetHitDie(string cl)
        {
            switch (cl)
            {
                case "wizard":
                case "sorcerer":
                    return 6;
                case "artificer":
                case "bard":
                case "cleric":
                case "druid":
                case "monk":
                case "rogue":
                case "warlock":
                    return 8;
                case "fighter":
                case "paladin":
                case "ranger":
                    return 10;
                case "barbarian":
                    return 12;
                default:
                    return 0;
            }
        }

        static public int CalculateMaxHP(int dice, int lvl, bool avarage, int cons)
        {
            Random rnd = new Random();
            int x = 0;
            x += dice;
            x += cons * lvl;
            if (avarage)
            {
                for (int index = 0; index < lvl - 1; index++)
                {
                    x += dice / 2 + 1;
                }
            }
            else
            {
                for (int index = 0; index < lvl - 1; index++)
                {
                    x += rnd.Next(1, dice + 1);
                }
            }
            return x;
        }

        static public List<Character> readInFully()
        {
            List<Character> fullCharacterList = new List<Character>();
            try
            {
                string text = File.ReadAllText("characters.txt");
                string[] JSONlines = text.Split("\n");

                foreach (string line in JSONlines)
                {
                    if (line != "")
                    {
                        // Character temp = JsonSerializer.Deserialize<Character>(line);
                        jsonCharacter js= JsonSerializer.Deserialize<jsonCharacter>(line);
                        Character temp = js.ToCharacter();
                        fullCharacterList.Add(temp);
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            catch (ArgumentNullException e) { Console.WriteLine(e.Message); }
            catch (JsonException e) { Console.WriteLine(e.Message); }
            catch (NotSupportedException e) { Console.WriteLine(e.Message); }

            return fullCharacterList;
        }

        static public Character readInCharacter(CharacterForSelection ch)
        {
            List<Character> chList = readInFully();
            return chList[ch.getIndex()];
        }

        static public List<CharacterForSelection> readInForSelection()
        {
            List<CharacterForSelection> characterSelectionList = new List<CharacterForSelection>();
            try
            {
                string text = File.ReadAllText("characters.txt");
                string[] JSONlines = text.Split("\n");

                int i = 0;

                foreach (string line in JSONlines)
                {
                    try
                    {
                        if (line != "")
                        {
                            Character temp = JsonSerializer.Deserialize<Character>(line);
                            characterSelectionList.Add(new CharacterForSelection(temp.GetName(), i));
                            i++;
                        }
                    }
                    catch (ArgumentNullException e) { Console.WriteLine(e.Message); }
                    catch (JsonException e) { Console.WriteLine(e.Message); }
                    catch (NotSupportedException e) { Console.WriteLine(e.Message); }
                    catch (Exception e) { Console.WriteLine("Exception: " + e.Message); }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            return characterSelectionList;
        }

        static public bool storeCharacter(Character ch)
        {
            List<Character> chList = readInFully();
            chList.Add(ch);
            Console.WriteLine(ch);
            try
            {
                string currentDirectory = Directory.GetCurrentDirectory();
                StreamWriter sw = new StreamWriter(currentDirectory + "\\characters.txt");
                foreach (Character character in chList)
                {
                    sw.WriteLine(JsonSerializer.Serialize(character));
                }
                sw.Close();
            }
            catch (IOException e)
            {
                Console.WriteLine("Exception: " + e.Message);
                return false;
            }
            return true;
        }

        static public Character createChFromTemp(tempcharacter tempch)
        {
            Character c = new Character(tempch.getClass(),tempch.getName(),tempch.getRace(),tempch.getLevel(),tempch.getStats(),tempch.getAverage());
            return c;
        }
    }
}
