using System;
using System.Globalization;

namespace MegaZord.Framework.MVC.Binders {
    public class MZDateTimeModelBinder : MZBaseModelBinder<DateTime> {
        protected override string ErrorMessage {
            get {
                return "Invalid datetime value";
            }
        }

        protected override bool TryParse(string s, out DateTime result) {
            return DateTime.TryParse(s, new CultureInfo(Culture), DateTimeStyles.None, out result);
        }
    }
}
