using UnityEngine;

public class ConditionPlayer : MonoBehaviour
{
    private Stamina _stamina;
    private Energy _energy;
    private Health _health;
    private Heat _heat;
    private Satiety _satiety;
    private Water _water;

    private PlayerController _playerController;
    private Animator _playerAnimator;
    private CharacterController _playerCharacterController;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _water = GetComponent<Water>();
        _energy = GetComponent<Energy>();
        _heat = GetComponent<Heat>();
        _stamina = GetComponent<Stamina>();
        _satiety = GetComponent<Satiety>();

        _health.OnDied += Kill;

        _energy.UsedUp += NegativeImpact;
        _heat.UsedUp += NegativeImpact;
        _satiety.UsedUp += NegativeImpact;
        _water.UsedUp += NegativeImpact;
    }

    private void OnDestroy()
    {
        _health.OnDied -= Kill;
        _energy.UsedUp -= NegativeImpact;
        _heat.UsedUp -= NegativeImpact;
        _satiety.UsedUp -= NegativeImpact;
        _water.UsedUp -= NegativeImpact;
    }

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
        _playerAnimator = GetComponent<Animator>();
        _playerCharacterController = GetComponent<CharacterController>();
    }

    private void Kill()
    {
        Debug.Log("Игрок умер");

        _playerController.IsLive = false;
        _playerAnimator.SetBool("IsDied", true);
        _playerCharacterController.height = 0.1f;
    }

    private void NegativeImpact(INegativeable negativeable)
    {
        negativeable.Init();
        negativeable.NegativeImpact();
    }
}