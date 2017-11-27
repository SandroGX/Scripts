using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.SistemaMotor;

public class Player : MonoBehaviour
{

    public enum S { XZ, XY, FF}
    public S s = S.XZ;

    Motor motor;
    Camara camara;

    public Transform camaraAlvoPlayer;
    Vector3 camaraAlvo;

    //public Character inimigo;

    public MotorEstado correr;
    public MotorEstado salto;
    public MotorEstado ataque;
    public MotorEstado olharEAndar;

    public float distanciaInimigo = 6;
    public float distanciaNaoInimigo = 8;

    float tempo = float.MinValue;
    public float tempoComboMin = 0.5f;

    bool olharinimigo;
    MotorEstado movAtual;

    PlayerUI ui;
    Character character;



    void Awake()
    {

        motor = GetComponent<Motor>();
        camara = Camera.main.GetComponent<Camara>();
        //character = GetComponent<Game.SistemaInventario.ItemHolder>().item.GetComponent<Character>();
        //ui = GameObject.Find("PlayerUI").GetComponent<PlayerUI>();
    }



    void Start()
    {
        motor.MotorStart();
        camara.OlharSeguirAlvo(camaraAlvo);
        camara.rotX = transform.eulerAngles.x;
        camara.rotY = transform.eulerAngles.y;
    }



    void Update()
    {
        //VerInimigos();
        Controlos();
    }

    

    void Controlos()
    {
        Vector3 i = input();

        motor.input = i;

        //Character inimigo = character.inimigo[0];

        //if (Input.GetButtonDown("Fire3"))
        //{
        //    if (inimigo && !olharinimigo)
        //    {
        //        olharinimigo = true;

        //        Vector3 dir = Vector3.Normalize(inimigo.item.holder.transform.position - transform.position);
        //        camara.rotX = 10;
        //        camara.rotY = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg - 15;
        //    }
        //    else olharinimigo = false;
        //}

        //if (inimigo)
        //    ((CharacterControllerMotor)motor).Alvo = inimigo.item.holder.transform.position;
        //else
        //{
        //    olharinimigo = false;
        //    if (i != Vector3.zero)
        //        ((CharacterControllerMotor)motor).Alvo = transform.position + i.normalized;
        //    else ((CharacterControllerMotor)motor).Alvo = transform.position + transform.forward;
        //}

        //if (olharinimigo)
        //{
        //    camaraAlvo = (inimigo.item.holder.transform.Find("Camara Alvo").position - camaraAlvoPlayer.position) * 0.5f + camaraAlvoPlayer.position;
        //    motor.proximoEstado = olharEAndar;
        //}
        //else
        //{
            camaraAlvo = camaraAlvoPlayer.position;
        //    motor.proximoEstado = motor.defaultEstado;
        //}

        ((CCMotor)motor).Alvo = transform.position + /*camara.transform.forward*/ i;

        if (Input.GetButton("Fire2"))
            motor.proximoEstado = correr;
        else if (motor.estadoAtual == correr) motor.proximoEstado = motor.defaultEstado;

        if (Input.GetButtonDown("Jump"))
            motor.proximoEstado = salto;


        ProximoEstadoCondicao(ataque, Input.GetButtonDown("Fire1"));

        camara.OlharSeguirAlvo(camaraAlvo);
    }



    Vector3 input()
    {
        Vector3 i = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        i = (i.magnitude > 1) ? i.normalized : i;

        switch (s)
        {
            case S.XZ: i = camara.transform.rotation * new Vector3(i.x, 0, i.y); break;

            case S.XY: i = camara.transform.rotation * new Vector3(i.x, i.y, 0); break;

            case S.FF: default: i = camara.transform.right * i.x + camara.transform.forward * i.y; break;
        }

        return i;
    }



    void ProximoEstadoCondicao(MotorEstado proximo, bool comando)
    {
        if (comando)
            tempo = Time.time;


        if (Time.time - tempo <= tempoComboMin)
            motor.proximoEstado = proximo;
        else if (motor.proximoEstado == proximo)
            motor.proximoEstado = motor.defaultEstado;
    }



    void VerInimigos()
    {
        Collider[] cs = Physics.OverlapSphere(transform.position, distanciaInimigo);

        foreach (Collider c in cs)
        {
            Character d = c.GetComponent<Character>();

            if (d && d != character && !character.inimigo.Contains(d))
            {
                character.inimigo.Add(d);
                d.morri += RetirarInimigo;
                ui.IniciarInimigoBarra(d, d.item.holder.transform.Find("BarraVida"));
            }
        }


        for (int i = 0; i < character.inimigo.Count; i++)
        {
            if (character.inimigo[i] != null)
            {
                if (Vector3.Distance(character.inimigo[i].item.holder.transform.position, transform.position) > distanciaNaoInimigo)
                {
                    ui.AcabarInimigoBarra(character.inimigo[i]);
                    character.inimigo[i].morri -= RetirarInimigo;
                    character.inimigo.RemoveAt(i);
                }
            }
        }


        if (!olharinimigo)
        {
            Character p = null;
            float disMin = float.MaxValue;

            for (int i = 0; i < character.inimigo.Count; i++)
            {
                if (character.inimigo[i] != null)
                {
                    float dis = Vector3.Distance(character.inimigo[i].item.holder.transform.position, transform.position);

                    if (dis < disMin)
                    {
                        disMin = dis;
                        p = character.inimigo[i];
                    }
                }
            }

            character.inimigo.Remove(p);
            character.inimigo.Insert(0, p);
        }
    }



    void RetirarInimigo(Character aRetirar)
    {
        ui.AcabarInimigoBarra(aRetirar);
        character.inimigo.Remove(aRetirar);
    }
}
