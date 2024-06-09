using UnityEngine;

namespace HF.Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsNull(this object Value) => Value == null;

        public static bool IsNotNull(this object Value) => Value != null;

        public static bool IsNull(this GameObject Value) => Value == null;

        public static bool IsNotNull(this GameObject Value) => Value != null;

        public static bool IsNull(this Transform Value) => ReferenceEquals(Value, null) ? false : (Value ? false : true);

        public static bool IsNotNull(this Transform Value) => (ReferenceEquals(Value, null) ? true : (Value ? true : false));

        public static bool IsNull(this Rigidbody Value) => ReferenceEquals(Value, null) ? false : (Value ? false : true);

        public static bool IsNotNull(this Rigidbody Value) => (ReferenceEquals(Value, null) ? true : (Value ? true : false));

        public static bool IsNull(this Rigidbody2D Value) => ReferenceEquals(Value, null) ? false : (Value ? false : true);

        public static bool IsNotNull(this Rigidbody2D Value) => (ReferenceEquals(Value, null) ? true : (Value ? true : false));

        public static bool IsNull(this Collider Value) => ReferenceEquals(Value, null) ? false : (Value ? false : true);

        public static bool IsNotNull(this Collider Value) => (ReferenceEquals(Value, null) ? true : (Value ? true : false));

        public static bool IsNull(this Collider2D Value) => ReferenceEquals(Value, null) ? false : (Value ? false : true);

        public static bool IsNotNull(this Collider2D Value) => (ReferenceEquals(Value, null) ? true : (Value ? true : false));

        public static bool IsNull(this SpriteRenderer Value) => ReferenceEquals(Value, null) ? false : (Value ? false : true);

        public static bool IsNotNull(this SpriteRenderer Value) => (ReferenceEquals(Value, null) ? true : (Value ? true : false));
    }
}