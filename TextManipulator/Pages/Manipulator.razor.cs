using System;
using System.Text.RegularExpressions;
using Humanizer;

namespace TextManipulator.Pages
{
    public partial class Manipulator
    {
        private bool Loading { get; set; } = false;
        private bool InputIsBlank => string.IsNullOrWhiteSpace(InputText);
        private string InputText { get; set; }
        private string OutputText { get; set; }
        private ManipulatorModel Model { get; set; } = new();
        private Random Random { get; set; } = new(new Guid().GetHashCode());

        private void Manipulate()
        {
            if (string.IsNullOrEmpty(InputText))
            {
                return;
            }

            Loading = true;
            string input = InputText;

            if (Model.TrimWhitespace)
            {
                // Trim trailing whitespace.
                input = input.Trim();

                // Shrink excess spaces between words.
                var regex = new Regex(@"\s+");
                input = regex.Replace(input, " ");
            }

            if (Model.ClapItUp)
            {
                input = input.Replace(" ", " 👏 ");
            }

            input = Model.TextCase switch
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

            if (Model.CharLimit != null)
            {
                input = input.Truncate((int) Model.CharLimit, "");
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

    public class ManipulatorModel
    {
        public TextCase TextCase { get; set; } = TextCase.Unchanged;
        public bool TrimWhitespace { get; set; } = false;
        public bool ClapItUp { get; set; } = false;
        public int? CharLimit { get; set; } = null;
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
