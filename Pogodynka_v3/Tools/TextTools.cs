using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pogodynka_v3.tools
{
    abstract public class TextTools
    {
        public static string StartWithCapital(string text)
        {
            char[] FinalText = new char[text.Length];
            FinalText[0] = System.Char.ToUpper(text[0]);
            for (int LetterIndex = 1; LetterIndex < text.Length; LetterIndex++)
            {
                FinalText[LetterIndex] = text[LetterIndex];
            }

            return new string(FinalText);
        }
    }
}
