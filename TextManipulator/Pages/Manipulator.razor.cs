using System;
using System.Text.RegularExpressions;
using Humanizer;

namespace TextManipulator.Pages
{
    public partial class Manipulator
    {
        private bool Loading { get; set; } = false;
        private string InputText { get; set; } = string.Empty;
        private string OutputText { get; set; } = string.Empty;
        private TextCase TextCase { get; set; } = TextCase.Unchanged;
        private bool ClapItUp { get; set; } = false;
        private int? CharLimit { get; set; } = null;
        private Random Random { get; set; } = new(new Guid().GetHashCode());

        private void Manipulate()
        {
            if (string.IsNullOrEmpty(InputText))
            {
                return;
            }

            Loading = true;

            string input = InputText;
            input = input.Trim();

            // Shrink excess spaces.
            var regex = new Regex(@"\s+");
            input = regex.Replace(input, " ");

            if (ClapItUp)
            {
                input = input.Replace(" ", " 👏 ");
            }

            input = TextCase switch
            {
                TextCase.Unchanged => input,
                TextCase.UpperCase => input.ToUpper(),
                TextCase.LowerCase => input.ToLower(),
                TextCase.CamelCase => input.Camelize(),
                TextCase.PascalCase => input.Pascalize(),
                TextCase.SentenceCase => input.Transform(To.SentenceCase),
                TextCase.TitleCase => input.Titleize(),
                TextCase.SnakeCase => input.Underscore(),
                TextCase.KebabCase => input.Kebaberize(),
                TextCase.RandomCase => RandomCase(input),
                _ => input
            };

            if (CharLimit != null)
            {
                input = input.Truncate((int) CharLimit, "");
            }

            OutputText = input;
            Loading = false;
        }

        private string RandomCase(string input)
        {
            string result = string.Empty;

            do
            {
                foreach (char c in input)
                {
                    int next = Random.Next(2);

                    result += next switch
                    {
                        0 => char.ToUpper(c).ToString(),
                        1 => char.ToLower(c).ToString(),
                        _ => c.ToString()
                    };
                }
            } while (result == input);

            return result;
        }
    }

    public enum TextCase
    {
        Unchanged,
        UpperCase,
        LowerCase,
        SentenceCase,
        TitleCase,
        CamelCase,
        PascalCase,
        SnakeCase,
        KebabCase,
        RandomCase
    }
}
