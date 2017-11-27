using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{

    public float rotX, rotY;

    //public Vector3 alvo;
    public Vector2 RotMinMax = new Vector2(-10, 55);
    public float distanciaDoAlvo = 12, camaraVelocidade = 10;
    public float movimentoSuavizacao = 0.3f;
    public float rotacaoSuavizacao = 0.3f;
    public float colliderOffset = 0.2f;

    Vector3 c;
    float cX;
    float cY;


    void Start ()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        //rotX = alvo.eulerAngles.x;
        //rotY = alvo.eulerAngles.y;
    }


    public void OlharSeguirAlvo(Vector3 alvo)
    {
        rotY += Input.GetAxis("Mouse X") * camaraVelocidade;
        rotX += Input.GetAxis("Mouse Y") * camaraVelocidade;
        rotX = Mathf.Clamp(rotX, RotMinMax.x, RotMinMax.y);

        RotPos(alvo);
    }


    public void RotPos(Vector3 alvo)
    {
        Vector3 rotAlvo = new Vector3(Mathf.SmoothDampAngle(transform.eulerAngles.x, rotX, ref cX, rotacaoSuavizacao), Mathf.SmoothDampAngle(transform.eulerAngles.y, rotY, ref cY, rotacaoSuavizacao));
        transform.eulerAngles = rotAlvo;

        RaycastHit hit;
        float distanciaAtual;

        if (Physics.Raycast(alvo, -transform.forward, out hit, distanciaDoAlvo))
            distanciaAtual = Vector3.Distance(alvo, hit.point) - colliderOffset;
        else distanciaAtual = distanciaDoAlvo;

        Vector3 posicaoAlvo = -transform.forward * distanciaAtual + alvo;
        transform.position = Vector3.SmoothDamp(transform.position, posicaoAlvo, ref c, movimentoSuavizacao);
    }
}
