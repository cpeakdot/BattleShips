using UnityEngine;
using System.Collections.Generic;

namespace BattleShips.Tiles
{
    public class TileManager : MonoBehaviour
    {
        [SerializeField] private GameObject tileObj;
        [SerializeField] private GameObject playerTileObj;

        [SerializeField] private int width;
        [SerializeField] private int height;
        [SerializeField] private float borderSize;

        private Tile[,] tileArrayAll;
        private List<Tile> tileListPlayer;
        private List<Tile> tileListEnemy;

        private int totalTileAmount;
        private int playerTileAmount;

        public int GetPlayerTileCount => playerTileAmount;

        private void Awake() 
        {
            totalTileAmount = width * height;
            playerTileAmount = totalTileAmount / 2;

            tileListEnemy = new (playerTileAmount);
            tileListPlayer = new (playerTileAmount);

            tileArrayAll = new Tile[width, height];    

            CreateTiles();
        }

        private void Start() 
        {
            SetupCamera();
        }

        private void CreateTiles()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    MakeTile(x, y);
                }
            }
        }

        private void MakeTile(int x, int y)
        {
            bool isOwnerPlayer = y < 5;
            GameObject tilePrefab = isOwnerPlayer ? playerTileObj : tileObj;

            GameObject tileObjInstance = Instantiate(tilePrefab, new Vector2(x,y), Quaternion.identity);

            Tile newTile = new Tile(x, y, tileObjInstance, isOwnerPlayer);
            tileArrayAll[x, y] = newTile;

            if(isOwnerPlayer)
            {
                tileListPlayer.Add(newTile);
            }
            else
            {
                tileListEnemy.Add(newTile);
            }

            tileObjInstance.name = newTile.ToString();
            tileObjInstance.transform.SetParent(this.transform);
        }

        private void SetupCamera()
        {
            Camera.main.transform.position = new Vector3((float)(width - 1) / 2, (float)(height - 1) / 2, -10f);

            float aspectRatio = (float)Screen.width / (float)Screen.height;

            float verticalSize = (float)height / 2f + (float)borderSize;
            float horizontalSize = ((float)width / 2f + (float)borderSize) / aspectRatio;

            Camera.main.orthographicSize = (verticalSize > horizontalSize) ? verticalSize : horizontalSize;
        }

        public Tile GetTile(Vector2 position)
        {
            if(position.x < 0 || position.y < 0 || position.x >= width || position.y >= height)
            {
                return null;
            }

            Tile tile = tileArrayAll[(int)position.x, (int)position.y];
            return tile;
        }

        public Tile[,] GetTiles()
        {
            return tileArrayAll;
        }

        public List<Tile> GetPlayerTiles()
        {
            return tileListPlayer;
        }

        public IEnumerable<Tile> GetPlayerTilesNumerable()
        {
            return tileListPlayer;
        }

        public List<Tile> GetEnemyTiles()
        {
            return tileListEnemy;
        }

        public IEnumerable<Tile> GetEnemyTilesNumerable()
        {
            return tileListEnemy;
        }
    }
}

