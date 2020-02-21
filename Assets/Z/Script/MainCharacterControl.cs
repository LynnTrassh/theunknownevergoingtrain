using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z
{
    public class MainCharacterControl : MonoBehaviour {
        [HideInInspector]
        public static MainCharacterControl Main;
        public Animator Anim;
        public GameObject VisionPoint;
        public Weapon CurrentWeapon;
        public List<Weapon> Weapons;
        public List<GameObject> WeaponObject;
        public float SwitchDelay;
        public float SwitchAnimDelay;
        public float SwitchProtectedTime;
        [HideInInspector] public float CurrentSwitchProtectedTime;

        public void Awake()
        {
            Main = this;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            CurrentSwitchProtectedTime -= Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Alpha1) && Weapons.Count > 0 && Weapons[0])
                SwitchWeapon(Weapons[0]);
            else if (Input.GetKeyDown(KeyCode.Alpha2) && Weapons.Count > 1 && Weapons[1])
                SwitchWeapon(Weapons[1]);

            if (Input.GetMouseButtonDown(0))
                TryAttack();
        }

        public void SwitchWeapon(Weapon New)
        {
            if (CurrentSwitchProtectedTime > 0)
                return;
            SetSwitchProtectedTime(SwitchProtectedTime);
            StartCoroutine(SwitchWeaponIE(New));
        }

        public IEnumerator SwitchWeaponIE(Weapon New)
        {
            if (CurrentWeapon)
                CurrentWeapon.OnSwitchOff();
            CurrentWeapon = New;
            CurrentWeapon.OnSwitch();
            CurrentWeapon.SetCDTime(SwitchDelay);
            SetAnim("Switch");
            yield return new WaitForSeconds(SwitchAnimDelay);
            int Index = Weapons.IndexOf(New);
            for (int i = WeaponObject.Count - 1; i >= 0; i--)
            {
                if (i == Index)
                    WeaponObject[i].SetActive(true);
                else
                    WeaponObject[i].SetActive(false);
            }
        }

        public void TryAttack()
        {
            if (!CurrentWeapon || !CurrentWeapon.CanAttack())
                return;
            CurrentWeapon.Attack();
        }

        public void SetAnim(string Key)
        {
            Anim.SetTrigger(Key);
        }

        public void SetSwitchProtectedTime(float Value)
        {
            if (CurrentSwitchProtectedTime < Value)
                CurrentSwitchProtectedTime = Value;
        }
    }
}