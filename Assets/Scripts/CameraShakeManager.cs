using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShakeManager : MonoBehaviour
{

    public float globalShakeForce = 1f;
    void Start()
    {
        
    }

    public void CameraShake(CinemachineImpulseSource impulseSource, float force = 0.1f){
        impulseSource.GenerateImpulseWithForce(force);
    }


}
