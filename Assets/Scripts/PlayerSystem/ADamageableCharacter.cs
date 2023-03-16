using UnityEngine;

namespace Scripts.PlayerSystem
{
    public abstract class ADamageableCharacter : MonoBehaviour
    {
        [SerializeField] protected int _maxLives = 3;

        protected int currentLife;

        public delegate void OnLifeUpdate(float percentage);
        public delegate void OnDied();

        private void Awake()
        {
            currentLife = _maxLives;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag(UtilsConstants.ENEMY_TAG))
            {
                TakeDamage();
            }
        }

        protected virtual void TakeDamage()
        {
            currentLife--;
            float percentage = (float)currentLife / (float)_maxLives;
            LifeUpdate(percentage);

            if (currentLife < 0)
            {
                Destroy(gameObject);
                Died();
            }
        }

        protected abstract void LifeUpdate(float percentage);

        protected abstract void Died();
    }
}