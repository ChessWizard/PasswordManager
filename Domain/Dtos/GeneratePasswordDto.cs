using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class GeneratePasswordDto
    {
        public bool IncludeNumbers { get; set; } = true;

        public bool IncludeLowerCaseCharacters { get; set; } = true;

        public bool IncludeUpperCaseCharacters { get; set; }

        public bool IncludeSpecialCaseCharacters { get; set; }

        public int Length { get; set; } = 4;

    }
}
