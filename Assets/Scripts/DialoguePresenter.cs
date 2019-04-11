using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePresenter : MonoBehaviour {

	public DialogueFile dialogueFile = null;
	public string dialogueFileName = null;

    private DialogueManager manager;
    private Dialogue currentDialogue;
    private Dialogue.Choice currentChoice = null;
    Dialogue.Choice[] choiceList;

    //UI Prefabs and Parts
    public GameObject playerPanel;
    public GameObject NPCPanel;
    public GameObject buttonPrefab;
    public Text NPCTextbox;
    public Button NPCNextButton;

	void OnMouseDown()
	{
		if(!playerPanel.gameObject.activeSelf)
		{
			playerPanel.gameObject.SetActive(true);
			NPCPanel.gameObject.SetActive(true);
			BeginDialogue();
		}
	}

	void Start()
	{
		playerPanel.gameObject.SetActive(false);
		NPCPanel.gameObject.SetActive(false);

	}

	// Use this for initialization
	public void BeginDialogue () 
	{
		manager = DialogueManager.LoadDialogueFile(dialogueFile);
        currentDialogue = manager.GetDialogue(dialogueFileName);
        currentChoice = currentDialogue.GetChoices()[0];
        currentDialogue.PickChoice(currentChoice);

        //create first dialogue
        if(currentChoice.speaker == "NPC")
        {
        	NPCTextbox.text = currentChoice.dialogue;
        	NPCNextButton.onClick.RemoveAllListeners();
        	NPCNextButton.onClick.AddListener(delegate { ButtonPressed(); });
        }
        else
        {
        	GameObject b = Instantiate(buttonPrefab);
        	b.transform.SetParent(playerPanel.transform);
        	b.GetComponent<Button>().onClick.AddListener(delegate { ButtonPressed(currentChoice); });
        	b.GetComponentInChildren<Text>().text = currentChoice.dialogue;
        }
	}

	public void ButtonPressed(Dialogue.Choice choice)
	{
		currentDialogue.PickChoice(choice);
        currentChoice = choice;
        DialogueNext();
	}

	public void ButtonPressed()
	{
		currentDialogue.PickChoice(currentChoice);
        DialogueNext();
	}

	public void DialogueNext()
	{
		//remove all children buttons of the panel before adding 
        //ones in next dialogue
        Button[] buttons = playerPanel.GetComponentsInChildren<Button>();
        for(int i = 0; i < buttons.Length; i++)
        {
        	DestroyImmediate(buttons[i].gameObject);
        }
		
		choiceList = currentDialogue.GetChoices();

		if(choiceList.Length == 0)
		{
			playerPanel.gameObject.SetActive(false);
			NPCPanel.gameObject.SetActive(false);
			return;
		}
		currentChoice = choiceList[0];
		if(currentChoice.speaker == "NPC")
        {
        	NPCTextbox.text = currentChoice.dialogue;
       		NPCNextButton.gameObject.SetActive(true);
        }
        else
        {
			NPCNextButton.gameObject.SetActive(false);
			choiceList = currentDialogue.GetChoices();
	        System.Array.Sort(choiceList, (o1, o2) => o1.userData.CompareTo(o2.userData));
			foreach (Dialogue.Choice choice in choiceList)
	        {
	        	GameObject b = Instantiate(buttonPrefab);
	        	b.transform.SetParent(playerPanel.transform);
	        	b.GetComponent<Button>().onClick.AddListener(delegate { ButtonPressed(choice); });
	        	b.GetComponentInChildren<Text>().text = choice.dialogue;
	        }
	    }
	}
}
