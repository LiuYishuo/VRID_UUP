using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FurnitureLoad : MonoBehaviour
{

    // Dictionary mapping Asset (Furniture) name to the object itself. Assumes Asset names are unique, but keys can be changed to [category] + '/' + [name]'
    private Dictionary<string, GameObject> Furnitures = new Dictionary<string, GameObject>();
    private Transform player;
    private static bool activeObject = false;
    private static GameObject currentFurniture;
    private int ROTATION_SPEED = 200;

    // Select which obj will hold the generated furniture (FurnitureObjects from Inspector)
    public GameObject furnitureContainer;

    // from OVRPlayerController
    public float Acceleration = 0.1f;
    public float Damping = 0.3f;
    public float BackAndSideDampen = 0.5f;
    public float RotationAmount = 1.5f;
    public float RotationRatchet = 45.0f;

    private float MoveScale = 1.0f;
    private Vector3 MoveThrottle = Vector3.zero;
    private float FallSpeed = 0.0f;
    private OVRPose? InitialPose;
    private float InitialYRotation = 0.0f;
    private float MoveScaleMultiplier = 1.0f;
    private float RotationScaleMultiplier = 1.0f;
    private bool SkipMouseRotation = false;
    private bool HaltUpdateMovement = false;
    private bool prevHatLeft = false;
    private bool prevHatRight = false;
    private float SimulationRate = 60f;


    void Start()
    {

        // Test variables. Change as necessary
        //string cat = "bed";
        //string test = "Anes_Double__Bed-3D";
        //string furnitureContainerName = "FurnitureObjects";

        // Assign furniture container so we can append our furniture clones to it
        //furnitureContainer = GameObject.Find(furnitureContainerName);

        //furnitureContainer = this;
        // SpawnFurniture(cat, test);
        
        // DO NOT SET FROM START! NOT WORK FOR SOME REASON
        //player = GameObject.Find("OVRPlayerController").transform; 

    }

    // Update is called once per frame
    void Update()
    {
        if (activeObject)
        {
            // if press LB (Button 4), rotate left
            //Debug.Log("is this valid?");
            //Debug.Log(OVRInput.Button.PrimaryShoulder);

            if (Input.GetKey("joystick button 4"))
            {
                //Debug.Log("left pressed: " + (ROTATION_SPEED * Time.deltaTime));
                currentFurniture.transform.Rotate(0, ROTATION_SPEED * Time.deltaTime, 0);
            }

            // if press RB (Button 5), rotate right
            if (Input.GetKey("joystick button 5"))
            {
                //Debug.Log("Right pressed: " + (ROTATION_SPEED * Time.deltaTime));
                currentFurniture.transform.Rotate(0, (-1) * ROTATION_SPEED * Time.deltaTime, 0);
            }



            // If user presses 'A' again, deactivate the obj and store in FurnitureContainer
            if (OVRInput.GetUp(OVRInput.Button.One))
            {
                Debug.Log("Placed object");
                activeObject = false;
                currentFurniture.transform.parent = furnitureContainer.transform;
                currentFurniture.transform.GetComponent<Rigidbody>().useGravity = true;
                currentFurniture.transform.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }


    public void SpawnFurniture(string path)
    {
        player = GameObject.Find("OVRPlayerController").transform;

        // Check if the furniture itself is in the dictionary
        if (!Furnitures.ContainsKey(path))
        {
            // Create path to the Asset, starting from the '/Resources' folder
            // Load the object 
            GameObject furniture = Resources.Load<GameObject>(path);
            if (furniture == null)
            {
                Debug.Log("Failed to load furniture: " + path);
                return;
            }

            // Pre-processing to set default position and scaling
            furniture.transform.position = this.transform.position + new Vector3(0, 0, 0);
            furniture.transform.localScale = new Vector3(0.2F, 0.2F, 0.2F);
            //furniture.transform.GetComponent<Collider>().attachedRigidbody.useGravity = true;

            furniture.AddComponent<Rigidbody>();
            furniture.transform.GetComponent<Rigidbody>().mass = 1000000;
            furniture.transform.GetComponent<Rigidbody>().isKinematic = true;
            furniture.transform.GetComponent<Rigidbody>().drag = 0; // air resistance to slow object from moving itself
            furniture.transform.GetComponent<Rigidbody>().angularDrag = 0.05f; // air resistance to slow object from rotating around itself

            Debug.Log("Successfully loaded furniture: " + path);
            Furnitures.Add(path, furniture);
        }

        // Instantiate the furniture in the scene but keep return value for further manipulation
        GameObject insF = GameObject.Instantiate(Furnitures[path]);

        // Assign parent object for tidiness
        //insF.transform.parent = furnitureContainer.transform;
        insF.transform.parent = player; // assign furniture to Player

        // Set instantiated furniture and children to 'Active'. The children are the subcomponents of the furniture.
        insF.SetActive(true);
        foreach (Transform child in insF.transform)
        {
            child.gameObject.SetActive(true);
        }

        // set our current pointer to the current furniture
        activeObject = true;
        currentFurniture = insF;
    }


    
    public virtual void UpdateMovement()
    {
        Vector3 moveDirection = Vector3.zero;
        MoveScale = 1.0f;
                
        MoveScale *= SimulationRate * Time.deltaTime;

        // Compute this for key movement
        float moveInfluence = Acceleration * 0.1f * MoveScale * MoveScaleMultiplier;
        

        Quaternion ort = transform.rotation;
        Vector3 ortEuler = ort.eulerAngles;
        ortEuler.z = ortEuler.x = 0f;
        ort = Quaternion.Euler(ortEuler);
        Vector3 euler = transform.rotation.eulerAngles;

        bool curHatLeft = OVRInput.Get(OVRInput.Button.PrimaryShoulder);

        if (curHatLeft && !prevHatLeft)
            euler.y -= RotationRatchet;

        prevHatLeft = curHatLeft;

        bool curHatRight = OVRInput.Get(OVRInput.Button.SecondaryShoulder);

        if (curHatRight && !prevHatRight)
            euler.y += RotationRatchet;

        prevHatRight = curHatRight;
        
        float rotateInfluence = SimulationRate * Time.deltaTime * RotationAmount * RotationScaleMultiplier;

#if !UNITY_ANDROID || UNITY_EDITOR
        if (!SkipMouseRotation)
            euler.y += Input.GetAxis("Mouse X") * rotateInfluence * 3.25f;
#endif

        moveInfluence = Acceleration * 0.1f * MoveScale * MoveScaleMultiplier;
        

        Vector2 secondaryAxis = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);

        if (secondaryAxis.y > 0.0f)
            MoveThrottle += ort * (secondaryAxis.y * transform.lossyScale.z * moveInfluence * Vector3.forward);

        if (secondaryAxis.y < 0.0f)
            MoveThrottle += ort * (Mathf.Abs(secondaryAxis.y) * transform.lossyScale.z * moveInfluence * BackAndSideDampen * Vector3.back);

        if (secondaryAxis.x < 0.0f)
            MoveThrottle += ort * (Mathf.Abs(secondaryAxis.x) * transform.lossyScale.x * moveInfluence * BackAndSideDampen * Vector3.left);

        if (secondaryAxis.x > 0.0f)
            MoveThrottle += ort * (secondaryAxis.x * transform.lossyScale.x * moveInfluence * BackAndSideDampen * Vector3.right);

        //transform.rotation = Quaternion.Euler(euler);
        moveDirection += MoveThrottle * SimulationRate * Time.deltaTime;

        transform.position = moveDirection;

    }
}