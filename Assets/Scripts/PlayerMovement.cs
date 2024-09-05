using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    //Allows speed to be set in scene
    public float moveSpeed;

    public Image HealthBar;
    public Image SprintBar;

    //creates place to write in name of scene
    public string SceneName;

    void Start()
    {
        //Sprint bar starts empty, health starts full
        SprintBar.fillAmount = 0f;
        HealthBar.fillAmount = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        //4-directional movement for player
        float x_value = (Input.GetAxisRaw("Horizontal") * moveSpeed);
        float y_value = (Input.GetAxisRaw("Vertical") * moveSpeed);

        transform.position += new Vector3(x_value * Time.deltaTime, y_value * Time.deltaTime, 0f);
        
        //Sprint bar decreases over time
        SprintBar.fillAmount += Time.deltaTime * 0.3f;

        //Sprint mechanic when holding Left Shift
        if(Input.GetKeyDown(KeyCode.LeftShift)){
            moveSpeed = moveSpeed*3;
        }
        //Sprint bar decreases as player sprints
        if(Input.GetKey(KeyCode.LeftShift)){
            SprintBar.fillAmount -= Time.deltaTime * 0.7f;
        }
        //Player slowed down when they stop sprinting
        if(Input.GetKeyUp(KeyCode.LeftShift)){
            moveSpeed = 1.5f;
        }
        //When sprint runs out player is slowed down
        if(SprintBar.fillAmount == 0f){
            moveSpeed = 1.5f;
        }

        //Sets the Health bar inverse to the Sprint bar
        HealthBar.fillAmount = 1 - SprintBar.fillAmount;

        //Scene restarts if Health bar runs out or sprint bar fills up
        if(HealthBar.fillAmount == 0){
            SceneManager.LoadScene(SceneName);
        }
    }
}
