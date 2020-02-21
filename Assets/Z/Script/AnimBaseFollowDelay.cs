using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z
{
    public class AnimBaseFollowDelay : MonoBehaviour {
        public MovementControl MC;
        public GameObject AnimBase;
        public GameObject FollowObject;
        public float LerpSpeed;
        public float LockAngle;
        public float UnlockAngle;
        public bool Locked;
        public Vector2 FollowDistance;
        public Vector2 SpeedScale;
        [Space]
        public Vector3 LastAngle;
        public List<float> MaxSpeed;
        public List<float> MaxDistance;
        public AnimationCurve Curve;
        public float CurveScale;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void FixedUpdate()
        {
            /*
            float x = Mathf.LerpAngle(AnimBase.transform.eulerAngles.x, FollowObject.transform.eulerAngles.x, LerpSpeed * Time.fixedDeltaTime);
            float y = Mathf.LerpAngle(AnimBase.transform.eulerAngles.y, FollowObject.transform.eulerAngles.y, LerpSpeed * Time.fixedDeltaTime);
            float z = Mathf.LerpAngle(AnimBase.transform.eulerAngles.z, FollowObject.transform.eulerAngles.z, LerpSpeed * Time.fixedDeltaTime);
            AnimBase.transform.eulerAngles = new Vector3(x, y, z);*/

            /*
            float SpeedX = GetSpeed(AnimBase.transform.eulerAngles.x, FollowObject.transform.eulerAngles.x);
            float SpeedY = GetSpeed(AnimBase.transform.eulerAngles.y, FollowObject.transform.eulerAngles.y);
            float SpeedZ = GetSpeed(AnimBase.transform.eulerAngles.z, FollowObject.transform.eulerAngles.z);

            float x = Mathf.MoveTowardsAngle(AnimBase.transform.eulerAngles.x, FollowObject.transform.eulerAngles.x, SpeedX * Time.fixedDeltaTime);
            float y = Mathf.MoveTowardsAngle(AnimBase.transform.eulerAngles.y, FollowObject.transform.eulerAngles.y, SpeedY * Time.fixedDeltaTime);
            float z = Mathf.MoveTowardsAngle(AnimBase.transform.eulerAngles.z, FollowObject.transform.eulerAngles.z, SpeedZ * Time.fixedDeltaTime);
            AnimBase.transform.eulerAngles = new Vector3(x, y, z);*/

            float Distance = new Vector3(Mathf.DeltaAngle(AnimBase.transform.eulerAngles.x, FollowObject.transform.eulerAngles.x),
                Mathf.DeltaAngle(AnimBase.transform.eulerAngles.y, FollowObject.transform.eulerAngles.y),
                Mathf.DeltaAngle(AnimBase.transform.eulerAngles.z, FollowObject.transform.eulerAngles.z)).magnitude;
            
            if (Distance <= LockAngle * Time.fixedDeltaTime && !Locked)
                Locked = true;
            else if (Distance >= UnlockAngle * Time.fixedDeltaTime && Locked)
                Locked = false;

            if (Locked)
                AnimBase.transform.eulerAngles = FollowObject.transform.eulerAngles;
            else
            {
                Vector3 TargetAngle = FollowObject.transform.eulerAngles + MC.CurrentRotationSpeed;
                float x = Mathf.LerpAngle(AnimBase.transform.eulerAngles.x, TargetAngle.x, LerpSpeed * Time.fixedDeltaTime);
                float y = Mathf.LerpAngle(AnimBase.transform.eulerAngles.y, TargetAngle.y, LerpSpeed * Time.fixedDeltaTime);
                float z = Mathf.LerpAngle(AnimBase.transform.eulerAngles.z, TargetAngle.z, LerpSpeed * Time.fixedDeltaTime);
                AnimBase.transform.eulerAngles = new Vector3(x, y, z);
            }

            float Scale = (Distance - FollowDistance.x) / (FollowDistance.y - FollowDistance.x);
            float Speed = SpeedScale.x + Scale * (SpeedScale.y - SpeedScale.x);
            AnimBase.transform.eulerAngles += MC.CurrentRotationSpeed * Speed;

            LastAngle = FollowObject.transform.eulerAngles;
        }

        public float GetSpeed(float Ori, float Target)
        {
            float Distance = Mathf.DeltaAngle(Ori, Target);
            float Scale = Distance / 360f;
            if (Scale > 1)
                Scale = 1;
            if (Scale < 0)
                Scale = 0;
            return Curve.Evaluate(Scale) * CurveScale;

            /*for (int i = 0; i < MaxDistance.Count; i--)
            {
                if (Distance < MaxDistance[i])
                {
                    return MaxSpeed[i];
                }
            }
            return MaxSpeed[MaxSpeed.Count - 1];*/
        }
    }
}