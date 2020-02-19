using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBehaviour : MonoBehaviour
{
    private float _shotRotation, _shotSpeed, _lifespan, lifeTime;
    [SerializeField] float shotRotation = 0f, shotSpeed = 10f, lifespan = 10f;
   

    // Update is called once per frame
    void Update()
    {
        if (lifeTime <= 0) this.gameObject.SetActive(false);

    }

    public void InitShot()
    {
        _shotRotation = shotRotation;
        _shotSpeed = shotSpeed;
        _lifespan = lifespan;
        lifeTime = _lifespan;
        this.gameObject.SetActive(true);
    }

    public void InitShot(float speed, float rotation, float newLifespan)
    {
        _shotSpeed = speed;
        _shotRotation = rotation;
        _lifespan = newLifespan;
        lifeTime = _lifespan;
        this.gameObject.SetActive(true);
    }
}
