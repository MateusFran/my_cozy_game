using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Vector2 movement;

    [Header("System")]
    [SerializeField] private InventoryManager invManager;

    [Header("Interactable")]
    [SerializeField] private bool isInteractable;
    [SerializeField] private Collider2D colInteractable;

    [Header("UI")]
    [SerializeField] private Animator ui_imageAnim;

    void Start()
    {

    }

    void Update()
    {
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetKeyDown(KeyCode.E) && isInteractable){
            if (invManager.AddItem(colInteractable.gameObject.GetComponent<ItemObject>().ownItem)){
                Destroy(colInteractable.gameObject);
                colInteractable = null;
            }
        }
        if (!isInteractable){ ui_imageAnim.SetTrigger("e_fadeOut");}
        //else {ui_imageAnim.SetTrigger("e_fadeOut");}
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        movement.Normalize();
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Item"){
            isInteractable = true;
            colInteractable = col;

            ui_imageAnim.SetTrigger("e_fadeIn");
        }
    }

    private void OnTriggerExit2D(Collider2D col){
        if (colInteractable.gameObject.tag == "Item"){
            isInteractable = false;
            //ui_imageAnim.SetTrigger("e_fadeOut");
        }
    }
}
