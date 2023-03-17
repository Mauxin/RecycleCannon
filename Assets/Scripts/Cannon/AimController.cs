using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Cannon
{
    public class AimController : MonoBehaviour
    {
        [SerializeField] LineRenderer _lineRenderer;
        [Range(0,100)]
        [SerializeField] int _lineSegmentCount;

        List<Vector3> _linePoints = new List<Vector3>();

        public static AimController Instance;

        private void Awake()
        {
            Instance = this;
        }

        public void DrawAim(Vector3 forceVector, float mass, Vector3 startPoint)
        {
            Vector3 velocity = (forceVector / mass) * Time.fixedDeltaTime;
            float flightTime = (2 * startPoint.y) / Physics.gravity.y;
            float stepTime = flightTime / _lineSegmentCount;

            _linePoints.Clear();

            for (int i = 0; i < _lineSegmentCount; i++)
            {
                float stepTimePassed = stepTime * i;

                Vector3 movementVector = new Vector3(
                    velocity.x * stepTimePassed,
                    (float)(velocity.y * stepTimePassed - 0.5 * Physics.gravity.y * stepTimePassed * stepTimePassed),
                    velocity.z * stepTimePassed);

                _linePoints.Add(-movementVector + startPoint);
            }

            _lineRenderer.positionCount = _linePoints.Count;
            _lineRenderer.SetPositions(_linePoints.ToArray());
        }
    }
}
