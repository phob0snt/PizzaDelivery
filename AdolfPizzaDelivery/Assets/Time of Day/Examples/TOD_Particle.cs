using UnityEngine;

public abstract class TOD_Particle : MonoBehaviour
{
	private ParticleSystem particleComponent;

	protected float GetEmission()
	{
		if (particleComponent)
		{
			return particleComponent.emission.rateOverTimeMultiplier;
		}
		else
		{
			return 0;
		}
	}

	protected void SetEmission(float value)
	{
		if (particleComponent)
		{
			var emission = particleComponent.emission;
			emission.rateOverTimeMultiplier = value;
		}
	}

	protected void Awake()
	{
		particleComponent = GetComponent<ParticleSystem>();
	}
}
