// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Sabotage/Ripple" {
  Properties {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _Origin ("Origin", Vector) = (0, 0, 0, 0)
    _Scale ("Scale", Range(0.1, 4)) = 1
    _WaveLength ("Wave Length", Range(0.1, 4)) = 1
    _WaveDistance1 ("Wave Distance 1", float) = 0
    _WaveDistance2 ("Wave Distance 2", float) = 0
    _WaveDistance3 ("Wave Distance 3", float) = 0
  }
  SubShader {
    Tags {
    }
    LOD 200

    Stencil {
      Ref 1
      Comp NotEqual
      Pass keep
    }

    CGPROGRAM
    #pragma vertex vert
    #pragma surface surf Standard alpha:blend
    #pragma target 3.0

    struct Input {
      float2 uv_MainTex;
      float4 color : COLOR;
    };

    const float M_PI = 3.1415926536;

    sampler2D _MainTex;
    float3 _Origin;
    float _Scale, _WaveLength;
    float _WaveDistance1, _WaveDistance2, _WaveDistance3;

    float waveHeight(float position, float waveDistance) {
      if (waveDistance == 0)
        return 0;

      half d = position - waveDistance;
      /* half d2 = pow(clamp(d, -_WaveLength, _WaveLength), 2);*/
      /* [> return 1 - clamp(sin(clamp(d, 0, M_PI)), 0, 1);<]*/
      /* [> return 1 - clamp(d, 0, 1);<]*/
      half d2 = clamp(abs(d) * _WaveLength, 0, 1);
      return 1 - d2;
    }

    void vert (inout appdata_full v) {
      float4 worldPos = mul(unity_ObjectToWorld, v.vertex);
      float c =
        waveHeight(length(worldPos.xz - _Origin.xz), _WaveDistance1)
        + waveHeight(length(worldPos.xz - _Origin.xz), _WaveDistance2)
        + waveHeight(length(worldPos.xz - _Origin.xz), _WaveDistance3);

      v.vertex.xyz += v.normal * c * _Scale;
      v.color = float4(c, c, c, c);
    }

    void surf (Input IN, inout SurfaceOutputStandard OUT) {
      fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
      /* OUT.Albedo = c.rgb;*/
      OUT.Albedo = IN.color.rgb;
      OUT.Alpha = IN.color.a;
      OUT.Metallic = 0.0;
      OUT.Smoothness = 0.5;
    }
    ENDCG
  }
  FallBack "Diffuse"
}
