using UnityEngine;
using TMPro;

namespace BattleShips
{
    public class StatisticHandler : MonoBehaviour
    {
        public static StatisticHandler Instance { get; private set; }

        [SerializeField] private TMP_Text playerMissedShotsText;
        [SerializeField] private TMP_Text playerAccurateShotsText;

        [SerializeField] private TMP_Text enemyMissedShotsText;
        [SerializeField] private TMP_Text enemyAccurateShotsText;

        [SerializeField] private TMP_Text statisticsText;

        private int playerMissedShotCount;
        private int playerAccurateShotCount;

        private int enemyMissedShotCount;
        private int enemyAccurateShotCount;

        private float playTime;

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        private void Update()
        {
            playTime += Time.deltaTime;
        }

        public static void PlayerMissedAShot()
        {
            Instance.playerMissedShotCount++;
            Instance.playerMissedShotsText.text = $"Missed Shots: {Instance.playerMissedShotCount}";
        }

        public static void PlayerHitAShot()
        {
            Instance.playerAccurateShotCount++;
            Instance.playerAccurateShotsText.text = $"Accurate Shots: {Instance.playerAccurateShotCount}";
        }

        public static void EnemyMissedAShot()
        {
            Instance.enemyMissedShotCount++;
            Instance.enemyMissedShotsText.text = $"Missed Shots: {Instance.enemyMissedShotCount}";
        }

        public static void EnemyHitAShot()
        {
            Instance.enemyAccurateShotCount++;
            Instance.enemyAccurateShotsText.text = $"Accurate Shots: {Instance.enemyAccurateShotCount}";
        }

        public static void SetStatistics()
        {
            string playTime = (Instance.playTime / 60).ToString("0.0");
            Instance.statisticsText.text = 
                $"ACCURATE SHOTS: {Instance.playerAccurateShotCount} \n " +
                $"MISSED SHOTS: {Instance.playerMissedShotCount} \n " +
                $"PLAY TIME: {playTime} MINS";
        }

        public static int GetPlayerMissedShots() => Instance.playerMissedShotCount;
        public static int GetPlayerAccurateShots() => Instance.playerAccurateShotCount;
        public static int GetEnemyMissedShots() => Instance.enemyMissedShotCount;
        public static int GetEnemyAccurateShots() => Instance.enemyAccurateShotCount;
    }
}

