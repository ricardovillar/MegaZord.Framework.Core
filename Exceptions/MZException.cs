using System;

namespace MegaZord.Framework.Exceptions {
    public class MZException : Exception {
        public MZException(string message)
            : base(message) {
        }
    }

}
