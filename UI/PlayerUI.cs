using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GX.InventorySystem;

public class PlayerUI : MonoBehaviour
{

    public CharacterUI playerCharacterUI;
    public GameObject bar;

    List<CharacterUI> enemiesUI = new List<CharacterUI>();

	void Awake ()
    {
        //playerCharacter = GameObject.FindGameObjectWithTag("Player").GetComponent<ItemHolder>().item.GetComponent<Character>();
        //playerCharacterUI.character = playerCharacter;
    }


    void Update()
    {
        if (GX.GameManager.PLAYER != null && playerCharacterUI.character == null)
            playerCharacterUI.character = GX.GameManager.PLAYER.GetComponent<ItemHolder>().item.GetComponent<Character>();    
    }


    public void CreateEnemyBar(Character c, Transform transfrom)
    {
        CharacterUI u = Instantiate(bar, Camera.main.WorldToScreenPoint(transfrom.position), Quaternion.Euler(Vector3.zero), transform).GetComponent<CharacterUI>();
        u.character = c;
        UIScreen s = u.gameObject.AddComponent<UIScreen>();
        s.i = transfrom;

        enemiesUI.Add(u);
        
    }


    public void AcabarInimigoBarra(Character  c)
    {
        foreach(CharacterUI u in enemiesUI)
        {
            if(u.character == c)
            {
                enemiesUI.Remove(u);
                Destroy(u.gameObject);
                break;
            }
        }
    }
}
