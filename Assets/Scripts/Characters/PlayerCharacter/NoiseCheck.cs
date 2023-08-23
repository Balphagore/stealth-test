namespace StealthTest.Characters
{
    using UnityEngine;

    public class NoiseCheck : MonoBehaviour
    {
        [SerializeField]
        private PlayerCharacterView playerCharacterView;
        [SerializeField]
        private Transform checkTransform;
        [SerializeField]
        private SpriteRenderer spriteRenderer;
        [SerializeField]
        private CircleCollider2D circleCollider;
        [SerializeField]
        private bool isActive = true;

        [SerializeField]
        private float checkRadius = 5f;

        public void Switch()
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            isActive = !isActive;
        }

        private void OnValidate()
        {
            checkTransform.localScale = new Vector3(checkRadius, checkRadius, 1);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (isActive && other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                playerCharacterView.SpottingPlayer(other.GetComponentInParent<EnemyCharacterView>());
            }
        }
    }
}
