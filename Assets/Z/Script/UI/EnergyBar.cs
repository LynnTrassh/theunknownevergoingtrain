using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Z
{
    public class EnergyBar : MonoBehaviour {
        public Image image;
        public Vector2 Range;
        public float CurrentValue;

        // Start is called before the first frame update
        void Start()
        {
            SetValue(CurrentValue);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetValue(float Value)
        {
            CurrentValue = Value;
            image.fillAmount = Range.x + Value * (Range.y - Range.x);
        }
    }
}