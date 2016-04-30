using UnityEngine;
using System.Collections;

namespace BIG
{

    /// <summary>
    /// This component handles basic character movement. It processes input from a virtual
    /// controller into movement and animation.
    /// </summary>
    public class BasicCharacter2D : MonoBehaviour
    {

        [Tooltip("The fastest the player can travel left and right.")]
        public float maxHorizontalSpeed = 8f;

        [Tooltip("The fastest the player can travel up and down.")]
        public float maxVerticalSpeed = 5f;

        [Tooltip("Tracks which direction the player is facing.")]
        public bool facingLeft = true;

        private Rigidbody2D m_rigidbody2D;
        private Animator m_animator;
        private VirtualController m_controller;

        protected virtual void Awake()
        {
            m_rigidbody2D = GetComponent<Rigidbody2D>();
            m_animator = GetComponent<Animator>();
            m_controller = GetComponent<VirtualController>();
            if (m_rigidbody2D == null) Debug.LogError("No Rigidbody2D found on " + name, this);
            if (m_animator == null) Debug.LogError("No Animator found on " + name, this);
            if (m_controller == null) Debug.LogError("No VirtualController found on " + name, this);
        }

        protected virtual void FixedUpdate() // Runs every physics update.
        {
            // Physically move the character:
            var move = new Vector2(m_controller.move.x * maxHorizontalSpeed, m_controller.move.y * maxVerticalSpeed);
            m_rigidbody2D.velocity = move;

            // Update the animator:
            var horizontalSpeed = Mathf.Abs(move.x);
            var verticalSpeed = Mathf.Abs(move.y);
            var speed = Mathf.Max(horizontalSpeed, verticalSpeed);
            m_animator.SetFloat("HorizontalSpeed", horizontalSpeed);
            m_animator.SetFloat("VerticalSpeed", verticalSpeed);
            m_animator.SetFloat("Speed", speed);

            // Flip the character if necessary:
            var needToFlip = ((move.x < 0 && !facingLeft) || (move.x > 0 && facingLeft));
            if (needToFlip)
            {
                facingLeft = !facingLeft;
                Vector3 theScale = transform.localScale;
                theScale.x *= -1; // Invert the character's X scale.
                transform.localScale = theScale;
            }
        }

    }
}