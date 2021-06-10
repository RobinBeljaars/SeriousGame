using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseGameEntity_CH3 : MonoBehaviour
{
    public int id;
    private int entityType;
    private bool _tag;
    [SerializeField]
    protected Vector3 _pos;
    protected Vector3 _scale;
    protected float boundingRadius;


    public Vector3 WorldPos() { return transform.position; }
    public Vector3 Pos() { return _pos; }
    public void SetPos(Vector3 new_pos) { _pos = new_pos; }

    public float BRadius() { return boundingRadius; }
    public void SetBRadius(float r) { boundingRadius = r; }

    public int ID() { return id; }
    public void SetID(int n) { id = n; }
    public void Tag() { _tag = true; }
    public void UnTag() { _tag = false; }
    public bool IsTagged()
    {
        return _tag == true ? true : false;
    }
    public Vector3 Scale() { return _scale; }
    public void SetScale(Vector3 val) { boundingRadius *= Mathf.Max(val.x, val.z) / Mathf.Max(_scale.x, _scale.z); _scale = val; }
    public void SetScale(float val) { boundingRadius *= (val / Mathf.Max(_scale.x, _scale.z)); _scale = new Vector3(val, val, val); }

    public int EntityType() { return entityType; }
    public void SetEntityType(int new_type) { entityType = new_type; }

    public void SetBaseEntity(int type, Vector3 pos, float r)
    {
        boundingRadius = r;
        _pos = pos;
        _scale = new Vector3(1.0f, 1.0f, 1.0f);
        entityType = type;
        _tag = false;   
    }
    private void Start()
    {

        id = 1;
        _tag = false;
    }

    public BaseGameEntity_CH3()
    {

        id = 1;
        _tag = false;
        boundingRadius = 2f;
    }

    

}