using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/DialogueContentFile")]
public class DialogueSO : ScriptableObject
{
	public bool RandomOrder = false;
	[TextArea]
	public string[] dialogues;
}
