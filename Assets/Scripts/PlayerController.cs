using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 1f;
    [SerializeField] FixedJoystick _inputController;
    [SerializeField] Rigidbody _body;
    [SerializeField] Animator _animator;

    Vector3 movement = Vector3.zero;
    Quaternion rotation = Quaternion.identity;

    public Rigidbody Body => _body;

    void Update()
    {
        movement.x = _inputController.Horizontal != 0 ?
            _inputController.Horizontal : Input.GetAxisRaw("Horizontal");
        movement.z = _inputController.Vertical != 0 ?
            _inputController.Vertical : Input.GetAxisRaw("Vertical");
        rotation.eulerAngles =
            new Vector3(0,
            -Vector2.SignedAngle(Vector2.up, _inputController.Direction), 0);
    }

    void FixedUpdate()
    {
        _animator.SetBool("Walking", movement == Vector3.zero);
        _animator.SetFloat("Speed", movement.magnitude*3);
        if (movement == Vector3.zero) return;

        _body.MovePosition(_body.position + movement * _moveSpeed * Time.deltaTime);
        _body.MoveRotation(rotation);
    }

}