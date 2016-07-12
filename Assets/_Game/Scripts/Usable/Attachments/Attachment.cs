using UnityEngine;
using UnityEngine.Events;

namespace BIG.IncredibleCityCrisis
{

    /// <summary>
    /// A Usable that can attach to a Body's AttachmentPoint, such as a gun 
    /// or piece of armor.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class Attachment : Usable
    {

        [Tooltip("Attach to this location on the character.")]
        public AttachmentLocation attachmentLocation = AttachmentLocation.LeftHand;

        public UnityEvent onAttach = new UnityEvent();

        public UnityEvent onDetach = new UnityEvent();

        /// <summary>
        /// Called when a character uses this Attachment. Attaches to the character's
        /// corresponding attachment point.
        /// </summary>
        /// <param name="character">The character to attach the </param>
        public override void UseBy(GameObject user)
        {
            foreach (var attachmentPoint in user.GetComponentsInChildren<AttachmentPoint>())
            { 
                if (attachmentPoint.location == attachmentLocation)
                {
                    // If attachment point already has something, drop it first:
                    var existingAttachment = attachmentPoint.GetComponentInChildren<Attachment>();
                    if (existingAttachment != null) existingAttachment.Detach();

                    // Disable trigger collider since it's been picked up:
                    var collider = GetComponent<Collider2D>();
                    collider.enabled = false;

                    // Parent the attachment to the attachment point:
                    transform.SetParent(attachmentPoint.transform);
                    transform.localPosition = Vector3.zero;
                    transform.localRotation = Quaternion.identity;
                    transform.localScale = Vector3.one;

                    // Tell the attachment that it has a player.
                    var body = attachmentPoint.GetComponentInParent<Body>();
                    if ((body != null) && (body.player != null))
                    {
                        SendMessage(Messages.OnAttachPlayer, body.player, SendMessageOptions.DontRequireReceiver);
                    }

                    // Invoke the onAttach event, which designers can use to run extra
                    // actions such as playing audio or animation:
                    onAttach.Invoke();
                }
            }
        }

        public void Detach()
        {
            // Tell the attachment that it no longer has a player.
            var body = GetComponentInParent<Body>();
            if ((body != null) && (body.player != null))
            {
                SendMessage(Messages.OnDetachPlayer, SendMessageOptions.DontRequireReceiver);
            }

            // Unparent the attachment:
            transform.SetParent(null);

            // Re-enable trigger collider to make it available for pickup:
            var collider = GetComponent<Collider2D>();
            collider.enabled = true;

            // Invoke the onDetach event, which designers can use to run extra
            // actions such as playing audio or animation:
            onDetach.Invoke();
        }

        public void OnDie(int fromPlayerNumber)
        {
            Detach();
        }

    }

}