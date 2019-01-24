using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    //0代表没折，1代表这一页在前面，2代表这一页在后面
    //public int foldStatus = 0;

    public float rotateAngle = 180f;
    //public float rotateInit = 0f;
    public float rotateSpeed;

    public Vector3 rotatePoint;

    public Quaternion initRoat;

    public Vector3 initPos;

    public bool pieceType;
    //右边的单元为true

    private void Start()
    {
        if (pieceType)
        {
            rotatePoint = this.transform.position - new Vector3(2.25f, 0f, 0f);
        }
        else
        {
            rotatePoint = this.transform.position + new Vector3(2.25f, 0f, 0f);
        }

        initRoat = this.transform.rotation;
        initPos = this.transform.position;
    }

    
    private void Update()
    {
        //Debug.Log("PieceQuat"+this.transform.rotation);
        //rotateAngle = this.transform.rotation.z - (-180);

        //if(this.transform.rotation.x < -90 && this.transform.rotation.x > -180 )
        //{
        //    this.transform.localScale = new Vector3(1f, -1f, 0.6f);
        //}
        //else if(this.transform.rotation.x < 180 && this.transform.rotation.x > 90)
        //{
        //    this.transform.localScale = new Vector3(1f, -1f, 0.6f);
        //}
        //else if (this.transform.rotation.x < 90 && this.transform.rotation.x > -90)
        //{
        //    this.transform.localScale = new Vector3(1f, 1f, 0.6f);
        //}
    }
}
