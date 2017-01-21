Shader "Sabotage/Subtractive" {
  SubShader {
    Tags {
      "Queue" = "Background"
      "RenderType" = "Opaque"
    }
    LOD 200

    ZWrite Off
    ColorMask 0
    Pass {
      Stencil {
        Ref 1
        Comp always
        Pass replace
      }
    }
  }
}
