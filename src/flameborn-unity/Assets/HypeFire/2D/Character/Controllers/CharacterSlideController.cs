using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace HF.TwoD.Character.Controllers
{
    /// <summary>
    /// This class handles character sliding logic and applies forces based on slide data.
    /// </summary>
    [System.Serializable]
    public class CharacterSlideController
    {
        /// <summary>
        /// Reference to the character's Rigidbody2D component.
        /// </summary>
        [SerializeField][Required] private Rigidbody2D m_Rigidbody2D;

        /// <summary>
        /// Data object containing slide parameters.
        /// </summary>
        [field: SerializeField][Required("Data object is required")] public CharacterSlideData Data { get; private set; } = null;

        /// <summary>
        /// Event triggered when the character starts or stops sliding.
        /// </summary>
        [Space(10f)]
        [Tooltip("Event triggered when the character starts or stops sliding. Passes the sender and a bool indicating sliding state.")]
        [SerializeField] public UnityEvent<object> OnSlidingEvent = new UnityEvent<object>();


        /// <summary>
        /// Constructor for the CharacterSlideController class.
        /// </summary>
        /// <param name="rigidbody2D">The character's Rigidbody2D component.</param>
        public CharacterSlideController(Rigidbody2D rigidbody2D)
        {
            m_Rigidbody2D = rigidbody2D;
            OnSlidingEvent = new UnityEvent<object>();
        }

        public void SetRigidbody2D(Rigidbody2D rigidbody2D)
        {
            m_Rigidbody2D = rigidbody2D;
        }

        public void SetData(CharacterSlideData data)
        {
            Data = data;
        }

        /// <summary>
        /// Applies slide force to the character based on slide data.
        /// </summary>
        public void Slide()
        {
            if (Data == null) return;

            OnSlidingEvent.Invoke(this);

            float speedDifference = Data.SlideSpeed - m_Rigidbody2D.velocity.y;
            float movement = speedDifference * Data.SlideAcceleration;

            // Clamp movement force to prevent excessive acceleration
            movement = Mathf.Clamp(movement, -Mathf.Abs(speedDifference) * (1 / Time.fixedDeltaTime), Mathf.Abs(speedDifference) * (1 / Time.fixedDeltaTime));

            // Apply slide force in the upward direction
            m_Rigidbody2D.AddForce(movement * Vector2.up, ForceMode2D.Force);
        }
    }
}
