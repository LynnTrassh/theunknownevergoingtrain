using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z
{
    public class CharacterMod : MonoBehaviour {
        public float Height = 0.649f;
        public float Speed = 3.5f;
        public float CharacterScale = 1f;
        public float ConstructionScale = 1f;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void EditorApply()
        {
            MainCharacterControl MCC = GetComponent<MainCharacterControl>();
            MovementControl MC = GetComponent<MovementControl>();
            ConstructionControl CC = GetComponent<ConstructionControl>();
            MCC.SetHeight(Height);
            MC.MovementSpeed = Speed;
            gameObject.transform.localScale = new Vector3(CharacterScale, CharacterScale, CharacterScale);
            CC.Scale = ConstructionScale;
        }
    }
}