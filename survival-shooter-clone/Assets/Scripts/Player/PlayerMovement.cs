using UnityEngine;

public class PlayerMovement : MonoBehaviour{
    public float speed = 6f;

    Vector3 movement;
    Animator anim;
    Rigidbody playerRigidbody;
    int floorMask;
    float camRayLength = 10000f;

    void Awake() {
        floorMask = LayerMask.GetMask("Floor");
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");

        Move(horizontalMovement,verticalMovement);
        Turning();
        Animating(horizontalMovement, verticalMovement);
    }

    void Move(float horizontalMovement, float verticalMovement) {
        movement.Set(horizontalMovement, 0.0f, verticalMovement);
        movement = movement.normalized * speed * Time.deltaTime;

        playerRigidbody.MovePosition(transform.position + movement);
    }

    void Turning() {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;

        if(Physics.Raycast (camRay, out floorHit, camRayLength, floorMask)) {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0.0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidbody.MoveRotation(newRotation);
        }
    }

    void Animating(float horizontalMovement, float verticalMovement) {
        bool walking = horizontalMovement != 0f || verticalMovement != 0f;
        anim.SetBool("IsWalking", walking);

    }
}
