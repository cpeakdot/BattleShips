using BattleShips.Ships;
using UnityEngine;
using System;

namespace BattleShips.Tiles
{
    public class Tile
    {
        private int x;
        private int y;
        private GameObject tileObj;
        private bool ownerPlayer = false;
        private ShipPartHandler shipPart = null;
        private TileStatus tileStatus;

        public static event Action<Tile> OnTileMissed;
        public static event Action<Tile> OnTileHit;
        /// <summary>
        /// General attacking action. Hit or Missed.
        /// </summary>
        public event Action OnTileAttacked;

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

        /// <summary>
        ///
        /// </summary>
        /// <returns> Returns true if hit something </returns>
        public bool TryAttackThisTile()
        {
            Debug.Log($"Attacked this tile : " + this.ToString());
            OnTileAttacked?.Invoke();
            if(!HasShipPart()) 
            {
                this.tileStatus = TileStatus.Missed;
                OnTileMissed?.Invoke(this);
                return false;
            }
            else
            {
                this.tileStatus = TileStatus.Hit;
                OnTileHit?.Invoke(this);
                return true;
            }
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

        public TileStatus GetTileStatus()
        {
            return tileStatus;
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
