using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 1f;
    [SerializeField] Rigidbody _body;
    [SerializeField] Animator _animator;

    const string PLAYER_TAG = "Player";
    const string WALL_NAME = "CityWall";

    Vector3 movement = Vector3.zero;
    Quaternion rotation = Quaternion.identity;
    Transform followingTransform;
    GameObject wall;

    private void Start()
    {
        wall = GameObject.Find(WALL_NAME);
        followingTransform = wall.transform;
    }

    void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag(PLAYER_TAG)) return;

        followingTransform = other.transform;
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(PLAYER_TAG)) return;

        followingTransform = wall.transform;
    }

    private void Update()
    {
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
        return (followingTransform.position - _body.position).normalized;
    }
}
