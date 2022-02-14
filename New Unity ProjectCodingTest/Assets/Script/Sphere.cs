using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sphere : MonoBehaviour
{
    public static bool isRotate;

    [Header("Transform Objects")]
    [SerializeField]
    Transform rotationCenter;

    [Header("Sphere Controlling Data")]
    [SerializeField] float rotationRadius;
    [SerializeField] float angularSpeed;

    float posX, posY, angle = 0f;

    GameObject selectedSphere;

    Animator circle1, circle2, circle3;
    CircleCollider2D circle1_collider, circle2_collider, circle3_collider;
    Sphere circle1_script, circle2_script, circle3_script;
    int nextScene;

    private void Awake()
    {
        isRotate = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (isRotate)
        {
            Rotate();
        }


    }

    //this function will be called inside update if the isRotate bool is true.
    //this will then calculate the position of X,Y and update the transform
    //of selected sphere so it can rotate 
    void Rotate()
    {
        posX = rotationCenter.position.x + Mathf.Cos(angle) * rotationRadius;
        posY = rotationCenter.position.y + Mathf.Sin(angle) * rotationRadius;

        transform.position = new Vector2(posX, posY);

        angle = angle + Time.deltaTime * angularSpeed;

        if (angle >= 360)
            angle = 0f;
    }

    //this function will be called when any of the sphere is clicked by the user 
    // it will get the name of selected sphere and also it is calling DontDestroyOnLoad method
    //So the selected sphere will not be destroyed when user navigate to scene3
    //Under this method fadeSpheres method is called and passing the selected gameobject so it will fade out and remove functionality of unselected spheres

    private void OnMouseDown()
    {
        nextScene = SceneManager.GetActiveScene().buildIndex + 1;

        string sphereName = this.gameObject.name;

        fade_and_removeCompnents_of_unselectedSphere(sphereName);

        selectedSphere = GameObject.Find($"{sphereName}");
        selectedSphere.name = "selectedSphere";

        DontDestroyOnLoad(selectedSphere);
        LevelLoader.instance.LoadScene(nextScene);
    }

    //this method will check which sphere is selected by using it's name 
    //and then by using if condition it will fade out the remaining spheres
    //and also it will get the collider and script component of the remaining spheres.
    //and disable them but it will disable collider even for selected Sphere
    //so the onMouseDown functionality does not work again when the user re-select the sphere.
    void fade_and_removeCompnents_of_unselectedSphere(string circleName)
    {
        Get_SphereComponents();

        if (circleName == "Circle1")
        {
            circle2.SetTrigger("fade");
            circle3.SetTrigger("fade");

            circle1_collider.enabled = !circle1_collider.enabled;
            circle2_collider.enabled = !circle2_collider.enabled;
            circle3_collider.enabled = !circle3_collider.enabled;

            circle2_script.enabled = false;
            circle3_script.enabled = false;
        }
        if (circleName == "Circle2")
        {
            circle1.SetTrigger("fade");
            circle3.SetTrigger("fade");

            circle1_collider.enabled = !circle1_collider.enabled;
            circle2_collider.enabled = !circle2_collider.enabled;
            circle3_collider.enabled = !circle3_collider.enabled;

            circle1_script.enabled = false;
            circle3_script.enabled = false;
        }
        if (circleName == "Circle3")
        {
            circle1.SetTrigger("fade");
            circle2.SetTrigger("fade");

            circle1_collider.enabled = !circle1_collider.enabled;
            circle2_collider.enabled = !circle2_collider.enabled;
            circle3_collider.enabled = !circle3_collider.enabled;

            circle1_script.enabled = false;
            circle2_script.enabled = false;
        }
    }

    void Get_SphereComponents()
    {
        circle1 = GameObject.Find("Circle1").transform.GetComponent<Animator>();
        circle2 = GameObject.Find("Circle2").transform.GetComponent<Animator>();
        circle3 = GameObject.Find("Circle3").transform.GetComponent<Animator>();

        circle1_collider = GameObject.Find("Circle1").transform.GetComponent<CircleCollider2D>();
        circle2_collider = GameObject.Find("Circle2").transform.GetComponent<CircleCollider2D>();
        circle3_collider = GameObject.Find("Circle3").transform.GetComponent<CircleCollider2D>();

        circle1_script = GameObject.Find("Circle1").transform.GetComponent<Sphere>();
        circle2_script = GameObject.Find("Circle2").transform.GetComponent<Sphere>();
        circle3_script = GameObject.Find("Circle3").transform.GetComponent<Sphere>();

    }

}
