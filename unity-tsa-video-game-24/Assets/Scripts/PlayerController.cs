using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    public bool isMoving;
    public PlayerInteractS playerInteract;
    


    Vector2 movementInput;
    Rigidbody2D rb;
    Animator animator;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    public SpriteRenderer spriteRender;

    bool canMove = true;
    bool puzzle = false;
    public int puzzleNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRender = GetComponent<SpriteRenderer>();
        PlayerInteractS.fairyTalk += PuzzleOne;

    }


    private void FixedUpdate() {
        // If movement input is not 0 try to move
        if (canMove) {
            if (movementInput.x != 0) movementInput.y = 0;

            if(movementInput != Vector2.zero) {
                animator.SetFloat("moveX", movementInput.x);
                animator.SetFloat("moveY", movementInput.y);
                
                bool success = TryMove(movementInput);

                if (!success && movementInput.x > 0) {
                    success = TryMove(new Vector2(movementInput.x, 0));

                }

                if(!success && movementInput.y > 0) {
                        success = TryMove(new Vector2(0, movementInput.y));     
                        
                    }

               animator.SetBool("isMoving", success);
                // isMoving = true;
            } else {
               animator.SetBool("isMoving", false);
                // isMoving = false;
            }

            // Set direction of sprite to movement direction
            if (movementInput.x < 0) {
                spriteRender.flipX = true;
            } else if (movementInput.x > 0) {
                spriteRender.flipX = false;

            }
            // UpdateAnimation();

        }

    }

    private bool TryMove(Vector2 direction) {
        if(direction != Vector2.zero) {

        int count = rb.Cast(
            direction, // X and Y values between -1 and 1 that represent the direction from the body to look for collision
            movementFilter, // List of settings that determine what the raycast can collide into and what it will ignore
            castCollisions, // List of collisions that store the results of the raycast
            moveSpeed * Time.fixedDeltaTime + collisionOffset); // the amount to cast equal to the movement plus an offset

            if (count == 0) {
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            return true;
            }
            else {
                return false;
            }
        } else {
            return false;
        }
    }

    void UpdateAnimation() {
//        if(!animLocked && canMove) {
        if(movementInput != Vector2.zero) {
            animator.Play("player_walk");
        } else {
            animator.Play("player_idle");
        }
//        }
    }

    void OnMove(InputValue movementValue) {
        movementInput = movementValue.Get<Vector2>();
    }

    void OnTalk() {
        // LockMovement();
        animator.SetTrigger("NPCInteract");
        // UnlockMovement();
    }

    public void PlayerInteract() {
        LockMovement();
        if(spriteRender.flipX == true) {
        playerInteract.InteractLeft();
        } else {
        playerInteract.InteractRight();
        }

    }

    public void EndPlayerInteract() {
        if (!puzzle) {
        UnlockMovement();
        print("end interact unlock");
        }
        playerInteract.StopInteract();
    }

    public void LockMovement() {//hi
        canMove = false;
    }

    private void UnlockMovement() {
        canMove = true;
        print("movementLocked");
    }

    private void PuzzleOne() {
        // need to trigger camera switch here by activating "Change" trigger
        print("2... puzzle one start");
        spriteRender.enabled = false;

        puzzle = true;
        LockMovement();
        puzzleNum++;
        if (puzzleNum == 2) {//hi
            print(puzzleNum);
            // transform.position = new Vector3(-8, -2);
        }
   
        PlayerInteractS.fairyTalk -= PuzzleOne;
        InventoryPage.gardenComplete += PuzzleOver;
    }

    private void PuzzleOver() {
        puzzle = false;
            if (puzzleNum == 2) {
            print(puzzleNum);
            transform.position = new Vector3(0, -2);
        }
        if (puzzleNum==1) {UnlockMovement();}
        spriteRender.enabled = true;
        InventoryPage.gardenComplete -= PuzzleOver;
    }
}
