using UnityEngine;
using System.Collections.Generic;
using BattleShips.Tiles;
using BattleShips.Ships;

namespace BattleShips.Battle
{
    public class BattleManager : MonoBehaviour
    {
        private bool battleIsOver = false;
        [SerializeField] private bool playersTurn = true;
        private bool playerHasShot = true;

        [Header("Refs")]
        [SerializeField] private TileManager tileManager;
        [SerializeField] private GameObject playerIndicator;
        [SerializeField] private GameObject enemyIndicator;

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
                EnemyTryShoot();
            }
        }

        private void Start() 
        {
            HandleOnTurnChange();

            Ship.OnShipWrecked += HandleOnShipWrecked;
        }

        private void Update() 
        {
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
            tile.TryAttackThisTile();
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
            tile.TryAttackThisTile();
            playerHasShot = true;
            playersTurn = true;
            HandleOnTurnChange();
        }

        private void HandleOnShipWrecked(Ship ship)
        {
            Debug.Log("Ship wrecked!");
        }
    }
}

