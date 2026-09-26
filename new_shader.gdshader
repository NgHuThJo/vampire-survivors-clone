shader_type canvas_item;

vec2 RotateVertex(vec2 vertex, float angle) {
	float newX = vertex.x * cos(angle) - vertex.y * sin(angle);
	float newY = vertex.x * sin(angle) + vertex.y * cos(angle);

	return vec2(newX, newY);
}

void vertex()
{
	float wind = sin(TIME * 2.0);
	float amplitude = 50.0;
	VERTEX += wind * amplitude;
	VERTEX *= 2.0;
	VERTEX = RotateVertex(VERTEX, TIME * PI);
}

void fragment()
{
}

//void light() {
//	// Called for every pixel for every light affecting the CanvasItem.
//	// Uncomment to replace the default light processing function with this one.
//}
