using System;
using System.Linq;
using System.Reflection;
using System.ComponentModel.DataAnnotations;

namespace MegaZord.Framework.Helpers {
    public static class MZHelperEnum {
        public static string GetDescriptionFromEnumValue(Enum value) {
            var attribute = value.GetType()
                .GetField(value.ToString())
                .GetCustomAttributes(typeof(DisplayAttribute), false)
                .SingleOrDefault() as DisplayAttribute;
            return attribute == null ? value.ToString() : attribute.Description;
        }
    }
}
