#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

Texture2D SpriteTexture;

static const float aspectRatio = 9.0 / 16.0;
static const float fadeLength = 0.05;
static const float resizer = 1 / fadeLength;


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

float2 AdjustForAspectRatio(float2 position)
{
    return float2(position.x, position.y * aspectRatio);
}

float4 MainPS(VertexShaderOutput input) : COLOR
{
    float4 pixelColor = tex2D(SpriteTextureSampler, input.TextureCoordinates);
    float2 pixelPosition = input.TextureCoordinates;
    float2 lightPosition = input.Color.xy;
    float1 lightRadius = input.Color.z;
    
    float2 dif = AdjustForAspectRatio(pixelPosition - lightPosition);
    //float2 dif = AdjustForAspectRatio(pixelPosition - lightPositions[0]);
    float distance = length(dif);
    pixelColor.a -= 1 - clamp((distance - (lightRadius - fadeLength)) * resizer, 0, 1);
    //pixelColor.a += IsInShadow(dif) * step(Distance, distance);
    
    //pixelColor.a = 1 - pixelColor.a;
    return pixelColor;
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};