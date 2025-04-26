using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SelfRotate : MonoBehaviour
{
    public float rotationSpeed;
    private GameObject espinhosObject;

    void Start()
    {
        espinhosObject = transform.Find("Espinhos").gameObject; // encontra o GameObject filho chamado "espinhos" e armazena a referência na variável 'espinhos'
    }

    void Update(){
        espinhosObject.transform.Rotate(new Vector3(rotationSpeed,0,0));
    }

}
