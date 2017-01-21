using UnityEngine;
using System.Collections;

public class ParticuleEffects : MonoBehaviour
{
    private ParticleSystem particleSystem;

    private ParticleSystemRenderer particleSystemRenderer;
    private Vector3[] _baseVertices;
    private bool RecalculateNormals = false;
    public Mesh meshWave;
    public int scaleFactorX=1;
    public int scaleFactorY = 1;
    public int scaleFactorZ = 1;

    public float speed;
    public float damage;
    public 

    void Start()
    {
        //particleSystem = GetComponent<ParticleSystem>();
        
        particleSystemRenderer = GetComponent<ParticleSystemRenderer>();
            particleSystemRenderer.renderMode=ParticleSystemRenderMode.Mesh;
      
    }

    void Update()
    {
        particleSystemRenderer.mesh = meshWave;

        Mesh mesh = particleSystemRenderer.mesh;
        
        if (mesh != null)
        {

            if (_baseVertices == null)
                _baseVertices = mesh.vertices;
            var vertices = new Vector3[_baseVertices.Length];
            for (var i = 0; i < vertices.Length; i++)
            {
                var vertex = _baseVertices[i];
                vertex.x = vertex.x * scaleFactorX;
                vertex.y = vertex.y * scaleFactorY;
                vertex.z = vertex.z * scaleFactorZ;
                vertices[i] = vertex;
            }
            mesh.vertices = vertices;
            if (RecalculateNormals)
                mesh.RecalculateNormals();
            mesh.RecalculateBounds();
        }

    }



}