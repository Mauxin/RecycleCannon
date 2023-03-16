namespace Scripts.PlayerSystem
{
    public class CityWallController : ADamageableCharacter
    {
        public static event OnLifeUpdate onLifeUpdate;
        public static event OnDied onDied;

        protected override void Died()
        {
            onDied();
        }

        protected override void LifeUpdate(float percentage)
        {
            onLifeUpdate(percentage);
        }
    }
}
