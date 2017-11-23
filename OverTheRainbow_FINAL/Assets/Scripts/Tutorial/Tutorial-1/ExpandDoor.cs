﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ExpandDoor : MonoBehaviour
{
    public float ZoomDist;
    private GameObject Tutorialchange;
    private BoxCollider2D objectcollider;
    public bool[] gettouch = new bool[2];

    void Start()
    {
        gettouch[0] = false;
        gettouch[1] = false;
        Tutorialchange = GameObject.Find("Dog");
        objectcollider = gameObject.GetComponent<BoxCollider2D>();
    }

    void Update()
    {

        //오브젝트 크기가 작을 때만 작동
        if (gameObject.transform.localScale.x < 2 && gameObject.transform.localScale.y < 2)
        {
            //터치를 두개 이상 받으면
            if (Input.touchCount == 2)
            {
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                Vector3 touch0 = Camera.main.ScreenToWorldPoint(new Vector3(touchZero.position.x, touchZero.position.y, 0));
                Vector3 touch1 = Camera.main.ScreenToWorldPoint(new Vector3(touchOne.position.x, touchOne.position.y, 0));

                Vector3 middletouch = (touch0 + touch1) / 2;
                Vector2 centerPos = new Vector2(middletouch.x, middletouch.y);

                if (gameObject.GetComponent<BoxCollider2D>().bounds.Contains(centerPos))
                {
                    // Find the position in the previous frame of each touch.
                    Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                    Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                    // Find the magnitude of the vector (the distance) between the touches in each frame.
                    float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                    float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                    // Find the difference in the distances between each frame.
                    float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
                    float change = -deltaMagnitudeDiff;//deltaMagnitudeDiff는 손가락 사이의 벌어짐을 -값으로 받아온다.
                    if (deltaMagnitudeDiff < 0)
                    {
                        gameObject.transform.localScale += new Vector3(change * Time.deltaTime, change * Time.deltaTime, 0);
                    }
                }
            }
        }
        else if (gameObject.transform.localScale.x >= 2 && gameObject.transform.localScale.y >= 2)
            Tutorialchange.GetComponent<DogControl>().Moving();

    }
}

