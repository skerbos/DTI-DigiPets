using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFaceCenter : MonoBehaviour
{
    public GameObject centerPoint;
    private Vector2 faceDir;
    private float faceAngle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        faceDir = (Vector2)(centerPoint.transform.position - transform.position);
        faceAngle = Mathf.Atan2(faceDir.y, faceDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, faceAngle-90);

    }
}
