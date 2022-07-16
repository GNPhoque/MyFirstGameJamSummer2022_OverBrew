using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerController : MonoBehaviour
{
	public static HealerController instance { get; private set; }
	[SerializeField]
	float moveSpeed;
	[SerializeField]
	float smoothInputSpeed;
	[SerializeField]
	float interactionMaxRange;
	[SerializeField]
	LayerMask interactableLayerMask;

	Rigidbody2D rb;
	SpriteRenderer _spriteRenderer;
    Animator _animator;
	PlayerInputActions inputs;

	Vector2 movementInput;
	Vector2 smoothInputVelocity;
	Vector2 currentMovementVector;
	Vector2 lastDirection;
	public Item CarriedItem { get => carriedItem; set { carriedItem = value; currentItemText.text = "Current item : " + carriedItem; } }
	private Item carriedItem;
	[SerializeField]
	TMPro.TMP_Text currentItemText;

	private void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
			return;
		}
		instance = this;

		_spriteRenderer = this.GetComponent<SpriteRenderer>();
        _animator = this.GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
		inputs = new PlayerInputActions();
	}

	private void OnEnable()
	{
		inputs.Player.Move.performed += Move_performed;
		inputs.Player.Move.canceled += Move_performed;
		inputs.Player.Action1.performed += Action1_performed;
        inputs.Player.Action1.canceled += Action1_canceled; 
		inputs.Player.Action2.performed += Action2_performed;
        inputs.Player.Action2.canceled += Action2_canceled; 
		inputs.Player.Enable();
	}

    
    private void OnDisable()
	{
		inputs.Player.Move.performed -= Move_performed;
		inputs.Player.Move.canceled -= Move_performed;
		inputs.Player.Action1.performed -= Action1_performed;
		inputs.Player.Action1.canceled -= Action1_canceled;
		inputs.Player.Action2.performed -= Action2_performed;
		inputs.Player.Action2.canceled -= Action2_canceled;
		inputs.Player.Disable();
	}
	private void Update()
	{
		currentMovementVector = Vector2.SmoothDamp(currentMovementVector, movementInput, ref smoothInputVelocity, smoothInputSpeed);
		if (currentMovementVector != Vector2.zero) lastDirection = currentMovementVector.normalized;
		Debug.DrawRay(rb.position, lastDirection.normalized, Color.red);

		if (currentMovementVector.x < 0) _spriteRenderer.flipX = true;
		else _spriteRenderer.flipX = false;
		_animator.SetFloat("speed", currentMovementVector.magnitude);
		
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

	private void Action2_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
	{
		RaycastHit2D hit;
		if (hit = Physics2D.Raycast(rb.position, lastDirection, interactionMaxRange, interactableLayerMask))
		{
			hit.collider.GetComponent<IInteractable>().Clean();
			inputs.Player.Move.Disable();
		}
	}

	private void Move_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
	{
		movementInput = obj.ReadValue<Vector2>();
	}
	private void Action1_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
	{
		RaycastHit2D hit;
		if (hit = Physics2D.Raycast(rb.position, lastDirection, interactionMaxRange, interactableLayerMask))
		{
			hit.collider.GetComponent<IInteractable>().Release();
			inputs.Player.Move.Enable();
		}
	}

	private void Action2_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
	{
		inputs.Player.Move.Enable();
	}

	/*public void TakeItem(Item item) {
		if (CarriedItem == null) {
			item.itemTransform.parent = this.transform;
			item.itemTransform.localPosition = Vector3.zero;
			CarriedItem = item;
		}
	}*/

	public void DropItem() {
		if (CarriedItem.itemTransform != null) Destroy(CarriedItem.itemTransform.gameObject);
        CarriedItem = null;
	}

}
