using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z
{
    public class Weapon_Hammer : Weapon {
        public Ray HammerRay;
        public LayerMask HammerRayMask;
        public float Delay;
        public float Range;
        public float Progression;

        public override void Attack()
        {
            base.Attack();
            StartCoroutine("AttackIE");
        }

        public IEnumerator AttackIE()
        {
            yield return new WaitForSeconds(Delay);
            HammerRay.origin = MainCharacterControl.Main.VisionPoint.transform.position;
            HammerRay.direction = MainCharacterControl.Main.VisionPoint.transform.forward;
            if (!Physics.Raycast(HammerRay, out RaycastHit Hit, Range, HammerRayMask) || !Hit.transform.GetComponent<Construction>())
                yield break;
            Hit.transform.GetComponent<Construction>().ChangeProgression(Progression);
        }
    }
}