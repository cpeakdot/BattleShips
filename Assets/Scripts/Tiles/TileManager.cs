using UnityEngine;

namespace BattleShips.Tiles
{
    public class TileManager : MonoBehaviour
    {
        [SerializeField] private GameObject tileObj;

        [SerializeField] private int width;
        [SerializeField] private int height;
        [SerializeField] private float borderSize;

        private Tile[,] tileArray;

        private void Awake() 
        {
            tileArray = new Tile[width, height];    
        }

        private void Start() 
        {
            CreateTiles();
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
            GameObject tileObjInstance = Instantiate(tileObj, new Vector2(x,y), Quaternion.identity);
            Tile newTile = new Tile(x, y, tileObjInstance);
            tileArray[x, y] = newTile;
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
    }
}

