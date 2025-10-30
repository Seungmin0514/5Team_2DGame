using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public GameObject scanObject;
    public GameActoinManager actoinManager;
    Rigidbody2D rd;
    // Start is called before the first frame update
    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        actoinManager = FindObjectOfType<GameActoinManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dirVec = new Vector2(transform.localScale.x, 0);
        Debug.DrawRay(rd.position,dirVec*0.7f,new Color(0,1,0));
        RaycastHit2D rayHit = Physics2D.Raycast(rd.position, dirVec, 0.7f, LayerMask.GetMask("Object"));
        if (rayHit.collider != null)
        {
            scanObject = rayHit.collider.gameObject;
        }
        else
        {
            scanObject = null;
        }
    }
    public void Interact(InputAction.CallbackContext context)
    {
        Debug.Log("감지");
        if (context.performed)
        {
            if(scanObject != null)
            {
                Debug.Log("상호작용");
                actoinManager.Action(scanObject);
            }
        }
    }
}
