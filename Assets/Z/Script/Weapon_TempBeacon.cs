using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z
{
    public class Weapon_TempBeacon : Weapon {
        public Beacon TargetBeacon;

        public override bool CanAttack()
        {
            return false;
        }

        public override void OnSwitch()
        {
            base.OnSwitch();
            ConstructionControl.Main.EnterConstruction(TargetBeacon);
        }

        public override void OnSwitchOff()
        {
            base.OnSwitchOff();
            ConstructionControl.Main.LeaveConstruction();
        }
    }
}