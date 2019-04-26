using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum ProjectileType
{
    Missile,
    Interceptor
}

//public class Projectile
//{
//    private GameObject go;
//    private ProjectileType projectileType;
//    public Projectile()
//    {
//        go = new GameObject("Projectile");
//        projectileType = ProjectileType.Generic;
//    }

//    public Projectile(string name, ProjectileType projectileType)
//    {
//        go = new GameObject(name);
//        this.projectileType = projectileType;
//    }
//}

public class ProjectileShooter : MonoBehaviour
{

    public GameObject missilePrefab, interceptorPrefab;
    public ProjectileType projectileType = ProjectileType.Missile;
    public float missileMagnitude = 40f;
    public float interceptorMagnitude = 40f;

    //missile info
    public int maxMissileAmmo = 5;
    private int currentMissileAmmo;
    public float researchMissileDuration = 5;
    //interceptor info
    public int maxInterceptorAmmo = 1;
    private int currentInterceptorAmmo;
    public float researchInterceptorDuration = 20;

    private float missileTimestamp;
    private bool missileResearchActive = false;
    private float interceptorTimestamp;
    private bool interceptorResearchActive = false;

    public Text lblAmmo;

    // Use this for initialization
    void Start()
    {
        missileTimestamp = Time.time + researchMissileDuration;
        interceptorTimestamp = Time.time + researchInterceptorDuration;
        currentMissileAmmo = maxMissileAmmo;
        currentInterceptorAmmo = maxInterceptorAmmo;
        UpdateAmmoUI();
        //InvokeRepeating("ResearchMissile", 0, researchMissileDuration);
        //InvokeRepeating("ResearchInterceptor", 0, researchInterceptorDuration);
    }

    // Update is called once per frame
    void Update()
    {
        ResearchMissile();
        ResearchInterceptor();
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 200.0f))
            {

                if (hit.transform.tag == "WeaponUI")
                {
                    //change weapon
                    SwitchWeapon();
                    GameObject activeWeapon = GameObject.FindWithTag("WeaponUI");
                    switch (projectileType)
                    {
                        case ProjectileType.Missile:
                            activeWeapon.SetActive(false);
                            GameObject.Find("WeaponsHolder").transform.GetChild((int)projectileType).gameObject.SetActive(true);
                            break;
                        case ProjectileType.Interceptor:
                            activeWeapon.SetActive(false);
                            GameObject.Find("WeaponsHolder").transform.GetChild((int)projectileType).gameObject.SetActive(true);
                            break;
                    }
                }
                else
                {
                    Fire();
                }
            }
            else
            {
                Fire();
            }

        }

    }

    void Fire()
    {

        Vector3 worldMousePosition = Input.mousePosition;
        worldMousePosition.z = 31.5f;
        worldMousePosition = Camera.main.ScreenToWorldPoint(worldMousePosition);
        worldMousePosition.z = 0;

        Vector3 direction = worldMousePosition - transform.position;
        direction = direction.normalized;

        Vector3 startingPosition = transform.position + direction;

        GameObject projectile = null;
        switch (projectileType)
        {
            case ProjectileType.Missile:
                if (currentMissileAmmo > 0)
                {
                    projectile = Instantiate(missilePrefab, startingPosition, transform.rotation) as GameObject;
                    Rigidbody rb = projectile.GetComponent<Rigidbody>();

                    rb.AddForce(direction * missileMagnitude);
                    currentMissileAmmo--;
                    if (currentMissileAmmo == (maxMissileAmmo - 1))
                    {
                        print("invoking");
                        //StartResearchMissile();
                        missileResearchActive = true;

                    }
                    projectile.transform.forward = direction;

                    Destroy(projectile, 20f);
                }

                break;
            case ProjectileType.Interceptor:
                if (currentInterceptorAmmo > 0)
                {
                    projectile = Instantiate(interceptorPrefab, startingPosition, transform.rotation) as GameObject;
                    Rigidbody rb = projectile.GetComponent<Rigidbody>();

                    rb.AddForce(direction * interceptorMagnitude);

                    currentInterceptorAmmo--;
                    if(currentInterceptorAmmo == (maxInterceptorAmmo - 1))
                    {
                        interceptorResearchActive = true;
                    }
                    projectile.transform.forward = direction;

                    Destroy(projectile, 60f);
                }

                break;
        }

        if (projectile != null)
        {
            //Rigidbody rb = projectile.GetComponent<Rigidbody>();

            //rb.AddForce(direction * magnitude);

            //projectile.transform.forward = direction;

            //Destroy(projectile, 20f);
            UpdateAmmoUI();
        }


    }
    void ResearchMissile()
    {
        if (missileResearchActive)
        {
            if (missileTimestamp < Time.time)
            {
                //do stuff
                if (currentMissileAmmo < maxMissileAmmo)
                {
                    currentMissileAmmo++;
                }

                if (currentMissileAmmo == maxMissileAmmo)
                {
                    //CancelInvoke("ResearchMissile");
                    missileResearchActive = false;
                }

                UpdateAmmoUI();

                missileTimestamp += researchMissileDuration;
            }
        }
        else
        {
            missileTimestamp = Time.time + researchMissileDuration;
        }
        
        
    }

    void ResearchInterceptor()
    {
        if (interceptorResearchActive)
        {
            if(interceptorTimestamp < Time.time)
            {
                if (currentInterceptorAmmo < maxInterceptorAmmo)
                {
                    currentInterceptorAmmo++;
                }

                if (currentInterceptorAmmo == maxInterceptorAmmo)
                {
                    CancelInvoke("ResearchInterceptor");
                }
                UpdateAmmoUI();
                interceptorTimestamp += researchInterceptorDuration;
            }
        }
        else
        {
            interceptorTimestamp = Time.time + researchInterceptorDuration;
        }
        
    }
    void UpdateAmmoUI()
    {
        switch (projectileType)
        {
            case ProjectileType.Missile:
                lblAmmo.text = string.Format("{0}/{1}", currentMissileAmmo, maxMissileAmmo);
                break;
            case ProjectileType.Interceptor:
                //print("Switched to interceptor");
                lblAmmo.text = string.Format("{0}/{1}", currentInterceptorAmmo, maxInterceptorAmmo);
                break;
        }

    }

    private static readonly IDictionary ModeMap = new Dictionary<ProjectileType, ProjectileType>
    {
        {ProjectileType.Missile, ProjectileType.Interceptor },
        {ProjectileType.Interceptor, ProjectileType.Missile }
    };
    void SwitchWeapon()
    {
        projectileType = (ProjectileType)ModeMap[projectileType];
        UpdateAmmoUI();
    }
}
