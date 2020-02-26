using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovementAndControls : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] private float shotSpeed, shotLife = 10f, shotRotation, shotCooldown = .1f;
    private bool pressA, pressD, pressSpace;
    private LevelController Controller;
    private string poolName;
    private float cooldown = .1f, restartTime = 3f, restartTimer = 0f;
    private bool restart = false;
    [SerializeField] private GameObject leftFire, rightFire;


    // Start is called before the first frame update
    void Start()
    {
        Controller = this.transform.GetComponentInParent<LevelController>();
        poolName = Controller.playerPool;
        this.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        {
            pressA = Input.GetKey(KeyCode.A);
            pressD = Input.GetKey(KeyCode.D);
            pressSpace = Input.GetKey(KeyCode.Space);
        }

        if ((pressA || pressD) && !(pressD && pressA))
        {
            if (pressD)
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime );
            }
            if (pressA)
            {
                transform.Translate(Vector3.left * speed * Time.deltaTime);
            }            
        }
        
        if (transform.position.x > 11.5 || transform.position.x < -11.5)
        {
            Vector3 clampedPos = transform.position;
            clampedPos.x = Mathf.Clamp(clampedPos.x, -11.5f, 11.5f);
            transform.position = clampedPos;
        }

        if (pressSpace && cooldown < 0)
        {
            cooldown = shotCooldown;
            ShipFire();
        }
        cooldown -= Time.deltaTime;

        if (restart && restartTimer > restartTime)
        {
            Restart();
        }
        else if (restart)
        {
            restartTimer += Time.deltaTime;
            Debug.Log(restartTimer);
            Debug.Log(restart);
        }
    }

    private void ShipFire()
    {
        GameObject tempObject = ProjectilePool.instance.GetNextShot(poolName);
        if (tempObject != null)
        {
            ShotBehaviour tempShot = tempObject.GetComponent<ShotBehaviour>();
            tempShot.transform.position = leftFire.transform.position;
            tempShot.InitShot(shotSpeed, 0f, shotLife);
            tempShot.transform.rotation = leftFire.transform.rotation;
        }
        tempObject = ProjectilePool.instance.GetNextShot(poolName);
        if (tempObject != null)
        {
            ShotBehaviour tempShot = tempObject.GetComponent<ShotBehaviour>();
            tempShot.transform.position = rightFire.transform.position;
            tempShot.InitShot(shotSpeed, 0f, shotLife);
            tempShot.transform.rotation = rightFire.transform.rotation;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Respawn")
        {
            Controller.speed = 0;
            restart = true;
        }
    }

    public void Restart()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
