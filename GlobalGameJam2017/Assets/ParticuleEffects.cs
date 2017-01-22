using UnityEngine;
using System.Collections;

public class ParticuleEffects : MonoBehaviour
{
    private Renderer rend;

    private Vector3[] _baseVertices;
    private bool RecalculateNormals = false;

    public float defaultSpeed =5;
    public float defaultDamage = 1;
    public float defaultScaleFactorYSpread = 5f;


    public GameObject projectile;
    public Light light;

    public Color colorA_Heal;
    public Color colorB_Power;
    public Color colorX_Spread;
    public Color colorY_Speed;
    [SerializeField]
    public GameObject domeEffect;
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
  

    private float targetTime = 1f;

    void Start()
    {
        rend = projectile.GetComponentInChildren<Renderer>();
    }

    void Reset()
    {
         frequency_final = frequency;
         scale_final = 1f;
         speed_final = defaultSpeed;
         heal_final = 0f;
         damage_final = 0f;
}

    void Update()
    {
        // GameObject.FindGameObjectWithTag("Player").GetComponent<Rotation>();
        float Angle = Quaternion.Angle(Quaternion.Euler(new Vector3(0, 0, 0)), transform.rotation);        
        //main.startRotationY = angle360(Angle,-particleSystem.startRotation3D,transform.parent.right) * Mathf.Deg2Rad;
    }

    float angle360(float angle, Vector3 to, Vector3 right)
    {
        return (Vector3.Angle(right, to) > 90f) ? 360f - angle : angle;
    }


    public void readInputs(GuitarInput[] guitarInput)
    {

        Reset();

        Color result = new Color(0, 0, 0, 0);

        // Default Value

        int inputStreak=0;

        GuitarInput lastGuitarInput= guitarInput[0];
        for (int i = 0; i < 3; i++)
        {
            switch (guitarInput[i])
            {
                //Healing
                case GuitarInput.A_HEAL:
                    GameObject player = GameObject.FindGameObjectWithTag("Player");
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
            
            if (guitarInput[i]==lastGuitarInput)
            {
                inputStreak++;
            }
          
        }
        
        if (inputStreak == 3)
        {
            switch (lastGuitarInput)
            {
                case GuitarInput.X_SPREAD:
                    GameObject obj = Instantiate(domeEffect);
                    obj.transform.position = transform.parent.transform.position;

                    break;
            }
        }
        else
        {
            // Set Material Color
            rend.sharedMaterial.color = result / guitarInput.Length;
            rend.sharedMaterial.SetColor("_EmissionColor", rend.sharedMaterial.color);

            // Set Light color
            Light light = projectile.GetComponentInChildren<Light>();
            light.color = rend.sharedMaterial.color;

            StartCoroutine("shoot");
        }
        



        
    }

    IEnumerator shoot()
    {
        for (int i = 0; i < frequency_final; i++)
        {

            GameObject obj = Instantiate(projectile);
            
            // Set position
            obj.transform.position = transform.position;

            // Set Rotation
            float angle = Vector3.Angle(Vector3.forward, transform.forward);
            angle = angle360(angle, transform.forward, Vector3.right);
            obj.transform.Rotate(new Vector3(0,angle,0));

            // Set speed
            Rigidbody rigid = obj.GetComponent<Rigidbody>();
            rigid.velocity = speed_final  * transform.forward;


            // Set scale
            Vector3 scale = obj.transform.localScale;
 
            scale.x = scale_final;

            obj.transform.localScale = scale;

            // Set damage

            yield return new WaitForSeconds(1.0f / frequency_final);
        }
        
    }

  
}