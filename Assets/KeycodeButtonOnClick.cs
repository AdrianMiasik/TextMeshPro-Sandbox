// Author: Adrian Miasik
// Personal Portfolio: http://AdrianMiasik.com
// Github Account: https://github.com/AdrianMiasik

// Creation Date: 2018-09-14
// Purpose: Invokes a buttons OnClick UnityEvent(s) when a keycode is pressed down

using UnityEngine;
using UnityEngine.UI;

namespace AdrianMiasik.Text
{
	public class KeycodeButtonOnClick : MonoBehaviour
	{
		[SerializeField] private Button _button;
		[SerializeField] private KeyCode _buttonKey;

		private void Update()
		{
			if (Input.GetKeyDown(_buttonKey))
			{
				_button.onClick.Invoke();
			}
		}
	}
}
