// Author: Adrian Miasik
// Personal Portfolio: http://AdrianMiasik.com
// Github Account: https://github.com/AdrianMiasik

/*
File Creation Date: (yyyy-mm-dd)
	2018-09-14
Purpose:
	Invokes a buttons OnClick UnityEvent(s) when a keycode is pressed down
*/

using UnityEngine;
using UnityEngine.UI;

namespace Text
{
	public class KeycodeButtonOnClick : MonoBehaviour
	{
		[SerializeField] private Button button = null;
		[SerializeField] private KeyCode buttonKey = KeyCode.None;

		private void Awake()
		{
			if (button == null)
			{
				Debug.LogAssertion("Button has not been assigned.");
			}

			if (buttonKey == KeyCode.None)
			{
				Debug.LogAssertion("Keycode has not been assigned.");
			}
		}

		private void Update()
		{
			if (Input.GetKeyDown(buttonKey))
			{
				button.onClick.Invoke();
			}
		}
	}
}