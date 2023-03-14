using UnityEngine;

public class CannonBallController : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 100f;
    [SerializeField] Rigidbody _body;

    const string ENEMY_TAG = "Enemy";
    const string ENVIRONMENT_TAG = "Environment";
    const string CANNON_NAME = "Cannon";

    private void Start()
    {
        var cannon = GameObject.Find(CANNON_NAME).GetComponent<CannonController>();

        _body.AddForce(new Vector3(cannon.Joystick.Horizontal, 0, cannon.Joystick.Vertical) *_moveSpeed,
            ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(ENEMY_TAG)) {
            Destroy(collision.body.gameObject);
            Destroy(gameObject);
            return;
        }

        if (collision.collider.CompareTag(ENVIRONMENT_TAG))
        {
            Destroy(gameObject);
            return;
        }
    }
}
