using System;
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

        private int wreckedBodyPartCount = 0;

        public static event Action<Ship> OnShipWrecked;
        public static event Action OnShipDamaged;

        public void AddShipPart(ShipPartHandler part)
        {
            if(shipParts.Contains(part)) { return; }
            shipParts.Add(part);
            part.OnShipPartWrecked += HandleOnShipPartWrecked;
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

        private void HandleOnShipPartWrecked()
        {
            wreckedBodyPartCount++;
            if(wreckedBodyPartCount == shipParts.Count)
            {
                OnShipWrecked?.Invoke(this);
            }
            else
            {
                OnShipDamaged?.Invoke();
            }
        }
    }
}

