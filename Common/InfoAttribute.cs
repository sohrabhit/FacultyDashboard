using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Common
{
    public class InfoAttribute : Attribute
    {
        public FieldType fieldType { get; }
        public Type Enum { get; }
        public string DisplayName_EN { get; }
        public string DisplayName_FA { get; }
        public InfoAttribute(FieldType _fieldType, Type _Enum, string _DisplayName_EN, string _DisplayName_FA)
        {
            this.fieldType = _fieldType;
            this.Enum = _Enum;
            this.DisplayName_EN = _DisplayName_EN;
            this.DisplayName_FA = _DisplayName_FA;
        }
    }
    public enum FieldType
    {
        [Display(Name = "String")]
        Strings,
        [Display(Name = "Int")]
        Integerr,
        [Display(Name = "Float")]
        Float,
        [Display(Name = "Double")]
        Double,
        [Display(Name = "Long")]
        Long,
        [Display(Name = "Multiple")]
        Multiple,
        [Display(Name = "Enum")]
        Enum,
        [Display(Name = "Bool")]
        Bool,
        [Display(Name = "DateTime")]
        DateTime,
        [Display(Name = "Collection")]
        Collection,
        [Display(Name = "Class")]
        Class
    }
}
