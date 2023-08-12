using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleShips.Ships
{
    [System.Serializable]
    public class Ship 
    {
        private Transform parentObjTransform;
        private List<ShipPartHandler> shipParts = new();
        private bool isOwnerPlayer = false;

        public void AddShipPart(ShipPartHandler part)
        {
            if(shipParts.Contains(part)) { return; }
            shipParts.Add(part);
        }

        public Ship(bool isOwnerPlayer, Transform parentObjTransform)
        {
            this.isOwnerPlayer = isOwnerPlayer;
            this.parentObjTransform = parentObjTransform;
        }

        public bool IsOwnerPlayer()
        {
            return isOwnerPlayer;
        }

        public IEnumerable GetShipParts()
        {
            return shipParts;
        }

        public Transform GetShipTransform()
        {
            return parentObjTransform;
        }
    }
}

