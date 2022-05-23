using UnityEngine;

public abstract class ThermalZone : MonoBehaviour
{
    protected int _temperature;

    [SerializeField] protected int _rateDeterioration;

    protected ThermalZone _thermalZoneType;
    
    public int RateDeterioration => _rateDeterioration;

    protected virtual void Impact(Heat heat)
    {
        heat.ChangeCurentValue(_rateDeterioration, this);
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Heat heat))
        {
            Impact(heat);
        }
    }
}