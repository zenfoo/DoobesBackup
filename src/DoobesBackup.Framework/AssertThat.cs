namespace DoobesBackup.Framework
{
    public static class AssertThat
    {
        public static void IsTrue(bool predicate, string message = "The specified value is not true!")
        {
            if (!predicate)
            {
                throw new AssertionException(message);
            }
        }

        public static void IsFalse(bool predicate, string message = "The specified value is not false!")
        {
            if (predicate)
            {
                throw new AssertionException(message);
            }
        }

        public static void IsNotNullOrEmpty(string input, string message = "The specified value is empty!")
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new AssertionException(message);
            }
        }

        public static void IsNotNull(object input, string message = "The specified value is null!")
        {
            if (input == null)
            {
                throw new AssertionException(message);
            }
        }
    }
}
