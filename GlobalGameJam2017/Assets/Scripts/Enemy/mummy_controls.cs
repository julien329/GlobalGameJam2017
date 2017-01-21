using UnityEngine;
using System.Collections;

public class mummy_controls : MonoBehaviour {

	public Animator anim;

	void Awake()
	{
		anim = GetComponent<Animator>();
    }

	public void Att()
	{
		anim.SetBool("Attack", false);
	}

	public void KickOver()
	{
		anim.SetBool("LowKick", false);
        transform.parent.gameObject.SendMessage("AttackIsOver");
	}

	public void DeathOver()
	{
        anim.SetBool("isDamage", false);
        transform.parent.gameObject.SendMessage("DeathIsOver");
    }
    public void DeathOver2()
	{
		anim.SetBool("isDeath2", false);
	}
	public void HitOver()
	{
		anim.SetBool("HitStrike", false);
	}
	public void DamageOver ()
	{
        if(!anim.GetBool("isDeath"))
        {
            anim.SetBool("isDamage", false);
        }
        
	}
}
