using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z
{
    public class Weapon : MonoBehaviour {
        public float CDRate;
        [HideInInspector] public float CDTime;
        public string AnimKey;
        public bool Toggle;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        public virtual void Update()
        {
            CDUpdate();
        }

        public void CDUpdate()
        {
            CDTime -= Time.deltaTime;
        }

        public void SetCDTime(float Value)
        {
            CDTime = Value;
        }

        public virtual void Attack()
        {
            CDTime = CDRate;
            MainCharacterControl.Main.SetSwitchProtectedTime(CDRate);
            if (AnimKey != "")
                MainCharacterControl.Main.SetAnim(AnimKey);
        }

        public virtual bool CanAttack()
        {
            return CDTime <= 0;
        }

        public virtual void OnSwitch()
        {

        }

        public virtual void OnSwitchOff()
        {

        }
    }
}