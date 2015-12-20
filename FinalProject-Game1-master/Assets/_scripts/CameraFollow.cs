using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
    public Transform player;
    public Vector2 Margin;
    public Vector2 Smoothing;
    public BoxCollider2D Bounds;
    private Vector3 _min, _max;
    
     
    public bool _isFollowing { get; set;}

    public void Start() {
        _min = Bounds.bounds.min;
        _max = Bounds.bounds.max;
        _isFollowing = true;

    }

    public void Update() {
        
        var x = transform.position.x;
        var y = transform.position.y;

        if (_isFollowing)
        {

            if (Mathf.Abs(x - player.position.x) > Margin.x)

                x = Mathf.Lerp(x, player.position.x, Smoothing.x * Time.deltaTime);


            if (Mathf.Abs(y - player.position.y) > Margin.y)
                y = Mathf.Lerp(y, player.position.y, Smoothing.y * Time.deltaTime);
        }
        var cameraHalfWidt = GetComponent<Camera>().orthographicSize * ((float)Screen.width / Screen.height);
        x = Mathf.Clamp(x, _min.x + cameraHalfWidt, _max.x - cameraHalfWidt);
        y = Mathf.Clamp(y, _min.y + GetComponent<Camera>().orthographicSize, _max.y - GetComponent<Camera>().orthographicSize);

        transform.position = new Vector3(x, y, transform.position.z);

    }





    //private Vector2 velocity;
    //public float smoothTimeY;
    //public float smoothTimeX;

    //public GameObject player;
   
    //void Start()
    //{
    //    //trouver le jouer
    //    player = GameObject.FindGameObjectWithTag("Player");

    //}
    //void FixedUpdate()
    //{
    //    //pour changer la position de la camera 
    //    float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
    //    float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);

    //    transform.position = new Vector3(posX, posY, transform.position.z);
    //}
}
