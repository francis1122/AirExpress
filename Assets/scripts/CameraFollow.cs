using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
	
	public GameObject cameraTarget; // object to look at or follow
	public GameObject player; // player object for moving
	public PlayerControl playerControl;

	public float smoothTime = 0.1f;    // time for dampen
	public bool cameraFollowX = true; // camera follows on horizontal
	public bool cameraFollowY = true; // camera follows on vertical
	public bool cameraFollowHeight = true; // camera follow CameraTarget object height
	public float cameraHeight = 2.5f; // height of camera adjustable
	public Vector2 velocity; // speed of camera movement
	
	private Transform thisTransform; // camera Transform


	public void Awake()
		
	{	
		if (tag == "MainCamera") {
					//	camera.orthographicSize = (Screen.height / 100f / 2.0f); // 100f is the PixelPerUnit that you have set on your sprite. Default is 100.	
				}
	}

	// Use this for initialization
	void Start()
	{
		thisTransform = transform;
	}
	
	// Update is called once per frame
	void LateUpdate()
	{
		if (Time.timeScale < 0.01) {
			return;
		}
		//create offset of ship
		float shipAngle = playerControl.GetComponent<PlayerPlaneController>().player_angle;
		Vector2 cameraOffset = Vector2.zero;
		float cameraOffsetScale = .8f;
		if (playerControl.Forward) {
			cameraOffsetScale = 1.6f;
				}

		cameraOffset.x = Mathf.Cos (shipAngle) * cameraOffsetScale;
		cameraOffset.y = Mathf.Sin (shipAngle) * cameraOffsetScale;
		// add offset to ship position;
		//float 
		Vector3 cameraPosition = player.transform.position;
		//Vector3 cameraPosition = Vector3.zero;
		cameraPosition.x += cameraOffset.x;
		cameraPosition.y += cameraOffset.y;
		//cameraPosition.z = -10.0f;
		if (cameraFollowX)
		{
			thisTransform.position = new Vector3(Mathf.SmoothDamp(thisTransform.position.x, cameraPosition.x, ref velocity.x, smoothTime), thisTransform.position.y, thisTransform.position.z);
			//transform.position = cameraPosition;
		}
		if (cameraFollowY)
		{
			thisTransform.position = new Vector3( thisTransform.position.x, Mathf.SmoothDamp(thisTransform.position.y, cameraPosition.y, ref velocity.y, smoothTime), thisTransform.position.z); 
			//transform.position = cameraPosition;
		}
		if (!cameraFollowX & cameraFollowHeight)
		{
			// to do
		}
		this.transform.rotation = Quaternion.identity;
	}
}