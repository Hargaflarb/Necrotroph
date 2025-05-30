#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

Texture2D SpriteTexture;

static const float fadeLength = 0.15;
static const float resizer = 1.0 / fadeLength;
static const float Pi = 3.14159265359;
static const float toRadians = 6.28318530718;

sampler2D SpriteTextureSampler = sampler_state
{
	Texture = <SpriteTexture>;
};

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	float2 TextureCoordinates : TEXCOORD0;
};

float IsInShadow(float2 dif, float1 offset, float1 upper)
{
    float Pa = atan2(dif.y, dif.x) + offset;
    return step((abs(upper - Pa) + abs(Pa)), upper);
}


float4 MainPS(VertexShaderOutput input) : COLOR
{
    float4 pixelColor = tex2D(SpriteTextureSampler, input.TextureCoordinates);
    
    float2 pixelPosition = input.TextureCoordinates;
    float2 lightPosition = float2(0.5, 0.5);
    float1 upperAngle = input.Color.x * toRadians;
    float1 angleOffset = (input.Color.y - 0.5) * toRadians;
    float1 casterDistance = input.Color.z;
    
    pixelColor.a = 0;
    
    //float2 dif = AdjustForAspectRatio(pixelPosition - lightPosition);
    float2 dif = pixelPosition - lightPosition;
    float pixelDistance = length(dif);
    pixelColor.a += IsInShadow(dif, angleOffset, upperAngle) * step(casterDistance, pixelDistance);
    

    return pixelColor;
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};