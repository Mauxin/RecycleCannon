using UnityEngine;

public class CityWallController : MonoBehaviour
{
    [SerializeField] int _maxLives = 20;

    int currentLife;

    public delegate void OnLifeUpdate(float percentage);
    public static event OnLifeUpdate onLifeUpdate;
    public delegate void OnWallDied();
    public static event OnWallDied onWallDied;

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

    void TakeDamage()
    {
        currentLife--;
        float percentage = (float)currentLife / (float)_maxLives;
        onLifeUpdate(percentage);

        if (currentLife < 0)
        {
            Destroy(gameObject);
            onWallDied();
        }
    }
}
