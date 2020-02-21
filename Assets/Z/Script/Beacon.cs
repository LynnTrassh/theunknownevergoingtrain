using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z
{
    public class Beacon : MonoBehaviour {
        [HideInInspector] public bool Active;
        public Vector3 Size;
        public GameObject AnimBase;
        public GameObject TargetObject;

        public void Awake()
        {
            SetActive(false);
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetPosition(Vector3 Value)
        {
            transform.position = Value;
        }

        public void SetRotation(Vector3 Value)
        {
            transform.eulerAngles = Value;
        }

        public void SetScale(float Value)
        {
            transform.localScale = new Vector3(Value, Value, Value);
        }

        public void SetActive(bool Value)
        {
            Active = Value;
            AnimBase.SetActive(Value);
        }

        public bool CanConstruct()
        {
            return Active;
        }

        public void Construct()
        {
            GameObject G = Instantiate(TargetObject);
            G.transform.position = transform.position;
            G.transform.eulerAngles = transform.eulerAngles;
            G.transform.localScale = transform.localScale;
            ConstructionControl.Main.OnConstruct(G);
            OnConstruct(G);
        }

        public void OnConstruct(GameObject G)
        {

        }
    }
}