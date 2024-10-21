namespace Web.Utils
{
    public static class StringFormatter
    {
        public static string GenerateUrlSlug(string _string)
        {
            return _string.ToLower().Replace(" ", "-").Replace("æ", "ae").Replace("ø", "o").Replace("å", "aa");
        }
    }
}
