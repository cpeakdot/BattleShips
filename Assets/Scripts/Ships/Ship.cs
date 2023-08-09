using System.Collections;
using UnityEngine;

namespace BattleShips.Ships
{
    public class Ship 
    {
        private GameObject[] shipParts;
        private bool isOwnerPlayer = false;

        public Ship(GameObject[] shipParts, bool isOwnerPlayer)
        {
            this.shipParts = shipParts;
            this.isOwnerPlayer = isOwnerPlayer;
        }

        public bool IsOwnerPlayer()
        {
            return isOwnerPlayer;
        }

        public IEnumerable GetShipParts()
        {
            return shipParts;
        }
    }
}

