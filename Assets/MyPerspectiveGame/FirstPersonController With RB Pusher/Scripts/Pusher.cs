using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConAirGames
{
    public class Pusher : MonoBehaviour
    {
        [Tooltip("Push force applied to rigidbodies.")]
        public float pushPower = 2.0f;

        [Tooltip("Layer mask for objects that can be pushed.")]
        public LayerMask pushableLayers;

        [Tooltip("Enable to allow player to push rigidbodies in the pushableLayers layer")]
        public bool pushing = true;
        void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (pushing)
            {
                Rigidbody body = hit.collider.attachedRigidbody;

                if (body == null || body.isKinematic || !IsInLayerMask(body.gameObject, pushableLayers))
                {
                    return;
                }

                Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
                body.linearVelocity = pushDir * pushPower;
            }
        }

        private bool IsInLayerMask(GameObject obj, LayerMask layerMask)
        {
            return (layerMask.value & (1 << obj.layer)) > 0;
        }
    }
}
