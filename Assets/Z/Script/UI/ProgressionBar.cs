using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z
{
    public class ProgressionBar : MonoBehaviour {
        public EnergyBar Bar;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Construction C = ConstructionControl.Main.CurrentConstruction;
            if (!C || C.Progression >= C.MaxProgression)
            {
                Bar.SetValue(0);
                return;
            }
            float a = C.Progression / C.MaxProgression;
            if (a <= 0.05f)
                a = 0.05f;
            Bar.SetValue(a);
        }
    }
}