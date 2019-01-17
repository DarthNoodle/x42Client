using System;


namespace x42Client.Utils.Validation
{
    public static class Guard
    {
        /// <summary>
        /// Throws an ArgumentNullException if input is null.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public static void Null<T>(T input, string parameterName)
        {
            if (input == null) { throw new ArgumentNullException(parameterName); }

        }//public static void Null<T>(T input, string parameterName)

        /// <summary>
        /// Throws an ArgumentNullException if input is null.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public static void Null<T>(T input, string parameterName, string errorMsg)
        {
            // we dont have to error we can use the call without an error msg
            if (string.IsNullOrWhiteSpace(errorMsg))
            {
                Guard.Null(input, parameterName);
                return;
            }//end of if (string.IsNullOrWhiteSpace(errorMsg))

            if (input == null) { throw new ArgumentNullException(parameterName, errorMsg); }

        }//public static void Null<T>(T input, string parameterName)

        /// <summary>
        /// Throws an ArgumentException if input is NOT null.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        public static void NotNull<T>(T input, string parameterName)
        {
            if (input != null) { throw new ArgumentException(parameterName); }

        }//public static void Null<T>(T input, string parameterName)

        /// <summary>
        /// Throws an ArgumentNullException if input is NOT null.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        public static void NotNull<T>(T input, string parameterName, string errorMsg)
        {
            // we dont have to error we can use the call without an error msg
            if (string.IsNullOrWhiteSpace(errorMsg))
            {
                Guard.NotNull(input, parameterName);
                return;
            }//end of if (string.IsNullOrWhiteSpace(errorMsg))

            if (input == null) { throw new ArgumentException(parameterName, errorMsg); }

        }//public static void Null<T>(T input, string parameterName)

        /// <summary>
        /// Throws an Exception if the condition is not true
        /// </summary>   
        /// <exception cref="Exception"></exception>     
        public static void AssertTrue(bool condition)
        {
            if (!condition) { throw new Exception("Assertion Failed! Expected 'true'"); }
        }//end of public static void AssertTrue(bool condition)


        /// <summary>
        /// Throws an Exception if the condition is not true
        /// </summary>   
        /// <exception cref="Exception"></exception>        
        public static void AssertTrue(bool condition, string errorMsg)
        {
            // we dont have to error we can use the call without an error msg
            if (string.IsNullOrWhiteSpace(errorMsg))
            {
                Guard.AssertTrue(condition);
                return;
            }//end of if (string.IsNullOrWhiteSpace(errorMsg))

            if (!condition) { throw new Exception(errorMsg); }
        }//end of public static void AssertTrue(bool condition)

        /// <summary>
        /// Throws an Exception if the condition is not false
        /// </summary>        
        public static void AssertFalse(bool condition)
        {
            if (!condition) { throw new Exception("Assertion Failed! Expected 'false'"); }
        }//end of public static void AssertTrue(bool condition)


        /// <summary>
        /// Throws an Exception if the condition is not false
        /// </summary>
        /// <exception cref="Exception"></exception>           
        public static void AssertFalse(bool condition, string errorMsg)
        {
            // we dont have to error we can use the call without an error msg
            if (string.IsNullOrWhiteSpace(errorMsg))
            {
                Guard.AssertFalse(condition);
                return;
            }//end of if (string.IsNullOrWhiteSpace(errorMsg))

            if (!condition) { throw new Exception(errorMsg); }
        }//end of public static void AssertTrue(bool condition)


        /// <summary>
        /// Throws an ArgumentNullException if input is null.
        /// Throws an ArgumentException if input is an empty string.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void NullOrEmpty(string input, string parameterName)
        {
            Guard.Null(input, parameterName);

            if (input == string.Empty) { throw new ArgumentException($"Required input {parameterName} was empty.", parameterName); }
        }//end of public static void NullOrEmpty(string input, string parameterName)


        /// <summary>
        /// Throws an ArgumentNullException if input is null.
        /// Throws an ArgumentException if input is an empty string.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void NullOrEmpty(string input, string parameterName, string errorMsg)
        {
            Guard.Null(input, parameterName);

            // we dont have to error we can use the call without an error msg
            if (string.IsNullOrWhiteSpace(errorMsg))
            {
                Guard.NullOrEmpty(input, parameterName);
                return;
            }//end of if (string.IsNullOrWhiteSpace(errorMsg))

            if (input == string.Empty) { throw new ArgumentException(errorMsg, parameterName); }
        }//end of public static void NullOrEmpty(string input, string parameterName)


    }//end of public static class Guard
}
