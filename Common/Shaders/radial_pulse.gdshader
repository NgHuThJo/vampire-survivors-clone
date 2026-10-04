shader_type canvas_item; 

uniform float radius : hint_range(0.0, 0.5) = 0.4;
uniform float pulseSpeed : hint_range(0.0, 5.0) = 0.5;
uniform float pulseStrength: hint_range(0.0, 0.5) = 0.5;
const vec2 center = vec2(0.5, 0.5);
const float pulseMidline = 0.5;

void fragment() {

    float oscillation = pulseStrength * sin(TIME * pulseSpeed) + pulseMidline;
    float distanceFromCenter = distance(UV, center);
    float outerMask = step(radius, distanceFromCenter);

    vec3 finalColor = outerMask + vec3(1.0, 0.5, 0.1);

    COLOR = vec4(finalColor, oscillation);
} 