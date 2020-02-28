using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z
{
    public class Weapon_Hammer : Weapon {
        public float Delay;
        public float Progression;

        public override void Update()
        {
            base.Update();
            if (Toggle && CanAttack())
                Attack();
        }

        public override void Attack()
        {
            base.Attack();
            StartCoroutine("AttackIE");
        }

        public IEnumerator AttackIE()
        {
            yield return new WaitForSeconds(Delay);
            if (!ConstructionControl.Main.CurrentConstruction)
                yield break;
            ConstructionControl.Main.CurrentConstruction.ChangeProgression(Progression);
        }
    }
}