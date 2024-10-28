using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator anim;
    private Vector2 movement;

    [Header("System")]
    [SerializeField] private InventoryManager invManager;

    [Header("Interactable")]
    //[SerializeField] private bool isInteractable;
    [SerializeField] private List<GameObject> liveCollider = new List<GameObject>();


    [Header("UI")]
    [SerializeField] private Animator ui_imageAnim;

    void Start()
    {
        
    }

    void Update()
    {
        Move();
        Flip();

        if (Input.GetKeyDown(KeyCode.E) && IsInteractable()){
            if (invManager.AddItem(liveCollider[0].gameObject.GetComponent<ItemObject>().ownItem)){
                Destroy(liveCollider[0]);
            }
        }

        anim.SetFloat("Speed", movement.sqrMagnitude);
        ui_imageAnim.SetBool("isInteractable", IsInteractable());
    }

    void FixedUpdate()
    {
        movement.Normalize();
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    private void Move()
    {
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void Flip() {
        if (movement.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (movement.x < 0) { 
            spriteRenderer.flipX = true;
        }
    }

    private bool IsInteractable()
    {
        if (liveCollider.Any()) { return true; }
        else { return false; }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Item" && !liveCollider.Contains(other.gameObject))
            liveCollider.Add(other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Item" && liveCollider.Contains(other.gameObject))
        {
            liveCollider.Remove(other.gameObject);
        }
    }

}
