using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

/* ��ͨ���̣��̳���ClickKeyboard��Ӧ��ֻ��ʵ���Լ���Axis2Letter����. */
public class NormalKeyboard : ClickKeyboard
{
    public bool Crossover = false;

    //private void Update()
    //{
    //    GameObject key;
    //    if (touched)
    //    {
    //        if (PadSlide[SteamVR_Input_Sources.LeftHand].axis != new Vector2(0, 0))
    //        {
    //            Axis2Letter(PadSlide[SteamVR_Input_Sources.LeftHand].axis, SteamVR_Input_Sources.LeftHand, 0, out key);
    //            //Debug.Log("Key: " + ascii);
    //        }
    //        if (PadSlide[SteamVR_Input_Sources.RightHand].axis != new Vector2(0, 0))
    //        {
    //            Axis2Letter(PadSlide[SteamVR_Input_Sources.RightHand].axis, SteamVR_Input_Sources.RightHand, 0, out key);
    //            //Debug.Log("Key: " + ascii);
    //        }
    //    }
    //}

    private int[,,] keys;

    private int[,,] keys_with_crossover = new int[6, 4, 6] { { { 0x00, ',', ',', 0x20, 0x20, 0x20 }, { 0x10, 'z', 'x', 'c', 'v', 'b' }, { 'a', 's', 'd', 'f', 'g', 'h' } ,{ 'q', 'w', 'e', 'r', 't', 'y'}},
                                                             { { 0x00, ',', ',', 0x20, 0x20, 0x20 }, { 0x10, 'Z', 'X', 'C', 'V', 'B' }, { 'A', 'S', 'D', 'F', 'G', 'H' } ,{ 'Q', 'W', 'E', 'R', 'T', 'Y'}},
                                                             { { 0x00, ',', ',', 0x20, 0x20, 0x20 }, { 0x10, '(', ')', '-', '_', ':' }, { '~', '!', '@', '#', '%', '\''} ,{ '1', '2', '3', '4', '5', '6'}},
                                                             { { 0x20, 0x20, 0x20, '.', '.', 0x0D }, {'c', 'v', 'b', 'n', 'm', 0x08 }, {'f', 'g', 'h', 'j', 'k', 'l' } ,{'t', 'y', 'u', 'i', 'o', 'p'}},
                                                             { { 0x20, 0x20, 0x20, '.', '.', 0x0D }, {'C', 'V', 'B', 'N', 'M', 0x08 }, {'F', 'G', 'H', 'J', 'K', 'L' } ,{'T', 'Y', 'U', 'I', 'O', 'P'}},
                                                             { { 0x20, 0x20, 0x20, '.', '.', 0x0D }, {'-', '_', ':', ';', '/', 0x08 }, {'#', '%', '\'', '&', '*', '?'} ,{'5', '6', '7', '8', '9', '0'}} };
    

    private int[,,] keys_without_crossover = new int[6, 4, 6] { { { 0x00, ',', ',', 0x20, 0x20, 0}, { 0x10, 'z', 'x', 'c', 'v', 0}, { 'a', 's', 'd', 'f', 'g', 0} ,{ 'q', 'w', 'e', 'r', 't', 0}},
                                                                { { 0x00, ',', ',', 0x20, 0x20, 0}, { 0x10, 'Z', 'X', 'C', 'V', 0}, { 'A', 'S', 'D', 'F', 'G', 0} ,{ 'Q', 'W', 'E', 'R', 'T', 0}},
                                                                { { 0x00, ',', ',', 0x20, 0x20, 0}, { 0x10, '(', ')', '-', '_', 0}, { '~', '!', '@', '#', '%', 0} ,{ '1', '2', '3', '4', '5', 0}},
                                                                { { 0x20, 0x20, '.', '.', 0x0D, 0}, {'v', 'b', 'n', 'm', 0x08 , 0}, {'g', 'h', 'j', 'k', 'l' , 0} ,{ 'y', 'u', 'i', 'o', 'p', 0}},
                                                                { { 0x20, 0x20, '.', '.', 0x0D, 0}, {'V', 'B', 'N', 'M', 0x08 , 0}, {'G', 'H', 'J', 'K', 'L' , 0} ,{ 'Y', 'U', 'I', 'O', 'P', 0}},
                                                                { { 0x20, 0x20, '.', '.', 0x0D, 0}, {'_', ':', ';', '/', 0x08 , 0}, {'%', '\'', '&', '*','?' , 0} ,{ '6', '7', '8', '9', '0', 0}} }; 

    public override int Axis2Letter(Vector2 axis, SteamVR_Input_Sources hand, int mode, out GameObject key)
    {
        keys = Crossover ? keys_with_crossover : keys_without_crossover;
        int column_number = Crossover ? 6 : 5;
        print(keys);
        print(column_number);

        int row, column;
        if (axis.y <= -0.5) row = 0;
        else if (axis.y < -0.05 && axis.y > -0.5) row = 1;
        else if (axis.y > -0.05 && axis.y < 0.4) row = 2;
        else row = 3;

        float width = Mathf.Sqrt(1 - axis.y * axis.y);
        float columnRatio = (axis.x + width) / (2 * width / column_number);
        column = Mathf.FloorToInt(columnRatio);
        if (column > column_number - 1) column = column_number - 1;
        else if (column < 0) column = 0;

        int handmode = (hand == SteamVR_Input_Sources.LeftHand) ? mode : mode + 3;
        char output = (char)keys[handmode, row, column];
        print(output);

        switch (output)
        {
            case (char)VKCode.Space:
                key = keyboardRoot.Find("space").gameObject;
                break;
            case (char)VKCode.Shift:
                key = keyboardRoot.Find("shift").gameObject;
                break;
            case (char)VKCode.Switch:
                key = keyboardRoot.Find("sym").gameObject;
                break;
            case (char)VKCode.Enter:
                key = keyboardRoot.Find("enter").gameObject;
                break;
            case (char)VKCode.Back:
                key = keyboardRoot.Find("back").gameObject;
                break;
            case ',':
                key = keyboardRoot.Find("comma").gameObject;
                break;
            case '.':
                key = keyboardRoot.Find("period").gameObject;
                break;
            default:
                string name = ((char)keys[handmode - mode, row, column]).ToString() + ((char)keys[handmode - mode + 2, row, column]).ToString();
                if (name[1] == '/')
                    name = "m\\";
                key = keyboardRoot.Find(name).gameObject;
                break;
        }

        return keys[handmode, row, column];
    }
}
