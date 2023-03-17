using System.Collections;
using Scripts.Cannon;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Enemies
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] Rigidbody _body;
        [SerializeField] Slider _lifeSlider;
        [SerializeField] EnemyData _enemyData;

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
            _lifeSlider.value = 1;
            _lifeSlider.gameObject.SetActive(false);
            wall = GameObject.Find(UtilsConstants.WALL_NAME);
            followingTransform = wall.transform;
            CannonBallController.onEnemyHit += damageTaken;
            StartCoroutine(DropRandomTrash());
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
            CannonBallController.onEnemyHit -= damageTaken;   
        }

        private void Update()
        {
            if (followingTransform == null) return;

            movement = FollowDirection();
            rotation = Quaternion.LookRotation(FollowDirection());
        }

        void FixedUpdate()
        {
            _body.MovePosition(_body.position +
                movement * _enemyData.MoveSpeed * Time.deltaTime);
            _body.MoveRotation(rotation);
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

        void damageTaken(GameObject hit)
        {
            if (gameObject != hit) return;
            if (AmmoController.SelectedType != _enemyData.DeadlyAmmo) return;

            currentLives--;
            _lifeSlider.value = (float)currentLives / (float)MAX_LIVES;
            _lifeSlider.gameObject.SetActive(true);

            if (currentLives <= 0)
            {
                DropTrash();
                onDie(gameObject);
            }
        }

        IEnumerator DropRandomTrash()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(30, 36));
                DropTrash();
            }
        }

        void DropTrash()
        {
            Instantiate(_enemyData.RandomTrashBag(),
                transform.position, transform.rotation);
        }

        Vector3 FollowDirection()
        {
            if (followingTransform == null) return Vector3.zero;

            return (followingTransform.position - _body.position).normalized;
        }
    }
}
