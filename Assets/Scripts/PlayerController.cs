using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Boundary
{
    public float xMax, xMin, zMax, zMin;
};

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float tilt;
	private int battery;
	private float recharge;
    private Rigidbody rb;
    public Boundary boundary=new Boundary();
    public GameObject shot;
    public GameObject shotbig;
	public GameObject shield;
    public Transform shotSpawn;
    private float fireRate;
    public AudioSource audio;
	public int weaponlevel;
	private float damp;
    private float nextFire;
    public GameController getscorer;
	private int playerdamage;
	public int shieldtime;
	private float starttime;
    void Update()

    {
		if (Time.time > starttime + shieldtime)
			shield.SetActive (false);
        if (getscorer.score >= 1000 && weaponlevel==1) UpgradeWeapon();
		if (Time.time > recharge && battery<100)
		{
			recharge = 1 + Time.time;
			battery = battery + 1;
			getscorer.UpdateBattery ();
		}
		if (battery > 20)
			damp = 1;
        if (Input.GetButton("Fire1") && Time.time > nextFire && battery>0)
        {
			battery = battery - 1;
			getscorer.UpdateBattery ();
            nextFire = Time.time + fireRate*damp;
            if (weaponlevel==2)
            Instantiate(shotbig, shotSpawn.position, shotSpawn.rotation);
            if(weaponlevel==1)
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            audio.Play();
			getscorer.restartText.text = "";
			if (battery < 20)
				damp = damp * 1.05f;
        }
        
    }
    void UpgradeWeapon()
    {
		battery = 100;
		getscorer.UpdateBattery ();
        weaponlevel++;
		fireRate = fireRate*0.8f;
		getscorer.restartText.text = "Weapon Upgraded";
    }

    void Start()
    {
		battery = 100;
		getscorer.UpdateBattery ();
        weaponlevel = 1;
        fireRate = 0.25f;
        rb = GetComponent<Rigidbody>();
        AudioSource audio = GetComponent<AudioSource>();
    }
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement * speed;
        rb.position = new Vector3
         (
             Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
             0.0f,
             Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
         );
        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }
	public int getBattery()
	{
		return battery;
	}
	public int getHealth()
	{
		return 100 - playerdamage * 10;
	}
	public void restoreHealth()
	{
		playerdamage = 0;
		getscorer.UpdateHealth ();
	}
	public void restoreBattery()
	{
		battery = 100;
		getscorer.UpdateBattery ();
	}
	public void increaseDamage()
	{
		++playerdamage;
	}
	public void activateShield()
	{
		starttime = Time.time;
		shield.SetActive (true);
	}
	public float getX()
	{
		return transform.position.x;
	}
}