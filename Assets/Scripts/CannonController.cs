using System.Diagnostics;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    [SerializeField] FixedJoystick _inputController;
    [SerializeField] Rigidbody _body;
    [SerializeField] GameObject _cannonBall;

    Quaternion rotation = Quaternion.identity;
    Stopwatch fireTimer = new Stopwatch();

    public Rigidbody Body => _body;
    public FixedJoystick Joystick => _inputController;

    private void Awake()
    {
        fireTimer.Reset();
    }

    void Update()
    {
        rotation.eulerAngles =
            new Vector3(0,
            -Vector2.SignedAngle(Vector2.up, _inputController.Direction), 0);


        VerifyFireInput();
    }

    void FixedUpdate()
    {
        _body.MoveRotation(rotation);
    }

    void VerifyFireInput()
    {
            if (Input.touchCount > 0 &
            (fireTimer.ElapsedMilliseconds >= 1000 || !fireTimer.IsRunning) )
            {
                foreach (var touch in Input.touches)
                {
                    if (touch.position.x < Screen.width / 2
                        & touch.position.y > Screen.height/3)
                    {
                        FireCannon();
                    }
                }
            }
    }

    void FireCannon()
    {
        fireTimer.Reset();

        Instantiate(_cannonBall, transform.position, transform.rotation);

        fireTimer.Start();
    }
}