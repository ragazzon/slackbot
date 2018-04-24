namespace Slackbot.Configuration
{
    public static class Utillities
    {
        public static string Clean(this string input)
        {
            return input
                .Replace("\u000310", "")
                .Replace("\u000312", "")
                .Replace("\u000302", "")
                .Replace("\u000303", "")
                .Replace("\u000304", "")
                .Replace("\u000305", "")
                .Replace("\u000306", "")
                .Replace("\u000226", "")
                .Replace("\u000314", "")
                .Replace("\u0002", "")
                .Replace("\u0003", "")
                .Replace("\t", "");
        }
    }
}
