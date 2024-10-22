using Sirius.Audio;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class ViewChanger : MonoBehaviour
{
    [SerializeField] private string AmbianceMusic = "";     //nom de la music qui est jouer lorsque le joueur entre dans la zone
    [SerializeField] private int DefaultLayer = 0;          //numerau du layer qui doit etre afficher
    [SerializeField] private int WallLayer = 7;             //numerau du layer qui n'est pas affecter par le raycast
    [SerializeField] private int InvisibleLayer = 6;        //numerau du layer qui ne doit pas etre visible
    [SerializeField] private GameObject Room;

    private bool InRoom = false;

    private void Start()
    {
        if(!InRoom)
        {
            //si le joueur ne demare pas dans la zone, elle devien invisible
            Room.layer = InvisibleLayer;
            RoomChild(InvisibleLayer, InvisibleLayer);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            //quand le joueur rentre dans la zone; la zone devien visible
            Room.layer = DefaultLayer;
            RoomChild(DefaultLayer, WallLayer);
            
            InRoom = true;

            //jouet une music si besoin
            AudioManager.Singleton.PlayAudio(AmbianceMusic);
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            //quand le joueur sort de la zone; la zone devien invisible
            Room.layer = InvisibleLayer;
            RoomChild(InvisibleLayer, InvisibleLayer);

            InRoom = false;
        }
    }


    private void RoomChild(int Layer, int wallLayer)
    {
        foreach (Transform child in Room.GetComponentsInChildren<Transform>(true))
        {

            if(child.tag == "Floor")
            {
                child.gameObject.layer = Layer;
            }
            else if (child.tag == "Untagged")
            {
                child.gameObject.layer = wallLayer;
            }
            else
            {
                child.gameObject.layer = wallLayer;
            }
            
        }
    }

}
