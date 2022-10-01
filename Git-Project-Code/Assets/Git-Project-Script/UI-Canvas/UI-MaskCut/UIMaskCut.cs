
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class UIMaskCut : Image
{
    public override Material materialForRendering
    {
        get
        {
            Material m_material = new Material(base.materialForRendering);
            m_material.SetInt("_StencilComp", (int)CompareFunction.NotEqual);
            return m_material;
        }
    }
}
