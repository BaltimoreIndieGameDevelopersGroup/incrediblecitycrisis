using UnityEngine;

namespace BIG.IncredibleCityCrisis
{

    /// <summary>
    /// Unity layers SpriteRenderers in the order specified by their sortingOrder values.
    /// To simulate depth, this script adjusts sortingOrders according to the GameObject's
    /// Y position.
    /// </summary>
    public class SpriteOrderByYPosition : MonoBehaviour
    {

        // All SpriteRenderers on this GameObject and its children:
        private SpriteRenderer[] m_spriteRenderers;

        // Any SpriteOrderOffsets on those SpriteRenderers to fine-tune sortingOrder:
        private SpriteOrderOffset[] m_spriteOrderOffsets;

        // Remember last sortingOrder value so we only update SpriteRenderers if it changes:
        private int m_lastOrder;

        private void Start()
        {
            RefreshSpriteList();
        }

        public void RefreshSpriteList()
        {
            m_spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
            m_spriteOrderOffsets = new SpriteOrderOffset[m_spriteRenderers.Length];
            for (int i = 0; i < m_spriteRenderers.Length; i++)
            {
                m_spriteOrderOffsets[i] = m_spriteRenderers[i].GetComponent<SpriteOrderOffset>();
            }
            m_lastOrder = -1;
        }

        private void Update() // Runs every frame.
        {
            var order = (int)(transform.position.y * -100) + 1000;
            if (order != m_lastOrder)
            {
                m_lastOrder = order;
                for (int i = 0; i < m_spriteRenderers.Length; i++)
                {
                    var offset = (m_spriteOrderOffsets[i] != null) ? m_spriteOrderOffsets[i].offset : 0;
                    m_spriteRenderers[i].sortingOrder = order + offset;
                }
            }
        }

    }

}
