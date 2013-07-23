using UnityEngine;
using System.Collections;

public class Event_ChangeRolePosition : MonoBehaviour
{

    public GameDefinition.ChangeRoleMode changeMode;
    // Use this for initialization
    void Start()
    {
        RolesCollection.script.ChangeRolePosition(changeMode);
        Destroy(this.gameObject);
        AudioSoundPlayer.script.PlayAudio("�}�b��o�g�b��");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
