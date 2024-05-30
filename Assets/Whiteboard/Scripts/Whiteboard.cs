using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whiteboard : MonoBehaviourPun
{
    public Texture2D texture;

    public Vector2 textureSize = new Vector2(2048, 2048);

    [SerializeField] PhotonView photonView;
    //public xrInteractableMarker[] Markers;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }


    void Start()

    {
        
        PutNewTexture();

    }

    public void PutNewTexture()
    {
        photonView.RPC("Rpc_PutNewTexture", RpcTarget.AllBuffered);
    }
    [PunRPC]
    protected virtual void Rpc_PutNewTexture()
    {
        var r = GetComponent<Renderer>();

        texture = new Texture2D((int)textureSize.x, (int)textureSize.y);

        r.material.mainTexture = texture;
    }
    //public void MarkerPosition()
    //{
    //    for(int i = 0; i < Markers.Length; i++)
    //    {
    //        if (Markers[i] != null)
    //        {
    //            Markers[i].ResetPosition();
    //        }
    //    }
    //}

}
