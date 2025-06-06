using Handler;
using Manager;
using UnityEngine;

public abstract class ICharacterSkill : MonoBehaviour
{
    protected virtual int Damage { get; set; }
    private int CommandLength { get; set; }
    public bool HasHit { get; set; }

    public abstract void Run();

    public void SetCommandCoff(int count)
    {
        CommandLength = count;
    }

    public virtual void Hit()
    {
        HasHit = true;
        VarManager.Manager.Opponent.Hit(Damage, CommandLength);
    }

    protected void CreateProjectile(Vector2 startDirection, string projectilePath)
    {
        GameObject projectile =
            Instantiate(Resources.Load<GameObject>(projectilePath), startDirection, Quaternion.identity);
        Vector3 endDirection = GameManager.Manager.ProjectileEndDirection();
        projectile.GetComponent<Rigidbody2D>().linearVelocity = endDirection * ConstController.Manager.ShootSpeed;
    }
}