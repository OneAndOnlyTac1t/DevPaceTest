using System.Text.RegularExpressions;

namespace DevPaceTestClient.Validation
{
    public static class EmailValidator
    {
        public static bool ValidateEmail(string email)
        {
            Regex regex = new Regex(@"[a-z1-9]+@[a-z1-9]+\.[a-z1-9]+");
            MatchCollection matchCollection = regex.Matches(email);
            if (matchCollection.Count == 0)
            {
                return false;
            }

            return true;
        }
    }
}
