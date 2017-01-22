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

    public Mesh meshWave;

    public Color colorA_Heal;
    public Color colorB_Power;
    public Color colorX_Spread;
    public Color colorY_Speed;

    public float speed=1;
    public float damage=1;
    public float scaleFactorYSpread = 1;
    public float timeEmitting = 5f;

    public float rateOverTime = 1f;
    public float speedIncrement=1;
    public float spreadIncrement=1;
    public float damageIncrement=1;
    public float healIncrement=1;

    private ParticleSystem.SizeOverLifetimeModule szLifetimeModule;
    private float targetTime = 1f;
    void Reset()
    {
       //scaleFactorX = 1;
       // scaleFactorZ = 0.5f;

        setSpread(scaleFactorYSpread);
        main.startSpeed = speed;
        damage = 1;
        emission.rateOverTime = rateOverTime;
        main.startRotation3D = true;
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
       // rend.material.color = colorA_Heal;
        particleSystemRenderer.mesh = meshWave;
        setSpread(scaleFactorYSpread);
        StartCoroutine(WaitAndStop(timeEmitting));


    }

    public void readInputs(GuitarInput[] guitarInput)
    {
        Color result = new Color(0, 0, 0, 0);
        for (int i = 0; i < 3; i++)
        {
            switch (guitarInput[i])
            {
                //Healing
                case GuitarInput.A_HEAL:
                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    result += colorA_Heal;
                    //player.heal(healIncrement);
                    break;
                //Power
                case GuitarInput.B_POWER:
                    this.damage += damageIncrement;
                    result += colorB_Power;
                    break;
                //Spreads
                case GuitarInput.X_SPREAD:
                    scaleFactorYSpread += spreadIncrement;
                    result += colorX_Spread;
                    break;
                //Speed
                case GuitarInput.Y_SPEED:
                    changeSpeed(speed += speedIncrement);
                    result += colorY_Speed;
                    break;
            }
        }
        rend.material.color = result/guitarInput.Length;
        rend.material.SetColor("_EmissionColor", rend.material.color);

        Color emis = rend.material.color;
        emis.a = 1;
        main.startColor=(emis);
        
        shoot();
    }

    void changeSpeed(float speed)
    {
        main.startSpeed = speed;
        //main.startLifetime = range/speed;
    }

    void setSpread(float spread)
    {
        Vector3 scale = transform.localScale;
        scale.x = scaleFactorYSpread;
        transform.localScale = scale;
    }

    private IEnumerator WaitAndStop(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        print("WaitAndPrint " );
       
        particleSystem.Stop();
    }
}