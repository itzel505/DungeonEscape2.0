using UnityEngine;

public class Attacker : MonoBehaviour
{
    public Animator Animator;
    public KeyboardInput KeyboardInput;
    public GamepadInput GamepadInput;

    private const string ATTACK_ANIMATION_NAME = "Attack";

    private void Awake()
    {
        InitializeComponents();
    }

    private void Update()
    {
        if (WasAttackButtonPressed())
        {
            TriggerAttackAnimation();
        }
    }

    private void InitializeComponents()
    {
        if (Animator == null)
            Animator = GetComponent<Animator>();

        if (KeyboardInput == null)
            KeyboardInput = GetComponent<KeyboardInput>();

        if (GamepadInput == null)
            GamepadInput = GetComponent<GamepadInput>();
    }

    private bool WasAttackButtonPressed()
    {
        return IsKeyboardAttackPressed() || IsGamepadAttackPressed();
    }

    private bool IsKeyboardAttackPressed()
    {
        return KeyboardInput != null && KeyboardInput.WasAttackButtonPressed();
    }

    private bool IsGamepadAttackPressed()
    {
        return GamepadInput != null && GamepadInput.WasAttackButtonPressed();
    }

    private void TriggerAttackAnimation()
    {
        if (Animator == null)
            return;

        Animator.Play(ATTACK_ANIMATION_NAME);
    }
}