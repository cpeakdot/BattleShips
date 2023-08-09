using BattleShips.Tiles;
using UnityEngine;

namespace BattleShips.Ships
{
    public class ShipPartHandler : MonoBehaviour
    {
        [SerializeField] private GameObject visual;
        private bool isAlive = true;
        private Tile tile;

        public Tile Tile 
        {
            get
            {
                return tile;
            }
            set
            {
                tile = value;
            }
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