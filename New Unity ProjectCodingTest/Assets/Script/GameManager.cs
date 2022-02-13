using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    SpriteRenderer square;
    GameObject selectedSphere;

    //In start method I am setting static method isRotate to false so the sphere will not rotate.
    //and then finding the object of selectedSphere if it find the selected sphere then it will
    //call the function moveToCenter method
    private void Start()
    {
        Sphere.isRotate = false;

        selectedSphere = GameObject.Find("selectedSphere");

        if (selectedSphere != null)
            StartCoroutine(MoveToCenter());

    }

    //this method will be using startCoroutine after every 0.1sec
    //it will add up alpha value by 0.1 the while loop will be active until the value of alpha is less than to 1.1
    IEnumerator FadeInGameObject()
    {
        square = GameObject.Find("Square").transform.GetComponent<SpriteRenderer>();

        var alpha = 0.0f;
        while (alpha < 1.1)
        {
            square.color = new Color(square.color.r, square.color.g, square.color.b, alpha);
            yield return new WaitForSeconds(0.1f);
            alpha += 0.1f;
        }
    }




    //this function will bring the selected sphere to the center of the screen and then 
    //call a function which will fadeIn a gamobject on the selected sphere
    IEnumerator MoveToCenter()
    {
        var center_pos = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(1);

        while (selectedSphere.transform.position != center_pos)
        {
            yield return new WaitForSeconds(0.05f);
            selectedSphere.transform.position = Vector3.Lerp(selectedSphere.transform.position, center_pos, 0.5f);
        }
        StartCoroutine(FadeInGameObject());

    }

    //this function is called on RestartButton and will get the levelNum as a parameter
    //and then by calling LoadScene method from the instance of LevelLoader we will load the scene1
    //Here I am using LoadScene method from levelLoader because we have this levelLoader instance in every scene
    //So we do not have to write the code again for navigating to scene1
    public void RestartLevel(int levelNum)
    {
        LevelLoader.instance.LoadScene(levelNum);
    }
}
