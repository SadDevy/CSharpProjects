using System;
using System.Text;

namespace NumberConverter
{
    /// <summary>
    /// Конвертирует десятичное число в различные системы счисления.
    /// </summary>
    public static class Converter
    {
        /// <summary>
        /// Конвертирует десятичное число в двоичное.
        /// </summary>
        /// <param name="value">Десятичное число.</param>
        /// <returns>Двоичное число в виде строки.</returns>
        private static string ToBin(int value)
        {
            const string digits = "01";
            return ToBase(value, digits);
        }

        private static string ToOct(int value)
        {
            const string digits = "01234567";
            return ToBase(value, digits);
        }

        private static string ToHex(int value)
        {
            const string digits = "0123456789ABCDEF";
            return ToBase(value, digits);
        }

        /// <summary>
        /// Конвертирует десятичное число в Base32.
        /// </summary>
        /// <param name="value">Десятичное число.</param>
        /// <returns>Base32 число в виде строки.</returns>
        private static string ToBase32(int value)
        {
            const string digits = "0123456789ABCDEFGHJKMNPQRSTVWXYZ";
            return ToBase(value, digits);
        }

        private static string ToBase64(int value)
        {
            const string digits = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmopqrstuvwxyz0123456789+/";
            return ToBase(value, digits);
        }

        /// <summary>
        /// Конвертирует десятичное число в заданную систему счисления.
        /// </summary>
        /// <param name="value">Десятичное число.</param>
        /// <param name="digits">Системя счисления.</param>
        /// <returns>Число в заданной системе счисления.</returns>
        private static string ToBase(int value, string digits)
        {
            int toBase = digits.Length;
            StringBuilder result = new StringBuilder();
            while (value > 0)
            {
                char digit = digits[value % toBase];
                result.Insert(0, digit);
                value /= toBase;
            }

            return result.ToString();
        }

        /// <summary>
        /// Конвертирует десятичное число в двоичное.
        /// </summary>
        /// <param name="value">Десятичное число.</param>
        /// <returns>Двоичное число в виде строки.</returns>
        private static string ToBinStandard(int value)
        {
            return Convert.ToString(value, (int)BaseSystem.Bin);
        }

        private static string ToOctStandard(int value)
        {
            return Convert.ToString(value, (int)BaseSystem.Oct);
        }

        private static string ToHexStandard(int value)
        {
            return Convert.ToString(value, (int)BaseSystem.Hex).ToUpper();
        }

        /// <summary>
        /// Конвертирует десятичное число в Base64 представление.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Base64 число в виде строки.</returns>
        private static string ToBase64Standard(int value)
        {
            return Convert.ToBase64String(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Представляет число в заданной системе счисления.
        /// </summary>
        /// <param name="value">десятичное число.</param>
        /// <param name="toBase">Система счисления.</param>
        /// <returns>Число в заданной системе счисления.</returns>
        public static string ToBase(int value, BaseSystem toBase)
        {
            switch (toBase)
            {
                case BaseSystem.Bin:
                    return ToBin(value);
                case BaseSystem.Oct:
                    return ToOct(value);
                case BaseSystem.Hex:
                    return ToHex(value);
                case BaseSystem.Base32:
                    return ToBase32(value);
                case BaseSystem.Base64:
                    return ToBase64(value);
                default: 
                    return null;
            }
        }

        public static string ToBaseStandard(int value, BaseSystem toBase)
        {
            switch (toBase)
            {
                case BaseSystem.Bin:
                    return ToBinStandard(value);
                case BaseSystem.Oct:
                    return ToOctStandard(value);
                case BaseSystem.Hex:
                    return ToHexStandard(value);
                case BaseSystem.Base64:
                    return ToBase64Standard(value);
                default:
                    return null;
            }
        }
    }
}
