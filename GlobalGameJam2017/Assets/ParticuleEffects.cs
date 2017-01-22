using UnityEngine;
using System.Collections;

public class ParticuleEffects : MonoBehaviour{

    ////////////////////////////////////////////////////////////////////////////////////////////////
    /// VARIABLES
    ////////////////////////////////////////////////////////////////////////////////////////////////

    private Renderer rend;

    public float defaultSpeed =5;
    public float defaultDamage = 1;
    public float defaultScale= 1f;

    public GameObject projectile;

    public Color colorA_Heal;
    public Color colorB_Power;
    public Color colorX_Spread;
    public Color colorY_Speed;

    public GameObject domeEffect;
    public GameObject healingEffect;
    public GameObject speedEffect;
    public GameObject powerEffect;

    public float timeEmitting = 5f;

    public float frequency ;
    public float speedIncrement;
    public float rateIncrement;
    public float spreadIncrement;
    public float damageIncrement;
    public float healIncrement=1;

    private float frequency_final = 0f;
    private float scale_final = 1f;
    private float speed_final = 0f;
    private float heal_final = 0f;
    private float damage_final = 0f;

    ////////////////////////////////////////////////////////////////////////////////////////////////
    /// UNITY
    ////////////////////////////////////////////////////////////////////////////////////////////////

    void Start() {
        rend = projectile.GetComponentInChildren<Renderer>();
    }


    /////////////////////////////////////////////////////////////////////////////////////////////////
    /// METHODS
    /////////////////////////////////////////////////////////////////////////////////////////////////

    void Reset() {
         frequency_final = frequency;
         scale_final = defaultScale;
         speed_final = defaultSpeed;
         heal_final = 0f;
         damage_final = 0f;
    }


    float angle360(float angle, Vector3 to, Vector3 right) {
        return (Vector3.Angle(right, to) > 90f) ? 360f - angle : angle;
    }


    public void readInputs(GuitarInput[] guitarInput) {

        Reset();
        Color result = new Color(0, 0, 0, 0);
   
        // Default Value
        int inputStreak=0;

        GuitarInput lastGuitarInput= guitarInput[0];
        for (int i = 0; i < 3; i++) {
            switch (guitarInput[i]) {
                //Healing
                case GuitarInput.A_HEAL:
                    result += colorA_Heal;
                    heal_final += healIncrement;
            
                    break;
                //Power
                case GuitarInput.B_POWER:
                    damage_final += damageIncrement;
                    result += colorB_Power;
                    break;
                //Spreads
                case GuitarInput.X_SPREAD:
                    scale_final += spreadIncrement;
                    result += colorX_Spread;
                    break;
                //Speed
                case GuitarInput.Y_SPEED:
                    speed_final += speedIncrement;
                    frequency_final += rateIncrement;
                    result += colorY_Speed;
                    break;
            }
            
            if (guitarInput[i]==lastGuitarInput) {
                inputStreak++;
            }
        }
        
        if (inputStreak == 3) {
            switch (lastGuitarInput) {
                case GuitarInput.X_SPREAD:
                    GameObject obj = Instantiate(domeEffect);
                    obj.transform.position = transform.parent.transform.position;
                    break;
                case GuitarInput.A_HEAL:
                    GameObject objHeal = Instantiate(healingEffect, transform.parent);
                    objHeal.transform.position = transform.parent.position;
                    break;
                case GuitarInput.Y_SPEED:
                    GameObject objSpeed = Instantiate(speedEffect, transform.parent);
                    objSpeed.transform.position = transform.parent.position;
                    break;
                case GuitarInput.B_POWER:
                    GameObject strSpeed = Instantiate(powerEffect);
                    strSpeed.transform.position = transform.position + transform.forward*1.5f;
                    float angle = Vector3.Angle(Vector3.forward, transform.forward);
                    angle = angle360(angle, transform.forward, Vector3.right);
                    strSpeed.transform.Rotate(new Vector3(0, 0, angle));
                    break;
            }
        }
        else {
            // Set Material Color
            rend.sharedMaterial.color = result / guitarInput.Length;
            rend.sharedMaterial.SetColor("_EmissionColor", rend.sharedMaterial.color);

            // Set Light color
            Light light = projectile.GetComponentInChildren<Light>();
            light.color = rend.sharedMaterial.color;

            StartCoroutine("shoot");
        }
    }


    IEnumerator shoot() {
        for (int i = 0; i < frequency_final; i++) {
            Debug.Log(i);
            GameObject obj = Instantiate(projectile);

            // Set position
            obj.transform.position = transform.position;

            // Set Rotation
            float angle = Vector3.Angle(Vector3.forward, transform.forward);
            angle = angle360(angle, transform.forward, Vector3.right);
            obj.transform.Rotate(new Vector3(0, angle, 0));

            // Set speed
            Rigidbody rigid = obj.GetComponent<Rigidbody>();
            rigid.velocity = speed_final*transform.forward;


            // Set scale
            Vector3 scale = obj.transform.localScale;
            scale.x = scale_final;
            obj.transform.localScale = scale;

            // Set damage and heal
            ProjectileInfo projectileInfo = obj.GetComponent<ProjectileInfo>();
            projectileInfo.damage =  damage_final;
            projectileInfo.lifeSteal = heal_final;
   
            yield return new WaitForSeconds(1.0f / frequency_final);
        }
    } 
}