using UnityEngine;

public class CannonController : MonoBehaviour
{
    [SerializeField] FixedJoystick _inputController;
    [SerializeField] Rigidbody _body;

    Quaternion rotation = Quaternion.identity;

    public Rigidbody Body => _body;

    void Update()
    {
        rotation.eulerAngles =
            new Vector3(0,
            -Vector2.SignedAngle(Vector2.up, _inputController.Direction), 0);
    }

    void FixedUpdate()
    {
        _body.MoveRotation(rotation);
    }

}