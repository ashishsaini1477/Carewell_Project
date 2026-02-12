using System.Text;

namespace CarwellAutoshop.Infrastructure.Utility
{
    public static class NumberToWordsConverter
    {
        private static readonly string[] Units =
        {
        "Zero", "One", "Two", "Three", "Four", "Five", "Six",
        "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve",
        "Thirteen", "Fourteen", "Fifteen", "Sixteen",
        "Seventeen", "Eighteen", "Nineteen"
    };

        private static readonly string[] Tens =
        {
        "", "", "Twenty", "Thirty", "Forty", "Fifty",
        "Sixty", "Seventy", "Eighty", "Ninety"
    };

        public static string Convert(decimal amount)
        {
            if (amount == 0)
                return "Zero Rupees Only";

            long rupees = (long)Math.Floor(amount);
            int paise = (int)Math.Round((amount - rupees) * 100);

            var words = new StringBuilder();

            words.Append("Rupees ");
            words.Append(ConvertToIndianWords(rupees));

            if (paise > 0)
            {
                words.Append(" and ");
                words.Append(ConvertTwoDigitNumber(paise));
                words.Append(" Paise");
            }

            words.Append(" Only");

            return words.ToString();
        }

        private static string ConvertToIndianWords(long number)
        {
            if (number == 0)
                return "Zero";

            var words = new StringBuilder();

            if ((number / 10000000) > 0)
            {
                words.Append(ConvertToIndianWords(number / 10000000));
                words.Append(" Crore ");
                number %= 10000000;
            }

            if ((number / 100000) > 0)
            {
                words.Append(ConvertToIndianWords(number / 100000));
                words.Append(" Lakh ");
                number %= 100000;
            }

            if ((number / 1000) > 0)
            {
                words.Append(ConvertToIndianWords(number / 1000));
                words.Append(" Thousand ");
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words.Append(ConvertToIndianWords(number / 100));
                words.Append(" Hundred ");
                number %= 100;
            }

            if (number > 0)
            {
                if (words.Length != 0)
                    words.Append("");

                if (number < 20)
                    words.Append(Units[number]);
                else
                {
                    words.Append(Tens[number / 10]);
                    if ((number % 10) > 0)
                        words.Append(" " + Units[number % 10]);
                }
            }

            return words.ToString().Trim();
        }

        private static string ConvertTwoDigitNumber(int number)
        {
            if (number < 20)
                return Units[number];

            var word = Tens[number / 10];
            if ((number % 10) > 0)
                word += " " + Units[number % 10];

            return word;
        }
    }
}
