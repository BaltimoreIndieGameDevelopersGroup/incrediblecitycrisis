using UnityEngine;

namespace BIG.IncredibleCityCrisis
{

    /// <summary>
    /// An attachment is a type of pickup that can attach to a character's attachment point.
    /// </summary>
    public class Attachment : Pickup
    {

        public AttachmentLocation requiredLocation = AttachmentLocation.LeftHand;

        public override void PickUpBy(GameObject user)
        {
            foreach (var attachmentPoint in user.GetComponentsInChildren<AttachmentPoint>())
            { 
                if (attachmentPoint.location == requiredLocation)
                {
                    // Disable trigger collider since it's been picked up:
                    var collider = GetComponent<Collider2D>();
                    collider.enabled = false;

                    // Parent the attachment to the attachment point:
                    transform.SetParent(attachmentPoint.transform);
                    transform.localPosition = Vector3.zero;
                    transform.localRotation = Quaternion.identity;

                    // Tell the attachment that it has a player.
                    var body = attachmentPoint.GetComponentInParent<Body>();
                    if (body != null && body.player != null)
                    {
                        SendMessage("OnAttachPlayer", body.player);
                    }
                }
            }
        }
    }

}