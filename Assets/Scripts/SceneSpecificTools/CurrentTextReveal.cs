using Text.Reveals.Base;
using TMPro;
using UnityEngine;

namespace SceneSpecificTools
{
	// A script used to quickly change between the text reveals without having to always change the references in the scene.
	// You can ignore this script.
	public class CurrentTextReveal : MonoBehaviour
	{
		public TextReveal TextReveal;

		public void ReplaceStringWithSources(TMP_InputField source)
		{
			TextReveal.ReplaceStringWithSources(source);
		}

		public void Reveal()
		{
			TextReveal.Reveal();
		}
	}
}
