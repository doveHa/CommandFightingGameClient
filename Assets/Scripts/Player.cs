using Handler;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 Position;
    [SerializeField] private GameObject opponent;
    [SerializeField] private int skill1CoolTime, skill2CoolTime;
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer[] _sprites;
    private SkillCoolHandler skill1Cool, skill2Cool;
    private bool _isLeft, _isGuard;
    public bool IsJumping { get; set; }

    private int health = 100;

    void Start()
    {
        skill1Cool = gameObject.AddComponent<SkillCoolHandler>();
        skill1Cool.Time = skill1CoolTime;
        skill2Cool = gameObject.AddComponent<SkillCoolHandler>();
        skill2Cool.Time = skill2CoolTime;

        _isLeft = IsLeft();
        spriteRenderer = GetComponent<SpriteRenderer>();
        _sprites = GetComponentsInChildren<SpriteRenderer>();
    }

    void Update()
    {
        bool newDirection = IsLeft();

        if (_isLeft != newDirection)
        {
            _isLeft = newDirection;
            FlipSprite(!_isLeft);
        }
    }

    public bool IsLeft()
    {
        return gameObject.transform.position.x < opponent.transform.position.x;
    }

    public void SetGuard(bool isGuard)
    {
        _isGuard = isGuard;
    }


    public void skill1On()
    {
        if (skill1Cool.IsOn)
        {
            if (_isLeft)
            {
                transform.Find("right").gameObject.SetActive(true);
            }
            else
            {
                transform.Find("left").gameObject.SetActive(true);
            }

            skill1Cool.StartCoolCoroutine();
        }
        else
        {
            Debug.Log("Skill 1 is off");
        }
    }

    public void skill1Off()
    {
        if (_isLeft)
        {
            transform.Find("right").gameObject.SetActive(false);
        }
        else
        {
            transform.Find("left").gameObject.SetActive(false);
        }
    }

    public void skill2On()
    {
        if (skill2Cool.IsOn)
        {
            Vector2 direction;
            Vector2 gun;
            if (_isLeft)
            {
                gun = transform.Find("right").transform.position;
                direction = Vector2.right;
            }
            else
            {
                gun = transform.Find("left").transform.position;
                direction = Vector2.left;
            }

            GameObject bullet = Instantiate(Resources.Load<GameObject>("Prefabs/Skill2"), gun, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().AddForce(direction * 10f, ForceMode2D.Impulse);
            skill2Cool.StartCoolCoroutine();
        }
        else
        {
            Debug.Log("Skill 2 is off");
        }
    }

    public void Hit(int atk)
    {
        if (_isGuard)
        {
            Debug.Log("guard");
        }
        else
        {
            health -= atk;
            Debug.Log(health);
        }
    }

    private void FlipSprite(bool direction)
    {
        spriteRenderer.flipX = direction;

        foreach (SpriteRenderer sprite in _sprites)
        {
            sprite.flipX = direction;
        }
    }
}