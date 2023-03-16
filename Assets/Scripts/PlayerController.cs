using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 1f;
    [SerializeField] int _maxLives = 3;
    [SerializeField] FixedJoystick _inputController;
    [SerializeField] Rigidbody _body;
    [SerializeField] Animator _animator;
    [SerializeField] Transform _trashAttach;

    Vector3 movement = Vector3.zero;
    Quaternion rotation = Quaternion.identity;
    GameObject carriedTrashBag = null;
    int currentLife;

    public delegate void OnLifeUpdate(float percentage);
    public static event OnLifeUpdate onLifeUpdate;
    public delegate void OnTashDelivered(TrashType type);
    public static event OnTashDelivered onTashDelivered;
    public delegate void OnPlayerDied();
    public static event OnPlayerDied onPlayerDied;

    private void Awake()
    {
        currentLife = _maxLives;
    }

    void Update()
    {
        movement.x = _inputController.Horizontal != 0 ?
            _inputController.Horizontal :
            Input.GetAxisRaw(UtilsConstants.INPUT_HORIZONTAL);

        movement.z = _inputController.Vertical != 0 ?
            _inputController.Vertical :
            Input.GetAxisRaw(UtilsConstants.INPUT_VERTICAL);

        rotation.eulerAngles =
            new Vector3(0,
            -Vector2.SignedAngle(Vector2.up, _inputController.Direction), 0);
    }

    void FixedUpdate()
    {
        _animator.SetBool(UtilsConstants.PLAYER_WALK_ANIMATION, movement == Vector3.zero);
        _animator.SetFloat(UtilsConstants.PLAYER_SPEED_ANIMATION, movement.magnitude*3);
        if (movement == Vector3.zero) return;

        _body.MovePosition(_body.position + movement * _moveSpeed * Time.deltaTime);
        _body.MoveRotation(rotation);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(UtilsConstants.ENEMY_TAG))
        {
            TakeDamage();
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(UtilsConstants.TRASH_BAG_TAG)) {
            VerifyCatchInput(other.transform);
        }

        if (other.CompareTag(UtilsConstants.TRASH_CAN_TAG))
        {
            VerifyThrowInput(other.transform);
        }
    }

    void TakeDamage()
    {
        currentLife--;
        float percentage = (float) currentLife / (float) _maxLives;
        onLifeUpdate(percentage);

        if (currentLife < 0)
        {
            Destroy(gameObject);
            onPlayerDied();
        }
    }

    void VerifyCatchInput(Transform trashBag)
    {
        if (Input.touchCount > 0 && carriedTrashBag == null)
        {
            foreach (var touch in Input.touches)
            {
                if (touch.position.x > Screen.width / 2
                    & touch.position.y > Screen.height / 3)
                {
                    trashBag.SetParent(_trashAttach);
                    trashBag.localPosition = Vector3.zero;
                    carriedTrashBag = trashBag.gameObject;
                }
            }
        }
    }

    void VerifyThrowInput(Transform trashCan)
    {
        if (Input.touchCount > 0 && carriedTrashBag != null)
        {
            foreach (var touch in Input.touches)
            {
                if (touch.position.x > Screen.width / 2
                    & touch.position.y > Screen.height / 3)
                {
                    ThrowTrash(trashCan.gameObject);
                }
            }
        }
    }

    void ThrowTrash(GameObject trashCan)
    {
        var trashBin = trashCan.GetComponent<TrashController>();
        var trash = carriedTrashBag.GetComponent<TrashController>();

        if (trashBin.Type == trash.Type)
        {
            onTashDelivered(trash.Type);

            Destroy(carriedTrashBag);
            carriedTrashBag = null;
        }
        else
        {
            Debug.Log("Wrong Trash Can");
        }
    }
}