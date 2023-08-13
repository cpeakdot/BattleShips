using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BattleShips.Tiles;
using BattleShips.Ships;
using UnityEngine.SceneManagement;

namespace BattleShips.Battle
{
    public class BattleManager : MonoBehaviour
    {
        private bool battleIsOver = false;
        private bool playersTurn = true;
        private bool playerHasShot = true;
        private float waitBeforeAttack = 1.5f;

        [Header("Refs")]
        [SerializeField] private TileManager tileManager;
        [SerializeField] private ShipManager shipManager;
        [SerializeField] private GameObject playerIndicator;
        [SerializeField] private GameObject enemyIndicator;
        [SerializeField] private GameObject shipWreckedMessage;
        [SerializeField] private GameObject shipDamagedMessage;
        [SerializeField] private GameObject infoMessagesParent;
        [SerializeField] private GameObject gameLostMessage;
        [SerializeField] private GameObject gameWonMessage;
        [SerializeField] private GameObject gameOverPanel;

        private int playerAliveShipCount;
        private int enemyAliveShipCount;

        private void HandleOnTurnChange()
        {
            if (playersTurn)
            {
                enemyIndicator.SetActive(false);
                playerIndicator.SetActive(true);
            }
            else
            {
                playerIndicator.SetActive(false);
                enemyIndicator.SetActive(true);
                StartCoroutine(AttackWaitSequence());
            }
        }

        private void Start() 
        {
            HandleOnTurnChange();

            Ship.OnShipWrecked += HandleOnShipWrecked;
            Ship.OnShipDamaged += HandleOnShipPartWrecked;

            playerAliveShipCount = enemyAliveShipCount = shipManager.GetShipCountPerPlayer();
        }

        private void Update() 
        {
            if(battleIsOver) { return; }
            // Player attacking
            if(!playersTurn || !playerHasShot) { return; }
            
            if(Input.GetMouseButtonDown(0))
            {
                Vector3 mousePosition = Input.mousePosition;
                mousePosition.z = Camera.main.nearClipPlane;
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                Vector2 worldPositionv2 = new Vector2(Mathf.RoundToInt(worldPosition.x), Mathf.RoundToInt(worldPosition.y));
                Tile selectedTile = tileManager.GetTile(worldPositionv2);
                PlayerTryShoot(selectedTile);
            }
        }

        private void PlayerTryShoot(Tile tile)
        {
            if (tileManager.IsTileOwnedByPlayer(tile)) { return; }
            if(tile.GetTileStatus() != TileStatus.NoEvent) { return; }

            playerHasShot = false;

            PlayerAttackTile(tile);
        }

        private void PlayerAttackTile(Tile tile)
        {

            if(tile.TryAttackThisTile())
            {
                StatisticHandler.PlayerHitAShot();
            }
            else
            {
                StatisticHandler.PlayerMissedAShot();
            }

            playersTurn = false;
            HandleOnTurnChange();
        }

        private void EnemyTryShoot()
        {
            Tile tileToAttack = null;

            while (tileToAttack == null && !battleIsOver)
            {
                List<Tile> playerTiles = tileManager.GetPlayerTiles();
                Tile tempTile = playerTiles[UnityEngine.Random.Range(0, playerTiles.Count)];
                if(tempTile.GetTileStatus() == TileStatus.NoEvent)
                {
                    tileToAttack = tempTile;
                }
            }

            EnemyAttackTile(tileToAttack);
        }

        private void EnemyAttackTile(Tile tile)
        {
            if(battleIsOver) { return; }

            if(tile.TryAttackThisTile())
            {
                StatisticHandler.EnemyHitAShot();
            }
            else
            {
                StatisticHandler.EnemyMissedAShot();
            }
            StartCoroutine(PlayerAttackWaitSequence());
        }

        private void HandleOnShipWrecked(Ship ship)
        {
            StartCoroutine(DisplayShipWreckedMessage());

            bool shipBelongsToPlayer = false;

            foreach (Ship s in shipManager.GetPlayerShips())
            {
                if(s == ship)
                {
                    shipBelongsToPlayer = true;
                    break;
                }
            }

            if(shipBelongsToPlayer)
            {
                playerAliveShipCount--;
            }
            else
            {
                enemyAliveShipCount--;
            }

            if(playerAliveShipCount == 0)
            {
                HandleOnGameLost();
            }
            else if(enemyAliveShipCount == 0)
            {
                HandleOnGameWon();
            }
        }

        private void HandleOnShipPartWrecked()
        {
            StartCoroutine(DisplayShipDamagedMessage());
        }

        private IEnumerator DisplayShipWreckedMessage()
        {
            float duration = 1f;
            shipWreckedMessage.SetActive(true);
            yield return new WaitForSeconds(duration);
            shipWreckedMessage.SetActive(false);
        }  

        private IEnumerator DisplayShipDamagedMessage()
        {
            float duration = 1f;
            shipDamagedMessage.SetActive(true);
            yield return new WaitForSeconds(duration);
            shipDamagedMessage.SetActive(false);
        }  

        private void HandleOnGameLost()
        {
            battleIsOver = true;
            StartCoroutine(EndGameSequence());
            infoMessagesParent.SetActive(false);
            gameOverPanel.SetActive(true);
            gameLostMessage.SetActive(true);
        }

        private void HandleOnGameWon()
        {
            battleIsOver = true;
            StartCoroutine(EndGameSequence());
            infoMessagesParent.SetActive(false);
            gameOverPanel.SetActive(true);
            gameWonMessage.SetActive(true);
        }

        #region Sequences
        IEnumerator AttackWaitSequence()
        {
            yield return new WaitForSeconds(waitBeforeAttack);
            EnemyTryShoot();
        }

        IEnumerator PlayerAttackWaitSequence()
        {
            yield return new WaitForSeconds(waitBeforeAttack);
            playerHasShot = true;
            playersTurn = true;
            HandleOnTurnChange();
        }

        IEnumerator EndGameSequence()
        {
            float duration = 1f;
            yield return new WaitForSeconds(duration);
            StatisticHandler.SetStatistics();
        }
        #endregion

        /// <summary>
        /// Replay button action.
        /// </summary>
        public void Replay()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}

