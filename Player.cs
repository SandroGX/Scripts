using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.MotorSystem;

public class Player : MonoBehaviour
{

    public enum S { XZ, XY, FF}
    public S s = S.XZ;
    [HideInInspector]
    public Motor motor;

    PlayerCamera playerCamera;
    public Transform camTargetTransform;

    public Control[] controls;
    [HideInInspector]
    public float time;

    bool lookEnemy;

    PlayerUI ui;
    Character character;


    void Awake()
    {
        GameManager.PLAYER = gameObject;
        motor = GetComponent<Motor>();
        playerCamera = Camera.main.GetComponent<PlayerCamera>();
        playerCamera.SetCamTarget(camTargetTransform);
        foreach (Control c in controls) c.player = this;
        //character = GetComponent<Game.SistemaInventario.ItemHolder>().item.GetComponent<Character>();
        //ui = GameObject.Find("PlayerUI").GetComponent<PlayerUI>();
    }


    void Start()
    {
        motor.MotorStart();
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
        foreach (Control c in controls) c.Action(motor);
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
