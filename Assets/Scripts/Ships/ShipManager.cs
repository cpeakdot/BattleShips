using UnityEngine;
using System.Collections;
using BattleShips.Tiles;

namespace BattleShips.Ships
{
    public class ShipManager : MonoBehaviour
    {
        private const int SHIP_PER_PLAYER = 5;

        private Vector2[] DIRECTIONS = new Vector2[]{
            Vector2.up,
            Vector2.down,
            Vector2.right,
            Vector2.left
        };

        [SerializeField] private TileManager tileManager;
        [SerializeField] private GameObject[] shipPartPrefabs = new GameObject[SHIP_PER_PLAYER];

        private void Start() 
        {
            StartCoroutine(SetPlayerShips());    
        }

        private IEnumerator SetPlayerShips()
        {
            for (int i = 0; i < SHIP_PER_PLAYER; i++)
            {
                CreateShip(i + 1);
                yield return null;
            }
        }

        private void CreateShip(int partAmount, bool isPlayer = true)
        {
            Tile startingTile = null;
            while (startingTile == null)
            {
                Tile tile = isPlayer
                ? tileManager.GetPlayerTiles()[UnityEngine.Random.Range(0, tileManager.GetPlayerTileCount)]
                : tileManager.GetEnemyTiles()[UnityEngine.Random.Range(0, tileManager.GetPlayerTileCount)];

                Vector2 direction = DIRECTIONS[UnityEngine.Random.Range(0, DIRECTIONS.Length)];

                bool canBePlaced = true;

                for (int i = 0; i < partAmount; i++)
                {
                    Vector2 nextTilePosition = tile.GetPosition() + (direction * i);

                    Tile tileToCheck = tileManager.GetTile(nextTilePosition);

                    canBePlaced = tileToCheck != null 
                    && !tileToCheck.HasShipPart() 
                    && (isPlayer == tileToCheck.IsOwnerPlayer());

                    if(!canBePlaced)
                    {
                        break;
                    }
                }   

                if(canBePlaced)
                {
                    startingTile = tile;

                    for (int i = 0; i < partAmount; i++)
                    {
                        Tile tileToAddShip = tileManager.GetTile((direction * i) + startingTile.GetPosition());

                        GameObject shipPart = Instantiate(shipPartPrefabs[partAmount - 1], (direction * i) + startingTile.GetPosition(), Quaternion.identity);

                        ShipPartHandler shipPartHandler = shipPart.GetComponent<ShipPartHandler>();

                        tileToAddShip.SetShipPart(shipPartHandler);

                        shipPartHandler.Tile = tileToAddShip;
                    }
                }
            }
        }
    }
}

