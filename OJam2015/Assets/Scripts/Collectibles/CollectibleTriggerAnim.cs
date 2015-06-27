using UnityEngine;
using System.Collections;
using System;

namespace Collectibles
{
    public class CollectibleTriggerAnim : MonoBehaviour
    {
        public Action OnAnimationOver;

        public string TriggererTag = "Player";
        public float Duration = 0.5f;

        // If set to false, it must be managed in another script, otherwise the gameobject will stay on the screen and it's going to take unnecessary memory
        public bool DestroyAfter = true;

        public Vector2 HUDViewportPosition = new Vector2(0.8f, 0.8f);
        public Vector3 TargetScale = new Vector3(1f, 1f, 1f);

        private Collider2D _collider2D;

        void Awake()
        {
            _collider2D = this.GetComponent<Collider2D>();
        }

        void OnTriggerEnter2D(Collider2D triggeredCollider)
        {
            if (triggeredCollider.tag == TriggererTag)
            {
                StartCoroutine(MoveToHUD());
            }
        }

        private IEnumerator MoveToHUD()
        {
            _collider2D.enabled = false;

            Vector3 startPosition = transform.position;

            Vector3 startScale = transform.localScale;

            float ratio = 0f;

            while (ratio < 1f)
            {
                // Here, we use Camera.main (any camera tagged as MainCamera) instead of Camera.current since it's unreliable in many ways

                /*
                ViewportToWorldPoint receives a Vector3 argument where x and y are the screen coordinates, and z is the distance from the camera.
                Since HUDViewportPosition doesn't have a z coordinate, what you're getting is the camera position. The viewport element position in
                the 2D screen corresponds to a line in the 3D world passing through the camera center and the viewport element, thus you must
                somehow select which point in this line you're interested in - that's why you must pass the distance from the camera in z.*/

                Vector3 endPosition = Camera.main.ViewportToWorldPoint(new Vector3(HUDViewportPosition.x, HUDViewportPosition.y, Mathf.Abs(Camera.main.transform.position.z - startPosition.z)));

                ratio += Time.deltaTime / Duration;

                transform.position = Vector3.Lerp(startPosition, endPosition, ratio);
                transform.localScale = Vector3.Lerp(startScale, TargetScale, ratio);

                yield return null;
            }

            if (OnAnimationOver != null)
            {
                OnAnimationOver();
            }

            if (DestroyAfter)
            {
                Destroy(this.gameObject);
            }
        }
    }
}