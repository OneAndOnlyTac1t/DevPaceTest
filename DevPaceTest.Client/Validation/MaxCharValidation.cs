namespace DevPaceTestClient.Validation
{
    public static class MaxCharValidation
    {
        public static bool ValidateMaxCharLength(string input)
        {
            if (input.Length > 50)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
