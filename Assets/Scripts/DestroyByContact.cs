using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

    public GameObject explosion;
    public GameObject playerExplosion;
	public GameObject shieldDisintegrate;
    public int scoreValue;
    private GameController gameController;
	private PlayerController playerController;
	public int maxdamage;
	private int damage;
    void Start()
    {
		damage = 0;
        GameObject gcObject = GameObject.FindWithTag("GameController");
		GameObject pcObject = GameObject.FindWithTag("Player");
        if(gcObject!=null)
        {
            gameController = gcObject.GetComponent<GameController>();
        }
        else
        {
            Debug.Log("GameController object not found");
        }
		if(gcObject!=null)
		{
			playerController = pcObject.GetComponent<PlayerController>();
		}
		else
		{
			Debug.Log("PlayerController object not found");
		}
    }
    void OnTriggerEnter(Collider other)
    {
		//Debug.Log (this.tag);
		if(other.tag=="Shield")
		{
			if(shieldDisintegrate!=null)
			Instantiate (shieldDisintegrate, transform.position, transform.rotation);
			else Instantiate (explosion, transform.position, transform.rotation);
			Destroy (this.gameObject);
			gameController.AddScore(scoreValue);
			return;
			//other.gameObject.SetActive (true);
		}
		if (other.tag == "Boundary" || other.tag=="Powerup" || other.tag=="Enemy" || other.tag=="Hazard")//boundary cannot be destroyed
        {
            return;
        }
		/*if (other.tag == "Enemy" || other.tag=="Hazard")
		{
			if(explosion!= null) Instantiate(explosion, transform.position, transform.rotation);
			Destroy (gameObject);
			GameObject othex = other.GetComponent<DestroyByContact> ().explosion;
			if(othex!=null)
			Instantiate (othex, transform.position, transform.rotation);
			Destroy (other.gameObject);
			return;
		}*/
		/*if (other.tag == "shield") 
		{
			Destroy (gameObject);
		}*/
        if (other.tag == "Player")//if object collides with player, immediately destroy it, and add 1 to player damage
		{
			if(explosion!= null) Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
			playerController.increaseDamage ();
			gameController.UpdateHealth ();
			if (playerController.getHealth() == 0)//destroy player if damage equals 10, game over 
			{
				Destroy (other.gameObject);
				gameController.GameOver ();
				Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
			}
        }
        if(other.tag!="Player")//collision with non player object
        {
			if (other.tag == "L1bolt")//red bolt inflicts 1 damage
				damage = damage + 1;
			if (other.tag == "L2bolt")//green bolt inflicts 2 damage
				damage = damage + 2;
            if (damage >= maxdamage)//if damage exceeds max, destroy this object and add to score
            {
                Instantiate(explosion, transform.position, transform.rotation);
                gameController.AddScore(scoreValue);
                Destroy(gameObject);
            }
            Destroy(other.gameObject);//destroy bolt
        }
    }
}

