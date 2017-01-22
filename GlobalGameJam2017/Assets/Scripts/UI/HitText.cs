using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitText : MonoBehaviour
{
    public float animationLength;
    
    public Animator animator;


    void Start()
    {
        AnimatorClipInfo[] aci = animator.GetCurrentAnimatorClipInfo(0);
        Destroy(gameObject,aci.Length);
    }

    public void setText(string text,Color color)
    {
          Text damageText = GetComponentInChildren<Text>();
         damageText.color= color;
         damageText.text = text;
    }

}
 