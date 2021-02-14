using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float SürüngenSpeed;
    public float AyakSpeed;
    public float JumpForce;
    public float GlideGravity;
    public float DropTime;

    public Transform GroundCheckPosition;

    Animator animator;
    Rigidbody2D CharacterRigidbody;
    SpriteRenderer spriteRenderer;

    [Header("DEBUG SETTINGS")]
    [SerializeField] private bool DoubleJumpActivated;
    [SerializeField] private bool HasFoot;
    [SerializeField] private bool HasGlider;
    [SerializeField] private bool isGrounded = true;
    [SerializeField] private bool doubleJumpAllowed = true;
    [SerializeField] private bool falling;
    private float Speed;
    private float dropTimer;

    LayerMask layerMask;

    void Start()
    {
        transform.position = RespawnSystem.instance.respawnPoint;
        animator = GetComponent<Animator>();
        CharacterRigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        layerMask = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        ItemDrop();
        ItemCheck();
        SpeedCheck();
        FallCheck();
        InputCheck();
        VelocityCheck();
        SpecsCheck();
        ItemTake();
    }
    private void FixedUpdate()
    {
        GroundCheck();
    }
    private void SpeedCheck()
    {
        if (HasFoot)
            Speed = AyakSpeed;
        else
            Speed = SürüngenSpeed;
    }
    public void GroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(GroundCheckPosition.position, 0.2f, layerMask);
    }
    public void InputCheck() 
    {
        if (Input.GetKey(KeyCode.D))
        {
            CharacterRigidbody.velocity = new Vector2(Speed * 10, CharacterRigidbody.velocity.y);
            animator.SetInteger("State", 1);
            spriteRenderer.flipX = false;
            AudioManager.instance.Walk.Play();
        }
        else if (Input.GetKey(KeyCode.A))
        {
            CharacterRigidbody.velocity = new Vector2(Speed * -10, CharacterRigidbody.velocity.y);
            animator.SetInteger("State", 1);
            spriteRenderer.flipX = true;
            AudioManager.instance.Walk.Play();
        }
        if (Input.GetKeyDown(KeyCode.Space) && HasFoot)
        {
            if (isGrounded)
            {
                CharacterRigidbody.velocity += Vector2.up * JumpForce * 10;
                doubleJumpAllowed = true;
                animator.SetInteger("State", 2);
                
                AudioManager.instance.StopAudios();
                AudioManager.instance.Jump.Play();
            }
            else if (doubleJumpAllowed && DoubleJumpActivated)
            {
                CharacterRigidbody.velocity += Vector2.up * JumpForce * 10;
                doubleJumpAllowed = false;
                animator.Play("Foot_Jump");

                AudioManager.instance.StopAudios();
                AudioManager.instance.DoubleJump.Play();
            }
        }
        else if (Input.GetKey(KeyCode.Space) && HasGlider)
        {
            if (falling)
            {
                CharacterRigidbody.gravityScale = GlideGravity;
                animator.SetBool("Glide", true);
            }
            else
            {
                CharacterRigidbody.gravityScale = 20;
                animator.SetBool("Glide", false);
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space) && HasGlider)
        {
            CharacterRigidbody.gravityScale = 20;
            animator.SetBool("Glide", false);
        }
    }
    public void VelocityCheck()
    {
        if (CharacterRigidbody.velocity.magnitude >= 0 && CharacterRigidbody.velocity.magnitude < .1f)
        {
            animator.SetInteger("State", 0);
            AudioManager.instance.StopAudios();
        }
    }
    public void FallCheck()
    {
        if (CharacterRigidbody.velocity.y <= 0 && !isGrounded)
            falling = true;
        else
            falling = false;
    }
    public void SpecsCheck()
    {
        animator.SetBool("IsFootAvaliable", HasFoot);
        animator.SetBool("Falling", falling);
    }
    public void ItemCheck()
    {
        HasGlider = false;
        HasFoot = false;
        DoubleJumpActivated = false;
        int footCount = 0;
        foreach (var item in Inventory.instance.ItemIDs)
        {
            List<GameObject> allItems = RespawnSystem.instance.checkPointData.AllItems;
            if (allItems[item].CompareTag("Glider"))
            {
                HasGlider = true;
            }
            else if (allItems[item].CompareTag("Foot"))
            {
                HasFoot = true;
                footCount++;
            }
        }
        if(footCount == 2)
        {
            DoubleJumpActivated = true;
        }
    }
    public void ItemDrop()
    {
        List<GameObject> allItems = RespawnSystem.instance.checkPointData.AllItems;
        if (Input.GetKey(KeyCode.Alpha1) && Inventory.instance.ItemIDs.Count > 0 && isGrounded)
        {
            dropTimer += Time.deltaTime;
            if (dropTimer >= DropTime)
            {
                AudioManager.instance.PickUp.Play();

                GameObject item = Instantiate(allItems[Inventory.instance.ItemIDs[0]]);
                item.transform.position = transform.position + Vector3.up/2;
                Vector2 direction = GetComponent<SpriteRenderer>().flipX ? Vector2.left : Vector2.right;
                direction += Vector2.up / 2;
                item.GetComponent<Rigidbody2D>().velocity = direction * 3f;
                Inventory.instance.ItemIDs.RemoveAt(0);
                dropTimer = 0;
            }
            Inventory.instance.FillBlock1.fillAmount = dropTimer / DropTime;
        }
        else if (Input.GetKey(KeyCode.Alpha2) && Inventory.instance.ItemIDs.Count > 1 && isGrounded)
        {
            dropTimer += Time.deltaTime;
            if (dropTimer >= DropTime)
            {
                AudioManager.instance.PickUp.Play();

                GameObject item = Instantiate(allItems[Inventory.instance.ItemIDs[1]]);
                item.transform.position = transform.position + Vector3.up / 2;
                Vector2 direction = GetComponent<SpriteRenderer>().flipX ? Vector2.left : Vector2.right;
                direction += Vector2.up / 2;
                item.GetComponent<Rigidbody2D>().velocity = direction * 3f;
                Inventory.instance.ItemIDs.RemoveAt(1);
                dropTimer = 0;
            }
            Inventory.instance.FillBlock2.fillAmount = dropTimer / DropTime;
        }
        else
        {
            dropTimer = 0;
            Inventory.instance.FillBlock1.fillAmount = 0;
            Inventory.instance.FillBlock2.fillAmount = 0;
        }
    }
    public void ItemTake()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(Inventory.instance.ItemIDs.Count < 2)
            {
                Collider2D itemCollider = Physics2D.OverlapBox(transform.position, Vector2.one, 0,LayerMask.GetMask("Item"));
                if (itemCollider)
                {
                    AudioManager.instance.PickUp.Play();
                    animator.SetTrigger("TakeObject");
                    GameObject item = itemCollider.gameObject;
                    StartCoroutine(WaitForPickup(item));
                }
            }
        }
    }
    IEnumerator WaitForPickup(GameObject item)
    {
        yield return new WaitForSeconds(.15f);
        int id = item.GetComponent<ItemIDCard>().id;
        Inventory.instance.ItemIDs.Add(id);
        Destroy(item);
    }


}