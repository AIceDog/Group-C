using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragScroll : MonoBehaviour
{

    private Vector3 dragCenter;
    public FoldControl foldControl;
    public Piece thisPiece;


    public float rotateIntense = 0.1f;

    private void Start()
    {
        dragCenter = this.transform.position - new Vector3(0f, 0f, 3f);
    }


    private void OnMouseDrag()
    {
        if (this.transform.rotation.x <= 0 && this.transform.rotation.x > -180)
        {
            this.transform.RotateAround(dragCenter, new Vector3(0f, 0f, 1f), Input.GetAxis("Mouse X") * rotateIntense);
        }
    }

    //private void OnMouseUp()
    //{
    //    if(this.transform.rotation.x > -90f && this.transform.rotation.x < 0f)
    //    {
    //        foldControl.GetRightPieceUnfold(thisPiece);
    //    }
    //    else if(this.transform.rotation.x > -180f && this.transform.rotation.x < 90f)
    //    {
    //        foldControl.GetRightPieceFold(thisPiece);
    //    }
    //}
}
