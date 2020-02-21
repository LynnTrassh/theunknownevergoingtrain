using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z
{
    public class ConstructionControl : MonoBehaviour {
        [HideInInspector]
        public static ConstructionControl Main;
        public bool ConstructionMode;
        public float Scale = 1f;
        public GameObject RayPoint;
        public GameObject CharacterPoint;
        public GameObject TempObject;
        public Beacon CurrentBeacon;
        [Space]
        public Ray BeaconRay;
        public float MaxDistance;
        public LayerMask BeaconRayMask;
        public LayerMask PositionRayMask;

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
            if (CurrentBeacon)
            {
                BeaconPositionUpdate();
                if (Input.GetMouseButtonDown(0))
                    TryConstruct();
            }
        }

        public void EnterConstruction(Beacon B)
        {
            if (ConstructionMode)
                return;
            ConstructionMode = true;
            GameObject G = Instantiate(B.gameObject);
            CurrentBeacon = G.GetComponent<Beacon>();
        }

        public void LeaveConstruction()
        {
            if (!ConstructionMode)
                return;
            ConstructionMode = false;
            if (CurrentBeacon)
                Destroy(CurrentBeacon.gameObject);
        }

        public void BeaconPositionUpdate()
        {
            BeaconRay.origin = RayPoint.transform.position;
            BeaconRay.direction = RayPoint.transform.forward;
            RaycastHit[] Hs = Physics.RaycastAll(BeaconRay, MaxDistance, BeaconRayMask);
            List<RaycastHit> Hits = new List<RaycastHit>(Hs);
            if (Hits.Count <= 0)
            {
                CurrentBeacon.SetActive(false);
                return;
            }

            Physics.Raycast(BeaconRay, out RaycastHit RH, 999f, BeaconRayMask);
            CurrentBeacon.SetActive(true);
            Vector3 TempPosition = new Vector3();
            Vector3 TempRotation = new Vector3();
            TempPosition = RH.point;
            TempObject.transform.position = TempPosition;
            if (RH.normal.y > -0.1f && RH.normal.y < 0.1f)
            {
                TempObject.transform.forward = RH.normal;
                TempRotation = new Vector3(0, TempObject.transform.eulerAngles.y, 0);
            }
            else
            {
                if (RH.point.y <= RayPoint.transform.position.y)
                    BeaconRay.origin = RH.point + new Vector3(0, 0.1f, 0);
                else
                    BeaconRay.origin = RH.point - new Vector3(0, 0.1f, 0);
                BeaconRay.direction = new Vector3(BeaconRay.origin.x, 0, BeaconRay.origin.z) - new Vector3(RayPoint.transform.position.x, 0, RayPoint.transform.position.z);
                Physics.Raycast(BeaconRay, out RaycastHit RHII, 999f, BeaconRayMask);
                if (RHII.normal.y > -0.1f && RHII.normal.y < 0.1f)
                {
                    TempPosition = RHII.point;
                    TempObject.transform.position = TempPosition;
                    TempObject.transform.forward = RHII.normal;
                    TempRotation = new Vector3(0, TempObject.transform.eulerAngles.y, 0);
                }
                else
                {
                    TempObject.transform.forward = -RayPoint.transform.forward;
                    TempRotation = new Vector3(0, TempObject.transform.eulerAngles.y, 0);
                    CurrentBeacon.SetActive(false);
                    return;
                }
            }
            Debug.DrawRay(BeaconRay.origin, BeaconRay.direction * MaxDistance, Color.green);

            Ray Y = new Ray(TempObject.transform.position + TempObject.transform.up * 0.01f, -TempObject.transform.up);
            if (Physics.Raycast(Y, out RaycastHit YH, 999f, BeaconRayMask))
            {
                TempPosition = YH.point + new Vector3(0, CurrentBeacon.Size.y * Scale, 0);
                TempObject.transform.position = TempPosition;
            }
            else
                CurrentBeacon.SetActive(false);

            Ray Z = new Ray(TempObject.transform.position + TempObject.transform.forward * 0.01f, -TempObject.transform.forward);
            if (Physics.Raycast(Z, out RaycastHit ZH, CurrentBeacon.Size.z * Scale, BeaconRayMask))
            {
                float z = ZH.distance;
                TempPosition += (CurrentBeacon.Size.z * Scale - ZH.distance) * TempObject.transform.forward;
                TempObject.transform.position = TempPosition;
                Ray ZII = new Ray(TempObject.transform.position - TempObject.transform.forward * (0.01f + CurrentBeacon.Size.z * Scale), TempObject.transform.forward);
                if (Physics.Raycast(ZII, CurrentBeacon.Size.z * 2 * Scale, PositionRayMask))
                {
                    CurrentBeacon.SetActive(false);
                    return;
                }
            }

            Ray X = new Ray(TempObject.transform.position - TempObject.transform.right * (0.01f + CurrentBeacon.Size.x * Scale), TempObject.transform.right);
            Ray XII = new Ray(TempObject.transform.position + TempObject.transform.right * (0.01f + CurrentBeacon.Size.x * Scale), -TempObject.transform.right);
            if (Physics.Raycast(X, out RaycastHit XH, CurrentBeacon.Size.x * 2 * Scale, PositionRayMask))
            {
                float x = XH.distance;
                TempPosition -= (CurrentBeacon.Size.x * 2 * Scale - XH.distance) * TempObject.transform.right;
                TempObject.transform.position = TempPosition;
                XII = new Ray(TempObject.transform.position + TempObject.transform.right * (0.01f + CurrentBeacon.Size.x * Scale), -TempObject.transform.right);
                if (Physics.Raycast(XII, CurrentBeacon.Size.x * 2 * Scale, PositionRayMask))
                {
                    CurrentBeacon.SetActive(false);
                    return;
                }
            }
            else if (Physics.Raycast(XII, out RaycastHit XHII, CurrentBeacon.Size.x * 2 * Scale, PositionRayMask))
            {
                float x = XHII.distance;
                TempPosition += (CurrentBeacon.Size.x * 2 * Scale - XHII.distance) * TempObject.transform.right;
                TempObject.transform.position = TempPosition;
                X = new Ray(TempObject.transform.position - TempObject.transform.right * (0.01f + CurrentBeacon.Size.x * Scale), TempObject.transform.right);
                if (Physics.Raycast(X, CurrentBeacon.Size.x * 2 * Scale, PositionRayMask))
                {
                    CurrentBeacon.SetActive(false);
                    return;
                }
            }

            if ((TempPosition - CharacterPoint.transform.position).magnitude > MaxDistance)
            {
                CurrentBeacon.SetActive(false);
                return;
            }

            CurrentBeacon.SetPosition(TempPosition);
            CurrentBeacon.SetRotation(TempRotation);
            CurrentBeacon.SetScale(Scale);
        }

        public void TryConstruct()
        {
            if (!CurrentBeacon || !CurrentBeacon.CanConstruct())
                return;
            CurrentBeacon.Construct();
        }

        public void OnConstruct(GameObject G)
        {

        }
    }
}