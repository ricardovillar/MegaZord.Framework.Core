using System;
using System.Globalization;

namespace MegaZord.Framework.MVC.Binders {
    public class MZDecimalModelBinder : MZBaseModelBinder<decimal> {
        protected override string ErrorMessage {
            get {
                return "Invalid decimal number";
            }
        }

        protected override bool TryParse(string s, out decimal result) {
            return Decimal.TryParse(s, NumberStyles.Currency, new CultureInfo(Culture), out result);

        }
    }
}
