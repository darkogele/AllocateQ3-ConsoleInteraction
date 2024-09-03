using AllocateQ3_ConsoleInteraction.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AllocateQ3_ConsoleInteraction.Common.Validators
{
    public static class StringValidator
    {
        public static int ValidatePositiveInteger(this string input)
        {
            var result = int.TryParse(input, out int parsed);

            if (!result || parsed < 0)
            {
                throw new InputException("Invalid Input");
            }

            return parsed;
        }
        
        public static string CheckNullOrEmpty(this string input)
        {
            if (String.IsNullOrEmpty(input)) // We are not JAVA use alias for string, same as Integer for int
            {
                throw new InputException("Invalid input");
            }

            return input;
        }
    }
}
