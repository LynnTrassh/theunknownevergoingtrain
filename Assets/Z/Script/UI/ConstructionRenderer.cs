using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Z
{
    public class ConstructionRenderer : MonoBehaviour {
        public TextMeshProUGUI NameText;
        public TextMeshProUGUI ProgressionText;
        public EnergyBar Bar;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Construction C = ConstructionControl.Main.CurrentConstruction;
            if (!C || !C.GetInfo())
            {
                NameText.text = "";
                ProgressionText.text = "";
                Bar.SetValue(0);
                return;
            }

            float a = C.Progression / C.MaxProgression;
            float b = Mathf.Round(a * 100f);
            if (a <= 0.05f)
                a = 0.05f;
            if (a >= 1)
                a = 0;
            Bar.SetValue(a);
            NameText.text = C.GetInfo().Name;
            if (b > 100)
                b = 100;
            if (b < 0)
                b = 0;
            ProgressionText.text = b + "%";
        }
    }
}