using System;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

class Program
{
    static void Main()
    {
        Console.WriteLine("Digite a quantidade de caracteres da senha:");
        int length = int.Parse(Console.ReadLine());

        Console.WriteLine("Incluir letra maiúscula? (S/N)");
        bool includeUpperCase = Console.ReadLine().ToUpper() == "S";

        Console.WriteLine("Incluir letra minúscula? (S/N)");
        bool includeLowerCase = Console.ReadLine().ToUpper() == "S";

        Console.WriteLine("Incluir número? (S/N)");
        bool includeNumber = Console.ReadLine().ToUpper() == "S";

        Console.WriteLine("Incluir símbolo? (S/N)");
        bool includeSymbol = Console.ReadLine().ToUpper() == "S";

        string password = GeneratePassword(length, includeUpperCase, includeLowerCase, includeNumber, includeSymbol);

        Console.WriteLine($"Senha gerada: {password}");

        // Verificar a força da senha
        int passwordStrength = CalculatePasswordStrength(password);
        Console.WriteLine($"Força da senha: {passwordStrength} de 100");

        // Copiar a senha para a área de transferência
        Console.WriteLine("Deseja copiar a senha para a área de transferência? (S/N)");
        if (Console.ReadLine().ToUpper() == "S")
        {
            TextCopy.ClipboardService.SetText(password);
            Console.WriteLine("Senha copiada para a área de transferência.");
        }
    }

    static string GeneratePassword(int length, bool includeUpperCase, bool includeLowerCase, bool includeNumber, bool includeSymbol)
    {
        // Definir os caracteres que podem ser usados na senha
        string uppercaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string lowercaseLetters = "abcdefghijklmnopqrstuvwxyz";
        string numbers = "0123456789";
        string symbols = "!@#$%^&*()";

        // Criar uma lista vazia para armazenar os caracteres possíveis
        var possibleCharacters = new StringBuilder();

        // Adicionar os caracteres selecionados à lista de caracteres possíveis
        if (includeUpperCase)
        {
            possibleCharacters.Append(uppercaseLetters);
        }

        if (includeLowerCase)
        {
            possibleCharacters.Append(lowercaseLetters);
        }

        if (includeNumber)
        {
            possibleCharacters.Append(numbers);
        }

        if (includeSymbol)
        {
            possibleCharacters.Append(symbols);
        }

        // Gerar a senha aleatoriamente
        var random = new Random();
        var password = new StringBuilder();

        for (int i = 0; i < length; i++)
        {
            int randomIndex = random.Next(possibleCharacters.Length);
            password.Append(possibleCharacters[randomIndex]);
        }

        return password.ToString();
    }

    static int CalculatePasswordStrength(string password)
    {
        // Definir critérios de força da senha (pode ser personalizado)
        int lengthScore = password.Length * 4;
        int uppercaseScore = (password.Count(char.IsUpper) > 0) ? (password.Length - password.Count(char.IsUpper)) * 2 : 0;
        int lowercaseScore = (password.Count(char.IsLower) > 0) ? (password.Length - password.Count(char.IsLower)) * 2 : 0;
        int numberScore = (password.Count(char.IsDigit) > 0) ? password.Count(char.IsDigit) * 4 : 0;
        int symbolScore = (password.Count(char.IsSymbol) > 0 || password.Count(char.IsPunctuation) > 0) ? password.Count(char.IsSymbol) * 6 : 0;

        // Calcular a pontuação total
        int totalScore = lengthScore + uppercaseScore + lowercaseScore + numberScore + symbolScore;

        return totalScore;
    }
}
