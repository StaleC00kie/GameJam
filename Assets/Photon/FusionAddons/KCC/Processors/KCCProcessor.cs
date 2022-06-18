namespace Fusion.KCC
{
	using UnityEngine;

	/// <summary>
	/// Default processor implementation without support of [Networked] properties. Supports runtime lookup and synchronization.
	/// Execution of methods is fully supported on 1) Prefabs, 2) Instances spawned with GameObject.Instantiate(), 3) Instances spawned with Runner.Spawn()
	/// </summary>
	[DisallowMultipleComponent]
	[RequireComponent(typeof(NetworkObject))]
	public abstract partial class KCCProcessor : BaseKCCProcessor, IKCCProcessorProvider
	{
		// IKCCProcessorProvider INTERFACE

		IKCCProcessor IKCCProcessorProvider.GetProcessor()
		{
			return this;
		}
	}
}
