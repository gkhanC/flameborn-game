using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace HF.TwoD.Gravity
{
    /// <summary>
    /// This class handles applying custom gravity to a character using Rigidbody2D.
    /// </summary>
    [System.Serializable]
    public class CharacterGravityController
    {
        /// <summary>
        /// Reference to the character's Rigidbody2D component.
        /// </summary>
        [SerializeField][Required] private Rigidbody2D m_Rigidbody2D;

        /// <summary>
        /// Data object containing custom gravity parameters. Assignable in the inspector.
        /// </summary>
        [field: SerializeField][Required("Data object is required")] public CharacterCustomGravityData Data { get; private set; } // Use property for data access

        /// <summary>
        /// Event triggered when the character's gravity settings change.
        /// </summary>
        [Tooltip("Event triggered when the character's gravity settings change. Passes the sender (this object).")]
        [field: SerializeField] public UnityEvent<object> OnGravityChange { get; private set; } = null; // Use properties for event access

        /// <summary>
        /// Constructor for the CharacterGravityController class.
        /// </summary>
        /// <param name="rigidbody2D">The character's Rigidbody2D component.</param>
        public CharacterGravityController(Rigidbody2D rigidbody2D)
        {
            m_Rigidbody2D = rigidbody2D;
            OnGravityChange = new UnityEvent<object>(); // Initialize event
        }

        public void SetRigidbody2D(Rigidbody2D rigidbody2D)
        {
            m_Rigidbody2D = rigidbody2D;
        }

        public void SetData(CharacterCustomGravityData data)
        {
            Data = data;
        }

        /// <summary>
        /// Sets the gravity scale applied to the character's Rigidbody2D.
        /// </summary>
        /// <param name="scale">The new gravity scale value.</param>
        public void SetGravityScale(float scale)
        {
            if (Data == null) return;

            OnGravityChange.Invoke(this);
            m_Rigidbody2D.gravityScale = scale;
        }
    }
}
