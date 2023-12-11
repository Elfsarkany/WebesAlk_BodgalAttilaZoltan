using System.Diagnostics;
using System.Runtime.Intrinsics.X86;
using System.Xml.Linq;
using System.Text.Json;
using System.Dynamic;

namespace FélévesFeladat_BodgálAttilaZoltánGVFPFT
{

    
    public class Character
    {
        private int hitdie { get; set; }

        private int[] stats = { 0, 0, 0, 0, 0, 0 };
        private string chClass { get; set; }
        private string name { get; set; }
        private int level { get; set; }
        private string race { get; set; }
        private int maxhp { get; set; }
        private int currenthp { get; set; }
        public Character(string cl, string chName, string race, int lvl, int[] stats, bool avg)
        {
            this.chClass = cl;
            this.hitdie = Controllers.GetHitDie(cl);
            this.name = chName;
            this.race = race;
            this.level = lvl;
            for (int index = 0; index < stats.Length; index++)
            {
                this.stats[index] = stats[index];
            }
            this.maxhp = Controllers.CalculateMaxHP(this.hitdie, lvl, avg,  (stats[2] - 11) / 2 );
            this.currenthp = this.maxhp;
        }

        public string GetName()
        {
            return name;
        }

        public void changeCurrentHpBy(int value)
        {
            this.currenthp += value;
        }
        public int GetStat(int which)
        {
            return stats[which];
        }
        public string getDiscription()
        {
            return  hitdie+ ", " + race + ", " + chClass + ", " + level + "-th lvl, " + currenthp + "/" + maxhp +
            " hp, STR: " + this.GetStat(0) + " DEX: " + this.GetStat(1) + " CON: " + this.GetStat(2) + " INT: " + this.GetStat(3) + " WIS: " + this.GetStat(4) + " CHA: " + this.GetStat(5);
        }
    }

    public class tempcharacter
    {
        private string name;
        private string race;
        private string chClass;
        private int level;
        private int[] stats = { 0, 0, 0, 0, 0, 0 };
        private bool average;

        public string getName()
        {
            return name;
        }
        public string getRace()
        {
            return race;
        }
        public string getClass()
        {
            return chClass;
        }
        public int getLevel()
        {
            return level;
        }
        public int[] getStats()
        {
            return stats;
        }
        public bool getAverage()
        {
            return average;
        }
    }

    public class CharacterForSelection
    {
        private string name;
        private int index;

        public CharacterForSelection(string name, int index)
        {
            this.name = name;
            this.index = index;
        }

        public int getIndex()
        {
            return index;
        }
    }
}
