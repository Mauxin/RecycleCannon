using UnityEngine;

namespace Scripts.Cannon
{
    public class CannonBallController : MonoBehaviour
    {
        [SerializeField] float _moveSpeed = 100f;
        [SerializeField] Rigidbody _body;

        public delegate void OnEnemyHit(GameObject hit);
        public static event OnEnemyHit onEnemyHit;

        private void Start()
        {
            CannonController cannon = GameObject.Find(UtilsConstants.CANNON_NAME)
                .GetComponent<CannonController>();

            _body.AddForce(new Vector3(cannon.Joystick.Horizontal, 0, cannon.Joystick.Vertical) * _moveSpeed,
                ForceMode.Impulse);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag(UtilsConstants.ENEMY_TAG))
            {
                onEnemyHit(collision.body.gameObject);
                Destroy(gameObject);
                return;
            }

            if (collision.collider.CompareTag(UtilsConstants.ENVIRONMENT_TAG))
            {
                Destroy(gameObject);
                return;
            }
        }
    }
}
