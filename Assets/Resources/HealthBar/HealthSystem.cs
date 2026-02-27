using Manager;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public static HealthSystem Manager;

    [SerializeField] private Image PlayerHealthBar;

    [SerializeField] private Image OpponentHealthBar;

    private bool isEnd;

    private float maxHitPoint = 100f;

    private float PlayerhitPoint;
    private float OpponenthitPoint;

    void Awake()
    {
        Manager = this;
        isEnd = false;
    }

    void Start()
    {
        PlayerhitPoint = maxHitPoint;
        OpponenthitPoint = maxHitPoint;
        UpdateGraphics();
    }

    void Update()
    {
        if (PlayerhitPoint <= 0 && !isEnd)
        {
            
            SteamNetworkManager.Manager.SendMsg(SteamNetworkManager.Manager.RemoteSteamId,
                Constant.SteamNetworkingType.END_GAME, true.ToString());
            
            StartCoroutine(GameManager.Manager.EndGame(false));
            isEnd = true;
        }

        if (OpponenthitPoint <= 0 && !isEnd)
        {
            
            SteamNetworkManager.Manager.SendMsg(SteamNetworkManager.Manager.RemoteSteamId,
                Constant.SteamNetworkingType.END_GAME, false.ToString());
            
            StartCoroutine(GameManager.Manager.EndGame(true));
            isEnd = true;
        }
    }

    private void UpdateHealthBar()
    {
        float ratio = PlayerhitPoint / maxHitPoint;
        PlayerHealthBar.rectTransform.localPosition = new Vector3(
            PlayerHealthBar.rectTransform.rect.width * ratio - PlayerHealthBar.rectTransform.rect.width, 0, 0);

        ratio = OpponenthitPoint / maxHitPoint;
        OpponentHealthBar.rectTransform.localPosition = new Vector3(
            OpponentHealthBar.rectTransform.rect.width * ratio - OpponentHealthBar.rectTransform.rect.width, 0, 0);
        //healthText.text = hitPoint.ToString("0") + "/" + maxHitPoint.ToString("0");
    }

/*
    private void UpdateHealthGlobe()
    {
        float ratio = hitPoint / maxHitPoint;
        currentHealthGlobe.rectTransform.localPosition = new Vector3(0,
            currentHealthGlobe.rectTransform.rect.height * ratio - currentHealthGlobe.rectTransform.rect.height, 0);
        healthText.text = hitPoint.ToString("0") + "/" + maxHitPoint.ToString("0");
    }
*/
    public void TakeDamage(bool isPlayer, float Damage)
    {
        if (isPlayer)
        {
            PlayerhitPoint -= Damage;
            if (PlayerhitPoint < 1)
            {
                PlayerhitPoint = 0;
            }
        }
        else
        {
            OpponenthitPoint -= Damage;
            if (OpponenthitPoint < 1)
            {
                OpponenthitPoint = 0;
            }
        }

        UpdateGraphics();
    }

    public void HealDamage(bool isPlayer, float Heal)
    {
        if (isPlayer)
        {
            PlayerhitPoint += Heal;
            if (PlayerhitPoint > maxHitPoint)
                PlayerhitPoint = maxHitPoint;
        }
        else
        {
            OpponenthitPoint += Heal;
            if (OpponenthitPoint > maxHitPoint)
                OpponenthitPoint = maxHitPoint;
        }

        UpdateGraphics();
    }

    public void SetMaxHealth(float max)
    {
        maxHitPoint += (int)(maxHitPoint * max / 100);

        UpdateGraphics();
    }

    private void UpdateGraphics()
    {
        UpdateHealthBar();
        //UpdateHealthGlobe();
    }
}