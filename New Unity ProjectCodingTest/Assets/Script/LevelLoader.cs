using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance;
    
    [Header("Animator")]
    [SerializeField] Animator levelLoader;
   
    [Header("UI")]
    [SerializeField] Button button1;
    [SerializeField] Button button2;
    [SerializeField] Button button3;

    ColorBlock colorBlock_button1, colorBlock_button2, colorBlock_button3;

    private Color defaultColor = Color.grey;
    private Color selectedLevelColor = Color.green;

    float startAnimTime = 2.0f;
    float endAnimTime = 0.5f;


    // Start is called before the first frame update
    //In start method I am setting up instance for this gameObject but first I will check that if
    //instance is null or not and destroying the gameobject if it is not null. if I will not do this then 
    //it will create multiple instance of this object. Also I am assigning the buttons color to colorBlock
    //so we can change the colour of the button when we want
    void Start()
    {

        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(instance);

        colorBlock_button1 = button1.colors;
        colorBlock_button2 = button2.colors;
        colorBlock_button3 = button3.colors;

    }

    //This function is called on timeline buttons which will pass the levelNum as a parameter
    //so then we can start the coroutine method which will seamlessly navigate to the next or selected scene
    public void LoadScene(int levelNum)
    {
        StartCoroutine(LoadNewScene(levelNum));
    }

    //this method will first fadeIn screen black and then wait for some time and load the new scene
    //after new scene is load it will check levelNum is not equal to 2 then it will find for gameObject by name
    //if it find the gameObject it will destroy that gameobject because we donot want it in our scene 1 and 2
    //again it will wait for few seconds and then fadeout animation will be called after fadeout we can see
    //that our new scene is loaded. and after that it will call the buttonColorChanger function and pass the level num as parameter

    IEnumerator LoadNewScene(int levelNum)
    {
        levelLoader.SetTrigger("Start");

        yield return new WaitForSeconds(startAnimTime);

        SceneManager.LoadSceneAsync(levelNum);

        //this piece of code is used to check if selectedSphere is not destroyed
        //if scene navigate to another scene other then scene2
        if (levelNum != 2)
        {
            Destroy_GameObject(GameObject.Find("selectedSphere"));
        }

        yield return new WaitForSeconds(endAnimTime);

        levelLoader.SetTrigger("End");

        buttonColorChanger(levelNum);

    }

    //In this method we will set the colour of the buttons using the levelNum it will check levelNum
    //using if conditions and set the button of the current scene to green and remaining into Grey color
    void buttonColorChanger(int levelNum)
    {
        if (levelNum == 1)
        {
            colorBlock_button2.normalColor = selectedLevelColor;
            button2.colors = colorBlock_button2;

            colorBlock_button1.normalColor = defaultColor;
            button1.colors = colorBlock_button1;

            colorBlock_button3.normalColor = defaultColor;
            button3.colors = colorBlock_button3;

        }
        else if (levelNum == 2)
        {
            colorBlock_button3.normalColor = selectedLevelColor;
            button3.colors = colorBlock_button3;

            colorBlock_button1.normalColor = defaultColor;
            button1.colors = colorBlock_button1;

            colorBlock_button2.normalColor = defaultColor;
            button2.colors = colorBlock_button2;

        }
        else
        {
            colorBlock_button1.normalColor = selectedLevelColor;
            button1.colors = colorBlock_button1;

            colorBlock_button2.normalColor = defaultColor;
            button2.colors = colorBlock_button2;

            colorBlock_button3.normalColor = defaultColor;
            button3.colors = colorBlock_button3;

        }
    }

    void Destroy_GameObject(GameObject selectedSphere)
    {
        if (selectedSphere != null)
            Destroy(selectedSphere);
    }
}
