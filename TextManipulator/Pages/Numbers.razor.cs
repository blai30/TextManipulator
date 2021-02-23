using Humanizer;

namespace TextManipulator.Pages
{
    public partial class Numbers
    {
        private bool Loading { get; set; } = false;
        private bool InputIsBlank => InputNumber == null;
        private int? InputNumber { get; set; }
        private string OutputText { get; set; }
        private NumbersModel Model { get; set; } = new();

        private void Transform()
        {
            if (InputNumber == null)
            {
                return;
            }

            Loading = true;

            int input = (int) InputNumber;
            string result = Model.Notation switch
            {
                Notation.Words => input.ToWords(),
                Notation.OrdinalWords => input.ToOrdinalWords(),
                Notation.RomanNumerals => input < 4000 && input > 0 ? input.ToRoman() : "Input must be >0 and <4000",
                Notation.Metrics => input.ToMetric(),
                _ => input.ToString()
            };

            OutputText = result;
            Loading = false;
        }
    }

    public class NumbersModel
    {
        public Notation Notation { get; set; } = Notation.Digits;
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
