using UnityEngine;

namespace HF.Extensions
{
    public static class VectorExtensions
    {
        public static Vector3 ElementwiseMultiplication(this Vector3 a, Vector3 b)
        {
            return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
        }

        public static Vector2 ElementwiseMultiplication(this Vector2 a, Vector2 b)
        {
            return new Vector3(a.x * b.x, a.y * b.y);
        }

        public static Vector3 ElementwiseMultiplication(this Vector2 a, Vector3 b)
        {
            return new Vector3(a.x * b.x, a.y * b.y, b.z);
        }

        public static Vector3 ElementwiseMultiplication(this Vector3 a, Vector2 b)
        {
            return new Vector3(a.x * b.x, a.y * b.y, a.z);
        }
    }
}
