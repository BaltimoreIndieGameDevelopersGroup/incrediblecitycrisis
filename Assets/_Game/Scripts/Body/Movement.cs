using UnityEngine;

namespace BIG.IncredibleCityCrisis
{

    /// <summary>
    /// This component handles basic character movement. It processes input from virtual
    /// input into movement and animation.
    /// </summary>
    public class Movement : MonoBehaviour
    {

        [Tooltip("The fastest the player can travel left and right.")]
        public float maxHorizontalSpeed = 8f;

        [Tooltip("The fastest the player can travel up and down.")]
        public float maxVerticalSpeed = 5f;

        [Tooltip("Tracks which direction the player is facing.")]
        public bool facingLeft = true;

        public bool aim { get; set; }

        public Vector3 aimDirection { get; private set; }

        private Rigidbody2D m_rigidbody2D;
        private Animator m_animator;
        private VirtualInput m_input;

        public virtual void OnAttachPlayer(Player player)
        {
            m_input = player.virtualInput;
            aim = false;
        }

        public void OnDetachPlayer()
        {
            m_input = GetComponent<VirtualInput>();
            UpdateAnimator(Vector2.zero);
        }

        protected virtual void Awake()
        {
            m_rigidbody2D = GetComponent<Rigidbody2D>();
            m_animator = GetComponent<Animator>();
            m_input = GetComponent<VirtualInput>();
            if (m_rigidbody2D == null) Debug.LogError("No Rigidbody2D found on " + name, this);
            if (m_animator == null) Debug.LogError("No Animator found on " + name, this);
        }

        protected virtual void FixedUpdate() // Runs every physics update.
        {
            if (m_input == null) return;

            // Physically move the character:
            var move = new Vector2(m_input.move.x * maxHorizontalSpeed, m_input.move.y * maxVerticalSpeed);
            m_rigidbody2D.velocity = move;

            // Update the animator:
            UpdateAnimator(move);

            // Flip the character if necessary:
            var needToFlip = ((move.x < 0 && !facingLeft) || (move.x > 0 && facingLeft));
            if (needToFlip)
            {
                facingLeft = !facingLeft;
                Vector3 theScale = transform.localScale;
                theScale.x *= -1; // Invert the character's X scale to flip.
                transform.localScale = theScale;
            }
        }

        private void UpdateAnimator(Vector2 move)
        {
            if (m_animator == null || m_input == null) return;
            var horizontalSpeed = Mathf.Abs(move.x);
            var verticalSpeed = Mathf.Abs(move.y);
            var speed = Mathf.Max(horizontalSpeed, verticalSpeed);
            m_animator.SetFloat("HorizontalSpeed", horizontalSpeed);
            m_animator.SetFloat("VerticalSpeed", verticalSpeed);
            m_animator.SetFloat("Speed", speed);
            aimDirection = ((m_input.move.magnitude > 0.1f) ? m_input.move : new Vector2(facingLeft ? -1 : 1, 0)).normalized;
            var angle = facingLeft ? Vector3.Angle(Vector3.left, aimDirection) : Vector3.Angle(Vector3.right, aimDirection);
            if (move.y < 0) angle = -angle;
            //if (!aim) angle = 0; [TODO] Aim only when weapon attachment says to aim.
            m_animator.SetFloat("AimAngle", angle);
        }

    }
}