using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 1f;
    [SerializeField] Rigidbody _body;
    [SerializeField] Animator _animator;
    [SerializeField] GameObject _trashBag;

    const int MAX_LIVES = 3;

    Vector3 movement = Vector3.zero;
    Quaternion rotation = Quaternion.identity;
    Transform followingTransform;
    GameObject wall;
    int currentLives = MAX_LIVES;

    public delegate void OnDie(GameObject dead);
    public static event OnDie onDie;

    private void Start()
    {
        wall = GameObject.Find(UtilsConstants.WALL_NAME);
        followingTransform = wall.transform;
        CannonBallController.onEnemyHit += damageTaken;
    }

    private void OnDestroy()
    {
        CannonBallController.onEnemyHit -= damageTaken;
    }

    void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag(UtilsConstants.PLAYER_TAG)) return;

        followingTransform = other.transform;
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(UtilsConstants.PLAYER_TAG)) return;

        followingTransform = wall.transform;
    }

    private void Update()
    {
        if (followingTransform == null) return;

        movement = FollowDirection();
        rotation = Quaternion.LookRotation(FollowDirection());
    }

    void FixedUpdate()
    {
        _body.MovePosition(_body.position + movement * _moveSpeed * Time.deltaTime);
        _body.MoveRotation(rotation);
    }

    Vector3 FollowDirection()
    {
        if (followingTransform == null) return Vector3.zero;

        return (followingTransform.position - _body.position).normalized;
    }

    void damageTaken(GameObject hit) {

        if (gameObject != hit) return;

        currentLives--;

        if (currentLives < 0)
        {
            Instantiate(_trashBag, transform.position, transform.rotation);
            onDie(gameObject);
        }
    }
}
