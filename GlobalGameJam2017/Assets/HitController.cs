using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitController : MonoBehaviour
{
    public GameObject hitTextObject;
    private Canvas canvas;

	// Use this for initialization
    void Start()
    {
        canvas = GameObject.Find("UI").GetComponent<Canvas>();
    }
    public void createHitText(float damage, Transform transformLocation)
    {
        GameObject hitTextObj = Instantiate(hitTextObject,canvas.transform,false);
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transformLocation.position);
        hitTextObj.transform.position = screenPosition;
 
        HitText hitText = hitTextObj.GetComponent<HitText>();
        hitText.setText(damage.ToString());

    }
}
