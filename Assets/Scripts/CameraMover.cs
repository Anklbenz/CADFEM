
using UnityEngine;

public class CameraMover : MonoBehaviour {

    [SerializeField]private  float SPEED = 5.0f;
    [SerializeField]private float MOUSE_SPEED = 100f;
    private Vector3 _direction;
    private float mouseX, mouseY;

    private void Update(){
        var xMove = Input.GetAxis("Horizontal");
        var yMove = Input.GetAxis("Vertical");

        mouseX += Input.GetAxis("Mouse X") * MOUSE_SPEED * Time.deltaTime;
        mouseY += Input.GetAxis("Mouse Y") * MOUSE_SPEED * Time.deltaTime;
        var moveVector = new Vector3(xMove, 0, yMove);

        transform.rotation = Quaternion.Euler(-mouseY, mouseX, 0);
        transform.Translate(moveVector * Time.deltaTime * SPEED);
    }
}