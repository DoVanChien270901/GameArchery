using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Movement))]
public class InputSystem : MonoBehaviour
{
    Movement move;
    public Bow bowScript;
    // Start is called before the first frame update
    [System.Serializable]
    public class InputSetting
    {
        public string horizontal = "Horizontal"; // truc x
        public string vertical = "Vertical"; // truc y
        public string sprint = "Sprint";
        public string aim = "Fire2";
        public string fire = "Fire1";
    }
    [SerializeField]

    InputSetting input;

    [Header("camera & character syncing")]
    public float lookDistance = 5;
    public float lookSpeed = 5;

    [Header("aim setting")]
    public RaycastHit hit;
    Ray ray;
    public LayerMask aimMask;

    [Header("spine rotate setting")]
    public Transform spine;
    public Vector3 spineOffset;

    [Header("head setting")]
    public float lookAt = 2.8f;

    Transform maincam;
    Transform camCenter;
    bool hitDeleted;
    void Start()
    {
        move = GetComponent<Movement>();
        camCenter = Camera.main.transform.parent;
        maincam = Camera.main.transform;
    }

    bool isAiming;

    // Update is called once per frame
    void Update()
    {
        move.CharacterMove(Input.GetAxis(input.horizontal), Input.GetAxis(input.vertical));
        move.CharacterSprint(Input.GetButton(input.sprint));
        RotateToCamView();

        isAiming = Input.GetButton(input.aim);
        move.CharacterAim(isAiming);

        if (isAiming)
        {
            aim();
            move.CharacterPullString(Input.GetButton(input.fire));
            if (Input.GetButtonUp(input.fire))
            {
                move.CharacterFire();
                if (hitDeleted)
                {
                    bowScript.Fire(hit.point);
                }
                else
                {
                    bowScript.Fire(ray.GetPoint(300f));
                }
            }
        }
        else
        {
            bowScript.Remove();
            DisableArrow();
            Release();
        }

    }
    private void LateUpdate()
    {
        if (isAiming)
        {
            rotateCharacterSpine();
        }
        
    }
    void RotateToCamView()
    {
        Vector3 camCenterPos = camCenter.position;
        Vector3 lookPoint = camCenterPos + (camCenter.forward * lookDistance);
        Vector3 dir = lookPoint - transform.position;

        Quaternion lookRotation = Quaternion.LookRotation(dir);
        lookRotation.x = 0;
        lookRotation.z = 0;

        Quaternion final = Quaternion.Lerp(transform.rotation, lookRotation, lookSpeed * Time.deltaTime);
        transform.rotation = final;
    }
    void aim()
    {
        Vector3 camPos = maincam.position;
        Vector3 dir = maincam.forward;

        ray = new Ray(camPos, dir);
        if (Physics.Raycast(ray, out hit, 500f, aimMask))
        {
            hitDeleted = true;
            bowScript.ShowCrossHair(hit.point);
        }else
        { 
            hitDeleted = false;
            bowScript.Remove();
        }
    }
    void rotateCharacterSpine()
    {
        spine.LookAt(ray.GetPoint(50));
        spine.Rotate(spineOffset);
    }

    public void Pull()
    {
        bowScript.PullString();
    }

    public void EnableArrow()
    {
        bowScript.PickArrow();
        bowScript.PullString();
    }
    public void DisableArrow()
    {
        bowScript.DisableArrow();
    }
    public void Release()
    {
        bowScript.ReleaseString();
    }
}
