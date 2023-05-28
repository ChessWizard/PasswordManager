using Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Utilities
{
    public class PasswordGenerator
    {
        private const string Numbers = "0123456789";
        private const string LowercaseCharacters = "abcdefghijklmnopqrstuvwxyz";
        private const string UppercaseCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string SpecialCharacters = "!@#$%^&*()[]{},.<>?%";

        private readonly Random _random;
        private readonly StringBuilder _charSetBuilder;
        private readonly StringBuilder _passwordBuilder;

        public PasswordGenerator()
        {
            _random = new Random();
            _charSetBuilder = new StringBuilder();
            _passwordBuilder = new StringBuilder();
        }

        /// <summary>
        /// Kullanıcı tercihlerine göre şifre yaratmaya yarayan metot
        /// </summary>
        /// <param name="generatePasswordDto"></param>
        /// <returns></returns>
        public string Generate(GeneratePasswordDto generatePasswordDto)
        {
            _charSetBuilder.Clear();
            _passwordBuilder.Clear();

            if (generatePasswordDto is null || generatePasswordDto.Length < 4) { return string.Empty; }

            if (generatePasswordDto.IncludeNumbers) { _charSetBuilder.Append(Numbers); }

            if (generatePasswordDto.IncludeLowerCaseCharacters) { _charSetBuilder.Append(LowercaseCharacters); }

            if (generatePasswordDto.IncludeUpperCaseCharacters) { _charSetBuilder.Append(UppercaseCharacters); }

            if (generatePasswordDto.IncludeSpecialCaseCharacters) { _charSetBuilder.Append(SpecialCharacters); }

            var charSet = _charSetBuilder.ToString();

            for (int i = 0; i < generatePasswordDto.Length; i++)
            {
                var randomIndex = _random.Next(charSet.Length);

                _passwordBuilder.Append(charSet[randomIndex]);
            }

            var password = _passwordBuilder.ToString();

            var isValidPassword = CheckIsPasswordIncludesAllConditions(password, generatePasswordDto);

            return isValidPassword ? password : Generate(generatePasswordDto);
        }

        /// <summary>
        /// Şifrede kullanıcının tüm seçeneklerine göre en az 1 karakter bulunuyor mu kontrolü
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        private bool CheckIsPasswordIncludesAllConditions(string password, GeneratePasswordDto generatePasswordDto)
        {
            bool isValidPassword = true;

            if (generatePasswordDto.IncludeNumbers)
            {
                isValidPassword = ValidatePasswordByCharSet(password, Numbers);
                if (!isValidPassword)
                {
                    return false;
                }
            }

            if (generatePasswordDto.IncludeLowerCaseCharacters)
            {
                isValidPassword = ValidatePasswordByCharSet(password, LowercaseCharacters);
                if (!isValidPassword)
                {
                    return false;
                }
            }

            if (generatePasswordDto.IncludeUpperCaseCharacters)
            {
                isValidPassword = ValidatePasswordByCharSet(password, UppercaseCharacters);
                if (!isValidPassword)
                {
                    return false;
                }
            }

            if (generatePasswordDto.IncludeSpecialCaseCharacters)
            {
                isValidPassword = ValidatePasswordByCharSet(password, SpecialCharacters);
                if (!isValidPassword)
                {
                    return false;
                }
            }

            return isValidPassword;
        }

        /// <summary>
        /// Her bir karakter seti kontrolü için kod tekrarının önüne geçilmesi amacıyla yazılmıştır
        /// </summary>
        /// <param name="password"></param>
        /// <param name="characterSet"></param>
        /// <returns></returns>
        private bool ValidatePasswordByCharSet(string password, string characterSet)
        {
            foreach (var character in characterSet)
            {
                if (password.Contains(character))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Yaratılan şifrenin yaratılma koşullarını döndürür
        /// </summary>
        /// <param name="generatePasswordDto"></param>
        /// <returns></returns>
        public string GetPasswordCreationConditions(GeneratePasswordDto generatePasswordDto)
        {
            List<string> conditions = new();

            if (generatePasswordDto.IncludeLowerCaseCharacters) { conditions.Add("Küçük Harf"); }

            if (generatePasswordDto.IncludeUpperCaseCharacters) { conditions.Add("Büyük Harf"); }

            if (generatePasswordDto.IncludeNumbers) { conditions.Add("Sayı"); }

            if (generatePasswordDto.IncludeSpecialCaseCharacters) { conditions.Add("Özel Karakter"); }

            return String.Join(" | ", conditions);
        }
    }
}
