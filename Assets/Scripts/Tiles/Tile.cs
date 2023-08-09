using UnityEngine;

namespace BattleShips.Tiles
{
    public class Tile
    {
        private int x;
        private int y;
        private GameObject tileObj;

        public Tile(int x, int y, GameObject tileObj)
        {
            this.x = x;
            this.y = y;
            this.tileObj = tileObj;
        }

        public Vector2 GetPosition()
        {
            return new Vector2(x, y);
        } 

        public void GetXY(out int X, out int Y)
        {
            X = this.x;
            Y = this.y;
        }

        public GameObject GetTileObj()
        {
            return tileObj;
        }
        public override string ToString()
        {
            return $"x= {x}, y= {y}";
        }
    }
}
