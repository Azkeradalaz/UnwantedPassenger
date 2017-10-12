using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour {


    private Rigidbody2D myRigidBody;
	private Animator myAnim;
	[Range(0, 1)]
	[SerializeField]
	private float crouchSpeedModifier =0.7f;

    [SerializeField]
    private float movementSpeed;
	private float tmpSpeedModifier = 1f;

    [SerializeField]
    private float jumpForce;

	[SerializeField]
	private LayerMask shootableLayers;

	private Transform groundCheck;
	const float groundedRadius = .2f;

	private Transform CellarCheck;
	const float cellarRadius = .1f;

	[SerializeField]
    private float groundRadius;
    [SerializeField]
    private LayerMask whatIsGround;
	[SerializeField]
	private LayerMask whatIsCellar;

	[SerializeField]
	private GameObject shootingPoint;
	[SerializeField]
	private Camera camMain;


	private bool isCrouching;
    private bool facingRight;
    private bool isGrounded;
    private bool jump;
    [SerializeField]
    private bool airControl;

    //[SerializeField]
    private float airForce;


    public bool onLadder;
    public float climbSpeed;
    private float climbVelocity;
    private float gravityStore;
    
    //Oxygen
    [SerializeField]
    private Stat oxygen;
    [SerializeField]
    private float oxygenDecRate;
    private float movementO2Multi=1f;
    
    //Energy
    [SerializeField]
    private Stat energy;
    [SerializeField]
    private float energyDecRate;

	public bool useButtonActive;


	private bool lightCanWork = true;
	[SerializeField]
	private GameObject lightInHand; //for disabling flashlight

	private bool changingLight;

    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();


	public bool isDead = true;

	[SerializeField]
	private GameObject bulletTrailPrefab;


	private Text gameOverText;

	private SpriteRenderer playerSprite;
	//private SpriteRenderer handSprite;

	private Vector3 upVect; //for normalising slope movement

	private PolygonCollider2D standCollider;
	private CircleCollider2D sitCollider;

	private bool hasRedCard;
	private bool hasBlueCard;
	private bool hasGreenCard;
	private bool hasSilverCard;
	private bool hasGoldCard;


	[SerializeField]
	private Text upgradeCountText;
	private int upgradeCount=0; //count of upgrade components


	private void Awake()
    {
		myAnim = gameObject.GetComponent<Animator>();
		playerSprite = gameObject.GetComponent<SpriteRenderer>();
		standCollider = gameObject.GetComponent<PolygonCollider2D>();
		sitCollider = gameObject.GetComponent<CircleCollider2D>();
		//handSprite = transform.GetChild(0).GetChild(2).GetComponent<SpriteRenderer>();
		groundCheck = transform.Find("GroundPoint");
		CellarCheck = transform.Find("CeilingPoint");
		facingRight = true;
		gameOverText = GameObject.Find("GameOverText").GetComponent<Text>();
		oxygen.Initialize();
        energy.Initialize();
    }

	private void Start () {
		facingRight = true;
        myRigidBody = GetComponent<Rigidbody2D>();
        AddKeys();
        gravityStore = myRigidBody.gravityScale;
		airControl = true;

	}
	

	// Update is called once per frame
	private void FixedUpdate () {

		isGrounded = false;
		Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
				isGrounded = true;
		}

		FlipToMousePosition();

		if (myRigidBody.velocity.y > 5f)
		{
			myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, 5f);
		}
		myAnim.SetBool("isGrounded", isGrounded);
		//InputControlFixedUpdate();

	}

    void Update()
    {
		
		FaceToMouse();
		if (isDead) { return; }
		
		
		float horizontal = Input.GetAxis("Horizontal");
		myAnim.SetFloat("horizontal", Mathf.Abs(horizontal));
		InputControlUpdate();
		
		CrouchingControl();
		MovementControl(horizontal);
		OxygenControl();
        EnergyControl();
		ResetBoolValues();
		DeathController();
		LightControl();


	}


    private void AddKeys()
    {
		//keys.Add("MoveRightButton", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("MoveRightButton", "D")));
		//keys.Add("MoveLeftButton", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("MoveLeftButton", "A")));
		keys.Add("JumpButton", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("JumpButton", "Space")));
		keys.Add("CrouchButton", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("CrouchButton", "LeftControl")));
		keys.Add("ShootButton", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ShootButton", "Mouse0")));
		keys.Add("UseButton", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("UseButton", "E")));
		keys.Add("Light", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Light", "Q")));

	}


	private IEnumerator WaitForSec(float wait)
	{
		yield return new WaitForSeconds(wait);
	}

	private void InputControlUpdate()
    {

		//if (Input.GetKey(keys["MoveRightButton"]))
		//{
		//	myRigidBody.velocity.Set(50)
		//}
		//if (Input.GetKey(keys["MoveLeftButton"]))
		//{
		//	myRigidBody.AddForce(new Vector2(-50f, 0f));
		//}

		if (Input.GetKeyDown(keys["JumpButton"]))
        {
            jump = true;
		}


		if (Input.GetKey(keys["CrouchButton"]))
		{
			
			if (!isCrouching && isGrounded)
			{
				isCrouching = true;
				standCollider.enabled = false;
				sitCollider.enabled = true;
			}
		}
		//if (Input.GetKeyUp(keys["CrouchButton"]))
		//{
		//	standCollider.enabled = true;
		//	sitCollider.enabled = false;
		//}

		if (Input.GetKeyDown(keys["ShootButton"]))
		{
			Shoot();
		}
		if (Input.GetKeyDown(keys["UseButton"]))
		{
			useButtonActive = true;
		}
		if (Input.GetKeyUp(keys["UseButton"]))
		{
			useButtonActive = false;
		}


		if (!changingLight)
		{
			if (Input.GetKeyDown(keys["Light"])&& lightCanWork)
			{
				float x = lightInHand.transform.localScale.x;
				int y = (int)x;
				switch (y)
				{
					case 0:
						lightInHand.transform.localScale = new Vector3(10, 10, 1);
						break;
					case 10:
						lightInHand.transform.localScale = new Vector3(0, 0, 1);
						break;

				}
				//lightInHand.active = !lightInHand.active;
				changingLight = true;
				WaitForSec(0.05f);
				changingLight = false;

			}
		}


		NormalizeSlope();
		#region for floating in 0 gravity
		//if (Input.GetKeyDown(KeyCode.W))
		//{
		//    //myRigidBody.AddForce(new Vector2(0, jumpForce));
		//    myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, movementSpeed);
		//}
		//if (Input.GetKeyUp(KeyCode.W))
		//{
		//    //myRigidBody.AddForce(new Vector2(0, jumpForce));
		//    myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, 0);
		//}

		//if (Input.GetKeyDown(KeyCode.S))
		//{
		//    //myRigidBody.AddForce(new Vector2(0, jumpForce));
		//    myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, -movementSpeed);
		//}
		//if (Input.GetKeyUp(KeyCode.S))
		//{
		//    //myRigidBody.AddForce(new Vector2(0, jumpForce));
		//    myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, 0);
		//}
		#endregion
	}

    private void MovementControl(float horizontal)
    {
		if (isCrouching)
		{
			tmpSpeedModifier = crouchSpeedModifier;
		}
		else
		{
			tmpSpeedModifier = 1f; 
		}

        if (isGrounded || airControl)
        {
            myRigidBody.velocity = new Vector2(horizontal * movementSpeed*tmpSpeedModifier, myRigidBody.velocity.y);
		}

		if (isGrounded && jump && !onLadder)
        {
			myAnim.SetBool("isGrounded", isGrounded);
			myAnim.SetBool("jump", jump);
			isGrounded = false;
			jump = false;
			StartCoroutine(WaitToResetJump());
			myRigidBody.AddForce(new Vector2(0f, jumpForce));
        }

        /**ladder mechanic*/
        if (onLadder)
        {
            myRigidBody.gravityScale = 0f;
            climbVelocity = climbSpeed * Input.GetAxisRaw("Vertical");

            myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, climbVelocity);
        }
        if (!onLadder)
        {
            myRigidBody.gravityScale = gravityStore;
        }
        /**ladder mechanic*/




    }


    void NormalizeSlope()
    {
		// Attempt vertical normalization
		if (isGrounded && !jump)
		{
            RaycastHit2D hit = Physics2D.Raycast(myRigidBody.transform.position, Vector2.down, 10f, whatIsGround);

            if (hit && Mathf.Abs(hit.normal.x) > 0.1f)
            {

				//Rigidbody2D myRigidBody = GetComponent<Rigidbody2D>();
				// Apply the opposite force against the slope force 
				// You will need to provide your own slopeFriction to stabalize movement
				myRigidBody.velocity = new Vector2(myRigidBody.velocity.x - (hit.normal.x * /*slopeFriction*/0), myRigidBody.velocity.y);

                //Move Player up or down to compensate for the slope below them
                Vector3 pos = myRigidBody.transform.position;
                pos.y += -hit.normal.x * Mathf.Abs(myRigidBody.velocity.x) * Time.deltaTime * (myRigidBody.velocity.x - hit.normal.x > 0 ? 1 : -1);
				
                myRigidBody.transform.position = pos;
				//myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, 0);
			}


        }
    }

    private void ResetBoolValues()
    {
        jump = false;

		
    }
    private void OxygenControl()
    {
        //(if oxygen.CurrentVal > oxygen.minVal)
        if (myRigidBody.velocity.x != 0 || myRigidBody.velocity.y > 0)
        {
            oxygen.CurrentVal -= oxygenDecRate * Time.deltaTime * movementO2Multi;
            
        }
        else
        {
            oxygen.CurrentVal -= oxygenDecRate * Time.deltaTime;
        }

    }

    private void EnergyControl()
    {
		if (lightInHand.transform.localScale != new Vector3(0, 0, 1))
		{
			energy.CurrentVal -= energyDecRate * Time.deltaTime;
		}
    }

	private void Shoot()
	{
		Effect();
	}




	private void Effect()
	{
		if (energy.CurrentVal >= 5)
		{
			energy.CurrentVal -= 5;
			Instantiate(bulletTrailPrefab, shootingPoint.transform.position, shootingPoint.transform.rotation);
		}

	}

	private void DeathController()
	{
		if (oxygen.CurrentVal <=0)
		{
			Death();
		}
	}

	private void OnTriggerEnter2D(Collider2D _collision)
	{
		if (_collision.tag == "Projectile")
		{
			Death();
		}
	}

	//private void OnCollisionEnter2D(Collision2D _collision)
	//{
	//	if (_collision.gameObject.tag == "Movable" && _collision.relativeVelocity.magnitude > 10)
	//	{
	//		Collider2D[] collidersCel = Physics2D.OverlapCircleAll(CellarCheck.position, cellarRadius, whatIsCellar);
	//		for (int i = 0; i < collidersCel.Length; i++)
	//		{
	//			if (collidersCel[i].gameObject != gameObject)
	//			{
	//				Death();
	//			}
	//		}
	//	}
	//}

	private void Death()
	{
		transform.FindChild("Hand").gameObject.SetActive(false);
		myRigidBody.velocity = new Vector2(0f, myRigidBody.velocity.y);
		gameOverText.enabled = true;
		isDead = true;
	}

	public void RechargeOxygen(float oxygenAmount)
	{
		oxygen.CurrentVal += oxygenAmount;
	}

	public void GetBattery(float energyAmount)
	{
		energy.CurrentVal += energyAmount;
	}

	private void LightControl()
	{
		if (energy.CurrentVal <= 0)
		{
			lightCanWork = false;
			lightInHand.transform.localScale = new Vector3(0, 0, 1);
		}
		else lightCanWork = true;
	}

	private void FaceToMouse()
	{
		Vector2 mousePosV2 = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 0);

		if (gameObject.transform.position.x - mousePosV2.x <= 0)
		{
			facingRight = true;
		}
		else facingRight = false;
	}

	private void FlipToMousePosition()
	{
		Vector2 mousePosV2 = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 0);

		if (facingRight)
		{
			gameObject.transform.localScale = new Vector2(1f, 1f);
			gameObject.transform.GetChild(0).localScale = new Vector2(1f, 1f);
			//playerSprite.flipX = false;
			//handSprite.flipY = false;


		}
		else if (!facingRight)
		{
			gameObject.transform.localScale = new Vector2(-1f, 1f);
			gameObject.transform.GetChild(0).localScale = new Vector2(-1f, 1f);
			//playerSprite.flipX = true;
			//handSprite.flipY = true;

		}
	}

	void OnCollisionStay2D(Collision2D _col)
	{
		if (_col.contacts[0].normal.y > 0.7f)
		{//check if ground is walkable, in this case 45 degrees and lower
			upVect = _col.contacts[0].normal;//set up direction
		}
	}
	public void GetRedCard()
	{
		hasRedCard = true;
	}

	public void GetUpgradeComponents(int _comp)
	{
		upgradeCount += _comp;
		upgradeCountText.text = upgradeCount + "/6";
	}

	private void CrouchingControl()
	{
		if (!Input.GetKey(keys["CrouchButton"]))
		{
			isCrouching = false;
		}
		
		Collider2D[] collidersCel = Physics2D.OverlapCircleAll(CellarCheck.position, cellarRadius, whatIsCellar);
		for (int i = 0; i < collidersCel.Length; i++)
		{
			if (collidersCel[i].gameObject != gameObject)
				isCrouching = true;

		}
		
		if (!isCrouching)
		{
			standCollider.enabled = true;
			sitCollider.enabled = false;
		}
	}
	private IEnumerator WaitToResetJump()
	{
		yield return new WaitForSeconds(1f);
		myAnim.SetBool("jump", jump);
	}





}
