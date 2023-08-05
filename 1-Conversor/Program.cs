using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Digite um número romano:");
        string romanNumeral = Console.ReadLine();

        if (!IsRomanNumeral(romanNumeral))
        {
            Console.WriteLine($"'{romanNumeral}' não é um número romano, digite outro.");
            return;
        }

        int decimalNumber = RomanToDecimal(romanNumeral);

        Console.WriteLine($"O número equivalente ao numeral romano '{romanNumeral}' é '{decimalNumber}'.");
    }

     static bool IsRomanNumeral(string romanNumber)
    {
        foreach (char c in romanNumber)
        {
            if (!"IVXLCDM".Contains(c))
            {
                return false;
            }
        }

        return true;
    }

    public static int RomanToDecimal(string romanNumber)
    {
        Dictionary<char, int> romanValues = new Dictionary<char, int>()
        {
            {'I', 1},
            {'V', 5},
            {'X', 10},
            {'L', 50},
            {'C', 100},
            {'D', 500},
            {'M', 1000}
        };

        int result = 0;

        for (int i = 0; i < romanNumber.Length; i++)
        {
            if (i + 1 < romanNumber.Length && romanValues[romanNumber[i]] < romanValues[romanNumber[i + 1]])
            {
                result -= romanValues[romanNumber[i]];
            }
            else
            {
                result += romanValues[romanNumber[i]];
            }
        }

        return result;
    }

}