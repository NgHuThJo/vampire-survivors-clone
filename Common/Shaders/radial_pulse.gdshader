shader_type canvas_item; 

uniform float radius : hint_range(0.0, 0.5) = 0.4;
uniform float pulseSpeed : hint_range(0.0, 5.0) = 0.5;
uniform float pulseStrength: hint_range(0.0, 0.5) = 0.5;
const vec2 center = vec2(0.5, 0.5);
const float pulseMidline = 0.5;

vec2 RotateVertex(vec2 vertex, float angle) {   
    mat2 rotationMatrix = mat2(vec2(cos(angle), sin(angle)), vec2(-sin(angle), cos(angle)));
    return rotationMatrix * vertex;
}

vec2 MoveInCircle(float angle, float r) {
    return r * vec2(cos(angle), sin(angle));
}

void vertex() { 
    // VERTEX = RotateVertex(VERTEX, 10.0 * TIME);
    float angle = TIME * 5.0;
    float r = sin(TIME) * 50.0 + 100.0;
    VERTEX += MoveInCircle(angle, r);
}

void fragment() {
    vec2 uv = UV;

    // uv = vec2(1.0, 1.0) - uv;
    vec4 texel = texture(TEXTURE, uv);

    // float oscillation = pulseStrength * sin(TIME * pulseSpeed) + pulseMidline;
    // float distanceFromCenter = distance(UV, center);
    // float outerMask = step(radius, distanceFromCenter);

    // vec3 finalColor = outerMask + vec3(1.0, 0.3, 0.1);

    COLOR = vec4(texel);
} 