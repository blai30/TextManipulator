using Humanizer;

namespace TextManipulator.Pages
{
    public partial class Numbers
    {
        private bool Loading { get; set; } = false;
        private string InputNumber { get; set; } = string.Empty;
        private string OutputText { get; set; } = string.Empty;
        private Notation Notation { get; set; } = Notation.Digits;

        private void Transform()
        {
            Loading = true;

            if (!int.TryParse(InputNumber, out int input))
            {
                return;
            }
            string result = string.Empty;

            result = Notation switch
            {
                Notation.Words => input.ToWords(),
                Notation.OrdinalWords => input.ToOrdinalWords(),
                Notation.RomanNumerals => input.ToRoman(),
                Notation.Metrics => input.ToMetric(),
                _ => input.ToString()
            };

            OutputText = result;
            Loading = false;
        }
    }

    public enum Notation
    {
        Digits,
        Words,
        OrdinalWords,
        RomanNumerals,
        Metrics
    }
}
