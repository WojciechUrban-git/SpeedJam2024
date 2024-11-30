Shader "Custom/HighlightShader"
{
    Properties
    {
        _OutlineColor ("Outline Color", Color) = (0, 1, 0, 1) // Green Outline
        _OutlineWidth ("Outline Width", Float) = 0.03 // Width of the Outline
    }
    SubShader
    {
        Tags { "Queue" = "Overlay" }
        Pass
        {
            Name "Outline"
            Cull Front
            ZWrite On
            ZTest LEqual

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float4 color : COLOR;
            };

            float _OutlineWidth;
            float4 _OutlineColor;

            v2f vert (appdata v)
            {
                v2f o;
                float3 norm = normalize(v.normal);
                float3 offset = norm * _OutlineWidth;

                o.pos = UnityObjectToClipPos(v.vertex + float4(offset, 0));
                o.color = _OutlineColor;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return i.color;
            }
            ENDCG
        }
    }
}
