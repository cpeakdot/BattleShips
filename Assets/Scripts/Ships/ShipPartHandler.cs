using BattleShips.Tiles;
using UnityEngine;
using System;

namespace BattleShips.Ships
{
    public class ShipPartHandler : MonoBehaviour
    {
        [SerializeField] private GameObject visual;
        private bool isAlive = true;
        private Tile tile;

        public event Action OnShipPartWrecked;

        public Tile Tile 
        {
            get
            {
                return tile;
            }
            set
            {
                tile = value;
                tile.OnTileAttacked += HandleOnTileAttacked;
            }
        }

        private void HandleOnTileAttacked()
        {
            isAlive = false;
            OnShipPartWrecked?.Invoke();
        }

        public void DisableVisual()
        {
            visual.SetActive(false);
        }

        public bool IsPartAlive()
        {
            return isAlive;
        }

    }
}