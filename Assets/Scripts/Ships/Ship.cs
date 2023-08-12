using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleShips.Ships
{
    [System.Serializable]
    public class Ship 
    {
        private List<ShipPartHandler> shipParts = new();
        private bool isOwnerPlayer = false;

        public void AddShipPart(ShipPartHandler part)
        {
            if(shipParts.Contains(part)) { return; }
            shipParts.Add(part);
        }

        public Ship(bool isOwnerPlayer)
        {
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

