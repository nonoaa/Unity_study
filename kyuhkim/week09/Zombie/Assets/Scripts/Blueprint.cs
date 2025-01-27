using System;
using System.Threading.Tasks;
using ExitGames.Client.Photon;
using UnityEngine;

public interface IMove
{
    public float StraightStep { get; }
    public float Rotate { get; }
    public float SideStep { get; }
}

public interface IAction
{
    public bool Fire { get; }
    public bool Reload { get; }
}

public interface IItem
{
    public void Use(GameObject target);
}

public interface IPooledItem : IItem
{
    public Action Release { get; set; }
}

public interface IWeapon
{
    public void Fire();
    public bool Reload();
    public int RefillableCount { get; set; }
    public int ChargedCount { get; }
}

public interface IWeaponHolder
{
    public IWeapon Weapon { get; }
}

public interface IDamageable
{
    public void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal);
}

public interface IRepairable
{
    public void Repair(float changeAmount);
}

public interface IDurability
{
    public float Durability { get; }
}

public interface ILiving : IDamageable, IRepairable, IDurability
{
    public bool IsDead { get; }
    public event Action OnDeath;
}

public interface IInput : IMove, IAction
{
}

public interface IShooter
{
    public IWeapon Weapon { get; set; }
    public IInput Input { get; set; }
}

public interface IGameManager
{
    public bool IsGameover { get; }
    public void AddScore(int score);
}

public interface ISyncObjectPool<T>
{
    public Task<T> RequestBy(int id = 0);
    public void Release(int key);
    // public Task SetPrefab(string addressName);
    public bool IsAccounted(int key);
}

public interface ISpawnReceiver
{
    public void EventSpawn(EventData eventData);
    public void EventDespawn(EventData eventData);
}

public interface ISpawnSender
{
    public void RaiseSpawnEvent(params object[] param);
    public void RaiseDespawnEvent(params object[] param);
}

public class CustomEventCode
{
    public const byte HealthPackEvent = 10;
    public const byte AmmoEvent = 11;
    public const byte CoinEvent = 12;
    public const byte RequestEvent = 0;
    public const byte ReleaseEvent = 1;
}
