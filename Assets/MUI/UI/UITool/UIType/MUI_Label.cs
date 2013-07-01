using UnityEngine;
using System.Collections;

#region �����������������������������������������������ץ���������������������������������������������������
/// 13/05/06    �ظm
/// 13/06/17    ���s�w�q�W�� MUI_Label            
#endregion

/// <summary>
/// ���� - ��r���Ҥ���
/// </summary>
/// �W�߯S�Ĩt�� - �s�yFontSize�ʵe�ĪG
public class MUI_Label : MUI_Base
{
    //��r
    public string Text = "Type Text here";
    //��r�j�p
    public int FontSize = 10;
    //��r��Ǥ覡
    public TextAnchor Alignment;

    private int _fontSize_backup;

    // Use this for initialization
    void Start()
    {
        _fontSize_backup = FontSize;
        MUI_Base_Start();
        LogWarning();
    }

    void LogWarning()
    {
        if (FontSize == 0) Debug.LogWarning(this.name + "-FontSize" + "-Unset");
    }

    // Update is called once per frame
    void Update()
    {
        //UIBase auto update
        MUI_Base_Update();
    }
    
    void OnGUI()
    {
        if (guiSkin)
            GUI.skin = this.guiSkin;

        GUI.skin.label.fontSize = (int)((_ScreenSize.x / Resolution) * FontSize);
        GUI.skin.label.normal.textColor = color;
        GUI.skin.label.alignment = Alignment;

        GUI.depth = depth;
        GUIUtility.RotateAroundPivot(angle, CenterPosition);

        if (scale != Vector2.zero)
            GUIUtility.ScaleAroundPivot(scale, CenterPosition);

        if (LayoutScale != Vector2.zero)
            GUIUtility.ScaleAroundPivot(LayoutScale, LayoutCenterPosition);

        if (Text != null)
        {
            GUI.BeginGroup(new Rect(_rect.x, _rect.y, _rect.width * offset.x, _rect.height * offset.y));
            {
                GUI.Label(new Rect(0, 0, _rect.width, _rect.height), Text);
            }
            GUI.EndGroup();
        }
    }


    /// <summary>
    /// �s�yFontSize�ʵe�ĪG (Create)
    /// Name - RectTo
    /// </summary>
    /// <param name="effect">�S�ĵ��c</param>
    void FontSizeTo(MUI_Enum.EffectStruct effect)
    {
        iTween.ValueTo(gameObject, iTween.Hash(
           "from", FontSize,
           "to", effect.fontSize,
           "delay", effect.delay,
           "time", effect.time,
           "easetype", effect.easeType.ToString(),
           "onupdate", "updateFontSize",
           "loopType", effect.looptype.ToString(),
           "name", "FontSizeTo"));

    }

    void StopFontSizeTo(bool Reset)
    {
        if (Reset)
            FontSize = _fontSize_backup;
        iTween.StopByName(this.gameObject, "FontSizeTo");
    }

    void updateFontSize(int input)
    {
        FontSize = input;
    }

}