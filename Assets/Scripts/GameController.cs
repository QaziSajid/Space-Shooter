using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    public GameObject hazard;
    public GameObject hazardbig;
	public GameObject enemy;
	public GameObject ienemy;
	public GameObject denemy;
	public GameObject health;
	public GameObject lpower;
	public GameObject spower;
    public Vector3 spawnvalues;
    private int hazardLowLimit;
    private int hazardUpLimit;
    private int hazardCount;
    private float spawnwait;
    public float startwait;
    private float wavewait;
    private int i;
    public GUIText scoreText;
	public GUIText healthText;
	public GUIText batteryText;
    public GUIText gameOverText;
    public GUIText restartText;
    private bool gameOver;
    private bool restart;
    public int score;
    public GameObject intro;
	public PlayerController player;
	private int whichazard;
	private int level;
	//public int playerdamage;
    void Start()
    {
		GameObject temp=GameObject.FindWithTag("Player");
		if(temp!=null)
			player = temp.GetComponent<PlayerController>();
		spawnwait = 1;
		wavewait = 5;
		hazardLowLimit = 5;
		hazardUpLimit = 12;
        gameOver = false;
        restart = false;
        gameOverText.text = "";
        restartText.text = "";
		healthText.text = "";
		batteryText.text = "";
        Destroy(intro, 3.0f);
        StartCoroutine(SpawnWaves());
		StartCoroutine(SpawnPower());
        score = 0;
        UpdateScore();
		UpdateBattery();
		UpdateHealth();
		level=1;
    }
    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
				SceneManager.LoadScene("main", LoadSceneMode.Single);
            }
            if (Input.GetKeyDown("escape"))
            {
                Application.Quit();
            }
        }
		if (score > 100 && score <= 200 && level!=2) 
		{
			level = 2;
		}
		if (score > 200 && score <= 300 && level != 3) 
		{
			level = 3;
		}
		if (score > 500 && score <= 1000 && level != 4) 
		{
			level = 4;
		}
		if (score > 1000 && level != 5) 
		{
			level = 5;
		}
		if (score > 1000 && score <= 1050) {
			hazardLowLimit = 8;
			hazardUpLimit = 16;
			spawnwait = 0.7f;
			wavewait = 4f;
		}
		if (score > 2000 && score<=2050) {
			hazardLowLimit = 10;
			hazardUpLimit = 25;
			spawnwait = 0.5f;
			wavewait = 2f;
		}
    }
    IEnumerator SpawnWaves()
    {
        
        yield return new WaitForSeconds(startwait);
        while (true)
        {
            hazardCount = (int)Random.Range(hazardLowLimit, hazardUpLimit);
            for (i = 0; i < hazardCount; i++)
            {
				if (gameOver)
				{
					yield return new WaitForSeconds(0);
					restartText.text = "Press 'R' to restart\n\nPress 'Esc' to Quit";
					restart = true;
					break;
				}
                whichazard = Random.Range(0, level);
                Vector3 spawnposition = new Vector3(Random.Range(-spawnvalues.x, spawnvalues.x), spawnvalues.y, spawnvalues.z);
				if(whichazard==0)
					Instantiate(hazard, spawnposition, Quaternion.identity);
				if(whichazard==1)
					Instantiate(hazardbig, spawnposition, Quaternion.identity);
				if(whichazard==2)
					Instantiate(enemy, spawnposition, Quaternion.Euler(0.0f, 180.0f, 0.0f));
				if (whichazard == 3)
					Instantiate (ienemy, spawnposition, Quaternion.identity);
				if(whichazard==4)
					Instantiate(denemy, spawnposition, Quaternion.Euler(0.0f, 180.0f, 0.0f));
                yield return new WaitForSeconds(spawnwait);
            }
			if (gameOver)
				break;
            yield return new WaitForSeconds(wavewait);
            
        }
    }
	IEnumerator SpawnPower()
	{
		int w = 0;
		yield return new WaitForSeconds(30);
		while (true)
		{
			Vector3 spawnposition = new Vector3(Random.Range(-spawnvalues.x, spawnvalues.x), spawnvalues.y, spawnvalues.z);
			if (w==1 && player.getHealth()<=40)
				Instantiate (health, spawnposition, Quaternion.identity);
			if (w==2 || (w==1 && player.getHealth()>40))
				Instantiate (lpower, spawnposition, Quaternion.Euler(90.0f, 0.0f, 0.0f));
			if(w==0)
				Instantiate (spower, spawnposition, Quaternion.identity);
			yield return new WaitForSeconds(30);
			w = (w + 1) % 3;
		}
	}
    public void AddScore(int newScore)
    {
        score += newScore;
        UpdateScore();
    }
	public void UpdateBattery()
	{
		batteryText.text = "Battery: " + player.getBattery () + "%";
	}
	public void UpdateHealth()
	{
		healthText.text = "Health: " + player.getHealth () + "%";
	}
    void UpdateScore()
    {
        scoreText.text = "Score : " + score;
    }
    public void GameOver()
    {
        gameOverText.text = "Game Over";
        gameOver = true;
    }
}