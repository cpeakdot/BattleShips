using UnityEngine;
using BattleShips.Tiles;

namespace BattleShips.Battle
{
    public class BattleManager : MonoBehaviour
    {
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
            }
        }

        private void Start() 
        {
            HandleOnTurnChange();
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
            }
        }

        private void PlayerTryShoot(Tile tile)
        {
            if (!tileManager.IsTileOwnedByPlayer(tile)) { return; }
            if(tile.GetTileStatus() == TileStatus.Missed) { return; }

            playerHasShot = false;
            
        }
    }
}

