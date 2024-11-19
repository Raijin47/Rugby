using System.Collections;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] protected Stats _stats;
    [SerializeField] protected Material _material;

    private Coroutine _coroutine;
    private Coroutine _aliveCoroutine;

    private readonly WaitForSeconds Interval = new(1f);
    private float _alive;
    public int Health => _stats.Health;
    public float Alive
    {
        get => _alive;
        set 
        {
            _alive = value;
            _material.SetFloat("_FullDistortionFade", _alive);
        }
    }
    private bool _isDoubleDamage;
    public bool IsDoubleDamage
    {
        get => _isDoubleDamage;
        set
        {
            _isDoubleDamage = value;
            _material.SetFloat("_TextureLayer2Fade", value ? 1 : 0);
        }
    }

    private void Start()
    {
        Game.Action.OnStart.AddListener(Ressurection);
        Alive = 0;
    }

    public void InitStats(int health, int attack, int defence)
    {
        _stats.MaxHealth = health;
        _stats.Health = health;
        _stats.Attack = attack;
        _stats.Defence = defence;
    }

    public void TakeDamage(int value)
    {
        int damage = value;
        if(_stats.Defence >= damage) return;
        damage -= _stats.Defence;

        _stats.Health -= damage;
        if (_stats.Health <= 0)
        {
            _stats.Health = 0;
            Release();
            ReleaseCoroutine();
            _coroutine = StartCoroutine(DeathProcess());
        }
        else
        {
            ReleaseCoroutine();
            _coroutine = StartCoroutine(TakeDamageProcess());
        }
    }

    public virtual void Ressurection()
    {
        if(_aliveCoroutine != null)
        {
            StopCoroutine(_aliveCoroutine);
            _aliveCoroutine = null;
        }
        _aliveCoroutine = StartCoroutine(ResurrectionProcess());

        _material.SetFloat("_ShiftingFade", 0);
        _material.SetFloat("_EnchantedFade", 0);
        _material.SetFloat("_PoisonFade", 0);
        _material.SetFloat("_NegativeFade", 0);
    }

    public void ApplyPercentDamage()
    {
        float current = _stats.Attack;
        _stats.Attack += Mathf.CeilToInt(current * 0.1f);

        ReleaseCoroutine();
        _coroutine = StartCoroutine(ApplyVisualEffect("_ShiftingFade"));
    }

    public void ApplyPercentDefence()
    {
        float current = _stats.Defence;
        _stats.Defence += Mathf.CeilToInt(current * 0.1f);

        ReleaseCoroutine();
        _coroutine = StartCoroutine(ApplyVisualEffect("_EnchantedFade"));
    }

    public void Heal()
    {
        float value = _stats.MaxHealth;
        _stats.Health += Mathf.CeilToInt(value * 0.03f);
        if (_stats.Health >= _stats.MaxHealth) _stats.Health = _stats.MaxHealth;

        ReleaseCoroutine();
        _coroutine = StartCoroutine(ApplyVisualEffect("_PoisonFade"));
    }

    private IEnumerator TakeDamageProcess()
    {
        for(int i = 0; i < 5; i++)
        {
            _material.SetFloat("_NegativeFade", 1);
            yield return new WaitForSeconds(0.05f);
            _material.SetFloat("_NegativeFade", 0);
            yield return new WaitForSeconds(0.05f);
        }
    }

    private IEnumerator ApplyVisualEffect(string name)
    {
        _material.SetFloat(name, 1);
        yield return Interval;
        _material.SetFloat(name, 0);
    }

    private IEnumerator DeathProcess()
    {
        Alive = 1;
        while (Alive > 0)
        {
            Alive -= Time.deltaTime;
            yield return null;
        }
        Alive = 0;
    }
    private IEnumerator ResurrectionProcess()
    {
        Alive = 0;
        while (Alive < 1)
        {
            Alive += Time.deltaTime;
            yield return null;
        }
        Alive = 1;
    }

    public abstract void ApplyDamage();
    protected abstract void Release();

    private void ReleaseCoroutine()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }
}