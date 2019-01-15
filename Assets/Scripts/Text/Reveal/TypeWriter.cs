// Author: Adrian Miasik
// Personal Portfolio: http://AdrianMiasik.com
// Github Account: https://github.com/AdrianMiasik

using UnityEngine;
using TMPro;
using UnityEngine.Profiling;

namespace Text.Reveal
{
	/// <summary>
	/// Reveals text characters over time much like a typewriter (Using the TextMeshProUGUI component).
	/// </summary>
	/// <summary>
	/// There is a delay in-between each character reveal.
	/// </summary>
	public class TypeWriter : MonoBehaviour
	{
		[SerializeField] [Tooltip("The text we want to reveal.")]
		private TextMeshProUGUI displayText;

		[SerializeField] [Tooltip("The text we will use to display statistics about this character reveal (Number of characters revealed, the delay in-between each character reveal, and the current characters reveal time).")]
		private TextMeshProUGUI statistics;

		[SerializeField] [Tooltip("The keycode that starts the reveal. When pressing this key down, the reveal will start.")]
		private KeyCode startKey = KeyCode.Space;

		[SerializeField] [Tooltip("The amount of time (in seconds) between each character reveal.")]
		private float delayBetweenReveal = 0.05f;

		private bool _isRevealing = false;
		private float _characterTime = 0f;
		private int _numberOfCharacters = 0;
		private int _numberOfCharactersRevealed;

		private void Reset()
		{
			Profiler.BeginSample("TypeWriter.Reset");
			
			// Quickly Fetch References.
			displayText = GetComponent<TextMeshProUGUI>();
			statistics = GetComponent<TextMeshProUGUI>();
			
			Profiler.EndSample();
		}

		private void Awake()
		{
			Profiler.BeginSample("TypeWriter.Awake");

			// If displayText is null...
			if (displayText == null)
			{
				// Attempt to assign
				displayText = GetComponent<TextMeshProUGUI>();

#if DEBUG_TEXT
				// Assign failed...
				if (displayText == null)
				{
					Debug.LogAssertion("No TextMeshProUGUI component found.");
				}
#endif
			}
			
			Profiler.EndSample();
		}

		private void Start()
		{
			Profiler.BeginSample("TypeWriter.Start");

			if (!displayText)
			{
				Debug.LogWarning("No TextMeshProUGUI component found.");
			}
			else
			{
				Initialize();
				_isRevealing = true;
			}

			if (!statistics)
			{
				Debug.LogWarning("No TextMeshProUGUI component found.");
			}
			
			Profiler.EndSample();
		}

		/// <summary>
		/// Initializes the text (which also means hiding it) and starts the character reveal within the update loop.
		/// </summary>
		public void Reveal()
		{
			Profiler.BeginSample("TypeWriter.Reveal");
			
			Initialize();
			Play();
			
			Profiler.EndSample();
		}

		/// <summary>
		/// Initializes the text reveal by hiding the text and defaulting some variables.
		/// </summary>
		private void Initialize()
		{
			Profiler.BeginSample("TypeWriter.Initialize");

			// Hide the text
			displayText.maxVisibleCharacters = 0;
			
			_numberOfCharactersRevealed = 0;
			_numberOfCharacters = displayText.text.Length;	
			
			Profiler.EndSample();
		}

		/// <summary>
		/// Updates the statistics UI string.
		/// </summary>
		private void UpdateStatisticsText()
		{
			Profiler.BeginSample("TypeWriter.UpdateStatisticsText");

			statistics.text = "Number of characters revealed: " + _numberOfCharactersRevealed + "/" + _numberOfCharacters + "\n" +
			                  "Delay in-between character reveal: " + delayBetweenReveal + " seconds.\n" +
			                  "Current character reveal time: " + _characterTime.ToString("F2") + " seconds.";
			
			Profiler.EndSample();
		}
		
		/// <summary>
		/// Starts revealing the characters.
		/// </summary>
		/// <summary>
		/// Note: You might need to initialize this script first.
		/// </summary>
		private void Play()
		{
			Profiler.BeginSample("TypeWriter.Play");
			
			_isRevealing = true;
			
			Profiler.EndSample();
		}

		// TMP INPUT FIELD - Incorrect character length value of the input fields text component:
		// The reason we are doing TMP_InputField instead of getting the TextMeshProUGUI component within the input field component
		// is because the TextMeshProUGUI text.Length and textInfo.CharacterCount values are incorrect. The text field could be an "empty"
		// string, but it still seems to provides us with a length of 1. 
		// According to a Unity Technologies user "Stephan_B" we shouldn't be accessing the input fields text component anyways.
		// However, if you do need to access the text input's TextMeshProUGUI component and you do need to get the character count value from
		// that component, there does seem to be a workaround a Unity forum user named "Chris-Trueman" has found.
		// Simply trimming the Unicode character 'ZERO WIDTH SPACE' (Code 8203) from the ugui text string does seem to return the correct length.
		// I've tested Chris' solution and it does seem to work if you need to use it, however for this case I won't be doing that.
		// SOURCE: https://forum.unity.com/threads/textmesh-pro-ugui-hidden-characters.505493/
		/// <summary>
		/// Caches the string in the source's text string. (Using the TextMeshProUGUI component).
		/// </summary>
		/// <param name="source"></param>
		public void SetText(TMP_InputField source)
		{
			Profiler.BeginSample("TypeWriter.SetText");
			displayText.text = source.text;
			Profiler.EndSample();
		}

		private void Update()
		{
			Profiler.BeginSample("TypeWriter.Update");

			// Prevent a negative value from being assigned
			delayBetweenReveal = Mathf.Clamp(delayBetweenReveal, 0, delayBetweenReveal);

			// Start text reveal on input
			if (Input.GetKeyDown(startKey))
			{
				Play();
			}

			// If we are text revealing...
			if (_isRevealing)
			{
				// If we don't have anything to reveal...
				if (_numberOfCharacters == 0)
				{
					UpdateStatisticsText();

					Profiler.EndSample();
					
					// Early exit
					return;
				}
				
				_characterTime += Time.deltaTime;

				// While loop used to calculate how many letters on the same frame needs to be drawn
				while (_characterTime > delayBetweenReveal)
				{				
					// Reveal a character
					displayText.maxVisibleCharacters = _numberOfCharactersRevealed + 1 % _numberOfCharacters;

					// If all characters are revealed, set the _isRevealing flag as dirty and break out of this while loop 
					if (_numberOfCharactersRevealed == _numberOfCharacters)
					{
						_characterTime = 0f;
						_isRevealing = false;
						break;
					}
					
					_numberOfCharactersRevealed++;
					_characterTime -= delayBetweenReveal;
				}

				UpdateStatisticsText();
			}
			
			Profiler.EndSample();
		}
	}
}