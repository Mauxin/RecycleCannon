using Scripts.HordeSystem;

namespace Scripts.PlayerSystem
{
    public class CityWallController : ADamageableCharacter
    {
        public static event OnLifeUpdate onLifeUpdate;
        public static event OnDied onDied;

        const int LIFE_HORDE_RECOVER = 10;

        private void Start()
        {
            HordeController.onHordeStart += OnHordeStart;
        }

        private void OnDestroy()
        {
            HordeController.onHordeStart -= OnHordeStart;
        }

        protected override void Died()
        {
            onDied();
        }

        protected override void LifeUpdate(float percentage)
        {
            onLifeUpdate(percentage);
        }

        void OnHordeStart(bool isInterval, int duration, int horde)
        {
            if (!isInterval) return;

            if (currentLife > _maxLives/2)
            {
                currentLife = _maxLives;
                LifeUpdate(1);
                return;
            }

            currentLife += LIFE_HORDE_RECOVER;
            LifeUpdate((float)currentLife / (float)_maxLives);
        }
    }
}
