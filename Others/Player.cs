using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GX.MotorSystem;

public class Player : MonoBehaviour
{

    public enum S { XZ, XY, FF}
    public S s = S.XZ;
    [HideInInspector]
    public Motor motor;

    PlayerCamera playerCamera;
    public Transform camTargetTransform;

    [HideInInspector]
    public float time;

    bool lookEnemy;

    PlayerUI ui;
    Character character;


    void Awake()
    {
       GX.GameManager.PLAYER = gameObject;
        motor = GetComponent<Motor>();
        playerCamera = Camera.main.GetComponent<PlayerCamera>();
        playerCamera.SetCamTarget(camTargetTransform);
    }


    void Start()
    {
        
    }


    void Update()
    {
        Controls();
    }


    void Controls()
    {
        Vector3 i = Input();

        motor.input = i;
        motor.target = transform.position + i;

        time += Time.deltaTime;
    }


    Vector3 Input()
    {
        Vector2 i = new Vector2(UnityEngine.Input.GetAxis("Horizontal"), UnityEngine.Input.GetAxis("Vertical"));
        i = (i.magnitude > 1) ? i.normalized : i;

        switch (s)
        {
            case S.XZ: return playerCamera.transform.rotation * new Vector3(i.x, 0, i.y);
            case S.XY: return playerCamera.transform.rotation * new Vector3(i.x, i.y, 0);
            case S.FF: default: return playerCamera.transform.right * i.x + playerCamera.transform.forward * i.y;
        }
    }
}
