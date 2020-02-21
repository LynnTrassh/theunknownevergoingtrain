using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z
{
    public class MovementControl : MonoBehaviour {
        public bool Active;
        [Space]
        public Rigidbody Rig;
        public Animator Anim;
        public float MovementSpeed;
        public float JumpSpeed;
        [Space]
        public GameObject HorizontalPivot;
        public GameObject VerticalPivot;
        public float RotationSpeed;
        public Vector2 VerticalAngleLimit;
        public Vector3 CurrentRotationSpeed;

        public void Awake()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.None;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (!Active)
            {
                SetSpeed(new Vector3(0, Rig.velocity.y, 0));
                return;
            }

            Vector3 Speed = Rig.velocity;
            float y = Rig.velocity.y;
            Speed = HorizontalPivot.transform.TransformDirection(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            Speed.Normalize();
            Speed *= MovementSpeed;
            if (Input.GetKeyDown(KeyCode.Space))
                Speed = new Vector3(Speed.x, JumpSpeed, Speed.z);
            else
                Speed = new Vector3(Speed.x, y, Speed.z);
            SetSpeed(Speed);
        }

        public void FixedUpdate()
        {
            HorizontalPivot.transform.eulerAngles = new Vector3(0, HorizontalPivot.transform.eulerAngles.y, 0);

            if (Rig.velocity.y < -50f)
                Rig.velocity = new Vector3(Rig.velocity.x, -50f, Rig.velocity.z);

            if (!Active)
                return;

            float x = -Input.GetAxis("Mouse Y") * RotationSpeed * Time.fixedDeltaTime;
            float y = Input.GetAxis("Mouse X") * RotationSpeed * Time.fixedDeltaTime;
            HorizontalPivot.transform.eulerAngles = new Vector3(0, HorizontalPivot.transform.eulerAngles.y + y, 0);
            VerticalPivot.transform.localEulerAngles = new Vector3(VerticalPivot.transform.localEulerAngles.x + x, 0, 0);
            if (AbsAngle(VerticalPivot.transform.localEulerAngles.x) > VerticalAngleLimit.y)
            {
                VerticalPivot.transform.localEulerAngles = 
                    new Vector3(VerticalAngleLimit.y, VerticalPivot.transform.localEulerAngles.y, VerticalPivot.transform.localEulerAngles.z);
            }
            else if (AbsAngle(VerticalPivot.transform.localEulerAngles.x) < VerticalAngleLimit.x)
            {
                VerticalPivot.transform.localEulerAngles = 
                    new Vector3(VerticalAngleLimit.x, VerticalPivot.transform.localEulerAngles.y, VerticalPivot.transform.localEulerAngles.z);
            }
            CurrentRotationSpeed = new Vector3(x, y, 0);
        }

        public void SetSpeed(Vector3 Value)
        {
            Rig.velocity = Value;
            if ((Value.x == 0 && Value.y == 0) || Value.z != 0)
                Anim.SetBool("Move", false);
            else
                Anim.SetBool("Move", true);
        }

        public static float AbsAngle(float Value)
        {
            if (Value > 180)
                return Value - 360;
            else if (Value <= -180)
                return Value + 360;
            else
                return Value;
        }
    }
}