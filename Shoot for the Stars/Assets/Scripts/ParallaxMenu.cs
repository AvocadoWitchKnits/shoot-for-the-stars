using UnityEngine;
using UnityEngine.InputSystem;

public class ParallaxMenu : MonoBehaviour
{
    public InputActionReference mousePosition;

    public Transform background;
    public Transform background1;
    public Transform background2;
    public Transform background3;
    
    public Transform midground;
    public Transform midground2;
    public Transform midground3;
   
    public Transform foreground;
    public Transform foreground1;
    public Transform foreground2;

    public float backgroundMovement = 0.2f;
    public float background1Movement = 0.2f;
    public float background2Movement = 0.2f;
    public float background3Movement = 0.2f;

    public float midgroundMovement = 0.5f;
    public float midground2Movement = 0.5f;
    public float midground3Movement = 0.5f;

    public float foregroundMovement = 1f;
    public float foreground1Movement = 1f;
    public float foreground2Movement = 1f;

    Vector3 backgroundStart;
    Vector3 background1Start;
    Vector3 background2Start;
    Vector3 background3Start;
   
    Vector3 midgroundStart;
    Vector3 midground2Start;
    Vector3 midground3Start;
   
    Vector3 foregroundStart;
    Vector3 foreground1Start;
    Vector3 foreground2Start;

    void Start() //Does not change positions to origin point
    {
        backgroundStart = background.localPosition;
        background1Start = background1.localPosition;
        background2Start = background2.localPosition;
        background3Start = background3.localPosition;
       
        midgroundStart = midground.localPosition;
        midground2Start = midground2.localPosition;
        midground3Start = midground3.localPosition;
       
        foregroundStart = foreground.localPosition;
        foreground1Start = foreground1.localPosition;
        foreground2Start = foreground2.localPosition;
    }

    void OnEnable()
    {
        mousePosition.action.Enable();
    }

    void OnDisable()
    {
        mousePosition.action.Disable();
    }

    void Update()
    {
        Vector2 mouse = mousePosition.action.ReadValue<Vector2>();

        float x = (mouse.x / Screen.width) - 0.5f;
        float y = (mouse.y / Screen.height) - 0.5f;

        background.localPosition = backgroundStart +
           new Vector3(x * backgroundMovement, y * backgroundMovement, 0);
        background1.localPosition = background1Start +
          new Vector3(x * background1Movement, y * background1Movement, 0);
        background2.localPosition = background2Start +
          new Vector3(x * background2Movement, y * background2Movement, 0);
        background3.localPosition = background3Start +
          new Vector3(x * background3Movement, y * background3Movement, 0);

        midground.localPosition = midgroundStart +
            new Vector3(x * midgroundMovement, y * midgroundMovement, 0);
        midground2.localPosition = midground2Start +
            new Vector3(x * midground2Movement, y * midground2Movement, 0);
        midground3.localPosition = midground3Start +
            new Vector3(x * midground3Movement, y * midground3Movement, 0);



        foreground.localPosition = foregroundStart +
            new Vector3(x * foregroundMovement, y * foregroundMovement, 0);
        foreground1.localPosition = foreground1Start +
            new Vector3(x * foreground1Movement, y * foreground1Movement, 0);
        foreground2.localPosition = foreground2Start +
            new Vector3(x * foreground2Movement, y * foreground2Movement, 0);
        
    }
}