using BattleShips.Ships;
using UnityEngine;

namespace BattleShips.Tiles
{
    public class Tile
    {
        private int x;
        private int y;
        private GameObject tileObj;
        private bool ownerPlayer = false;
        private ShipPartHandler shipPart = null;

        public Tile(int x, int y, GameObject tileObj, bool isOwnerPlayer)
        {
            this.x = x;
            this.y = y;
            this.tileObj = tileObj;
            this.ownerPlayer = isOwnerPlayer;
        }

        public void SetShipPart(ShipPartHandler newShipPart)
        {
            shipPart = newShipPart;
        }

        public bool HasShipPart()
        {
            return shipPart != null;
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

        public bool IsOwnerPlayer()
        {
            return ownerPlayer;
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
