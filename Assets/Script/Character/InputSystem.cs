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
    void Start()
    {
        move = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        move.CharacterMove(Input.GetAxis(input.horizontal), Input.GetAxis(input.vertical));
        move.CharacterSprint(Input.GetButton(input.sprint));
    }
}
