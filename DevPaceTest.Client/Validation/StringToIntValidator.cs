namespace DevPaceTestClient.Validation
{
    public static class StringToIntValidator
    {
        public static bool ValidateNumber(out int output, string number)
        {
            var tempResult = int.TryParse(number, out output);
            if (output > 0 && tempResult)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
