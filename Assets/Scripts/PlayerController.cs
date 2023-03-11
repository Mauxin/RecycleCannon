using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 3f;
    [SerializeField] FixedJoystick _inputController;
    [SerializeField] Rigidbody _body;

    Vector3 movement = Vector3.zero;
    Quaternion rotation = Quaternion.identity;

    public Rigidbody Body => _body;

    void Update()
    {
        movement.x = _inputController.Horizontal;
        movement.z = _inputController.Vertical;
        rotation.eulerAngles =
            new Vector3(0,
            -Vector2.SignedAngle(Vector2.up, _inputController.Direction), 0);
    }

    void FixedUpdate()
    {
        if (movement == Vector3.zero) return;

        _body.MovePosition(_body.position + movement * _moveSpeed * Time.deltaTime);
        _body.MoveRotation(rotation);
    }

}