Shader "Sabotage/Ripple" {
  Properties {
    _Color ("Color", Color) = (1, 1, 1, 1)
    _Origin ("Origin", Vector) = (0, 0, 0, 0)
    _Scale ("Scale", Range(0.1, 10)) = 1
    _Refraction ("Refraction", Range(0, 50)) = 10
    _WaveLength ("Wave Length", Range(0.1, 4)) = 1
    _WaveDistance1 ("Wave Distance 1", float) = 0
    _WaveDistance2 ("Wave Distance 2", float) = 0
    _WaveDistance3 ("Wave Distance 3", float) = 0
  }
  SubShader {
    Tags {
      "RenderType" = "Transparent"
      "Queue" = "Overlay"
    }
    LOD 200

    Stencil {
      Ref 1
      Comp NotEqual
      Pass keep
    }

    GrabPass {
      Name "BASE"
      Tags { "LightMode" = "Always" }
    }

    CGPROGRAM
    #pragma vertex vert
    #pragma surface surf Lambert alpha:blend
    #pragma target 3.0

    fixed4 LightingNoLighting(SurfaceOutput s, fixed3 lightDir, fixed atten) {
      fixed4 c;
      c.rgb = s.Albedo;
      c.a = s.Alpha;
      return c;
    }

    struct Input {
      float4 color : COLOR;
      float3 worldNormal;
      float3 worldRefl;
      float4 screenPos;
      INTERNAL_DATA
    };

    const float M_PI = 3.1415926536;

    sampler2D _GrabTexture : register(s0);
    float4 _GrabTexture_TexelSize;

    float4 _Color;
    float3 _Origin;
    float _Scale, _Refraction, _WaveLength;
    float _WaveDistance1, _WaveDistance2, _WaveDistance3;

    float waveHeight(float2 position, float waveDistance) {
      return waveDistance == 0
        ? 0
        : 1 - clamp(abs(length(position) - waveDistance) * _WaveLength, 0, 1);
    }

    float3 waveNormal(float3 direction, float3 normal, float distance, float waveDistance) {
      if (waveDistance == 0)
        return 0;
      else if (clamp((distance - waveDistance) * _WaveLength, 0, 1) > 0)
        return normalize((normal + direction) * _Scale);
      else if (clamp((distance - waveDistance) * _WaveLength, 0, 1) < 0)
        return normalize((normal - direction) * _Scale);
      else
        return 0;
    }

    void vert(inout appdata_full v) {
      float4 worldPos = mul(unity_ObjectToWorld, v.vertex);
      float2 d = worldPos.xz - _Origin.xz;
      float3 dir = normalize(float3(d.x, 0, d.y));

      float c =
        waveHeight(d, _WaveDistance1)
        + waveHeight(d, _WaveDistance2)
        + waveHeight(d, _WaveDistance3);

      float3 up = float3(0, 1, 0);
      float3 n =
        waveNormal(dir, up, length(d), _WaveDistance1)
        + waveNormal(dir, up, length(d), _WaveDistance2)
        + waveNormal(dir, up, length(d), _WaveDistance3);

      v.vertex.xyz += v.normal * c * _Scale;
      v.color = float4(n.xyz, c);
    }

    void surf (Input IN, inout SurfaceOutput OUT) {
      float3 distort = IN.color.rgb;
      float2 offset = distort.xz * _Refraction * _GrabTexture_TexelSize.xy;

      IN.screenPos.xy = offset * IN.screenPos.z + IN.screenPos.xy;

      float4 refrColor = tex2Dproj(_GrabTexture, IN.screenPos);

      OUT.Emission =
        (refrColor.rgb > 0.5) * (1 - (1-2*(refrColor.rgb-0.5)) * (1-_Color.rgb))
        + (refrColor.rgb <= 0.5) * ((2*refrColor.rgb) * _Color.rgb);
      OUT.Alpha = IN.color.a * _Color.a;
    }
    ENDCG
  }
  FallBack "Diffuse"
}
