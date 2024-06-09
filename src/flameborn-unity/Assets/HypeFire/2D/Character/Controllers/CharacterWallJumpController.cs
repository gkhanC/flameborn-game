using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace HF.TwoD.Character
{
    /// <summary>
    /// This class handles character wall jump logic and applies force based on provided wall jump data.
    /// </summary>
    [System.Serializable]
    public class CharacterWallJumpController
    {
        /// <summary>
        /// Reference to the character's Rigidbody2D component.
        /// </summary>
        [SerializeField][Required] private Rigidbody2D m_Rigidbody2D;

        /// <summary>
        /// Data object containing wall jump parameters.
        /// </summary>
        [field: SerializeField][Required("Data object is required")] public CharacterWallJumpData Data { get; private set; } = null;

        /// <summary>
        /// On jump event distributor
        /// </summary>
        /// <typeparam name="object">sender</typeparam>
        /// <returns></returns>
        [Space(10f)]
        [field: SerializeField] public UnityEvent<object> OnJumpEvent = new UnityEvent<object>();


        /// <summary>
        /// Constructor for the CharacterWallJumpController class.
        /// </summary>
        /// <param name="rigidbody2D">The character's Rigidbody2D component.</param>
        public CharacterWallJumpController(Rigidbody2D rigidbody2D)
        {
            m_Rigidbody2D = rigidbody2D;
            OnJumpEvent = new UnityEvent<object>();
        }

        public void SetRigidbody2D(Rigidbody2D rigidbody2D)
        {
            m_Rigidbody2D = rigidbody2D;
        }

        public void SetData(CharacterWallJumpData data)
        {
            Data = data;
        }

        /// <summary>
        /// Applies wall jump force to the character based on wall jump data and facing direction.
        /// </summary>
        /// <param name="facingDirection">The direction the character is facing (-1 for left, 1 for right).</param>
        public void Jump(int facingDirection)
        {
            if (Data == null) return;

            Vector2 jumpForce = new Vector2(Data.JumpForce.x * facingDirection, Data.JumpForce.y);
            OnJumpEvent.Invoke(this);

            m_Rigidbody2D.velocity = Vector2.zero;
            m_Rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            
            // Counteract existing horizontal velocity
            if (Mathf.Sign(m_Rigidbody2D.velocity.x) != Mathf.Sign(jumpForce.x))
            {
                jumpForce.x -= m_Rigidbody2D.velocity.x;
            }

            // Counteract downward velocity
            if (m_Rigidbody2D.velocity.y < 0f)
            {
                jumpForce.y -= m_Rigidbody2D.velocity.y;
            }

            m_Rigidbody2D.AddForce(jumpForce, ForceMode2D.Impulse);
        }
    }
}
