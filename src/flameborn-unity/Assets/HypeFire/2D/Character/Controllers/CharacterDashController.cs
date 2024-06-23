using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace HF.TwoD.Character.Controllers
{
    /// <summary>
    /// This class handles character dash logic and applies forces based on dash data.
    /// </summary>
    [System.Serializable]
    public class CharacterDashController
    {
        /// <summary>
        /// Reference to the character's Rigidbody2D component.
        /// </summary>
        [SerializeField][Required] private Rigidbody2D m_Rigidbody2D;

        /// <summary>
        /// Data object containing dash parameters.
        /// </summary>
        [field: SerializeField][Required("Data object is required")] public CharacterDashData Data { get; private set; } = null;

        /// <summary>
        /// Event triggered when the character starts or stops dashing.
        /// </summary>
        [Space(10f)]
        [Tooltip("Event triggered when the character starts or stops dashing. Passes the sender and a bool indicating dashing state.")]
        [SerializeField] public UnityEvent<object> OnSlidingEvent = new UnityEvent<object>();


        /// <summary>
        /// Constructor for the CharacterDashController class.
        /// </summary>
        /// <param name="rigidbody2D">The character's Rigidbody2D component.</param>
        public CharacterDashController(Rigidbody2D rigidbody2D)
        {
            m_Rigidbody2D = rigidbody2D;
            OnSlidingEvent = new UnityEvent<object>();
        }

        public void SetRigidbody2D(Rigidbody2D rigidbody2D)
        {
            m_Rigidbody2D = rigidbody2D;
        }

        public void SetData(CharacterDashData data)
        {
            Data = data;
        }

        /// <summary>
        /// Applies dash force to the character based on dash data.
        /// </summary>
        public void Dash(float direction = 1f)
        {
            if (Data == null) return;

            OnSlidingEvent.Invoke(this);
            m_Rigidbody2D.constraints = (RigidbodyConstraints2D)6;
            float movement = Data.DashSpeed * Data.DashAcceleration;

            // Apply dash force in the upward direction
            m_Rigidbody2D.AddForce(movement * Vector2.right * direction, ForceMode2D.Force);
        }
    }
}
