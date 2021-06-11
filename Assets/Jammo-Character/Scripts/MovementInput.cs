using UnityEngine;
using UnityEngine.UI;
using TMPro;

//This script requires you to have setup your animator with 3 parameters, "InputMagnitude", "InputX", "InputZ"
//With a blend tree to control the inputmagnitude and allow blending between animations.
[RequireComponent(typeof(CharacterController))]
public class MovementInput : Photon.MonoBehaviour {

    public float Velocity = 30f;
	[Space]

	public float InputX;
	public float InputZ;
	public Vector3 desiredMoveDirection;
	public bool blockRotationPlayer;
	public float desiredRotationSpeed = 0.1f;
	public Animator anim;
	public float Speed;
	public float allowPlayerRotation = 0.1f;
	public Camera cam;
	public CharacterController controller;
	public bool isGrounded;

    [Header("Animation Smoothing")]
    [Range(0, 1f)]
    public float HorizontalAnimSmoothTime = 0.2f;
    [Range(0, 1f)]
    public float VerticalAnimTime = 0.2f;
    [Range(0,1f)]
    public float StartAnimTime = 0.3f;
    [Range(0, 1f)]
    public float StopAnimTime = 0.15f;

	public GameObject Playercam;

    public float verticalVel;
    private Vector3 moveVector;
	public Joystick joystick;
	public PhotonView photonview;
	public TextMeshProUGUI usernameText;
	public GameObject maincamRef;

	public Image health;

	void Awake()
    {
		if (photonview.isMine)
        {
			maincamRef.SetActive(true);
			usernameText.text = PhotonNetwork.playerName;
        }
		else
        {
			usernameText.text = photonview.owner.name;
			usernameText.color = Color.cyan;
			health.color = Color.red;
        }

	}

	// Use this for initialization
	void Start () {
		anim = this.GetComponent<Animator> ();
		cam = Camera.main;
		controller = this.GetComponent<CharacterController> ();
		joystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<FixedJoystick>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (photonview.isMine)
        {
			InputMagnitude();

			isGrounded = controller.isGrounded;
			if (isGrounded)
			{
				verticalVel -= 0;
			}
			else
			{
				verticalVel -= 1;
			}
			moveVector = new Vector3(0, verticalVel * .2f * Time.deltaTime, 0);
			controller.Move(moveVector);
		}
		
	}
	
    void PlayerMoveAndRotation() {

		var camera = Camera.main;
		var forward = cam.transform.forward;
		var right = cam.transform.right;

		forward.y = 0f;
		right.y = 0f;

		forward.Normalize ();
		right.Normalize ();

		desiredMoveDirection = forward * InputZ + right * InputX;

		if (blockRotationPlayer == false) {
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (desiredMoveDirection), desiredRotationSpeed);
            controller.Move(desiredMoveDirection * Time.deltaTime * Velocity);
		}
	}

	void InputMagnitude() {
		//Calculate Input Vectors
		InputX = joystick.Horizontal;
		InputZ = joystick.Vertical;

		//Calculate the Input Magnitude
		Speed = new Vector2(InputX, InputZ).sqrMagnitude;

        //Physically move player

		if (Speed > allowPlayerRotation) {
			anim.SetFloat ("Blend", Speed, StartAnimTime, Time.deltaTime);
			PlayerMoveAndRotation ();
		} else if (Speed < allowPlayerRotation) {
			anim.SetFloat ("Blend", Speed, StopAnimTime, Time.deltaTime);
		}
	}
}
