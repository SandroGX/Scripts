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

    Camara camara;
    public Transform camaraTargetPlayer;

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
        camara = Camera.main.GetComponent<Camara>();
        camaraTargetPlayer = transform.Find("Camara Alvo");
        foreach (Control c in controls) c.player = this;
        //character = GetComponent<Game.SistemaInventario.ItemHolder>().item.GetComponent<Character>();
        //ui = GameObject.Find("PlayerUI").GetComponent<PlayerUI>();
    }


    void Start()
    {
        motor.MotorStart();
        camara.OlharSeguirAlvo(camaraTargetPlayer.position);
        camara.rotX = transform.eulerAngles.x;
        camara.rotY = transform.eulerAngles.y;
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

        camara.OlharSeguirAlvo(camaraTargetPlayer.position);
    }


    Vector3 Input()
    {
        Vector3 i = new Vector2(UnityEngine.Input.GetAxis("Horizontal"), UnityEngine.Input.GetAxis("Vertical"));
        i = (i.magnitude > 1) ? i.normalized : i;

        switch (s)
        {
            case S.XZ: i = camara.transform.rotation * new Vector3(i.x, 0, i.y); break;
            case S.XY: i = camara.transform.rotation * new Vector3(i.x, i.y, 0); break;
            case S.FF: default: i = camara.transform.right * i.x + camara.transform.forward * i.y; break;
        }

        return i;
    }
}
