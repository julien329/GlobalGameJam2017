using UnityEngine;
using System.Collections;

public class ParticuleEffects : MonoBehaviour
{
    private ParticleSystem particleSystem;

    private ParticleSystemRenderer particleSystemRenderer;
    private ParticleSystem.MainModule main;
    private ParticleSystem.EmissionModule emission;
    private Renderer rend;

    private Vector3[] _baseVertices;
    private bool RecalculateNormals = false;
 //   private float scaleFactorX =1;
  //  private float scaleFactorZ = 1;


    public float defaultSpeed =5;
    public float defaultDamage = 1;
    public float defaultScaleFactorYSpread = 5f;


    public Mesh meshWave;

    public Color colorA_Heal;
    public Color colorB_Power;
    public Color colorX_Spread;
    public Color colorY_Speed;


    public float timeEmitting = 5f;

    public float rateOverTime ;
    public float speedIncrement;
    public float spreadIncrement;
    public float damageIncrement;
    public float healIncrement=1;

    private ParticleSystem.SizeOverLifetimeModule szLifetimeModule;
    private float targetTime = 1f;
    void Reset()
    {

        emission.rateOverTime = rateOverTime;
        main.startRotation3D = true;

        main.startSpeed = defaultSpeed;



        Vector3 scale = transform.localScale;
        float yVal = defaultScaleFactorYSpread;

        scale.y = yVal;
        transform.localScale = scale;

    }
    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        particleSystemRenderer = GetComponent<ParticleSystemRenderer>();
        main = particleSystem.main;
        emission = particleSystem.emission;

        particleSystemRenderer.renderMode=ParticleSystemRenderMode.Mesh;

        szLifetimeModule  = particleSystem.sizeOverLifetime;

        

        rend = GetComponent<Renderer>();
        Reset();
       
    }
                     
    void Update()
    {
        // GameObject.FindGameObjectWithTag("Player").GetComponent<Rotation>();
        float Angle = Quaternion.Angle(Quaternion.Euler(new Vector3(0, 0, 0)), transform.rotation);        
        main.startRotationY = angle360(Angle,-particleSystem.startRotation3D,transform.parent.right) * Mathf.Deg2Rad;
    }

    float angle360(float angle, Vector3 to, Vector3 right)
    {
        return (Vector3.Angle(right, to) > 90f) ? 360f - angle : angle;
    }


    public void shoot()
    {
        particleSystem.Play();
        particleSystemRenderer.mesh = meshWave;
        StartCoroutine(WaitAndStop(timeEmitting));

    }

    public void readInputs(GuitarInput[] guitarInput)
    {
        Color result = new Color(0, 0, 0, 0);
        Reset();
        float speedAcc = 0;
        float heallAcc = 0;
        float damageAcc = 0;
        float spreadAcc = 0;
        for (int i = 0; i < 3; i++)
        {
            switch (guitarInput[i])
            {
                //Healing
                case GuitarInput.A_HEAL:
                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    result += colorA_Heal;
                    heallAcc += healIncrement;
            
                    break;
                //Power
                case GuitarInput.B_POWER:
                    damageAcc += damageIncrement;
                    result += colorB_Power;
                    break;
                //Spreads
                case GuitarInput.X_SPREAD:
                    spreadAcc += spreadIncrement;
                    result += colorX_Spread;
                    break;
                //Speed
                case GuitarInput.Y_SPEED:
                    speedAcc = speedIncrement;
                    result += colorY_Speed;
                    break;
            }
        }
        //player.heal(heallAcc);
        //enemy.damage(damageAcc);
        setSpread(spreadAcc);
        setSpeed(speedAcc);
        rend.material.color = result/guitarInput.Length;
        rend.material.SetColor("_EmissionColor", rend.material.color);

        Color emis = rend.material.color;
        emis.a = 1;
        main.startColor=(emis);
        
        shoot();
    }

    void setSpeed(float speedIncre)
    {
        float speedval = defaultSpeed+speedIncre;
        main.startSpeed = speedval;
    }

    void setSpread(float spread)
    {
        Vector3 scale = transform.localScale;
        float yVal = scale.y + spread;

        scale.y = yVal;
        transform.localScale = scale;
    }

    private IEnumerator WaitAndStop(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        print("WaitAndPrint " );
       
        particleSystem.Stop();
    }
}