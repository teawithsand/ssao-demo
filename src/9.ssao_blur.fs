#version 330 core
out float FragColor;

in vec2 TexCoords;

uniform sampler2D ssaoInput;
uniform int blurRadius;

void main() 
{
    if(blurRadius > 0) {
        float result = 0.0;
        vec2 texelSize = 1.0 / vec2(textureSize(ssaoInput, 0));
        for (int x = -blurRadius; x < blurRadius; ++x) 
        {
            for (int y = -blurRadius; y < blurRadius; ++y) 
            {
                vec2 offset = vec2(float(x), float(y)) * texelSize;
                result += texture(ssaoInput, TexCoords + offset).r;
            }
        }
           FragColor = result / (blurRadius*2 * blurRadius*2);
    } else {
        vec2 offset = vec2(float(0), float(0));
        FragColor = texture(ssaoInput, TexCoords).r;
    }
 
}  