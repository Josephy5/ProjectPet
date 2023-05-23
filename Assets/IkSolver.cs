using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IkSolver : MonoBehaviour
{
    [SerializeField] Transform body = default;
    [SerializeField] IkSolver otherFoot = default;
    [SerializeField] LayerMask ground = default;
    [SerializeField] float speed = 1;
    [SerializeField] float stepDistance = 4;
    [SerializeField] float stepLength = 4;
    [SerializeField] float stepHeight = 0.04f;
    [SerializeField] Vector3 footoffset = default;

    float footSpacing, lerp;
    Vector3 oldPos, curPos, newPos;
    Vector3 oldNor, curNor, newNor;
    // Start is called before the first frame update
    void Start()
    {
        footSpacing = transform.localPosition.x;
        curPos = newPos = oldPos = transform.position;
        curNor = newNor = oldNor = transform.up;
        lerp = 1;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position = curPos;
        transform.up = curNor;

        Ray ray = new Ray(body.position + (body.right * footSpacing), Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit info, 10, ground))
        {
            if (Vector3.Distance(newPos, info.point) > stepDistance && !otherFoot.IsMoving() && lerp >= 1)
            {
                lerp = 0;
                int direction = body.InverseTransformPoint(info.point).z > body.InverseTransformPoint(newPos).z ? 1 : -1;
                newPos = info.point + (body.forward * stepLength * direction) + footoffset;
                newNor = info.normal;
            }
        }

        if (lerp < 1)
        {
            Vector3 tempPosition = Vector3.Lerp(oldPos, newPos, lerp);
            tempPosition.y += Mathf.Sin(lerp * Mathf.PI) * stepHeight;

            curPos = tempPosition;
            curNor = Vector3.Lerp(oldNor, newNor, lerp);
            lerp += Time.deltaTime * speed;
        }
        else
        {
            oldPos = newPos;
            oldNor = newNor;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(newPos, 0.03f);
    }
    public bool IsMoving()
    {
        return lerp < 1;
    }
}
