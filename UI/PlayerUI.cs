using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Game.SistemaInventario;

public class PlayerUI : MonoBehaviour
{

    Character playerCharacter;
    public CharacterUI playerCharacterUI;
    public GameObject barra;

    List<CharacterUI> inimigosUI = new List<CharacterUI>();

	void Awake ()
    {
        playerCharacter = GameObject.FindGameObjectWithTag("Player").GetComponent<ItemHolder>().item.GetComponent<Character>();
        playerCharacterUI.character = playerCharacter;
    }
	
	
	void Update ()
    {
        if (playerCharacterUI.character == null)
        {
            playerCharacter = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
            playerCharacterUI.character = playerCharacter;
        }
    }


    public void IniciarInimigoBarra(Character c, Transform lugar)
    {
        CharacterUI u = Instantiate(barra, Camera.main.WorldToScreenPoint(lugar.position), Quaternion.Euler(Vector3.zero), transform).GetComponent<CharacterUI>();
        u.character = c;
        UIScreen s = u.gameObject.AddComponent<UIScreen>();
        s.i = lugar;

        inimigosUI.Add(u);
        
    }


    public void AcabarInimigoBarra(Character  c)
    {
        foreach(CharacterUI u in inimigosUI)
        {
            if(u.character == c)
            {
                inimigosUI.Remove(u);
                Destroy(u.gameObject);
                break;
            }
        }
    }
}
