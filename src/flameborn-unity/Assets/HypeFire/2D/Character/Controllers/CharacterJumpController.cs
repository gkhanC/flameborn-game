using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace HF.TwoD.Character.Controllers
{
    /// <summary>
    /// This class handles character jump logic and applies force based on provided jump data.
    /// </summary>
    [System.Serializable]
    public class CharacterJumpController
    {
        /// <summary>
        /// Reference to the character's Rigidbody2D component.
        /// </summary>
        [SerializeField][Required] private Rigidbody2D m_Rigidbody2D;

        /// <summary>
        /// Data object containing jump parameters.
        /// </summary>
        [field: SerializeField][Required("Data object is required")] public CharacterJumpData Data { get; private set; } = null;

        /// <summary>
        /// On jump event distributor
        /// </summary>
        /// <typeparam name="object">sender</typeparam>
        /// <returns></returns>
        [Space(10f)]
        [field: SerializeField] public UnityEvent<object> OnJumpEvent = new UnityEvent<object>();


        /// <summary>
        /// Constructor for the CharacterJumpController class.
        /// </summary>
        /// <param name="rigidbody2D">The character's Rigidbody2D component.</param>
        public CharacterJumpController(Rigidbody2D rigidbody2D)
        {
            m_Rigidbody2D = rigidbody2D;
            OnJumpEvent = new UnityEvent<object>();
        }

        public void SetRigidbody2D(Rigidbody2D rigidbody2D)
        {
            m_Rigidbody2D = rigidbody2D;
        }

        public void SetData(CharacterJumpData data)
        {
            Data = data;
        }

        /// <summary>
        /// Applies jump force to the character based on jump data.
        /// </summary>
        public void Jump(float direction = 1f)
        {
            if (Data == null) return;

            float force = Data.Force;
            OnJumpEvent.Invoke(this);

            m_Rigidbody2D.velocity = Vector2.zero;
            m_Rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            // Apply additional force to counteract downward velocity
            if (m_Rigidbody2D.velocity.y < 0f)
            {
                force -= m_Rigidbody2D.velocity.y;
            }

            m_Rigidbody2D.AddForce((Vector2.up * force) + (Vector2.right * direction * force), ForceMode2D.Impulse);
        }
    }
}
