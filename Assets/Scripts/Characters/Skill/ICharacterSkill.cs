using Handler;
using Manager;
using UnityEngine;

public abstract class ICharacterSkill : MonoBehaviour
{
    public virtual int Damage { get; protected set; }
    public int CommandLength { get; private set; }
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
        Debug.Log(endDirection);
        projectile.GetComponent<Rigidbody2D>().linearVelocity = endDirection * ConstController.Manager.ShootSpeed;
    }
}