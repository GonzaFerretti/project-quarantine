using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModelNPC : InteractableObject
{
    public Image dialogBox;
    public Text textField;
    public NPCAttributes npcAttributes;
    Resource _currentResource;
    float _currentAmountRequired;
    public List<NPCDialog> currentDialog;

    public int currentLine;
    public int maxLine;


    protected override void Start()
    {
        base.Start();
        SetAttributes();
    }

    void SetAttributes()
    {
        _currentResource = npcAttributes.resources[Random.Range(0, npcAttributes.resources.Count)];
        _currentAmountRequired = _currentResource.amounts[Random.Range(0, _currentResource.amounts.Count)];
        InitModel(ref animator, npcAttributes.characterModel, npcAttributes.animations);
        if (currentDialog == null) currentDialog = new List<NPCDialog>();

        for (int i = 0; i < npcAttributes.dialog.Count; i++)
        {
            if (npcAttributes.dialog[i].resource == _currentResource)
                currentDialog.Add(npcAttributes.dialog[i]);
        }

        //Que mande esta info a un manager de objetos asi se guarda la info del npc en particular
        //Que los requisitos de recursos se pasen al ResourceManager
    }

    public virtual void InitModel(ref Animator animator, GameObject characterModel, RuntimeAnimatorController animations)
    {
        GameObject myPlayerCharacter = Instantiate(characterModel, transform);
        myPlayerCharacter.transform.localPosition = new Vector3(0, 0, 0);
        if (animations)
        {
            myPlayerCharacter.GetComponent<Animator>().runtimeAnimatorController = animations;
            animator = myPlayerCharacter.GetComponent<Animator>();
        }
        myPlayerCharacter.name = "characterModel";
    }
}
