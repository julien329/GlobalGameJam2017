using UnityEngine;
using UnityEngine.UI;

public class EffectManager : MonoBehaviour {

    ////////////////////////////////////////////////////////////////////////////////////////////////
    /// VARIABLES
    ////////////////////////////////////////////////////////////////////////////////////////////////

    public int lifeTimeMax = 12;
    public float growScale = 1.04f;

    private Vector3 initSize;
    private Image image;
    private int lifeTime = 0;


    ////////////////////////////////////////////////////////////////////////////////////////////////
    /// UNITY
    ////////////////////////////////////////////////////////////////////////////////////////////////

    void Awake() {
        image = GetComponent<Image>();
    }


    void Start () {
        initSize = this.transform.localScale;
    }
	

	void Update () {
        if (image.enabled == true && lifeTime < lifeTimeMax) {
            transform.localScale = new Vector3(transform.localScale.x * growScale, transform.localScale.y * growScale, transform.localScale.z);
            lifeTime++;
        }

        if (lifeTime >= lifeTimeMax) {
            lifeTime = 0;
            image.enabled = false;
            transform.localScale = initSize;
        }
	}
}
