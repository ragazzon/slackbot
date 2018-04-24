using System.Collections.Generic;

namespace Slackbot.Configuration
{
    public class Settings
    {
        public static string SlackApiKey => "";
        public static string SitePass => "";
        public static string MunkSite => "";
        public static string DefaultChannel => "";
        public static string ChangesLink => "";
        public static Dictionary<string, string> ChannelList = new Dictionary<string, string>();

        public static List<Player> Players = new List<Player>()
        {

        };
    }

    #region Class

    public class Player
    {
        public string PlayerName { get; set; }
        public string SlackUserId { get; set; }
        public Race Race { get; set; }
        public string RaceAsString => Race.ToString();
        public Personality Personality { get; set; }
        public string PersonalityAsString => Personality.ToString();
    }

    public enum Race
    {
        Avian,
        Elf,
        Halfling,
        Orc,
        Human,
        Faery,
        Bocan,
        Dryad,
        Gnome,
        DarkElf,
        Dwarf
    }

    public enum Personality
    {
        Heretic,
        Mystic,
        Rogue,
        Paladin,
        Sage,
        Tactitian,
        Undead,
        WarHero,
        Warrior
    }

    #endregion
}
