using UnityEngine;

namespace BIG.IncredibleCityCrisis
{

    public class AttachOnStart : MonoBehaviour
    {

        public void Start()
        {
            var attachment = GetComponent<Attachment>();
            var body = GetComponentInParent<Body>();
            attachment.UseBy(body);
        }

    }

}