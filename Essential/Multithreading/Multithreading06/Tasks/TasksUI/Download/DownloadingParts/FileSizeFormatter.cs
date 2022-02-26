using System;

namespace TasksUI
{
    public static class FileSizeFormatter
    {
        public static string Format(int fileSize)
        {
            const double oneKb = 1024.0;
            const double oneMb = oneKb * oneKb;
            const double oneGb = oneMb * oneKb;

            string result = $"{fileSize}B";
            if (fileSize / oneKb < oneKb)
            {
                int formatted = (int)Math.Round(fileSize / oneKb, 0);
                return $"{formatted}Kb";
            }

            if (fileSize / oneMb < oneMb)
            {
                int formatted = (int)Math.Round(fileSize / oneMb, 0);
                return $"{formatted}Mb";
            }

            if (fileSize / oneGb < oneGb)
            {
                int formatted = (int)Math.Round(fileSize / oneGb, 0);
                return $"{formatted}Gb";
            }

            return result;
        }
    }
}
