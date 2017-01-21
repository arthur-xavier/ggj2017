Shader "Unlit/Shadow" {
  Properties {
    _Color ("Color", Color) = (0, 0, 0, 0.5)
  }
  SubShader {
    Tags { "RenderType"="Opaque" }
    LOD 100

    Blend OneMinusDstAlpha Zero
    AlphaToMask On

    Pass {

      CGPROGRAM
      #pragma vertex vert
      #pragma fragment frag
      // make fog work
      #pragma multi_compile_fog

      #include "UnityCG.cginc"

      struct appdata {
        float4 vertex : POSITION;
        float2 uv : TEXCOORD0;
      };

      struct v2f {
        UNITY_FOG_COORDS(1)
        float4 vertex : SV_POSITION;
      };

      float4 _Color;

      v2f vert (appdata v) {
        v2f o;
        o.vertex = UnityObjectToClipPos(v.vertex);
        UNITY_TRANSFER_FOG(o,o.vertex);
        return o;
      }

      fixed4 frag (v2f i) : SV_Target {
        fixed4 col = _Color;
        UNITY_APPLY_FOG(i.fogCoord, col);
        return col;
      }
      ENDCG
    }
  }
}
