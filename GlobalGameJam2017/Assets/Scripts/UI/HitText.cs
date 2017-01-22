using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitText : MonoBehaviour
{
    public float animationLength;
    
    public Animator animator;
    public Color color;


    void Start()
    {
        AnimatorClipInfo[] aci = animator.GetCurrentAnimatorClipInfo(0);
        Destroy(gameObject,aci.Length);
    }

    public void setText(string text)
    {
          Text damageText = GetComponentInChildren<Text>();
         damageText.color= color;
         damageText.text = text;
    }

}
 