using UnityEngine;

public class InteractableTalk : IActionInteractableObject
{
    NPCDialog _currentDialog;

    public void Do(InteractableObject obj)
    {
        if (obj is ModelNPC)
        {
            ModelNPC _npc = (obj as ModelNPC);
            if (_currentDialog == null)
            {
                _currentDialog = _npc.currentDialog[Random.Range(0, _npc.currentDialog.Count)];
                _npc.maxLine = _currentDialog.dialog.Length;
            }

            if (!_npc.dialogBox.gameObject.activeSelf)
            {
                _npc.dialogBox.gameObject.SetActive(true);
                LineChanger(_npc);
            }
            else
            {
                if (_npc.currentLine < _npc.maxLine)
                {
                    LineChanger(_npc);
                }
                else
                {
                    _currentDialog = null;
                    _npc.currentLine = 0;
                    _npc.dialogBox.gameObject.SetActive(false);
                }
            }
        }
    }

    void LineChanger(ModelNPC npc)
    {
        npc.textField.text = _currentDialog.dialog[npc.currentLine];
        npc.currentLine++;
    }
}