using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerController : MonoBehaviour
{
	[SerializeField]
	float moveSpeed;
	[SerializeField]
	float smoothInputSpeed;
	[SerializeField]
	float interactionMaxRange;
	[SerializeField]
	LayerMask interactableLayerMask;

	Rigidbody2D rb;
	PlayerInputActions inputs;

	Vector2 movementInput;
	Vector2 smoothInputVelocity;
	Vector2 currentMovementVector;
	Vector2 lastDirection;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		inputs = new PlayerInputActions();
	}

	private void OnEnable()
	{
		inputs.Player.Move.performed += Move_performed;
		inputs.Player.Move.canceled += Move_performed;
		inputs.Player.Action1.performed += Action1_performed;
        inputs.Player.Action1.canceled += Action1_canceled; 
		inputs.Player.Enable();
	}

    
    private void OnDisable()
	{
		inputs.Player.Move.performed -= Move_performed;
		inputs.Player.Move.canceled -= Move_performed;
		inputs.Player.Action1.performed -= Action1_performed;
		inputs.Player.Action1.canceled -= Action1_canceled;
		inputs.Player.Disable();
	}
	private void Update()
	{
		currentMovementVector = Vector2.SmoothDamp(currentMovementVector, movementInput, ref smoothInputVelocity, smoothInputSpeed);
		if (currentMovementVector != Vector2.zero) lastDirection = currentMovementVector.normalized;
		Debug.DrawRay(rb.position, lastDirection.normalized, Color.red);
		
	}

	private void FixedUpdate()
	{
		rb.MovePosition(rb.position + currentMovementVector * moveSpeed * Time.deltaTime);
	}

	private void Action1_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
	{
		RaycastHit2D hit;
		if (hit = Physics2D.Raycast(rb.position, lastDirection, interactionMaxRange, interactableLayerMask))
		{
			hit.collider.GetComponent<IInteractable>().Use();
			inputs.Player.Move.Disable();
		}
	}

	private void Move_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
	{
		movementInput = obj.ReadValue<Vector2>();
	}
	private void Action1_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
	{
		inputs.Player.Move.Enable();
	}

}
