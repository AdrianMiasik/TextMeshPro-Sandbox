// Author: Adrian Miasik
// Personal Portfolio: http://AdrianMiasik.com
// Github Account: https://github.com/AdrianMiasik

using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.UI;

namespace SceneSpecificTools
{
	/// <summary>
	/// Invokes a buttons OnClick UnityEvent(s) when a keycode is pressed down.
	/// </summary>
	public class KeycodeButtonOnClick : MonoBehaviour
	{
		[SerializeField] private Button button = null;
		[SerializeField] private KeyCode buttonKey = KeyCode.None;

		private void Awake()
		{
			Profiler.BeginSample("KeycodeButtonOnClick.Awake");

			if (button == null)
			{
				Debug.LogAssertion("Button has not been assigned.");
			}

			if (buttonKey == KeyCode.None)
			{
				Debug.LogAssertion("Keycode has not been assigned.");
			}
			
			Profiler.EndSample();
		}

		private void Update()
		{
			Profiler.BeginSample("KeycodeButtonOnClick.Update");

			if (Input.GetKeyDown(buttonKey))
			{
				button.onClick.Invoke();
			}
			
			Profiler.EndSample();
		}
	}
}