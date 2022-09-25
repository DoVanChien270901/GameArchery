using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    Animator anim;
    CharacterController cc;

    [System.Serializable] //tuan tu hoa
    public class CharacterSetting 
    {
        public string forward = "forward";
        public string strafe = "strafe";
        public string sprint = "sprint";
        public string aim = "aim";
        public string pullstring = "pullstring";
        public string fire = "fire";
    }
    [SerializeField] // dong tuan tu hoa
    CharacterSetting setting;   
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
    }
    public void CharacterMove(float strafe, float forward)
    {
        anim.SetFloat(setting.strafe, strafe);
        anim.SetFloat(setting.forward, forward);
    }
    public void CharacterSprint(bool isSprinting)
    {
        anim.SetBool(setting.sprint, isSprinting);
    }
    public void CharacterAim(bool isAiming)
    {
        anim.SetBool(setting.aim, isAiming);
    }
    public void CharacterPullString(bool pullstring)
    {
        anim.SetBool(setting.pullstring, pullstring);
    }
    public void CharacterFire()
    {
        anim.SetTrigger(setting.fire);
    }

}
