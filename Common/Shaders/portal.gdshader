shader_type canvas_item;

uniform float radius : hint_range(0.0, 0.5) = 0.4;
uniform float rimWidth : hint_range(0.0, 0.3) = 0.05;
uniform float softness: hint_range(0.0, 0.2) = 0.01;
uniform float breathingStrength: hint_range(0.0, 0.1, 0.001) = 0.02;
uniform float displacementStrength: hint_range(0.0, 0.2) = 0.05;
uniform vec2 noiseDirection = vec2(1.0, 1.0);
uniform float noiseSpeed : hint_range(0.0, 1.0) = 0.1;
uniform float innerGradientStart: hint_range(0.0, 0.5) = 0.1;
uniform float innerGradientWidth: hint_range(0.0, 0.5) = 0.2;
uniform vec3 innerStartColor: source_color = vec3(0.0, 0.0, 0.0);
uniform vec3 innerEndColor: source_color = vec3(0.15, 0.21, 0.53);
uniform vec3 rimStartColor: source_color = vec3(0.38, 0.63, 1.0);
uniform vec3 rimEndColor: source_color = vec3(0.69, 0.88, 0.99);
uniform float rimGradientWidth : hint_range(0.0, 0.5) = 0.07;

void fragment() {
	vec3 portalColor = vec3(0.23, 0.35, 0.65);	
	vec2 center = vec2(0.5, 0.5);
	float oscillation = sin(TIME);
	vec2 sampleUV = UV + TIME * noiseDirection * noiseSpeed;
	float noiseDisplacement = texture(TEXTURE, sampleUV).r;
	float distanceFromCenter = distance(UV, center) + noiseDisplacement * displacementStrength + oscillation * breathingStrength;

	float innerGradientFactor = smoothstep(innerGradientStart, innerGradientStart + innerGradientWidth, distanceFromCenter);
	vec3 innerGradient = mix(innerStartColor, innerEndColor, innerGradientFactor);

	float rimGradientStart = radius - rimWidth;
	float rimGradientFactor = smoothstep(rimGradientStart, rimGradientStart + rimGradientWidth, distanceFromCenter);
	vec3 rimGradient = mix(rimStartColor, rimEndColor, rimGradientFactor);

	float maskOuter = 1.0 - smoothstep(radius - softness, radius, distanceFromCenter);
	float maskInner = 1.0 - smoothstep(rimWidth - softness, rimWidth, distanceFromCenter);
	float maskRim = maskOuter - maskInner;

	vec3 finalColor = maskRim * rimGradient + maskInner * innerGradient;

	COLOR = vec4(finalColor, maskOuter);
}


