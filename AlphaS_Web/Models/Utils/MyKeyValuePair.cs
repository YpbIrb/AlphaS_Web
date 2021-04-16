using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaS_Web.Models.Utils
{
    public class MyKeyValuePair
    {
        [DisplayName("Переменная")]
        public string Key { get; set; }

        [DisplayName("Описание")]
        public string Value { get; set; }

    }
}
