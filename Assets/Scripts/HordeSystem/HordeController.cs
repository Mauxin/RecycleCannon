using System.Collections;
using System.Diagnostics;
using Scripts.PlayerSystem;
using UnityEngine;

namespace Scripts.HordeSystem
{
    public class HordeController : MonoBehaviour
    {
        int hordeDurationSeconds;
        int hordeIntervalSeconds;
        int hordeAmount;
        int currentHorde = 0;
        bool gameStarted = false;
        bool isInterval = false;
        Stopwatch hordeTimer = new Stopwatch();

        public delegate void OnHordeStart(bool isInterval, int duration, int horde);
        public static event OnHordeStart onHordeStart;

        public delegate void OnGameWin();
        public static event OnGameWin onGameWin;

        private void Awake()
        {
            PlayerController.onDied += OnGameOver;
            CityWallController.onDied += OnGameOver;
        }

        private void OnDestroy()
        {
            PlayerController.onDied -= OnGameOver;
            CityWallController.onDied -= OnGameOver;
        }

        void Update()
        {
            VerifyTime();
        }

        public void Setup(int hordeDuration, int hordeInterval, int amount)
        {
            hordeDurationSeconds = hordeDuration;
            hordeIntervalSeconds = hordeInterval;
            hordeAmount = amount;

            StartCoroutine(WaitNextFrame());
        }

        IEnumerator WaitNextFrame()
        {
            yield return new WaitForSeconds(1);

            isInterval = true;
            hordeTimer.Start();
            gameStarted = true;
            onHordeStart(isInterval, hordeIntervalSeconds, currentHorde);
        }

        void VerifyTime()
        {
            if (gameStarted)
            {
                if (isInterval)
                {
                    if (RemainingTime(hordeIntervalSeconds) <= 0)
                    {
                        StartNextHorde();
                    }
                }
                else
                {
                    if (RemainingTime(hordeDurationSeconds) <= 0)
                    {
                        StartInterval();
                    }
                }
            }
        }

        void StartNextHorde()
        {
            isInterval = false;
            currentHorde++;

            if (currentHorde > hordeAmount)
            {
                gameStarted = false;
                onGameWin();
                return;
            }

            hordeTimer.Reset();
            hordeTimer.Start();
            onHordeStart(isInterval, hordeDurationSeconds, currentHorde);
        }

        void StartInterval()
        {
            isInterval = true;
            hordeTimer.Reset();
            hordeTimer.Start();
            onHordeStart(isInterval, hordeIntervalSeconds, currentHorde);
        }

        int RemainingTime(int maxTime)
        {
            return Mathf.FloorToInt( maxTime - (hordeTimer.ElapsedMilliseconds / 1000));
        }

        void OnGameOver()
        {
            hordeTimer.Stop();
            gameStarted = false;
        }
    }
}
