using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Camera mapCamera;
    public Camera tankCamera;

    public Rect pictureInPictureRect;
    public Rect activeCameraRect;

    public Camera activeCamera;

    // Use this for initialization
    void Start () {
        activeCamera = mapCamera;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            activeCamera.rect = pictureInPictureRect;
            activeCamera.depth = 0;
            if(activeCamera == mapCamera)
            {
                tankCamera.rect = activeCameraRect;
                tankCamera.depth = -1;
                activeCamera = tankCamera;
            }
            else
            {
                mapCamera.rect = activeCameraRect;
                mapCamera.depth = -1;
                activeCamera = mapCamera;
            }
        }

    }
}
