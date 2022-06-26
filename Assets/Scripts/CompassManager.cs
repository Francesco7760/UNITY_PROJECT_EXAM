using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompassManager : MonoBehaviour
{
    Transform player;
    Transform exit;
    Image compass;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        exit = GameObject.FindGameObjectWithTag("Exit").transform;
        compass = GameObject.FindGameObjectWithTag("Compass").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 directionExit = player.position - exit.position;
        float angle = Mathf.Atan2(directionExit.x, directionExit.y) * Mathf.Rad2Deg;
        compass.rectTransform.rotation = Quaternion.Euler(0f,0f, -angle-180);
    }
}
