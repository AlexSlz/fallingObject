using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class Object : MonoBehaviour
{
    [SerializeField]
    private float _fallingSpeed = 1;

    private ParticleSystem _particleSystem;
    private Animator _animator;

    public bool CanClick = false;
    public bool PlayingAnimation = false;


    private Score _score;
    private void Awake()
    {
        _score = FindObjectOfType<Score>();
        _particleSystem = GetComponent<ParticleSystem>();
        _animator = GetComponent<Animator>();
        SetSkin(PlayerPrefs.GetInt("SkinId"));
    }

    public void SpawnObject(Vector3 position, float fallingSpeed)
    {
        transform.position = position;
        this.gameObject.SetActive(true);
        this._fallingSpeed = fallingSpeed;
    }

    private void SetSkin(int id)
    {
        if (SkinsSelector.SkinsDataBase != null)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = SkinsSelector.SkinsDataBase.Skins[id].Sprite;

            ParticleSystem.MainModule settings = gameObject.GetComponent<ParticleSystem>().main;
            settings.startColor = SkinsSelector.SkinsDataBase.Skins[id].Color;
        }
    }

    private void LateUpdate()
    {
        Move(new Vector3(0, -1 * (_fallingSpeed * Time.deltaTime)));
    }

    private void Move(Vector3 position)
    {
        transform.position += position;
    }

    private void OnMouseDown()
    {
        ClickOnObject();
    }

    private void ClickOnObject()
    {
        if (CanClick)
        {
            _score.Add(1);
            PlayParticle();
        }
    }
    public void DestroyObject()
    {
        CanClick = false;
        PlayingAnimation = false;
        this.gameObject.SetActive(false);
    }

    private void PlayParticle()
    {
        PlayingAnimation = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        _particleSystem.Play();
    }

    public void PlayDestroyAnimation()
    {
        try
        {
            _animator.Play("Destroy");
        }
        catch
        {
            DestroyObject();
        }
    }
    private void OnParticleSystemStopped()
    {
        DestroyObject();
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

}
