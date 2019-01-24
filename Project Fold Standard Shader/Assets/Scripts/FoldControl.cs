using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class FoldControl : MonoBehaviour
{

    //public Piece[] piecelist;

    public Piece leftPiece;
    public Piece rightPiece;

    public Camera PCamera;
    public Camera OCamera;

    public Button LeftInner, LeftOut, RightInner, RightOut;

    public Canvas ButtonCanvas;
    public Canvas ThisCanvas;

    public int RightStatus;
    public int LeftStatus;

    public GameObject folded12;
    public GameObject folded21;
    public GameObject folded23;
    public GameObject folded32;

    // YYC
    public GameObject cowboy;
    public int foldTime;
    public bool isCowboyInPiece;
    // YYC

    public bool AllPiecesShowing = true;

    void Start()
    {
        LeftInner.onClick.AddListener(delegate { GetLeftPieceFoldOrUnfold(leftPiece, true); });
        LeftOut.onClick.AddListener(delegate { GetLeftPieceFoldOrUnfold(leftPiece, false); });
        RightInner.onClick.AddListener(delegate { GetRightPieceFoldOrUnfold(rightPiece, true); });
        RightOut.onClick.AddListener(delegate { GetRightPieceFoldOrUnfold(rightPiece, false); });

        foldTime = 0;
        isCowboyInPiece = false;
        AllPiecesShowing = true;
    }

    // New
    void SetCowBoyParent(Piece currentPiece)
    {
        if (Mathf.Abs(cowboy.transform.position.x - currentPiece.transform.position.x) < 2.25)
        {
            cowboy.transform.SetParent(currentPiece.transform);
            isCowboyInPiece = true;
        }
        else
        {
            isCowboyInPiece = false;
        }
    }

    void SetCowboyPosition(int PiecePosition)
    {
        // currentPiecePosition = 0 -------->Piece_Middle、Piece_Right、Piece_Left
        // currentPiecePosition = 1 -------->Piece_1_23、Piece_1_32、Piece_12_3、Piece_123、Pieces_21_3
        switch (PiecePosition)
        {
            case 0:
                cowboy.transform.position = new Vector3(cowboy.transform.position.x, cowboy.transform.position.y, -0.2f);
                break;
            case 1:
                cowboy.transform.position = new Vector3(cowboy.transform.position.x, cowboy.transform.position.y, 19.5f);
                break;
        }
    }
    // New

    public void GetRightPieceFoldOrUnfold(Piece currPiece, bool direction)
    {
        if (RightStatus == 0)   //
        {
            StartCoroutine(RightPieceFold(currPiece, direction));
        }
        else if (RightStatus == 1)  //
        {
            StartCoroutine(RightPieceUnfold(currPiece, direction));
        }
        else  //
        {
            StartCoroutine(RightPieceUnfold(currPiece, direction));
        }
    }

    // 开始锁死UI
    void beginStopSkill(bool b)
    {
        LeftInner.GetComponent<Button>().interactable = b;
        LeftOut.GetComponent<Button>().interactable = b;
        RightInner.GetComponent<Button>().interactable = b;
        RightOut.GetComponent<Button>().interactable = b;
    }

    // 右边的纸折叠
    IEnumerator RightPieceFold(Piece currPiece, bool direction)
    {
        beginStopSkill(false);

        // 设置玩家位置
        SetCowBoyParent(currPiece);
        foldTime++;
        // 设置玩家位置

        if (!direction)
        {

            float speed = -120;
            
            for (float rotateInit = 0; rotateInit > -180; rotateInit = rotateInit + Time.deltaTime * speed)
            {
                currPiece.transform.RotateAround(currPiece.rotatePoint, new Vector3(0f, 1f, 0f), Time.deltaTime * speed);
                
                Debug.Log("GoingFold1");
                yield return null;
                
                //Debug.Log("rotateSpeed" + speed);
            }

            Instantiate(folded23);
            
            
            this.RightStatus = 2;
        }
        else
        {
            float speed = 120;


            for (float rotateInit = 0; rotateInit < 180; rotateInit = rotateInit + Time.deltaTime * speed)
            {
                currPiece.transform.RotateAround(currPiece.rotatePoint, new Vector3(0f, 1f, 0f), Time.deltaTime * speed);
                Debug.Log("GoingFold2");
                yield return null;
            }

            //currPiece.foldStatus = 1;

            Instantiate(folded32);

            this.RightStatus = 1;
        }

        // 设置玩家位置
        cowboy.transform.SetParent(null);
        SetCowboyPosition(1);

        if(isCowboyInPiece)
        {
            cowboy.transform.rotation = Quaternion.identity;
            cowboy.transform.localScale = new Vector3(cowboy.transform.localScale.x * (-1.0f), cowboy.transform.localScale.y, cowboy.transform.localScale.z);
        }
        // 设置玩家位置

        switchCamera();

        Vector3 v = currPiece.transform.eulerAngles;
        v.z = 180f;
        Quaternion q = Quaternion.Euler(v);
        currPiece.transform.rotation = q;

        beginStopSkill(true);
        yield return null;

    }

    public void UnlockOrigami()
    {
        ButtonCanvas.GetComponent<Canvas>().enabled = true;
        ThisCanvas.GetComponent<Canvas>().enabled = false;
    }

    // 右边的纸打开
    IEnumerator RightPieceUnfold(Piece currPiece,bool direction)
    {
        beginStopSkill(false);

        // 设置玩家位置
        SetCowBoyParent(currPiece);
        foldTime--;
        // 设置玩家位置

        if (direction)
        {

            float speed = -120;
            switchCamera();
            destroyAll();
            

            for (float rotateInit = 0; rotateInit > -180; rotateInit = rotateInit + Time.deltaTime * speed )
            {
                currPiece.transform.RotateAround(currPiece.rotatePoint, new Vector3(0f, 1f, 0f), Time.deltaTime * speed);
                //Debug.Log("Init" + rotateInit);
                Debug.Log("GoingUnfold1");
                yield return null;
            }

            //currPiece.foldStatus = 0;
            this.RightStatus = 0;
        }
        else
        {
            float speed = 120;
            switchCamera();

            GameObject fold23clone = GameObject.Find("Piece_1_23(Clone)");
            Destroy(fold23clone);
            destroyAll();

            for (float rotateInit = 0; rotateInit < 180; rotateInit = rotateInit + Time.deltaTime * speed)
            {
                currPiece.transform.RotateAround(currPiece.rotatePoint, new Vector3(0f, 1f, 0f), Time.deltaTime * speed);
                //Debug.Log("Init" + rotateInit);
                Debug.Log("GoingUnfold2");
                yield return null;
                //Debug.Log("eulerOfUnfold"+currPiece.transform.rotation.eulerAngles);
            }

            //currPiece.foldStatus = 0;
            this.RightStatus = 0;
        }

        // 设置玩家位置
        cowboy.transform.SetParent(null);
        if(foldTime == 0)
        {
            SetCowboyPosition(0);
        }
        else
        {
            SetCowboyPosition(1);
        }
        if (isCowboyInPiece)
        {
            cowboy.transform.rotation = Quaternion.identity;
            cowboy.transform.localScale = new Vector3(cowboy.transform.localScale.x * (-1.0f), cowboy.transform.localScale.y, cowboy.transform.localScale.z);
        }
        // 设置玩家位置

        Vector3 v = currPiece.transform.eulerAngles;
        v.z = 0f;
        Quaternion q = Quaternion.Euler(v);
        currPiece.transform.rotation = currPiece.initRoat;
        currPiece.transform.position = currPiece.initPos;
        beginStopSkill(true);
        yield return null;

    }

    private void Revert(GameObject currPiece)
    {
        Transform currTrans = currPiece.transform;
        currTrans.localScale = new Vector3(1f,-1f,-1f);
        

    //    currPiece.GetComponent<Transform>().localScale = currPiece.GetComponent<Transform>().localScale * trans;

    }

    private void RevertBack(GameObject currPiece)
    {
        Transform currTrans = currPiece.transform;
        currTrans.localScale = new Vector3(1f,1f,1f);
    }

    public bool switchCamera()
    {
        

        if (AllPiecesShowing)
        {
            PCamera.enabled = false;
            OCamera.enabled = true;

            AllPiecesShowing = false;
            return AllPiecesShowing;
        }
        else
        {
            PCamera.enabled = true;
            OCamera.enabled = false;

            AllPiecesShowing = true;
            return AllPiecesShowing;
        }
    }

    public void GetLeftPieceFoldOrUnfold(Piece currPiece, bool direction)
    {
        if (LeftStatus == 0)
            StartCoroutine(LeftPieceFold(currPiece, direction));
        else if (LeftStatus == 1)
            StartCoroutine(LeftPieceUnfold(currPiece, direction));
        else
            StartCoroutine(LeftPieceUnfold(currPiece, direction));
    }

    // 左边的纸折叠
    IEnumerator LeftPieceFold(Piece currPiece, bool direction)
    {
        beginStopSkill(false);

        // 设置玩家位置
        SetCowBoyParent(currPiece);
        foldTime++;
        // 设置玩家位置

        if (!direction)
        {
            float speed = 120;

            for (float rotateInit = 0; rotateInit < 180; rotateInit = rotateInit + Time.deltaTime * speed)
            {
                currPiece.transform.RotateAround(currPiece.rotatePoint, new Vector3(0f, 1f, 0f), Time.deltaTime * speed);

                Debug.Log("GoingFold1");
                yield return null;

                //Debug.Log("rotateSpeed" + speed);
            }

            this.LeftStatus = 2;
            Instantiate(folded21, folded21.transform);
            switchCamera();
        }
        else
        {
            float speed = -120;

            for (float rotateInit = 0; rotateInit > -180; rotateInit = rotateInit + Time.deltaTime * speed)
            {
                currPiece.transform.RotateAround(currPiece.rotatePoint, new Vector3(0f, 1f, 0f), Time.deltaTime * speed);
                Debug.Log("GoingFold2");
                yield return null;
            }

            //currPiece.foldStatus = 1;
            this.LeftStatus = 1;
            Instantiate(folded12, folded12.transform);
            switchCamera();
        }

        // 设置玩家位置
        cowboy.transform.SetParent(null);
        SetCowboyPosition(1);

        if (isCowboyInPiece)
        {
            cowboy.transform.rotation = Quaternion.identity;
            cowboy.transform.localScale = new Vector3(cowboy.transform.localScale.x * (-1.0f), cowboy.transform.localScale.y, cowboy.transform.localScale.z);
        }
        // 设置玩家位置

        Vector3 v = currPiece.transform.eulerAngles;
        v.z = 180f;
        Quaternion q = Quaternion.Euler(v);
        currPiece.transform.rotation = q;

        beginStopSkill(true);
        yield return null;

    }

    // 左边的纸打开
    IEnumerator LeftPieceUnfold(Piece currPiece, bool direction)
    {
        beginStopSkill(false);

        // 设置玩家位置
        SetCowBoyParent(currPiece);
        foldTime--;
        // 设置玩家位置

        if (direction)
        {

            float speed = 120;
            switchCamera();
            GameObject fold21clone = GameObject.Find("Piece_21_3(Clone)");
            Destroy(fold21clone);
            destroyAll();
            for (float rotateInit = 0; rotateInit < 180; rotateInit = rotateInit + Time.deltaTime * speed)
            {
                currPiece.transform.RotateAround(currPiece.rotatePoint, new Vector3(0f, 1f, 0f), Time.deltaTime * speed);
                //Debug.Log("Init" + rotateInit);
                Debug.Log("GoingUnfold1");
                yield return null;
            }

            //currPiece.foldStatus = 0;

            this.LeftStatus = 0;
        }
        else
        {
            float speed = -120;
            switchCamera();
            GameObject fold12clone = GameObject.Find("Piece_12_3(Clone)");
            Destroy(fold12clone);
            destroyAll();
            for (float rotateInit = 0; rotateInit > -180; rotateInit = rotateInit + Time.deltaTime * speed)
            {
                currPiece.transform.RotateAround(currPiece.rotatePoint, new Vector3(0f, 1f, 0f), Time.deltaTime * speed);
                //Debug.Log("Init" + rotateInit);
                Debug.Log("GoingUnfold2");
                yield return null;
                //Debug.Log("eulerOfUnfold"+currPiece.transform.rotation.eulerAngles);
            }

            //currPiece.foldStatus = 0;
            this.LeftStatus = 0;
        }

        // 设置玩家位置
        cowboy.transform.SetParent(null);
        if (foldTime == 0)
        {
            SetCowboyPosition(0);
        }
        else
        {
            SetCowboyPosition(1);
        }
        if (isCowboyInPiece)
        {
            cowboy.transform.rotation = Quaternion.identity;
            cowboy.transform.localScale = new Vector3(cowboy.transform.localScale.x * (-1.0f), cowboy.transform.localScale.y, cowboy.transform.localScale.z);
        }
        // 设置玩家位置

        Vector3 v = currPiece.transform.eulerAngles;
        v.z = 0f;
        Quaternion q = Quaternion.Euler(v);
        currPiece.transform.rotation = currPiece.initRoat;
        currPiece.transform.position = currPiece.initPos;
        beginStopSkill(true);
        yield return null;

    }

    void destroyAll()
    {
        GameObject fold21clone = GameObject.Find("Pieces_21_3(Clone)");
        GameObject fold12clone = GameObject.Find("Piece_12_3(Clone)");
        GameObject fold23clone = GameObject.Find("Pieces_1_23(Clone)");
        GameObject fold32clone = GameObject.Find("Piece_1_32(Clone)");


        Destroy(fold21clone);
        Destroy(fold12clone);
        Destroy(fold23clone);
        Destroy(fold32clone);
    }
}
