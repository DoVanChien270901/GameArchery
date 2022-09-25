using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Movement))]
public class InputSystem : MonoBehaviour
{
    Movement move;
    // Start is called before the first frame update
    [System.Serializable]
    public class InputSetting
    {
        public string horizontal = "Horizontal"; // truc x
        public string vertical = "Vertical"; // truc y
        public string sprint = "Sprint";
    }
    [SerializeField]

    InputSetting input;

    [Header("camera & character syncing")]
    public float lookDistance = 5;
    public float lookSpeed = 5;

    Transform camCenter;
    void Start()
    {
        move = GetComponent<Movement>();
        camCenter = Camera.main.transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        move.CharacterMove(Input.GetAxis(input.horizontal), Input.GetAxis(input.vertical));
        move.CharacterSprint(Input.GetButton(input.sprint));
        RotateToCamView();
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
}
